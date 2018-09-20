using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using BusMap.Mobile.Helpers;
using BusMap.Mobile.Models;
using BusMap.Mobile.ViewModels;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace BusMap.Mobile.Views
{
	[XamlCompilation(XamlCompilationOptions.Compile)]
	public partial class BusStopsMap : ContentPage
	{

	    public BusStopsMap(BusStopsMapViewModel viewModel)
	    {
	        BindingContext = viewModel;
            InitializeComponent();
	    }

	}
}