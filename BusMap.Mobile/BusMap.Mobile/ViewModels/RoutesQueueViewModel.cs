using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Text;
using System.Threading.Tasks;
using BusMap.Mobile.Helpers;
using BusMap.Mobile.Models;
using BusMap.Mobile.Services;
using Prism.Navigation;

namespace BusMap.Mobile.ViewModels
{
    public class RoutesQueueViewModel : ViewModelBase
    {
        private readonly IDataService _dataService;

        private ObservableCollection<RouteQueued> _routeQueue;

        public ObservableCollection<RouteQueued> RouteQueue
        {
            get => _routeQueue;
            set => SetProperty(ref _routeQueue, value);
        }


        public RoutesQueueViewModel(INavigationService navigationService, IDataService dataService) 
            : base(navigationService)
        {
            _dataService = dataService;
            Title = "Queued routes";
            RouteQueue = new ObservableCollection<RouteQueued>();
        }






        //--NAVIGATION--
        public async override void OnNavigatingTo(NavigationParameters parameters)
        {
            var queuedRoutes = await DownloadQueuedRoutesAsync();
            RouteQueue.AddRange(queuedRoutes);
        }

        //TODO: download range using current localization
        private async Task<IEnumerable<RouteQueued>> DownloadQueuedRoutesAsync()
            => await _dataService.GetQueuedRoutesAsync();
    }
}
