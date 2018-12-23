using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;
using BusMap.Mobile.Helpers;
using BusMap.Mobile.Models;
using BusMap.Mobile.Services;
using BusMap.Mobile.SQLite.Models;
using BusMap.Mobile.SQLite.Repositories;
using BusMap.Mobile.Views;
using Prism.Commands;
using Prism.Navigation;
using SQLite;
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
        private bool _isRouteDelayed;
        private List<RouteDelay> _routeDelays;
        private TimeSpan _currentRouteDelay;
        private bool _isBusy;

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

        public bool IsRouteDelayed
        {
            get => _isRouteDelayed;
            set => SetProperty(ref _isRouteDelayed, value);
        }

        public List<RouteDelay> RouteDelays
        {
            get => _routeDelays;
            set => SetProperty(ref _routeDelays, value);
        }

        public TimeSpan CurrentRouteDelay
        {
            get => _currentRouteDelay;
            set => SetProperty(ref _currentRouteDelay, value);
        }

        public bool IsBusy
        {
            get => _isBusy;
            set => SetProperty(ref _isBusy, value);
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
            RouteDelays = new List<RouteDelay>();
            CurrentRouteDelay = new TimeSpan();
        }

        


        //--DetailsTab--
        public ICommand TrackButtonCommand => new DelegateCommand(async () =>
        {
            var navParams = new NavigationParameters();
            navParams.Add("route", Route);

            await NavigationService.NavigateAsync(nameof(TraceTrackingPage), navParams);
        });

        public ICommand RouteDetailsRefreshCommand => new DelegateCommand(async () 
            => await DownloadAndSetRouteDetailsData());


        //--BusStopsTab--

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
                _isRouteInFavorites = false;
            }
            else
            {
                try
                {
                    _favoriteRoutesRepository.AddFavorite(new FavoriteRoute
                    {
                        Id = Route.Id,
                        AddedDate = DateTime.Now
                    });
                    MessagingHelper.Toast("Route added to favorites.", ToastTime.LongTime);
                    FavoriteStarColor = Color.Gold;
                    _isRouteInFavorites = true;
                }
                catch (SQLiteException)
                {
                    MessagingHelper.Toast("Route is already in favorites.", ToastTime.LongTime);
                }
                
            }
        });

        public ICommand ReportDelayCommand => new DelegateCommand(async () =>
        {
            var navParams = new NavigationParameters();
            navParams.Add("route", Route);

            await NavigationService.NavigateAsync(nameof(RouteDelayReportPage), navParams);
        });

        public ICommand TestCommand => new DelegateCommand(() => 
            MessagingHelper.Toast("Juhu", ToastTime.LongTime));

        public ICommand SetAlarmCommand => new DelegateCommand<BusStop>(b =>
        {
            MessagingHelper.Toast($"selected: {b.Address}", ToastTime.LongTime);
        });

        //Todo: to use with switching tabs onClick
        public ICommand ShowBusStopOnMapCommand => new DelegateCommand<BusStop>(async busStop =>
        {
            var positionFromBusStop = new Position(busStop.Latitude, busStop.Longitude);
            MapPosition = MapSpan.FromCenterAndRadius(positionFromBusStop, Distance.FromKilometers(20));
            //await NavigationService.NavigateAsync("RouteDetailsPage?selectedTab=Details");
        });



        //--MapTab--
        public ICommand MapAppearingCommand => new DelegateCommand(async () =>
        {
            var currentPosition = await LocalizationHelpers.GetCurrentUserPositionAsync(true);
            MapPosition = currentPosition.ToMapSpan(Distance.FromKilometers(20));
        });


        //--NAVIGATION--
        public override async void OnNavigatedTo(NavigationParameters parameters)
        {
            

            if (parameters.ContainsKey("route"))
            {
                Route = parameters["route"] as Route;
                Route.DayOfTheWeek = Route.DayOfTheWeek.ConvertToFullDayNames();
                //Route = await _dataService.GetRouteAsync(routeParam.Id);
                Title = Route.Name;
                Pins.AddRange(Route.BusStops.ToGoogleMapsPinsWithPunctualityColorsPins());   //Freezing thread?
            }

            _isRouteInFavorites = _favoriteRoutesRepository.IsRouteInFavorites(Route.Id);
            if (_isRouteInFavorites)
            {
                Device.BeginInvokeOnMainThread(() =>
                {
                    FavoriteStarColor = Color.Gold;
                });
            }

            await DownloadAndSetRouteDetailsData();
        }

        private async Task DownloadAndSetRouteDetailsData()
        {
            IsBusy = true;
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
            RouteDelays = await _dataService.GetRouteDelays(Route.Id);
            if (RouteDelays.Any())
                IsRouteDelayed = true;

            CurrentRouteDelay = CalculateCurrentDelay();
            IsBusy = false;
        }

        private void MarkRecentBusStop(BusStop busStop)
        {
            Route.BusStops.FirstOrDefault(b => b.Id == busStop.Id).IsRecentBusStop = true;
        }

        private TimeSpan CalculateCurrentDelay()
        {
            var lastDelayTime = RouteDelays.LastOrDefault().DateTime.TimeOfDay;
            var delay = DateTime.Now.TimeOfDay.Subtract(lastDelayTime);

            return delay;
        }

    }
}
