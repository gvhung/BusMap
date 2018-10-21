using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;
using BusMap.Mobile.Annotations;
using BusMap.Mobile.Helpers;
using BusMap.Mobile.Models;
using BusMap.Mobile.Services;
using BusMap.Mobile.Views;
using Microsoft.EntityFrameworkCore.Internal;
using Prism;
using Prism.Commands;
using Prism.Navigation;
using Rg.Plugins.Popup.Services;
using Xamarin.Forms;
using Xamarin.Forms.GoogleMaps;

namespace BusMap.Mobile.ViewModels
{
    public class TrackNewRouteViewModel : ViewModelBase
    {
        private readonly IDataService _dataService;
        private ObservableCollection<Pin> _mapPins;
        private MapSpan _mapPosition;
        private ObservableCollection<BusStop> _busStops;
        private Carrier _carrier;

        private int _editingElementIndex = -1;
        private bool _saveButtonEnabled;

        public ObservableCollection<Pin> MapPins
        {
            get => _mapPins;
            set => SetProperty(ref _mapPins, value);
        }

        public MapSpan MapPosition
        {
            get => _mapPosition;
            set => SetProperty(ref _mapPosition, value);
        }

        public ObservableCollection<BusStop> BusStops
        {
            get => _busStops;
            set => SetProperty(ref _busStops, value);
        }

        public Carrier Carrier
        {
            get => _carrier;
            set => SetProperty(ref _carrier, value);
        }

        public bool SaveButtonEnabled
        {
            get => _saveButtonEnabled;
            set => SetProperty(ref _saveButtonEnabled, value);
        }


        public TrackNewRouteViewModel(IDataService dataService, INavigationService navigationService)
            : base (navigationService)
        {
            _dataService = dataService;
            Title = "Add route";
            MapPins = new ObservableCollection<Pin>();
            BusStops = new ObservableCollection<BusStop>();

            Carrier = new Carrier
            {
                Id = 1,
                Name = "Placeholder carrier"
            };
        }


        public ICommand PopupCommand => new DelegateCommand(async () =>
        {
            var navigationParams = new NavigationParameters();
            if (BusStops.Count < 1)
            {
                navigationParams.Add("lastIndex", 0);
            }
            else
            {
                navigationParams.Add("lastIndex", BusStops.First().Id);
            }
            
            await NavigationService.NavigateAsync(nameof(AddNewRoutePage), navigationParams);
        });

        public ICommand MapAppearingCommand => new DelegateCommand(async () =>
        {
            try
            {
                var currentPosition = await LocalizationHelpers.GetCurrentUserPositionAsync(true);
                MapPosition = null;
                MapPosition = MapSpan.FromCenterAndRadius(currentPosition.ToGoogleMapsPosition(), Distance.FromKilometers(10));
                if (MapPins == null || MapPins.Count < 1)
                    MapPins.AddRange(BusStops.ToGoogleMapsPins());
            }
            catch (TaskCanceledException)
            {
                MessagingHelper.Toast("Unable to get position.", ToastTime.ShortTime);
            }

        });

        public ICommand SelectedBusStopCommand => new DelegateCommand<BusStop>(async busStop =>
        {
            var navigationParameters = new NavigationParameters();
            navigationParameters.Add("busStopToEdit", busStop);
            _editingElementIndex = BusStops.IndexOf(busStop);

            await NavigationService.NavigateAsync(nameof(EditBusStopPage), navigationParameters);
        });



        public override void OnNavigatedTo(NavigationParameters parameters)
        {
            if (parameters.ContainsKey("newBusStop"))
            {
                AddBusStopToLists(parameters["newBusStop"] as BusStop);
                if (BusStops.Count > 1)
                {
                    SaveButtonEnabled = true;
                }
            }
                
            if (parameters.ContainsKey("busStopFromEdit"))
            {
                var busStopFromEdit = parameters["busStopFromEdit"] as BusStop;
                AddEditedBusStopToLists(busStopFromEdit, ref _editingElementIndex);
            }

            if (parameters.ContainsKey("removeBusStopId"))
            {
                var busStopToRemoveId = (int) parameters["removeBusStopId"];
                RemoveBusStop(busStopToRemoveId);
                if (BusStops.Count < 2)
                {
                    SaveButtonEnabled = false;
                }
            }

        }

        private void AddBusStopToLists(BusStop busStop)
        {
            BusStops.Insert(0, busStop);
            MapPins.Insert(0, busStop.ToGoogleMapsPin());
        }

        private void AddEditedBusStopToLists(BusStop busStop, ref int index)
        {
            BusStops[index] = busStop;
            MapPins.RemoveAt(index);
            MapPins.Insert(index, busStop.ToGoogleMapsPin());
            index = -1;
        }

        private void RemoveBusStop(int id)
        {
            BusStops.Remove(BusStops.SingleOrDefault(b => b.Id == id));
        }

        private async Task<int> GetLastBusStopId()
        {
            MessagingHelper.Toast("Downloading data...", ToastTime.ShortTime);
            return await _dataService.GetBusStopLastId();
        }

    }
}
