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

	}
}