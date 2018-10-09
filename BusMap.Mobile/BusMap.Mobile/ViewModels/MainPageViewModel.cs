using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Runtime.CompilerServices;
using System.Text;
using System.Windows.Input;
using BusMap.Mobile.Annotations;
using BusMap.Mobile.Helpers;
using BusMap.Mobile.Models;
using BusMap.Mobile.Services;
using BusMap.Mobile.Views;
using Xamarin.Forms;

namespace BusMap.Mobile.ViewModels
{
    public class MainPageViewModel : INotifyPropertyChanged
    {
        private readonly IDataService _dataService;

        private string _startBusStopName;
        private string _destinationBusStopName;
        private bool _isBusy;


        public string StartBusStopName
        {
            get => _startBusStopName;
            set
            {
                _startBusStopName = value;
                OnPropertyChanged();
            }
        }

        public string DestinationBusStopName
        {
            get => _destinationBusStopName;
            set
            {
                _destinationBusStopName = value;
                OnPropertyChanged();
            }
        }

        public bool IsBusy
        {
            get => _isBusy;
            set
            {
                _isBusy = value;
                OnPropertyChanged();
            }
        }


        public MainPageViewModel(IDataService dataService)
        {
            _dataService = dataService;
        }


        public ICommand SearchCommand => new Command(async () =>
        {
            IsBusy = true;
            
            try
            {
                if (String.IsNullOrEmpty(StartBusStopName) || String.IsNullOrEmpty(DestinationBusStopName))
                {
                    MessagingHelper.Toast("Please enter bus stops in both entries.", ToastTime.ShortTime);
                    return;
                }
                    

                var allRoutes = await _dataService.FindRoutes(StartBusStopName, DestinationBusStopName);
                if (allRoutes.Count <= 0)
                {
                    MessagingHelper.Toast("No routes found.", ToastTime.LongTime);
                    return;
                }

                var viewModel =
                    new RoutesListPageViewModel(_dataService, allRoutes, StartBusStopName, DestinationBusStopName);
                await Application.Current.MainPage.Navigation.PushAsync(new RoutesListPage(viewModel));
            }
            catch (Exception ex)
            {
                MessagingHelper.Toast(ex.Message, ToastTime.LongTime);
            }
            finally
            {
                IsBusy = false;
            }

        });


        public ICommand AdvancedButtonCommand => new Command(async () =>
        {
            var test = await _dataService.GetBusStops();
        });





        public event PropertyChangedEventHandler PropertyChanged;

        [NotifyPropertyChangedInvocator]
        protected virtual void OnPropertyChanged([CallerMemberName] string propertyName = null)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }
    }
}
