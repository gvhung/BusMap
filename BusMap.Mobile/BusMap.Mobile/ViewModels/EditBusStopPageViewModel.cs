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
using Xamarin.Forms.GoogleMaps;
using Position = Plugin.Geolocator.Abstractions.Position;

namespace BusMap.Mobile.ViewModels
{
    public class EditBusStopPageViewModel : ViewModelBase
    {
        private BusStopQueued _busStopToEdit;
        private string _cityNameEntry;
        private string _stopNameEntry;
        private TimeSpan _time;
        private Position _geoPosition;
        private MapSpan _mapPosition;
        private ObservableCollection<Pin> _mapPins;


        public BusStopQueued BusStopToEdit
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

        public MapSpan MapPosition
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
            MapPins = new ObservableCollection<Pin>();
        }


        public ICommand SaveButtonCommand => new DelegateCommand(async () =>
        {
            if (!ValidateEntries())
                return;

            var navigationParams = new NavigationParameters();
            var updatedBusStop = new BusStopQueued()
            {
                Address = CityNameEntry,
                Label = StopNameEntry,
                Latitude = GeoPosition.Latitude,
                Longitude = GeoPosition.Longitude,
                Hour = Time
            };

            navigationParams.Add("busStopFromEdit", updatedBusStop);
            await NavigationService.GoBackAsync(navigationParams);
        });

        public ICommand SetCurrentLocationButtonCommand => new DelegateCommand(async () =>
        {
            var currentPosition = await LocalizationHelpers.GetCurrentUserPositionAsync(true);
            UpdateDataUsingPosition(currentPosition);
        });

        public ICommand PinDraggingEndCommand => new DelegateCommand<PinDragEventArgs>(args =>
        {
            var position = args.Pin.Position;
            GeoPosition = new Position(position.Latitude, position.Longitude);
        });

        public ICommand RemoveButtonCommand => new DelegateCommand(async () =>
        {
            var navigationParameters = new NavigationParameters();
            navigationParameters.Add("removeBusStopLabel", BusStopToEdit.Label);
            navigationParameters.Add("removeBusStopAddress", BusStopToEdit.Address);

            await NavigationService.GoBackAsync(navigationParameters);
        });


        public override void OnNavigatingTo(NavigationParameters parameters)
        {
            if (parameters.ContainsKey("busStopToEdit"))
            {
                BusStopToEdit = parameters["busStopToEdit"] as BusStopQueued;
                SetBusStopToChilds(BusStopToEdit);

                Title = $"{BusStopToEdit.Address}, {BusStopToEdit.Label}";
                MapPosition = GeoPosition.ToMapSpan(Distance.FromKilometers(10));
                var pinToAdd = BusStopToEdit.ToGoogleMapsPin();
                pinToAdd.IsDraggable = true;
                MapPins.Add(pinToAdd);
            }
        }

        private void SetBusStopToChilds(BusStopQueued busStop)
        {
            CityNameEntry = busStop.Address;
            StopNameEntry = busStop.Label;
            GeoPosition = new Position(busStop.Latitude, busStop.Longitude);
            Time = busStop.Hour;
        }

        private void UpdateDataUsingPosition(Position position)
        {
            BusStopToEdit.Latitude = position.Latitude;
            BusStopToEdit.Longitude = position.Longitude;
            GeoPosition = position;
            MapPins.Add(BusStopToEdit.ToGoogleMapsPin());
            MapPosition = position.ToMapSpan(Distance.FromKilometers(10));
        }

        private bool ValidateEntries()
        {
            if (string.IsNullOrEmpty(CityNameEntry)
                || CityNameEntry.Length < 3
                || string.IsNullOrEmpty(StopNameEntry)
                || StopNameEntry.Length < 3)
            {
                MessagingHelper.Toast("City name and bus stop name must have at least 3 characters.",
                    ToastTime.LongTime);
                return false;
            }

            return true;
        }


    }
}
