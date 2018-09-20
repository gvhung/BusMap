using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using BusMap.Mobile.Models;
using BusMap.Mobile.ViewModels;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace BusMap.Mobile.Views
{
	[XamlCompilation(XamlCompilationOptions.Compile)]
	public partial class RoutesListPage : ContentPage
	{
		public RoutesListPage (string from, string to)
		{
			InitializeComponent ();
		    ((RoutesListPageViewModel) this.BindingContext).StartPoint = from;
		    ((RoutesListPageViewModel)this.BindingContext).DestinationPoint = to;
        }

	    private void Button_OnClicked(object sender, EventArgs e)
	    {
	        Button button = sender as Button;
            StackLayout listViewItem = button.Parent as StackLayout;
	        var route = listViewItem.BindingContext as Route;

	        Navigation.PushAsync(new BusStopsMap(route));
	    }
	}
}