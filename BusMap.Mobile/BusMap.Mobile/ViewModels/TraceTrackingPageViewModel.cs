using System;
using System.Collections.Generic;
using System.Data.SqlTypes;
using System.Linq;
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
using Xamarin.Forms;
using Xamarin.Forms.GoogleMaps;

namespace BusMap.Mobile.ViewModels
{
    public class TraceTrackingPageViewModel : ViewModelBase
    {
        private readonly IDataService _dataService;
        private readonly IGeolocationBackgroundService _geolocationBackgroundService;

        private int _lastBusStopId;

        private Route _route;
        private bool _isTrackingStarted;
        private string _testLabelText;

        private List<BusStop> _busStops;
        private bool _isTrackingEnabled = true;

        public Route Route
        {
            get => _route;
            set
            {
                SetProperty(ref _route, value);
                _busStops.AddRange(_route.BusStops);
                _lastBusStopId = _busStops.Last().Id;
            }
        }

        public bool IsTrackingStarted
        {
            get => _isTrackingStarted;
            set => SetProperty(ref _isTrackingStarted, value);
        }

        public string TestLabelText
        {
            get => _testLabelText;
            set => SetProperty(ref _testLabelText, value);
        }

        public bool IsTrackingEnabled
        {
            get => _isTrackingEnabled;
            set => SetProperty(ref _isTrackingEnabled, value);
        }


        public TraceTrackingPageViewModel(INavigationService navigationService, IDataService dataService) 
            : base(navigationService)
        {
            _dataService = dataService;
            _geolocationBackgroundService = DependencyService.Get<IGeolocationBackgroundService>();
            _busStops = new List<BusStop>();

            MessagingCenter.Subscribe<Application>(this, "STOP_TRACKING", a => IsTrackingStarted = false);
        }


        public ICommand StartTrackingCommand => new DelegateCommand(async () =>
        {
            

            if (!IsTrackingStarted)
            {
                IsTrackingStarted = true;
                await _geolocationBackgroundService.StartService();
                CrossGeolocator.Current.PositionChanged += GeolocatorOnPositionChanged;
            }
            else
            {
                CrossGeolocator.Current.PositionChanged -= GeolocatorOnPositionChanged;
                await _geolocationBackgroundService.StopServiceAsync();
                IsTrackingStarted = false;
            }

            
        });

        private void GeolocatorOnPositionChanged(object sender, PositionEventArgs e)
        {
            Device.BeginInvokeOnMainThread(() =>
            {
                var position = e.Position;

                TestLabelText = $"{position.Latitude}, {position.Longitude}";

                var distanceArray = new double[Route.BusStops.Count];

                for (int i = 0; i < _busStops.Count; i++)
                {
                    var busStop = _busStops[i];
                    var busStopPosition =
                        new Plugin.Geolocator.Abstractions.Position(busStop.Latitude, busStop.Longitude);
                    var distance = GeolocatorUtils.CalculateDistance(position, busStopPosition, 
                        GeolocatorUtils.DistanceUnits.Kilometers);
                    distanceArray[i] = distance;

                    if (distance < 0.05)
                    {
                        TestLabelText = $"{busStop.Address}, {busStop.Label}";
                        _busStops.Remove(busStop);
                        var currentHour = DateTime.Now;
                        _dataService.PostBusStopTraceAsync(new BusStopTrace
                        {
                            BusStopId = busStop.Id,
                            Hour = new TimeSpan(currentHour.Hour, currentHour.Minute, 0)
                        });
                        MessagingHelper.Toast($"Trace added: {busStop.Address}, {busStop.Label}", ToastTime.LongTime);

                        if (busStop.Id == _lastBusStopId)
                        {
                            CrossGeolocator.Current.PositionChanged -= GeolocatorOnPositionChanged;
                            _geolocationBackgroundService.StopServiceAsync().Wait();
                            IsTrackingStarted = false;
                            MessagingHelper.Toast($"Last bus stop arrived. Tracking ended.", ToastTime.LongTime);
                        }

                        return;
                    }

                }
            });
        }


        //--NAVIGATION--
        public override void OnNavigatingTo(NavigationParameters parameters)
        {
            if (parameters.ContainsKey("route"))
            {
                Route = parameters["route"] as Route;
                Title = $"New Track for {Route.Name}";
            }

            var routeStartingTime = Route.BusStops.First().Hour;
            var routeEndingTime = Route.BusStops.Last().Hour;
            var currentTime = DateTime.Now.TimeOfDay;

            if (currentTime < routeStartingTime.Subtract(new TimeSpan(1, 0, 0))
                || currentTime > routeEndingTime.Add(new TimeSpan(1, 0, 0)))
            {
                IsTrackingEnabled = false;
            }
                
        }

        public override async void OnNavigatedFrom(NavigationParameters parameters)
        {
            await CrossGeolocator.Current.StopListeningAsync();
        }

    }
}
