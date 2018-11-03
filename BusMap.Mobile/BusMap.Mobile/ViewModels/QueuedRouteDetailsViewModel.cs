using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Text;
using System.Windows.Input;
using BusMap.Mobile.Helpers;
using BusMap.Mobile.Models;
using BusMap.Mobile.Services;
using Prism.Commands;
using Prism.Navigation;
using Xamarin.Forms.GoogleMaps;

namespace BusMap.Mobile.ViewModels
{
    public class QueuedRouteDetailsViewModel :ViewModelBase
    {
        private readonly IDataService _dataService;
        private RouteQueued _routeQueued;
        private ObservableCollection<Pin> _pins;
        private MapSpan _mapPosition;
        private bool _votingButtonsEnabled = true;

        public RouteQueued RouteQueued
        {
            get => _routeQueued;
            set => SetProperty(ref _routeQueued, value);
        }

        public ObservableCollection<Pin> Pins
        {
            get => _pins;
            set => SetProperty(ref _pins, value);
        }

        public MapSpan MapPosition
        {
            get => _mapPosition;
            set => SetProperty(ref _mapPosition, value);
        }

        public bool VotingButtonsEnabled
        {
            get => _votingButtonsEnabled;
            set => SetProperty(ref _votingButtonsEnabled, value);
        }


        public QueuedRouteDetailsViewModel(INavigationService navigationService, IDataService dataService) 
            : base(navigationService)
        {
            _dataService = dataService;
            Title = "Queued route details";
            Pins = new ObservableCollection<Pin>();
        }



        public ICommand PlusClickedCommand => new DelegateCommand(async () =>
        {
            RouteQueued.PositiveVotes++;
            VotingButtonsEnabled = false;
            var updateSuccess = await _dataService.UpdateQueuedRoute(RouteQueued.Id, RouteQueued);
            if (updateSuccess)
            {
                MessagingHelper.Toast("Success! Thank You for voting", ToastTime.LongTime);
            }
            else
            {
                VotingButtonsEnabled = true;
                MessagingHelper.Toast("Failed. Connection ussue", ToastTime.LongTime);
            }
                
        });

        public ICommand MinusClickedCommand => new DelegateCommand(async () =>
        {
            RouteQueued.NegativeVotes++;
            VotingButtonsEnabled = false;
            var updateSuccess = await _dataService.UpdateQueuedRoute(RouteQueued.Id, RouteQueued);
            if (updateSuccess)
            {
                MessagingHelper.Toast("Success! Thank You for voting", ToastTime.LongTime);
            }
            else
            {
                VotingButtonsEnabled = true;
                MessagingHelper.Toast("Failed. Connection ussue", ToastTime.LongTime);
            }
        });


        public override void OnNavigatingTo(NavigationParameters parameters)
        {
            if (parameters.ContainsKey("selectedQueuedRoute"))
            {
                RouteQueued = parameters["selectedQueuedRoute"] as RouteQueued;
            }

            var pins = RouteQueued.BusStopsQueued.ToGoogleMapsPins();
            Pins.AddRange(pins);
            if (Pins.Count > 0)
            {
                MapPosition = MapSpan.FromCenterAndRadius(Pins[0].Position, Distance.FromKilometers(20));
            }
            
        }


    }
}
