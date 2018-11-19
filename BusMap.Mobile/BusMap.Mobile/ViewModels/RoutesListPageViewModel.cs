using System;
using System.Collections.Generic;
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
using BusMap.Mobile.Views;
using Prism.Commands;
using Prism.Navigation;
using Xamarin.Forms;

namespace BusMap.Mobile.ViewModels
{
    public class RoutesListPageViewModel : ViewModelBase
    {
        private IDataService _dataService;
        private List<Route> _routes;
        private bool _isRefreshing;

        public string StartPoint { get; set; }
        public string DestinationPoint { get; set; }

        public List<Route> Routes
        {
            get => _routes;
            set => SetProperty(ref _routes, value);
        }

        public bool IsRefreshing
        {
            get => _isRefreshing;
            set => SetProperty(ref _isRefreshing, value);
        }

        public RoutesListPageViewModel(IDataService dataService, INavigationService navigationService)
            : base(navigationService)
        {
            _dataService = dataService;
        }


        public ICommand RefreshCommand => new DelegateCommand(async () =>
        {
            await GetRoutes();
        });

        public ICommand SelectedRouteCommand => new DelegateCommand<Route>(async route =>
        {
            var parameters = new NavigationParameters();
            var routeToSend = await _dataService.GetRouteAsync(route.Id);
            parameters.Add("route", routeToSend);
            await NavigationService.NavigateAsync(nameof(RouteDetailsPage), parameters);
            //TODO: Add activity indicator at the middle
        });

        public ICommand SelectedRouteCommand2 => new DelegateCommand<Route>(async route =>
        {
            Helpers.MessagingHelper.Toast("toast", ToastTime.LongTime);
        });



        private async Task GetRoutes()
        {
            IsRefreshing = true;
            Routes = await _dataService.GetRoutes();
            IsRefreshing = false;
        }



        public override void OnNavigatingTo(NavigationParameters parameters)
        {
            Routes = parameters["foundedRoutes"] as List<Route>;
            var startName = parameters["startBusStopName"] as string;
            var destName = parameters["destinationBusStopName"] as string;
            Title = $"{startName} - {destName}";
        }




    }
}
