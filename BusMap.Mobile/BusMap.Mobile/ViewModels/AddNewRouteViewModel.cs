using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Text;
using System.Threading.Tasks;
using BusMap.Mobile.Helpers;
using Plugin.Geolocator;
using Plugin.Geolocator.Abstractions;
using Prism.Navigation;
using Prism.Services;
using Xamarin.Forms;

namespace BusMap.Mobile.ViewModels
{
    public class AddNewRouteViewModel : ViewModelBase
    {
        private IPageDialogService _pageDialogService;
        private Position _geoPosition;
        private string _cityLabel;
        private string _addressLabel;
        private DateTime _time;

        public Position GeoPosition
        {
            get => _geoPosition;
            set => SetProperty(ref _geoPosition, value);
        }

        public string CityLabel
        {
            get => _cityLabel;
            set => SetProperty(ref _cityLabel, value);
        }

        public string AddressLabel
        {
            get => _addressLabel;
            set => SetProperty(ref _addressLabel, value);
        }

        public DateTime Time
        {
            get => _time;
            set => SetProperty(ref _time, value);
        }


        public AddNewRouteViewModel(INavigationService navigationService, IPageDialogService pageDialogService) 
            : base (navigationService)
        {
            _pageDialogService = pageDialogService;
            Title = "Add new bus stop";
        }


        private async Task<Position> GetCurrentUserPositionAsync()
        {
            var locator = CrossGeolocator.Current;
            locator.DesiredAccuracy = 20;

            MessagingHelper.Toast("Getting your localization...", ToastTime.ShortTime);
            var geoPosition =
                await locator.GetPositionAsync(timeout: TimeSpan.FromSeconds(10));

            if (geoPosition != null)
                MessagingHelper.Toast("Position obtained successfully.", ToastTime.ShortTime);
            
            return geoPosition;
        }




        public override async void OnNavigatedTo(NavigationParameters parameters)
        {
            GeoPosition = await GetCurrentUserPositionAsync();
            Time = DateTime.Now;
        }

        public override void Destroy()
        {
            
        }

    }
}
