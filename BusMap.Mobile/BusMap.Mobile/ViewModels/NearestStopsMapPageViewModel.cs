using System.Collections.ObjectModel;
using System.Threading.Tasks;
using BusMap.Mobile.Helpers;
using BusMap.Mobile.Services;
using Prism.Navigation;
using Xamarin.Forms;
using Xamarin.Forms.GoogleMaps;


namespace BusMap.Mobile.ViewModels
{
    public class NearestStopsMapPageViewModel : ViewModelBase
    {
        private readonly ILogger _logger = DependencyService.Get<ILogManager>().GetLog();
        private readonly IDataService _dataService;

        private ObservableCollection<Pin> _pins;
        private MapSpan _mapPosition;

        public ObservableCollection<Pin> Pins
        {
            get => _pins;
            set => SetProperty(ref _pins, value);
        }

        public MapSpan MapPosition
        {
            get => _mapPosition;
            set => SetProperty(ref _mapPosition, value);
        }


        public NearestStopsMapPageViewModel(IDataService dataService, INavigationService navigationService)
            : base (navigationService)
        {
            _dataService = dataService;
            Pins = new ObservableCollection<Pin>();
        }


        private async Task GetPins()
        {
            var busStops = await _dataService.GetBusStops();
            var pins = busStops.ToGoogleMapsPins();
            Pins.AddRange(pins);
        }



        public override async void OnNavigatedTo(NavigationParameters parameters)
        {
            try
            {
                var position = await LocalizationHelpers.GetCurrentUserPositionAsync(true);
                MapPosition = position.ToMapSpan(Distance.FromKilometers(10));
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
