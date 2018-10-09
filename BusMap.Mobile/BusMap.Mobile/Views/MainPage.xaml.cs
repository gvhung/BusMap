using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using BusMap.Mobile.Helpers;
using BusMap.Mobile.Models;
using BusMap.Mobile.Services;
using Plugin.Geolocator;
using Xamarin.Forms;

namespace BusMap.Mobile.Views
{
    public partial class MainPage : MasterDetailPage
    {
        private readonly ILogger _logger = DependencyService.Get<ILogManager>().GetLog();

        public MainPage()
        {
            InitializeComponent();
            NavigationPage.SetHasNavigationBar(this, false);
            LocalizationAlert();
        }

        private void LocalizationAlert()
        {
            if (!CheckIfLocalizationIsEnabled())
                MessagingHelper.DisplayAlert("Warning.", 
                    "Localization in your device is disabled.\n" +
                    "Please enable localization, if you want use all app features.");
        }

        private bool CheckIfLocalizationIsEnabled()
        {
            var locator = CrossGeolocator.Current;
            return locator.IsGeolocationEnabled;
        }

        private async void NearestStopsButton_OnClicked(object sender, EventArgs e)
        {
            await Navigation.PushAsync(new NearestStopsMapPage());
        }

        private async void SearchButton_OnClicked(object sender, EventArgs e)
        {
            await Navigation.PushAsync(new RoutesListPage(FromEntry.Text, ToEntry.Text));
        }



        private async void TrackNewRouteButton_OnClicked(object sender, EventArgs e)
        {
            await Navigation.PushAsync(new TrackNewRoutePage());
        }
    }
}
