using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;
using BusMap.Mobile.Services;
using BusMap.Mobile.SQLite.Models;
using BusMap.Mobile.SQLite.Repositories;
using BusMap.Mobile.Views;
using Prism.Commands;
using Prism.Navigation;

namespace BusMap.Mobile.ViewModels
{
    public class MainMasterDetailPageViewModel : ViewModelBase
    {
        private readonly IRecentSearchRepository _recentSearchRepository;


        private List<RecentSearch> _recentSearch;

        public List<RecentSearch> RecentSearch
        {
            get => _recentSearch;
            set => SetProperty(ref _recentSearch, value);
        }


        public MainMasterDetailPageViewModel(INavigationService navigationService, IRecentSearchRepository recentSearchRepository) 
            : base(navigationService)
        {
            _recentSearchRepository = recentSearchRepository;



            _recentSearchRepository.RecentSearchEvent += DataServiceOnRecentSearchEvent;
        }

        private void DataServiceOnRecentSearchEvent(object sender, string e)
        {
            RecentSearch = _recentSearchRepository.GetRecentSearches().ToList();
        }

        public ICommand OnNavigateCommand => new DelegateCommand<string>(async pageUri => 
            await NavigationService.NavigateAsync(new Uri(pageUri, UriKind.Relative)));

        public ICommand NavigateToMainPageCommand => new DelegateCommand(async () =>
            await NavigateToPageAsync("MainPage"));

        public ICommand NavigateToNearestStopsCommand => new DelegateCommand(async () => 
            await NavigateToPageAsync("NearestStopsMapPage"));

        public ICommand NavigateToTrackNewRouteCommand => new DelegateCommand(async () => 
            await NavigateToPageAsync("TrackNewRoutePage"));

        public ICommand NavigateToFavoriteRoutesCommand => new DelegateCommand(async () => 
            await NavigateToPageAsync("FavoritesPage"));

        public override void OnNavigatedTo(NavigationParameters parameters)
        {
            var search = _recentSearchRepository.GetRecentSearches().ToList();
            if (search.Any())
                RecentSearch = search;
            Debug.WriteLine("----Test----");
        }


        private async Task NavigateToPageAsync(string pageName)
            => await NavigationService.NavigateAsync(
                new Uri($"/MainMasterDetailPage/CustomNavigationPage/" + pageName, 
                    UriKind.Absolute));

    }
}
