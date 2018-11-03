using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;
using BusMap.Mobile.Helpers;
using BusMap.Mobile.Models;
using BusMap.Mobile.Services;
using BusMap.Mobile.Views;
using Prism.Commands;
using Prism.Navigation;

namespace BusMap.Mobile.ViewModels
{
    public class RoutesQueueViewModel : ViewModelBase
    {
        private readonly IDataService _dataService;

        private ObservableCollection<RouteQueued> _routeQueue;
        private bool _downloadingRoutes;

        public ObservableCollection<RouteQueued> RouteQueue
        {
            get => _routeQueue;
            set => SetProperty(ref _routeQueue, value);
        }

        public bool DownloadingRoutes
        {
            get => _downloadingRoutes;
            set => SetProperty(ref _downloadingRoutes, value);
        }


        public RoutesQueueViewModel(INavigationService navigationService, IDataService dataService) 
            : base(navigationService)
        {
            _dataService = dataService;
            Title = "Queued routes";
            RouteQueue = new ObservableCollection<RouteQueued>();
        }


        public ICommand SelectedRouteCommand => new DelegateCommand<RouteQueued>(async routeQueued =>
        {
            var navigationParams = new NavigationParameters();
            navigationParams.Add("selectedQueuedRoute", routeQueued);

            await NavigationService.NavigateAsync(nameof(QueuedRouteDetailsPage), navigationParams);
        });




        //--NAVIGATION--
        public async override void OnNavigatingTo(NavigationParameters parameters)
        {
            DownloadingRoutes = true;
            var queuedRoutes = await DownloadQueuedRoutesAsync();
            RouteQueue.AddRange(queuedRoutes);
            DownloadingRoutes = false;
        }

        //TODO: download range using current localization
        private async Task<IEnumerable<RouteQueued>> DownloadQueuedRoutesAsync()
        {
            var currentPosition = await LocalizationHelpers.GetCurrentUserPositionAsync(false);
            var routes = await _dataService.GetQueuedRoutesInRange(currentPosition, StaticVariables.Range);
            return routes;
        }
    }
}
