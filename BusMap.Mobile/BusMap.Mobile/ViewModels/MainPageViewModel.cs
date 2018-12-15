using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Net.Http;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;
using BusMap.Mobile.Annotations;
using BusMap.Mobile.Helpers;
using BusMap.Mobile.Models;
using BusMap.Mobile.Services;
using BusMap.Mobile.Views;
using Plugin.Geolocator;
using Prism.Commands;
using Prism.Navigation;
using Prism.Services;
using Xamarin.Forms;

namespace BusMap.Mobile.ViewModels
{
    public class MainPageViewModel : ViewModelBase
    {
        private readonly IDataService _dataService;
        private readonly IPageDialogService _pageDialogService;

        private string _searchRoutesQueryString;

        private string _startBusStopName;
        private string _destinationBusStopName;
        private bool _isBusy;
        private string _queueButtonText = "New routes queue";
        private bool _queuedRoutesButtonIsVisible;


        public string StartBusStopName
        {
            get => _startBusStopName;
            set => SetProperty(ref _startBusStopName, value);
        }

        public string DestinationBusStopName
        {
            get => _destinationBusStopName;
            set => SetProperty(ref _destinationBusStopName, value);
        }

        public bool IsBusy
        {
            get => _isBusy;
            set => SetProperty(ref _isBusy, value);
        }

        public string QueueButtonText
        {
            get => _queueButtonText;
            set => SetProperty(ref _queueButtonText, value);
        }

        public bool QueuedRoutesButtonIsVisible
        {
            get => _queuedRoutesButtonIsVisible;
            set => SetProperty(ref _queuedRoutesButtonIsVisible, value);
        }


        public MainPageViewModel(IDataService dataService, INavigationService navigationService, 
            IPageDialogService pageDialogService) 
            : base(navigationService)
        {
            _pageDialogService = pageDialogService;
            _dataService = dataService;
            LocalizationAlert();
            _dataService.HttpClientFindEvent += (s, e) => _searchRoutesQueryString = e;
        }


        private void LocalizationAlert()
        {
            if (!CheckIfLocalizationIsEnabled())
                MessagingHelper.DisplayAlert("Warning.",
                    "Localization in your device is disabled.\n" +
                    "Please enable localization, if you want use all app features.");
        }

        private bool CheckIfLocalizationIsEnabled()
        {
            var locator = CrossGeolocator.Current;
            return locator.IsGeolocationEnabled;
        }



        public ICommand SearchAndNavigateToRoutesListPageCommand => new DelegateCommand(async () =>
        {
            IsBusy = true;
            List<Route> foundedRoutes = await SearchRoutes();
            IsBusy = false;

            if (foundedRoutes == null)
                return;

            var parameters = new NavigationParameters();
            parameters.Add("foundedRoutes", foundedRoutes);
            parameters.Add("startBusStopName", StartBusStopName);
            parameters.Add("destinationBusStopName", DestinationBusStopName);
            parameters.Add("searchRoutesQueryString", _searchRoutesQueryString);

            await NavigationService.NavigateAsync(nameof(RoutesListPage), parameters);
        });

        public ICommand NavigateToNearestStopsPageCommand => new DelegateCommand(async () =>
            await NavigationService.NavigateAsync(nameof(NearestStopsMapPage)));

        public ICommand NavigateToTrackNewRouteCommand => new DelegateCommand(async () => 
            await NavigationService.NavigateAsync(nameof(TrackNewRoutePage)));

        public ICommand NavigateToQueueCommand => new DelegateCommand(async () =>
            await NavigationService.NavigateAsync(nameof(RoutesQueuePage)));

        public ICommand AdvancedButtonCommand => new Command(async () =>
        await NavigationService.NavigateAsync(nameof(AdvancedSearchPage)));

        public ICommand NavigateToFavoritePage => new DelegateCommand(async () => 
            await NavigationService.NavigateAsync(nameof(FavoritesPage)));


        public override async void OnNavigatedTo(NavigationParameters parameters)
        {
            if (!CrossGeolocator.Current.IsGeolocationAvailable)    //Checking if permission Granted. Else app will crash on 1 run.
                await Task.Delay(1000);

            var currentPosition = await LocalizationHelpers.GetCurrentUserPositionAsync(false);
            var nOfNewRoutesInRange = 0;

            try
            {
                nOfNewRoutesInRange = await _dataService.GetNumberOfQueuedRoutesInRangeAsync(currentPosition, StaticVariables.Range);
            }
            catch (HttpRequestException)
            {
                await DialogWhenHttpRequestException();
            }


            if (nOfNewRoutesInRange > 0)
            {
                QueuedRoutesButtonIsVisible = true;
            }
            
            QueueButtonText = $"New routes queue ({nOfNewRoutesInRange})";
        }


        private async Task<List<Route>> SearchRoutes()
        {
            List<Route> resultRoutes = null;

            if (ValidateCityEntries())
            {
                try
                {
                    resultRoutes = await _dataService.FindRoutesAsync(StartBusStopName, DestinationBusStopName);
                }
                catch (HttpRequestException ex)
                {
                    MessagingHelper.Toast(ex.Message, ToastTime.LongTime);
                }

            }
            else
            {
                return null;
            }

            return resultRoutes;
        }

        private bool ValidateCityEntries()
        {
            if (String.IsNullOrEmpty(StartBusStopName)
                || String.IsNullOrEmpty(DestinationBusStopName)
                || String.IsNullOrWhiteSpace(StartBusStopName)
                || String.IsNullOrWhiteSpace(DestinationBusStopName))
            {
                MessagingHelper.Toast("Please enter bus stops in both entries.", ToastTime.ShortTime);
                return false;
            }

            return true;
        }

        private async Task DialogWhenHttpRequestException()
        {
            var dialogResult = await _pageDialogService.DisplayAlertAsync("Connection with server failed",
                "Please enable internet connection in your device.", "Done", "Close app");
            if (dialogResult)
            {
                OnNavigatedTo(new NavigationParameters());
            }
            else
            {
                //Closing app
                System.Diagnostics.Process.GetCurrentProcess().CloseMainWindow();
            }
            
        }

    }
}
