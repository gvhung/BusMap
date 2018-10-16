using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;
using BusMap.Mobile.Annotations;
using BusMap.Mobile.Helpers;
using BusMap.Mobile.Models;
using BusMap.Mobile.Services;
using Plugin.Geolocator;
using Prism.Navigation;
using Xamarin.Forms;
using Xamarin.Forms.Maps;

namespace BusMap.Mobile.ViewModels
{
    public class NearestStopsMapPageViewModel : ViewModelBase
    {
        private readonly ILogger _logger = DependencyService.Get<ILogManager>().GetLog();
        private IDataService _dataService;

        private Position _userPosition;
        private ObservableCollection<Pin> _pins;

        public Position UserPosition
        {
            get => _userPosition;
            set => SetProperty(ref _userPosition, value);
        }

        public ObservableCollection<Pin> Pins
        {
            get => _pins;
            set => SetProperty(ref _pins, value);
        }


        public NearestStopsMapPageViewModel(IDataService dataService, INavigationService navigationService)
            : base (navigationService)
        {
            _dataService = dataService;
            Pins = new ObservableCollection<Pin>();
            UserPosition = new Position();
            //SetCurrentUserLocation();
            //GetPins();
        }


        private async Task GetPins()
        {
            Pins = new ObservableCollection<Pin>();
            var pins = await _dataService.GetBusStops();
            Pins = pins.ConvertToMapPins();
        }


        //private async void SetCurrentUserLocation()
        //{
        //    Position startMapPosition = UserPosition;
        //    try
        //    {
        //        var position = await GetCurrentUserLocationAsync();
        //        UserPosition = position;

        //        if (startMapPosition != position)
        //        {
        //            MessagingHelper.Toast("Position obtained successfully.", ToastTime.LongTime);
        //        }
        //        else
        //        {
        //            MessagingHelper.DisplayAlert("Could not obtain position");
        //            await Application.Current.MainPage.Navigation.PopAsync(true);
        //        }
        //    }
        //    catch (Exception)
        //    {
        //        MessagingHelper.Toast("Unable to get location", ToastTime.LongTime);
        //        await Application.Current.MainPage.Navigation.PopToRootAsync();
        //    }
        //}


        //private async Task<Position> GetCurrentUserLocationAsync()
        //{
        //    var locator = CrossGeolocator.Current;
        //    locator.DesiredAccuracy = 20;
            

        //    MessagingHelper.Toast("Getting your localization...", ToastTime.ShortTime);
        //    var geoPosition =
        //        await locator.GetPositionAsync(timeout: TimeSpan.FromSeconds(10)); //TODO: cancel-token (?)

        //    return new Position(geoPosition.Latitude, geoPosition.Longitude);
        //}


        public override async void OnNavigatingTo(NavigationParameters parameters)
        {
            try
            {
                var position = await NavigationHelpers.GetCurrentUserPositionAsync(true);
                UserPosition = position.ToMapsPosition();
            }
            catch (TaskCanceledException)
            {
                MessagingHelper.Toast("Unable to get location", ToastTime.ShortTime);
                await NavigationService.GoBackAsync();
            }
            
            await GetPins();
        }


    }
}
