using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Runtime.CompilerServices;
using System.Text;
using System.Windows.Input;
using BusMap.Mobile.Annotations;
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


        public ICommand Command => new Command(async () =>
        {
            IsBusy = true;
            var allRoutes = await _dataService.GetRoutes();

            //var routes = await _dataService?.FindRoute(x => x.StartingBusStop.Address.ToLowerInvariant().
            //    Trim().Equals(StartBusStopName.ToLowerInvariant().Trim()));

            var viewModel = new RoutesListPageViewModel(_dataService, allRoutes, StartBusStopName, DestinationBusStopName);
            IsBusy = false;
            await Application.Current.MainPage.Navigation.PushAsync(new RoutesListPage(viewModel));
        });








        public event PropertyChangedEventHandler PropertyChanged;

        [NotifyPropertyChangedInvocator]
        protected virtual void OnPropertyChanged([CallerMemberName] string propertyName = null)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }
    }
}
