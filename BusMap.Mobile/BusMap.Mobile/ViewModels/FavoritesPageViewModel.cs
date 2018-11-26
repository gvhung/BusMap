using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Windows.Input;
using BusMap.Mobile.Models;
using BusMap.Mobile.Services;
using BusMap.Mobile.SQLite.Models;
using BusMap.Mobile.SQLite.Repositories;
using BusMap.Mobile.Views;
using Prism.Commands;
using Prism.Navigation;

namespace BusMap.Mobile.ViewModels
{
    public class FavoritesPageViewModel : ViewModelBase
    {
        private readonly IFavoriteRoutesRepository _repository;
        private readonly IDataService _dataService;

        private IEnumerable<Route> _routes;
        private IEnumerable<FavoriteRoute> _favoriteRoutes;


        public IEnumerable<Route> Routes
        {
            get => _routes;
            set => SetProperty(ref _routes, value);
        }

        public IEnumerable<FavoriteRoute> FavoriteRoutes
        {
            get => _favoriteRoutes;
            set => SetProperty(ref _favoriteRoutes, value);
        }

        public FavoritesPageViewModel(INavigationService navigationService, 
            IFavoriteRoutesRepository repository,
            IDataService dataService) : base(navigationService)
        {
            _repository = repository;
            _dataService = dataService;
        }





        public ICommand SelectedRouteCommand => new DelegateCommand<Route>(async route =>
        {
            var parameters = new NavigationParameters();
            var routeToSend = await _dataService.GetRouteAsync(route.Id);
            parameters.Add("route", routeToSend);
            await NavigationService.NavigateAsync(nameof(RouteDetailsPage), parameters);
            //TODO: Add activity indicator at the middle
        });



        public override void OnNavigatingTo(NavigationParameters parameters)
        {
            FavoriteRoutes = _repository.GetAllFavorites();
        }

        public override async void OnNavigatedTo(NavigationParameters parameters)
        {
            //var routes = new List<Route>();

            //foreach (var favoriteRoute in FavoriteRoutes)
            //{
            //    routes.Add(await _dataService.GetRouteAsync(favoriteRoute.Id));
            //}
            var ids = FavoriteRoutes.Select(r => r.Id).ToList();
            Routes = await _dataService.GetFavoriteRoutes(ids);
        }

    }
}
