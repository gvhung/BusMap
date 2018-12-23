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
	public partial class RouteDetailsPage : TabbedPage
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


        private void SetAlarmButton_OnClicked(object sender, EventArgs e) //Library doesn't support behaviors.
        {
            var button = sender as Button;
            var stack = button.Parent as StackLayout;
            var busStop = stack.BindingContext as BusStop;
            _viewModel.SetAlarmCommand.Execute(busStop);
        }

        private void ShowOnMapButton_OnClicked(object sender, EventArgs e) //Library doesn't support behaviors.
        {
            var button = sender as Button;
            var stack = button.Parent as StackLayout;
            var busStop = stack.BindingContext as BusStop;
            _viewModel.ShowBusStopOnMapCommand.Execute(busStop);
        }

    }
}