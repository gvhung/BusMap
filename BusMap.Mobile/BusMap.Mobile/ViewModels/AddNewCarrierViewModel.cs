using System;
using System.Collections.Generic;
using System.Text;
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

        public string CarrierName
        {
            get => _carrierName;
            set => SetProperty(ref _carrierName, value);
        }


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
                var carrierToAdd = new Carrier{ Name = CarrierName };
                var successfullyAddedCarrier = await _dataService.PostCarrierAsync(carrierToAdd);

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



    }
}
