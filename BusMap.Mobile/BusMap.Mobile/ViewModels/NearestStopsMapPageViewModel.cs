using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;
using BusMap.Mobile.Annotations;
using BusMap.Mobile.Services;
using Plugin.Geolocator;
using Xamarin.Forms.Maps;

namespace BusMap.Mobile.ViewModels
{
    public class NearestStopsMapPageViewModel : INotifyPropertyChanged
    {
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
            var pos = await GetCurrentUserLocation();
            UserPosition = pos;
        }

        private async Task<Position> GetCurrentUserLocation()
        {
            var locator = CrossGeolocator.Current;
            locator.DesiredAccuracy = 20;

            var geoPosition =  await locator.GetPositionAsync(timeout: TimeSpan.FromSeconds(5));    //TODO: cancel-token (?)
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
