using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;
using BusMap.Mobile.Annotations;
using BusMap.Mobile.Models;
using BusMap.Mobile.Services;
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
        public string StartDestinationTitle => $"{StartPoint} - {DestinationPoint}";

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
