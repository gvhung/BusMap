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
using BusMap.Mobile.Services;
using Plugin.Geolocator;
using Xamarin.Forms;
using Xamarin.Forms.Maps;

namespace BusMap.Mobile.ViewModels
{
    public class NearestStopsMapPageViewModel : INotifyPropertyChanged
    {
        private readonly ILogger _logger = DependencyService.Get<ILogManager>().GetLog();
        private IDataService _dataService;

        private Position _userPosition;
        private ObservableCollection<Pin> _pins;

        public Position UserPosition
        {
            get => _userPosition;
            set
            {
                _userPosition = value;
                OnPropertyChanged();
            }
        }

        public ObservableCollection<Pin> Pins
        {
            get => _pins;
            set
            {
                _pins = value;
                OnPropertyChanged();
            }
        }


        public NearestStopsMapPageViewModel(IDataService dataService)
        {
            _dataService = dataService;
            SetCurrentUserLocation();
            GetPins();

        }

        private async Task GetPins()
        {
            Pins = new ObservableCollection<Pin>();
            Pins = await _dataService.GetPins();
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


        #region TestPurposes only
        public ICommand GetPinsCommand => new Command(async () =>
        {
            await GetPins();
        });

        public ICommand CheckPinsCommand => new Command(() =>
        {
            var msg = $"Count: {Pins.Count}\n";
            foreach (var pin in Pins)
            {
                msg += $"{pin.Label}, {pin.Address}, {pin.Position.Latitude}, {pin.Position.Longitude};";
            }
            MessagingHelper.DisplayAlert(msg);
        });
        #endregion



        public event PropertyChangedEventHandler PropertyChanged;

        [NotifyPropertyChangedInvocator]
        protected virtual void OnPropertyChanged([CallerMemberName] string propertyName = null)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }
    }
}
