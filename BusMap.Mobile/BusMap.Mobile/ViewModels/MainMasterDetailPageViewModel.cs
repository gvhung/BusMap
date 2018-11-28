using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;
using BusMap.Mobile.Views;
using Prism.Commands;
using Prism.Navigation;

namespace BusMap.Mobile.ViewModels
{
    public class MainMasterDetailPageViewModel : ViewModelBase
    {
        public MainMasterDetailPageViewModel(INavigationService navigationService) 
            : base(navigationService)
        {
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






        private async Task NavigateToPageAsync(string pageName)
            => await NavigationService.NavigateAsync(
                new Uri($"/MainMasterDetailPage/CustomNavigationPage/" + pageName, 
                    UriKind.Absolute));

    }
}
