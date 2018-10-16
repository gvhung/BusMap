using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Text;
using System.Windows.Input;
using BusMap.Mobile.Helpers;
using BusMap.Mobile.Models;
using Microsoft.EntityFrameworkCore.Metadata.Internal;
using Prism.Commands;
using Prism.Navigation;
using Xamarin.Forms.Maps;
using Position = Plugin.Geolocator.Abstractions.Position;

namespace BusMap.Mobile.ViewModels
{
    public class EditBusStopPageViewModel : ViewModelBase
    {
        private BusStop _busStopToEdit;
        private string _cityNameEntry;
        private string _stopNameEntry;
        private TimeSpan _time;
        private Position _geoPosition;
        private Xamarin.Forms.Maps.Position _mapPosition;
        private ObservableCollection<Pin> _mapPins;


        public BusStop BusStopToEdit
        {
            get => _busStopToEdit;
            set => SetProperty(ref _busStopToEdit, value);
        }

        public string CityNameEntry
        {
            get => _cityNameEntry;
            set => SetProperty(ref _cityNameEntry, value);
        }

        public string StopNameEntry
        {
            get => _stopNameEntry;
            set => SetProperty(ref _stopNameEntry, value);
        }

        public TimeSpan Time
        {
            get => _time;
            set => SetProperty(ref _time, value);
        }

        public Position GeoPosition
        {
            get => _geoPosition;
            set => SetProperty(ref _geoPosition, value);
        }

        public Xamarin.Forms.Maps.Position MapPosition
        {
            get => _mapPosition;
            set => SetProperty(ref _mapPosition, value);
        }

        public ObservableCollection<Pin> MapPins
        {
            get => _mapPins;
            set => SetProperty(ref _mapPins, value);
        }


        public EditBusStopPageViewModel(INavigationService navigationService) 
            : base(navigationService)
        {
            MapPosition = new Xamarin.Forms.Maps.Position();
            MapPins = new ObservableCollection<Pin>();
        }


        public ICommand SaveButtonCommand => new DelegateCommand(async () =>
        {
            var navigationParams = new NavigationParameters();
            var updatedBusStop = new BusStop
            {
                Address = CityNameEntry,
                Label = StopNameEntry,
                Latitude = GeoPosition.Latitude,
                Longitude = GeoPosition.Longitude
            };

            navigationParams.Add("busStopFromEdit", updatedBusStop);
            await NavigationService.GoBackAsync(navigationParams);
        });

        public ICommand SetCurrentLocationButtonCommand => new DelegateCommand(async () =>
        {
            var currentPosition = await LocalizationHelpers.GetCurrentUserPositionAsync(false);
            UpdateDataUsingPosition(currentPosition);
        });

        private void UpdateDataUsingPosition(Position position)
        {
            BusStopToEdit.Latitude = position.Latitude;
            BusStopToEdit.Longitude = position.Longitude;
            GeoPosition = position;
            MapPins = new ObservableCollection<Pin> { BusStopToEdit.ConvertToFormsMapsPin() };
            MapPosition = position.ToMapsPosition();
        }



        public override void OnNavigatingTo(NavigationParameters parameters)
        {
            if (parameters.ContainsKey("busStopToEdit"))
            {
                BusStopToEdit = parameters["busStopToEdit"] as BusStop;
                SetBusStopToChilds(BusStopToEdit);

                Title = $"{BusStopToEdit.Address}, {BusStopToEdit.Label}";
                MapPosition = GeoPosition.ToMapsPosition();
                var pinToAdd = BusStopToEdit.ConvertToFormsMapsPin();
                MapPins.Add(pinToAdd);
            }
        }

        private void SetBusStopToChilds(BusStop busStop)
        {
            CityNameEntry = busStop.Address;
            StopNameEntry = busStop.Label;
            GeoPosition = new Position(busStop.Latitude, busStop.Longitude);
            Time = DateTime.Now.TimeOfDay;  //TODO: update after db update
        }


    }
}
