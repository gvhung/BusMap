using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;
using BusMap.Mobile.Annotations;
using BusMap.Mobile.Models;
using BusMap.Mobile.Services;
using BusMap.Mobile.Views;
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
        private List<BusStop> _busStops;
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

        public List<BusStop> BusStops
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

            BusStops = new List<BusStop>
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
                    Address = "Address1",
                    Id = 1,
                    Label = "Label1",
                    Latitude = 1,
                    Longitude = 1
                },
                new BusStop
                {
                    Address = "Address1",
                    Id = 1,
                    Label = "Label1",
                    Latitude = 1,
                    Longitude = 1
                },new BusStop
                {
                    Address = "Address1",
                    Id = 1,
                    Label = "Label1",
                    Latitude = 1,
                    Longitude = 1
                },new BusStop
                {
                    Address = "Address1",
                    Id = 1,
                    Label = "Label1",
                    Latitude = 1,
                    Longitude = 1
                },
                new BusStop
                {
                    Address = "Address1",
                    Id = 1,
                    Label = "Label1",
                    Latitude = 1,
                    Longitude = 1
                },new BusStop
                {
                    Address = "Address1",
                    Id = 1,
                    Label = "Label1",
                    Latitude = 1,
                    Longitude = 1
                }
            };
        }


        public ICommand PopupCommand => new DelegateCommand(async () =>
        {
            await NavigationService.NavigateAsync("AddNewRoutePage");
        });

    }
}
