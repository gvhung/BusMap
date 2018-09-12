﻿using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;
using BusMap.Mobile.Annotations;
using BusMap.Mobile.Helpers;
using BusMap.Mobile.Services;
using Plugin.Geolocator;
using Xamarin.Forms;
using Xamarin.Forms.Maps;

namespace BusMap.Mobile.ViewModels
{
    public class NearestStopsMapPageViewModel : INotifyPropertyChanged
    {
        private  readonly ILogger _logger = DependencyService.Get<ILogManager>().GetLog();
        private readonly IDataService _dataService = new DataService();

        private Position _userPosition;
        public Position UserPosition
        {
            get => _userPosition;
            set
            {
                _userPosition = value;
                OnPropertyChanged();
            }
        }

        public ObservableCollection<Pin> Pins { get; set; }

        public NearestStopsMapPageViewModel()
        {
            //UserPosition = new Position(50.036124, 22.0035);
            SetCurrentUserLocation();
            Pins = _dataService.GetPins();
        }

        private async void SetCurrentUserLocation()
        {

            Position startMapPosition = UserPosition;
            try
            {
                var position = await GetCurrentUserLocationAsync();
                UserPosition = position;

                if (startMapPosition != position)
                {
                    MessagingHelper.Toast("Position obtained successfully.", ToastTime.LongTime);
                }
                else
                {
                    MessagingHelper.DisplayAlert("Could not obtain position");
                    await Application.Current.MainPage.Navigation.PopAsync(true);
                }
            }
            catch (Exception)
            {
                MessagingHelper.Toast("Unable to get location", ToastTime.LongTime);
                await Application.Current.MainPage.Navigation.PopToRootAsync();
            }
        }

        private async Task<Position> GetCurrentUserLocationAsync()
        {
            var locator = CrossGeolocator.Current;
            locator.DesiredAccuracy = 20;
            

            MessagingHelper.Toast("Getting your localization...", ToastTime.ShortTime);
            var geoPosition =
                await locator.GetPositionAsync(timeout: TimeSpan.FromSeconds(10)); //TODO: cancel-token (?)

            return new Position(geoPosition.Latitude, geoPosition.Longitude);
        }


        public event PropertyChangedEventHandler PropertyChanged;

        [NotifyPropertyChangedInvocator]
        protected virtual void OnPropertyChanged([CallerMemberName] string propertyName = null)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }
    }
}
