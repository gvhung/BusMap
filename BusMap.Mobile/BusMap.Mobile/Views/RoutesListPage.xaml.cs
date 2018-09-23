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

	    public RoutesListPage(RoutesListPageViewModel viewModel)
	    {
            BindingContext = viewModel;
            InitializeComponent();
        }

	    private void Button_OnClicked(object sender, EventArgs e)
	    {
	        Button button = sender as Button;
            Grid gridItem = button.Parent as Grid;
	        var route = gridItem.BindingContext as Route;
	        Navigation.PushAsync(new BusStopsMap(new BusStopsMapViewModel(route)));
	    }
	}
}