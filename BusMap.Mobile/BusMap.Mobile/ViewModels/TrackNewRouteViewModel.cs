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
using Prism;
using Prism.Commands;
using Prism.Navigation;
using Rg.Plugins.Popup.Services;
using Xamarin.Forms;
using Xamarin.Forms.Maps;

namespace BusMap.Mobile.ViewModels
{
    public class TrackNewRouteViewModel : ViewModelBase
    {
        private readonly IDataService _dataService;
        private ObservableCollection<Pin> _mapPins;
        private Position _mapPosition;
        private ObservableCollection<BusStop> _busStops;
        private Carrier _carrier;

        public ObservableCollection<Pin> MapPins
        {
            get => _mapPins;
            set => SetProperty(ref _mapPins, value);
        }

        public Position MapPosition
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


        public TrackNewRouteViewModel(IDataService dataService, INavigationService navigationService)
            : base (navigationService)
        {
            _dataService = dataService;
            Title = "Add route";

            MapPins = new ObservableCollection<Pin>();
            Carrier = new Carrier
            {
                Id = 1,
                Name = "Placeholder carrier"
            };

            BusStops = new ObservableCollection<BusStop>
            {
                new BusStop
                {
                    Address = "Address1",
                    Id = 1,
                    Label = "Label1",
                    Latitude = 1,
                    Longitude = 1
                },
                new BusStop
                {
                    Address = "Address2",
                    Id = 1,
                    Label = "Label2",
                    Latitude = 1,
                    Longitude = 1
                }
            };
        }


        public ICommand PopupCommand => new DelegateCommand(async () =>
        {
            await NavigationService.NavigateAsync("AddNewRoutePage");
        });

        public ICommand TestCommand => new DelegateCommand(async () =>
        {
            try
            {
                var currentPosition = await NavigationHelpers.GetCurrentUserPositionAsync(false);
                MapPosition = currentPosition.ToMapsPosition();
            }
            catch (TaskCanceledException)
            {
                MessagingHelper.Toast("Unable to get position.", ToastTime.ShortTime);
            }

        });

        //public override void OnNavigatingTo(NavigationParameters parameters)
        //{
        //    if (parameters.ContainsKey("newBusStop"))
        //    {
        //        BusStops.Add(parameters["newBusStop"] as BusStop);
        //    }
        //}

        public override void OnNavigatedTo(NavigationParameters parameters)
        {
            if (parameters.ContainsKey("newBusStop"))
            {
                //BusStops.Add(parameters["newBusStop"] as BusStop);
                BusStops.Insert(0, parameters["newBusStop"] as BusStop);
            }
        }

        //newBusStop
    }
}
