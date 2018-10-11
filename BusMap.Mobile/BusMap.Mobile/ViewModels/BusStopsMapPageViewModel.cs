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
using Xamarin.Forms.Maps;

namespace BusMap.Mobile.ViewModels
{
    public class BusStopsMapPageViewModel : ViewModelBase
    {
        private ObservableCollection<Pin> _pins;
        private Position _mapPosition;

        public Position MapPosition
        {
            get => _mapPosition;
            set => SetProperty(ref _mapPosition, value);
        }

        public ObservableCollection<Pin> Pins
        {
            get => _pins;
            set => SetProperty(ref _pins, value);
        }




        //public BusStopsMapViewModel(Route route)
        //{
        //    Pins = route.BusStops.ConvertToMapPins();
        //    MapPosition = Pins[0].Position;
        //    PageTitle = route.Name;
        //}

        public BusStopsMapPageViewModel(INavigationService navigationService)
            : base(navigationService)
        {
            Pins = new ObservableCollection<Pin>();
        }

        public override void OnNavigatingTo(NavigationParameters parameters)
        {
            var route = parameters["route"] as Route;
            Pins = route.BusStops.ConvertToMapPins();
            MapPosition = Pins[0].Position;

            Title = $"{route.StartingBusStop.Address} - {route.DestinationBusStop.Address}";
        }


    }
}
