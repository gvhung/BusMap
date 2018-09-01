using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
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
    }
}
