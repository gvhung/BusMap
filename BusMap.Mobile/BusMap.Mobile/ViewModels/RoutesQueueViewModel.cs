using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;
using BusMap.Mobile.Helpers;
using BusMap.Mobile.Models;
using BusMap.Mobile.Services;
using BusMap.Mobile.SQLite.Repositories;
using BusMap.Mobile.Views;
using Prism.Commands;
using Prism.Navigation;

namespace BusMap.Mobile.ViewModels
{
    public class RoutesQueueViewModel : ViewModelBase
    {
        private readonly IDataService _dataService;
        private readonly IVotedQueuedRoutesRepository _votedQueuedRoutesRepository;

        private List<RouteQueued> _routeQueue;
        private bool _downloadingRoutes;

        public List<RouteQueued> RouteQueue
        {
            get => _routeQueue;
            set => SetProperty(ref _routeQueue, value);
        }

        public bool DownloadingRoutes
        {
            get => _downloadingRoutes;
            set => SetProperty(ref _downloadingRoutes, value);
        }


        public RoutesQueueViewModel(INavigationService navigationService, IDataService dataService, 
            IVotedQueuedRoutesRepository votedQueuedRoutesRepository) 
            : base(navigationService)
        {
            _dataService = dataService;
            _votedQueuedRoutesRepository = votedQueuedRoutesRepository;

            Title = "Queued routes";
            //RouteQueue = new List<RouteQueued>();
        }


        public ICommand SelectedRouteCommand => new DelegateCommand<RouteQueued>(async routeQueued =>
        {
            var navigationParams = new NavigationParameters();
            navigationParams.Add("selectedQueuedRoute", routeQueued);

            await NavigationService.NavigateAsync(nameof(QueuedRouteDetailsPage), navigationParams);
        });




        //--NAVIGATION--
        //public override async void OnNavigatingTo(NavigationParameters parameters)
        //{
            
        //}

        public override async  void OnNavigatedTo(NavigationParameters parameters)
        {
            DownloadingRoutes = true;
            var queuedRoutes = await DownloadQueuedRoutesAsync();
            RouteQueue = new List<RouteQueued>(queuedRoutes);
            DownloadingRoutes = false;
        }


        //TODO: download range using current localization
        private async Task<IEnumerable<RouteQueued>> DownloadQueuedRoutesAsync()
        {
            var currentPosition = await LocalizationHelpers.GetCurrentUserPositionAsync(false);
            var routes = await _dataService.GetQueuedRoutesInRange(currentPosition, StaticVariables.Range);
            var notVotedRoutes = DistinctUsingLocalDb(routes);
            return notVotedRoutes;
        }

        private IEnumerable<RouteQueued> DistinctUsingLocalDb(IEnumerable<RouteQueued> routes)
        {
            List<int> votedQueuedIds = _votedQueuedRoutesRepository?.GetAllVotedQueuedRoutes().Select(x => x.Id).ToList();

            if (votedQueuedIds.Count == 0)
                return routes;

            var result = routes.Where(r => !votedQueuedIds.Contains(r.Id));
            return result;
        }
    }
}
