using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Runtime.CompilerServices;
using System.Text;
using BusMap.Mobile.Annotations;
using BusMap.Mobile.Helpers;
using BusMap.Mobile.Models;
using Prism.Mvvm;
using Prism.Navigation;
using Xamarin.Forms.GoogleMaps;

namespace BusMap.Mobile.ViewModels
{
    public class BusStopsMapPageViewModel : ViewModelBase
    {
        private ObservableCollection<Pin> _pins;
        private MapSpan _mapPosition;

        public MapSpan MapPosition
        {
            get => _mapPosition;
            set => SetProperty(ref _mapPosition, value);
        }

        public ObservableCollection<Pin> Pins
        {
            get => _pins;
            set => SetProperty(ref _pins, value);
        }

        public BusStopsMapPageViewModel(INavigationService navigationService)
            : base(navigationService)
        {
            Pins = new ObservableCollection<Pin>();
        }

        public override void OnNavigatingTo(NavigationParameters parameters)
        {
            var route = parameters["route"] as Route;
            Pins.AddRange(route.BusStops.ToGoogleMapsPins());
            MapPosition = MapSpan.FromCenterAndRadius(Pins[0].Position, Distance.FromKilometers(10));

            Title = $"{route.StartingBusStop.Address} - {route.DestinationBusStop.Address}";
        }


    }
}
