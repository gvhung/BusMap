using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Net.Http;
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
    public class AddNewBusStopViewModel : ViewModelBase
    {
        private readonly IPageDialogService _pageDialogService;
        private readonly IDataService _dataService;
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

        public AddNewBusStopViewModel(INavigationService navigationService, IPageDialogService pageDialogService,
            IDataService dataService) : base(navigationService)
        {
            _pageDialogService = pageDialogService;
            _dataService = dataService;
            Title = "Add new bus stop";
            Time = DateTime.Now.TimeOfDay;
        }


        public ICommand SaveButtonCommand => new DelegateCommand(async () =>
        {
            if (!ValidateEntries())
                return;

            var newBusStop = new BusStopQueued()
            {
                Address = CityNameEntry,
                Label = StopNameEntry,
                Latitude = GeoPosition.Latitude,
                Longitude = GeoPosition.Longitude,
                Hour = new TimeSpan(Time.Hours, Time.Minutes, 0),
                Id = 0
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
                await DownloadAndSetEntryNames();
                SaveButtonIsEnabled = true;
                PositionIsDownloading = false;
            }
            catch (TaskCanceledException)
            {
                MessagingHelper.Toast("Unable to get position.", ToastTime.ShortTime);
            }

        }


        private bool ValidateEntries()
        {
            if (string.IsNullOrEmpty(CityNameEntry) || CityNameEntry.Length < 3)
            {
                MessagingHelper.Toast("City name must be entered and have at least 3 characters.",
                    ToastTime.LongTime);
                return false;
            }

            return true;
        }

        private async Task DownloadAndSetEntryNames()
        {
            try
            {
                var reversedGeocode = await _dataService.GetReversedGeocodeForLatLngAsync(GeoPosition);
                CityNameEntry = reversedGeocode.City;
                StopNameEntry = reversedGeocode.Street;
            }
            catch (HttpRequestException)
            {
                MessagingHelper.Toast("Could not obtain your position", ToastTime.LongTime);
                await NavigationService.GoBackAsync();
            }
            
        }


    }
}
