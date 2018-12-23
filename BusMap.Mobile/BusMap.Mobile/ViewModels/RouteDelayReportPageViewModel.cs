using System;
using System.Collections.Generic;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;
using BusMap.Mobile.Helpers;
using BusMap.Mobile.Models;
using BusMap.Mobile.Services;
using Prism.Commands;
using Prism.Navigation;
using Prism.Services;

namespace BusMap.Mobile.ViewModels
{
    public class RouteDelayReportPageViewModel : ViewModelBase
    {
        private readonly IDataService _dataService;
        private readonly IPageDialogService _pageDialogService;

        private Route _route;
        private RouteDelay _routeDelay;

        public Route Route
        {
            get => _route;
            set => SetProperty(ref _route, value);
        }

        public RouteDelay RouteDelay
        {
            get => _routeDelay;
            set => SetProperty(ref _routeDelay, value);
        }


        public RouteDelayReportPageViewModel(INavigationService navigationService, IDataService dataService, 
            IPageDialogService pageDialogService) : base(navigationService)
        {
            _dataService = dataService;
            _pageDialogService = pageDialogService;
            RouteDelay = new RouteDelay();
        }


        public ICommand ReportDelayButtonCommand => new DelegateCommand(async () =>
        {
            if (!await ValidateReportCompletion())
                return;

            var dialogResult = await _pageDialogService.DisplayAlertAsync("Everything is correct?",
                $"Title: {RouteDelay.Title}\nDescription: {RouteDelay.Description}",
                "Yes, send", "No");
            if (!dialogResult)
                return;

            var sendReportResult = await _dataService.PostRouteDelay(RouteDelay);
            if (!sendReportResult)
            {
                MessagingHelper.Toast("Could not send raport. Please check your internet connection", ToastTime.LongTime);
                return;
            }

            MessagingHelper.Toast("Report sent", ToastTime.LongTime);
            await NavigationService.GoBackAsync();
        });


        public override void OnNavigatingTo(NavigationParameters parameters)
        {
            Route = parameters["route"] as Route;
            RouteDelay.DateTime = DateTime.Now;
            RouteDelay.RouteId = Route.Id;
            Title = $"Report delay on {Route.Name}";
        }

        private async Task<bool>ValidateReportCompletion()
        {
            if (RouteDelay.Title != null && RouteDelay.Title.Length > 4)
                return true;

            await _pageDialogService.DisplayAlertAsync("Title is to short.",
                "Route delay title must have at least 5 characters.", "Ok");
            return false;
        }

    }
}
