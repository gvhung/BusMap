using System;
using System.Collections.Generic;
using System.Text;
using System.Windows.Input;
using BusMap.Mobile.Helpers;
using BusMap.Mobile.Models;
using BusMap.Mobile.Services;
using Prism.Commands;
using Prism.Navigation;
using Prism.Services;

namespace BusMap.Mobile.ViewModels
{
    public class RouteReportViewModel : ViewModelBase
    {
        private readonly IDataService _dataService;
        private readonly IPageDialogService _pageDialogService;

        private Route _route;
        private string _description;

        public Route Route
        {
            get => _route;
            set => SetProperty(ref _route, value);
        }

        public string Description
        {
            get => _description;
            set => SetProperty(ref _description, value);
        }


        public RouteReportViewModel(INavigationService navigationService, IDataService dataService,
            IPageDialogService pageDialogService) : base(navigationService)
        {
            _dataService = dataService;
            _pageDialogService = pageDialogService;
            Title = "Report route";
        }

        public ICommand SaveReportButtonCommand => new DelegateCommand(async () =>
        {
            var routeReport = new RouteReport
            {
                Description = Description,
                RouteId = Route.Id
            };

            var dialogResult = await _pageDialogService.DisplayAlertAsync("Are you sure?", 
                $"Route you sure you want report route {Route.Name}", "Yes", "No");

            if (dialogResult)
            {
                var result = await _dataService.PostRouteReportAsync(routeReport);
                if (result)
                    MessagingHelper.Toast("Route reported successfully.", ToastTime.LongTime);
                else
                    MessagingHelper.Toast("Something went wrong, try again later.", ToastTime.LongTime);
            }
        });


        public override void OnNavigatingTo(NavigationParameters parameters)
        {
            if (parameters.ContainsKey("route"))
            {
                Route = parameters["route"] as Route;
            }
        }
    }
}
