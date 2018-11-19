using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Text;
using System.Windows.Input;
using BusMap.Mobile.Helpers;
using BusMap.Mobile.Models;
using BusMap.Mobile.Services;
using BusMap.Mobile.Views;
using Prism.Commands;
using Prism.Navigation;
using Xamarin.Forms.GoogleMaps;

namespace BusMap.Mobile.ViewModels
{
    public class RouteDetailsPageViewModel : ViewModelBase
    {
        private readonly IDataService _dataService;

        private Route _route;
        private ObservableCollection<Pin> _pins;
        private MapSpan _mapPosition;

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


        public RouteDetailsPageViewModel(INavigationService navigationService, IDataService dataService) 
            : base(navigationService)
        {
            _dataService = dataService;
            Route = new Route();
            Pins = new ObservableCollection<Pin>();
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
        }

    }
}
