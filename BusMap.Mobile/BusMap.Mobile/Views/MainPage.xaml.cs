using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using BusMap.Mobile.Helpers;
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

        private void Button_OnClicked(object sender, EventArgs e)
        {
            string msg = "test toast";
            MessagingHelper.Toast(msg, ToastTime.LongTime);
            _logger.Info("Test toast showed.");
        }
    }
}
