using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;
using BusMap.Mobile.Helpers;
using BusMap.Mobile.Models;
using BusMap.Mobile.Services;
using BusMap.Mobile.SQLite.Models;
using BusMap.Mobile.SQLite.Repositories;
using Prism.Commands;
using Prism.Navigation;
using Xamarin.Forms;
using Xamarin.Forms.GoogleMaps;

namespace BusMap.Mobile.ViewModels
{
    public class QueuedRouteDetailsViewModel : ViewModelBase
    {
        private readonly IDataService _dataService;
        private readonly IVotedQueuedRoutesRepository _votedQueuedRoutesRepository;

        private RouteQueued _routeQueued;
        private ObservableCollection<Pin> _pins;
        private MapSpan _mapPosition;
        private bool _routeVotingButtonsEnabled = true;
        private bool _carrierVotingButtonsEnabled = true;
        private bool _routeHaveCarrierQueued;

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

        public bool RouteVotingButtonsEnabled
        {
            get => _routeVotingButtonsEnabled;
            set => SetProperty(ref _routeVotingButtonsEnabled, value);
        }

        public bool CarrierVotingButtonsEnabled
        {
            get => _carrierVotingButtonsEnabled;
            set => SetProperty(ref _carrierVotingButtonsEnabled, value);
        }

        public bool RouteHaveCarrierQueued
        {
            get => _routeHaveCarrierQueued;
            set => SetProperty(ref _routeHaveCarrierQueued, value);
        }


        public QueuedRouteDetailsViewModel(INavigationService navigationService, IDataService dataService,
            IVotedQueuedRoutesRepository votedQueuedRoutesRepository)
            : base(navigationService)
        {
            _dataService = dataService;
            _votedQueuedRoutesRepository = votedQueuedRoutesRepository;

            CarrierVotingButtonsEnabled = true;
            Title = "Queued route details";
            Pins = new ObservableCollection<Pin>();
        }



        public ICommand RoutePlusClickedCommand => new DelegateCommand(async () =>
        {
            RouteQueued.PositiveVotes++;

            if (!await IsRouteVoteSend())
            {
                RouteQueued.PositiveVotes--;
            }
        });

        public ICommand RouteMinusClickedCommand => new DelegateCommand(async () =>
        {
            RouteQueued.NegativeVotes++;
            if (!await IsRouteVoteSend())
            {
                RouteQueued.NegativeVotes--;
            }
        });

        public ICommand CarrierPlusClickedCommand => new DelegateCommand(async () =>
        {
            RouteQueued.CarrierQueued.PositiveVotes++;
            if (!await IsCarrierVoteSend())
            {
                RouteQueued.CarrierQueued.PositiveVotes--;
            }
        });

        public ICommand CarrierMinusClickedCommand => new DelegateCommand(async () =>
        {
            RouteQueued.CarrierQueued.NegativeVotes++;
            if (!await IsCarrierVoteSend())
            {
                RouteQueued.CarrierQueued.NegativeVotes--;
            }
        });

        public override void OnNavigatingTo(NavigationParameters parameters)
        {
            if (parameters.ContainsKey("selectedQueuedRoute") && RouteQueued == null)
            {
                RouteQueued = parameters["selectedQueuedRoute"] as RouteQueued;
                RouteQueued.DayOfTheWeek = RouteQueued.DayOfTheWeek.ConvertToFullDayNames();
                RouteHaveCarrierQueued = RouteQueued.CarrierQueued != null ? true : false;
            }

            var pins = RouteQueued.BusStopsQueued.ToGoogleMapsPins();
            Pins.AddRange(pins);
            if (Pins.Count > 0)
            {
                MapPosition = MapSpan.FromCenterAndRadius(Pins[0].Position, Distance.FromKilometers(20));
            }
        }

        public override void OnNavigatedFrom(NavigationParameters parameters)
        {
            if (!RouteVotingButtonsEnabled || !CarrierVotingButtonsEnabled)
                AddVoteToLocalDb(true);
        }


        private async Task<bool> IsRouteVoteSend()
        {
            var updateSuccess = await UpdateQueuedRouteOrCarrier();
            if (!updateSuccess)
            {
                return false;
            }

            RouteVotingButtonsEnabled = false;
            return true;
        }

        private async Task<bool> IsCarrierVoteSend()
        {
            var updateSuccess = await UpdateQueuedRouteOrCarrier();
            if (!updateSuccess)
            {
                return false;
            }

            CarrierVotingButtonsEnabled = false;
            return true;
        }

        private async Task<bool> UpdateQueuedRouteOrCarrier()
        {
            var updateSuccess = await _dataService.UpdateQueuedRoute(RouteQueued.Id, RouteQueued);
            if (updateSuccess)
            {
                MessagingHelper.Toast("Success! Thank You for voting", ToastTime.LongTime);
            }
            else
            {
                MessagingHelper.Toast("Failed. Connection ussue", ToastTime.LongTime);
                return false;
            }

            return true;
        }

        private void AddVoteToLocalDb(bool votedPositive)
        {
            var route = new VotedQueuedRoute
            {
                Id = RouteQueued.Id,
                VoteType = votedPositive,
                AddedDate = DateTime.Now
            };
            _votedQueuedRoutesRepository.AddVotedRoute(route);
        }


    }
}
