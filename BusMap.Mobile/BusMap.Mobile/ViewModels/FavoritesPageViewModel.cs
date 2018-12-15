using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Windows.Input;
using BusMap.Mobile.Helpers;
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
        private readonly IFavoriteRoutesRepository _favoritesRepository;
        private readonly IDataService _dataService;

        private ObservableCollection<Route> _routes;
        private IEnumerable<FavoriteRoute> _favoriteRoutes;


        public ObservableCollection<Route> Routes
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
            IFavoriteRoutesRepository favoritesRepository,
            IDataService dataService) : base(navigationService)
        {
            _favoritesRepository = favoritesRepository;
            _dataService = dataService;
            FavoriteRoutes = new List<FavoriteRoute>();
            Routes = new ObservableCollection<Route>();
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
            FavoriteRoutes = _favoritesRepository.GetAllFavorites();
        }

        public override async void OnNavigatedTo(NavigationParameters parameters)
        {
            if (Routes.Count > 0)
            {
                FavoriteRoutes = _favoritesRepository.GetAllFavorites();
            }
                
            if (_favoritesRepository.GetAllFavorites().Count() < Routes.Count || Routes.Count == 0)
            {
                Routes.Clear();
                var ids = FavoriteRoutes.Select(r => r.Id).ToList();
                if (ids.Count > 0)
                    Routes.AddRange(await _dataService.GetFavoriteRoutes(ids));
            }
        }

    }
}
