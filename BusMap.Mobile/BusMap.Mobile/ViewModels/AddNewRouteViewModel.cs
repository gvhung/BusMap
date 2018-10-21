using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;
using BusMap.Mobile.Helpers;
using BusMap.Mobile.Models;
using BusMap.Mobile.Services;
using Plugin.Geolocator;
using Plugin.Geolocator.Abstractions;
using Prism.Commands;
using Prism.Navigation;
using Prism.Services;
using Xamarin.Forms;

namespace BusMap.Mobile.ViewModels
{
    public class AddNewRouteViewModel : ViewModelBase
    {
        private int _lastIdFromDb;

        private IPageDialogService _pageDialogService;
        private Position _geoPosition;
        private string _cityNameEntry;
        private string _stopNameEntry;
        private TimeSpan _time;
        private bool _saveButtonIsEnabled;
        private bool _positionIsDownloading;

        public Position GeoPosition
        {
            get => _geoPosition;
            set => SetProperty(ref _geoPosition, value);
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

        public bool SaveButtonIsEnabled
        {
            get => _saveButtonIsEnabled;
            set => SetProperty(ref _saveButtonIsEnabled, value);
        }

        public bool PositionIsDownloading
        {
            get => _positionIsDownloading;
            set => SetProperty(ref _positionIsDownloading, value);
        }

        public AddNewRouteViewModel(INavigationService navigationService, IPageDialogService pageDialogService) 
            : base (navigationService)
        {
            _pageDialogService = pageDialogService;
            Title = "Add new bus stop";
            Time = DateTime.Now.TimeOfDay;
        }


        public ICommand SaveButtonCommand => new DelegateCommand(async () =>
        {
            var newBusStop = new BusStop
            {
                Address = CityNameEntry,
                Label = StopNameEntry,
                Latitude = GeoPosition.Latitude,
                Longitude = GeoPosition.Longitude,
                Id = _lastIdFromDb
            };

            var navigationParameters = new NavigationParameters();
            navigationParameters.Add("newBusStop", newBusStop);

            await NavigationService.GoBackAsync(navigationParameters);
        });



        public override async void OnNavigatedTo(NavigationParameters parameters)
        {
            try
            {               
                PositionIsDownloading = true;
                GeoPosition = await LocalizationHelpers.GetCurrentUserPositionAsync(false);
                _lastIdFromDb = (int)parameters["lastIndex"] + 1;
                SaveButtonIsEnabled = true;               
                PositionIsDownloading = false;
            }
            catch (TaskCanceledException)
            {
                MessagingHelper.Toast("Unable to get position.", ToastTime.ShortTime);
            }
            
        }

        



    }
}
