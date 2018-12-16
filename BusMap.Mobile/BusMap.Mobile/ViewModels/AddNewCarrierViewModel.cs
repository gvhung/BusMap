using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;
using BusMap.Mobile.Helpers;
using BusMap.Mobile.Models;
using BusMap.Mobile.Services;
using Prism.Commands;
using Prism.Navigation;
using Prism.Services;

namespace BusMap.Mobile.ViewModels
{
    public class AddNewCarrierViewModel : ViewModelBase
    {
        private readonly IDataService _dataService;
        private readonly IPageDialogService _pageDialogService;
        private string _carrierName;
        private string _autoSuggestText;

        public string CarrierName
        {
            get => _carrierName;
            set => SetProperty(ref _carrierName, value);
        }

        public string AutoSuggestText
        {
            get => _autoSuggestText;
            set => SetProperty(ref _autoSuggestText, value);
        }

        public Carrier SelectedCarrier { get; set; }

        public List<Carrier> CarrierSuggestions { get; set; }


        public AddNewCarrierViewModel(INavigationService navigationService, IDataService dataService, 
            IPageDialogService pageDialogService) : base (navigationService)
        {
            _dataService = dataService;
            _pageDialogService = pageDialogService;
            Title = "Add new carrier";
        }



        public ICommand SaveButtonCommand => new DelegateCommand(async () =>
        {
            MessagingHelper.Toast("Sending data...", ToastTime.LongTime);
            var carrierExist = await _dataService.CheckIfCarrierExistAsync(CarrierName);

            if (!carrierExist)
            {
                var carrierToAdd = new CarrierQueued{ Name = CarrierName };
                var successfullyAddedCarrier = await _dataService.PostCarrierQueuedAsync(carrierToAdd);

                if (successfullyAddedCarrier != null)
                    MessagingHelper.Toast("Carrier uploaded successfully!", ToastTime.LongTime);
                else
                {
                    return;
                }

                var navParameters = new NavigationParameters();
                navParameters.Add("addedCarrier", successfullyAddedCarrier);
                await NavigationService.GoBackAsync(navParameters);
            }
            else
            {
                var dialogAnswer = await _pageDialogService.DisplayAlertAsync(
                    "Warning!", "This carrier name already exist in our database.\nIf it is correct carrier, " +
                                "You can search for him on previous page", "Ok", "Stay here");
                if (dialogAnswer)
                    await NavigationService.GoBackAsync();
            }
        });



        public override async void OnNavigatedTo(NavigationParameters parameters)
        {
            var carriers = await GetCarriersFromApiAsync();
            CarrierSuggestions = carriers;
        }

        private async Task<List<Carrier>> GetCarriersFromApiAsync()
        {
            var carriers = await _dataService.GetAllCarriersAsync();
            if (carriers.Count == 0)
                return new List<Carrier>();
            return carriers;
        }
    }
}
