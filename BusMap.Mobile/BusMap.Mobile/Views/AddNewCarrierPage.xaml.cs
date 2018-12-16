using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using BusMap.Mobile.Models;
using BusMap.Mobile.ViewModels;
using dotMorten.Xamarin.Forms;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace BusMap.Mobile.Views
{
	[XamlCompilation(XamlCompilationOptions.Compile)]
	public partial class AddNewCarrierPage : ContentPage
	{
	    private AddNewCarrierViewModel _viewModel;

		public AddNewCarrierPage ()
		{
			InitializeComponent ();
		    _viewModel = BindingContext as AddNewCarrierViewModel;

		}

	    


	    private void AutoSuggestBox_OnTextChanged(object sender, AutoSuggestBoxTextChangedEventArgs e)
	    {
	        if (e.Reason != AutoSuggestionBoxTextChangeReason.UserInput)
	        {
                return;
	        }

	        AutoSuggestBox box = sender as AutoSuggestBox;

	        if (string.IsNullOrWhiteSpace(box.Text) || box.Text.Length < 3)
	            box.ItemsSource = null;
	        else
	        {
	            var foundSuggestions = _viewModel.CarrierSuggestions
	                .Where(x => x.Name.ToLowerInvariant()
	                    .StartsWith(box.Text.ToLowerInvariant())).ToList();

	            box.ItemsSource = foundSuggestions;
	        }

	    
        }

       


	    private void AutoSuggestBox_OnSuggestionChosen(object sender, AutoSuggestBoxSuggestionChosenEventArgs e)
	    {
	        var box = sender as AutoSuggestBox;
	        var selectedCarrier = e.SelectedItem as Carrier;

	        box.Text = selectedCarrier?.ToString();
	        _viewModel.CarrierName = box.Text;
	        _viewModel.SelectedCarrier = selectedCarrier; //add selected carrier oject
	    }






	}
}