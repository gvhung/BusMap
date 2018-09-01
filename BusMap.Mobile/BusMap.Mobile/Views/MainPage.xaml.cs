using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using BusMap.Mobile.Helpers;
using Xamarin.Forms;

namespace BusMap.Mobile.Views
{
    public partial class MainPage : MasterDetailPage
    {
        public MainPage()
        {
            InitializeComponent();
            NavigationPage.SetHasNavigationBar(this, false);
        }

        private async void NearestStopsButton_OnClicked(object sender, EventArgs e)
        {
            await Navigation.PushAsync(new NearestStopsMapPage());
        }

        private void Button_OnClicked(object sender, EventArgs e)
        {
            string msg = "test toast";
            ToastMessage.LongTime(msg);
        }
    }
}
