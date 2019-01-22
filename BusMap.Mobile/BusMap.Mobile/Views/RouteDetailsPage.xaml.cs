using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using BusMap.Mobile.Models;
using BusMap.Mobile.ViewModels;
using Plugin.Iconize;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace BusMap.Mobile.Views
{
	[XamlCompilation(XamlCompilationOptions.Compile)]
	public partial class RouteDetailsPage : IconTabbedPage
    {
        private readonly RouteDetailsPageViewModel _viewModel;

		public RouteDetailsPage ()
		{
			InitializeComponent ();
            _viewModel = BindingContext as RouteDetailsPageViewModel;

        }

        private void RouteDelaysListView_OnItemSelected(object sender, SelectedItemChangedEventArgs e)
        {
            var listView= sender as ListView;
            listView.SelectedItem = null;
        }


    }
}