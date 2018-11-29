using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Windows.Input;
using BusMap.Mobile.Helpers;
using BusMap.Mobile.Models;
using BusMap.Mobile.Services;
using BusMap.Mobile.SQLite.Models;
using BusMap.Mobile.SQLite.Repositories;
using BusMap.Mobile.Views;
using Prism.Commands;
using Prism.Navigation;
using Xamarin.Forms;
using Xamarin.Forms.GoogleMaps;

namespace BusMap.Mobile.ViewModels
{
    public class RouteDetailsPageViewModel : ViewModelBase
    {
        private readonly IDataService _dataService;
        private readonly IFavoriteRoutesRepository _favoriteRoutesRepository;
        private bool _isRouteInFavorites;

        private Route _route;
        private ObservableCollection<Pin> _pins;
        private MapSpan _mapPosition;
        private Color _favoriteStarColor;
        private int _currentLatency;
        private BusStop _recentBusStop;
        private bool _currentLatencyIsVisible;

        public Route Route
        {
            get => _route;
            set => SetProperty(ref _route, value);
        }

        public ObservableCollection<Pin> Pins
        {
            get => _pins;
            set => SetProperty(ref _pins, value);
        }

        public MapSpan MapPosition
        {
            get => _mapPosition;
            set => SetProperty(ref _mapPosition, value);
        }

        public Color FavoriteStarColor
        {
            get => _favoriteStarColor;
            set => SetProperty(ref _favoriteStarColor, value);
        }

        public int CurrentLatency
        {
            get => _currentLatency;
            set
            {
                SetProperty(ref _currentLatency, value);
                CurrentLatencyIsVisible = value != 0;
            }
        }

        public bool CurrentLatencyIsVisible
        {
            get => _currentLatencyIsVisible;
            set => SetProperty(ref _currentLatencyIsVisible, value);
        }

        public BusStop RecentBusStop
        {
            get => _recentBusStop;
            set => SetProperty(ref _recentBusStop, value);
        }

        public RouteDetailsPageViewModel(INavigationService navigationService, 
            IDataService dataService,
            IFavoriteRoutesRepository favoriteRoutesRepository) 
            : base(navigationService)
        {
            _dataService = dataService;
            _favoriteRoutesRepository = favoriteRoutesRepository;
            Route = new Route();
            Pins = new ObservableCollection<Pin>();
            FavoriteStarColor = Color.White;

        }


        //--DetailsTab--
        public ICommand TrackButtonCommand => new DelegateCommand(async () =>
        {
            var navParams = new NavigationParameters();
            navParams.Add("route", Route);

            await NavigationService.NavigateAsync(nameof(TraceTrackingPage), navParams);
        });


        //--BusStopsTab--
        //Todo: to use with switching tabs onClick
        public ICommand SelectedBusStopCommand => new DelegateCommand<BusStop>(async busStop =>
        {
            var positionFromBusStop = new Position(busStop.Latitude, busStop.Longitude);
            MapPosition = MapSpan.FromCenterAndRadius(positionFromBusStop, Distance.FromKilometers(20));
        });

        public ICommand ReportButtonCommand => new DelegateCommand(async () =>
        {
            var parameters = new NavigationParameters {{"route", Route}};
            await NavigationService.NavigateAsync(nameof(RouteReportPage), parameters);
        });

        public ICommand FavoritesButtonCommand => new DelegateCommand(() =>
        {
            if (_isRouteInFavorites)
            {
                _favoriteRoutesRepository.RemoveFavorite(Route.Id);
                MessagingHelper.Toast("Route removed from favorites.", ToastTime.LongTime);
                FavoriteStarColor = Color.White;
            }
            else
            {
                _favoriteRoutesRepository.AddFavorite(new FavoriteRoute
                {
                    Id = Route.Id,
                    AddedDate = DateTime.Now
                });
                MessagingHelper.Toast("Route added to favorites.", ToastTime.LongTime);
                FavoriteStarColor = Color.Gold;
            }
        });

        public ICommand TestCommand => new DelegateCommand(() => 
            MessagingHelper.Toast("Juhu", ToastTime.LongTime));


        //--MapTab--
        public ICommand MapAppearingCommand => new DelegateCommand(async () =>
        {
            var currentPosition = await LocalizationHelpers.GetCurrentUserPositionAsync(true);
            MapPosition = currentPosition.ToMapSpan(Distance.FromKilometers(20));
        });


        //--NAVIGATION--
        public override async void OnNavigatingTo(NavigationParameters parameters)
        {
            if (parameters.ContainsKey("route"))
            {
                Route = parameters["route"] as Route;
                //Route = await _dataService.GetRouteAsync(routeParam.Id);
                Title = Route.Name;
                Pins.AddRange(Route.BusStops.ToGoogleMapsPins());   //Freezing thread?
            }

            _isRouteInFavorites = _favoriteRoutesRepository.IsRouteInFavorites(Route.Id);
            if (_isRouteInFavorites)
            {
                Device.BeginInvokeOnMainThread(() =>
                {
                    FavoriteStarColor = Color.Gold;
                });
            }

            try
            {
                CurrentLatency = await _dataService.GetRouteCurrentLatency(Route.Id);
                RecentBusStop = await _dataService.GetRouteRecentBusStop(Route.Id);
                MarkRecentBusStop(RecentBusStop);
            }
            catch (System.Net.Http.HttpRequestException)
            {
                //Sorry for that :(
            }
            
        }

        private void MarkRecentBusStop(BusStop busStop)
        {
            Route.BusStops.FirstOrDefault(b => b.Id == busStop.Id).IsRecentBusStop = true;
        }

    }
}
