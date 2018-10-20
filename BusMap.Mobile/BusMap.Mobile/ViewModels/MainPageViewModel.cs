using System;
using System.Collections.Generic;
using System.ComponentModel;
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
using Xamarin.Forms;

namespace BusMap.Mobile.ViewModels
{
    public class MainPageViewModel : ViewModelBase
    {
        private readonly IDataService _dataService;

        private string _startBusStopName;
        private string _destinationBusStopName;
        private bool _isBusy;


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


        public MainPageViewModel(IDataService dataService, INavigationService navigationService) 
            : base(navigationService)
        {
            _dataService = dataService;
            LocalizationAlert();
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


        private async Task<List<Route>> searchRoutes()
        {
            IsBusy = true;
            var resultRoutes = new List<Route>();

            try
            {
                if (String.IsNullOrEmpty(StartBusStopName) 
                    || String.IsNullOrEmpty(DestinationBusStopName)
                    || String.IsNullOrWhiteSpace(StartBusStopName) 
                    || String.IsNullOrWhiteSpace(DestinationBusStopName))
                {
                    MessagingHelper.Toast("Please enter bus stops in both entries.", ToastTime.ShortTime);
                    return null;
                }


                resultRoutes = await _dataService.FindRoutes(StartBusStopName, DestinationBusStopName);
                if (resultRoutes.Count <= 0)
                {
                    MessagingHelper.Toast("No routes found.", ToastTime.LongTime);
                    return null;
                }
            }
            catch (Exception ex)
            {
                MessagingHelper.Toast(ex.Message, ToastTime.LongTime);
            }
            finally
            {
                IsBusy = false;
            }
            return resultRoutes;
        }

        public ICommand SearchAndNavigateToRoutesListPageCommand => new DelegateCommand(async () =>
        {
            var foundedRoutes = await searchRoutes();

            if (foundedRoutes == null)
                return;

            var parameters = new NavigationParameters();
            parameters.Add("foundedRoutes", foundedRoutes);
            parameters.Add("startBusStopName", StartBusStopName);
            parameters.Add("destinationBusStopName", DestinationBusStopName);

            await NavigationService.NavigateAsync(nameof(RoutesListPage), parameters);
        });

        
        public ICommand NavigateToNearestStopsPageCommand => new DelegateCommand(async () =>
            await NavigationService.NavigateAsync(nameof(NearestStopsMapPage)));


        public ICommand NavigateToTrackNewRouteCommand => new DelegateCommand(async () => 
            await NavigationService.NavigateAsync(nameof(TrackNewRoutePage)));


        public ICommand AdvancedButtonCommand => new Command(async () =>
        {
            var test = await _dataService.GetBusStops();
        });



    }
}
