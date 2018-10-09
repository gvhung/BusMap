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
        }


        private async Task<List<Route>> searchRoutes()
        {
            IsBusy = true;
            var resultRoutes = new List<Route>();

            try
            {
                if (String.IsNullOrEmpty(StartBusStopName) || String.IsNullOrEmpty(DestinationBusStopName))
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

                //var viewModel =
                //    new RoutesListPageViewModel(_dataService, resultRoutes, StartBusStopName, DestinationBusStopName);
                //await Application.Current.MainPage.Navigation.PushAsync(new RoutesListPage(viewModel));
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

            var parameters = new NavigationParameters();
            parameters.Add("foundedRoutes", foundedRoutes);
            parameters.Add("startBusStopName", StartBusStopName);
            parameters.Add("destinationBusStopName", DestinationBusStopName);

            await NavigationService.NavigateAsync("RoutesListPage", parameters);
        });



        public ICommand AdvancedButtonCommand => new Command(async () =>
        {
            var test = await _dataService.GetBusStops();
        });



    }
}
