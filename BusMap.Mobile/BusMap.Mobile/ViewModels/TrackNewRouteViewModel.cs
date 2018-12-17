using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Linq;
using System.Net.Http.Headers;
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
using Prism.Services;
using Rg.Plugins.Popup.Services;
using Xamarin.Forms;
using Xamarin.Forms.GoogleMaps;

namespace BusMap.Mobile.ViewModels
{
    public class TrackNewRouteViewModel : ViewModelBase
    {
        private readonly IDataService _dataService;
        private readonly IPageDialogService _pageDialogService;

        private int _editingElementIndex = -1;
        private bool _saveButtonEnabled;
        private List<SelectableItem<DayOfWeek>> _weekDays;

        private ObservableCollection<Pin> _mapPins;
        private MapSpan _mapPosition;
        private ObservableCollection<BusStopQueued> _busStops;
        private Carrier _carrier;
        private string _weekDaysString;


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

        public ObservableCollection<BusStopQueued> BusStops
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

        public string WeekDaysString
        {
            get => _weekDaysString;
            set => SetProperty(ref _weekDaysString, value);
        }

        public List<Carrier> CarrierSuggestions { get; set; }
        public string AutoSuggestText { get; set; }


        public TrackNewRouteViewModel(IDataService dataService, INavigationService navigationService,
            IPageDialogService pageDialogService)
            : base (navigationService)
        {
            _dataService = dataService;
            _pageDialogService = pageDialogService;
            Title = "Add route";
            MapPins = new ObservableCollection<Pin>();
            BusStops = new ObservableCollection<BusStopQueued>();
            _weekDays = AddDaysToCollection();

            //Carrier = new Carrier
            //{
            //    Name = "Placeholder carrier",
            //    Id = 2  //Todo: get id carrier checked on list
            //};
        }


        public ICommand PopupCommand => new DelegateCommand(async () =>
        {            
            await NavigationService.NavigateAsync(nameof(AddNewBusStopPage));
        });

        public ICommand MapAppearingCommand => new DelegateCommand(async () =>
        {
            try
            {
                var currentPosition = await LocalizationHelpers.GetCurrentUserPositionAsync(true);
                MapPosition = null;
                MapPosition = MapSpan.FromCenterAndRadius(currentPosition.ToGoogleMapsPosition(), Distance.FromKilometers(10));
                MapPins.ToCustomIconPins();
                if (MapPins == null || MapPins.Count < 1)
                    MapPins.AddRange(BusStops.ToGoogleMapsPins());
            }
            catch (TaskCanceledException)
            {
                MessagingHelper.Toast("Unable to get position.", ToastTime.ShortTime);
            }

        });

        public ICommand EditBusStopCommand => new DelegateCommand<BusStopQueued>(async busStop =>
        {
            var navigationParameters = new NavigationParameters();
            navigationParameters.Add("busStopToEdit", busStop);
            _editingElementIndex = BusStops.IndexOf(busStop);

            await NavigationService.NavigateAsync(nameof(EditBusStopPage), navigationParameters);
        });

        public ICommand SaveButtonCommand => new DelegateCommand(async () =>
        {
            var busStopsReversed = BusStops.Reverse().ToList();

            if(!CheckIfCarrierIsEntered())
                return;
            if (!_weekDays.Any(d => d.IsChecked))
            {
                MessagingHelper.Toast("Please set coursing days.", ToastTime.LongTime);
                return;
            }

            var carrierQueued = new CarrierQueued();
            if (Carrier != null)
            {
                carrierQueued.Name = Carrier.Name;
            }
            else
            {
                var dialogAnswerCarrier = await _pageDialogService
                    .DisplayAlertAsync("Information.",
                        "Entered carrier name was not found in our database. " +
                        "We strongly recommend to check if it is not on list. " +
                        "If are you sure about this is new carrier and his name is correct, " +
                        "please click Ok.", "Ok", "I want check again");
                if (!dialogAnswerCarrier)
                    return;
            }

            var route = new RouteQueued()
            {
                BusStopsQueued = busStopsReversed,
                CarrierQueuedId = Carrier.Id,
                Name = DateTime.Now.ToShortTimeString(), //TODO: From Entry
                DayOfTheWeek = DaysToNumbersString(_weekDays)
            };

            var dialogAnswer = await _pageDialogService
                .DisplayAlertAsync("Are you sure?", "You would not edit route after it.", "Yes", "No");
            if (dialogAnswer)
            {
                await PostDataAsync(route);
            }

            
        });

        public ICommand ChooseDaysCommand => new DelegateCommand(async () =>
            {
                var param = new NavigationParameters();
                param.Add("days", _weekDays);
                await NavigationService.NavigateAsync(nameof(WeekDaySelectionPage), param);
            });

        private async Task PostDataAsync(RouteQueued route)
        {
            MessagingHelper.Toast("Uploading new route...", ToastTime.ShortTime);
            var result = await _dataService.PostRouteQueuedAsync(route);
            if (result)
            {
                //MessagingHelper.Toast("Upload successful!", ToastTime.LongTime);
                //await NavigationService.GoBackAsync();
                await _pageDialogService.DisplayAlertAsync("Success!",
                    "Route added successfully.\nYou can find it in new routes queue.", "Ok");
                await NavigationService.NavigateAsync(
                    new Uri($"/MainMasterDetailPage/CustomNavigationPage/MainPage",
                    UriKind.Absolute));
            }
            else
            {
                MessagingHelper.Toast("Upload failed!", ToastTime.ShortTime);
            }
        }

        private void AddBusStopToLists(BusStopQueued busStop)
        {
            BusStops.Insert(0, busStop);
            MapPins.Insert(0, busStop.ToGoogleMapsPin());
        }

        private void AddEditedBusStopToLists(BusStopQueued busStop, ref int index)
        {
            BusStops[index] = busStop;
            MapPins.RemoveAt(index);
            MapPins.Insert(index, busStop.ToGoogleMapsPin());
            index = -1;
        }

        private async Task RemoveBusStop(string address, string label)
        {
            var busStopToRemove = BusStops
                .Where(b => b.Address.Equals(address))
                .SingleOrDefault(b => b.Label.Equals(label));

            if (busStopToRemove != null)
            {
                BusStops.Remove(busStopToRemove);
                MapPins.Remove(busStopToRemove.ToGoogleMapsPin());
            }
            else
            {
                await _pageDialogService.DisplayAlertAsync("Alert!", "Could not remove busStop.\nPlease try again.", "Ok");
            }
        }

        private List<SelectableItem<DayOfWeek>> AddDaysToCollection()
            => new List<SelectableItem<DayOfWeek>>
            {
                new SelectableItem<DayOfWeek>(DayOfWeek.Monday),
                new SelectableItem<DayOfWeek>(DayOfWeek.Tuesday),
                new SelectableItem<DayOfWeek>(DayOfWeek.Wednesday),
                new SelectableItem<DayOfWeek>(DayOfWeek.Thursday),
                new SelectableItem<DayOfWeek>(DayOfWeek.Friday),
                new SelectableItem<DayOfWeek>(DayOfWeek.Saturday),
                new SelectableItem<DayOfWeek>(DayOfWeek.Sunday),

            };

        private string DaysToString(IEnumerable<SelectableItem<DayOfWeek>> daysList)
        {
            var builder = new StringBuilder();

            if (!daysList.Any())
                return builder.ToString();

            foreach (var day in daysList)
            {
                if (day.IsChecked)
                    builder.Append(day.TObject + ", ");
            }
            if (builder.Length > 1)
                builder.Length -= 2;
            return builder.ToString();
        }

        private string DaysToNumbersString(IEnumerable<SelectableItem<DayOfWeek>> daysList)
        {
            var builder = new StringBuilder();

            foreach (var day in daysList)
            {
                if (day.IsChecked)
                    builder.Append((int)day.TObject + ",");
            }

            builder.Length--;
            return builder.ToString();
        }



        public override async void OnNavigatingTo(NavigationParameters parameters)
        {
            var carriers = await GetCarriersFromApiAsync();
            CarrierSuggestions = carriers;
        }

        public override async void OnNavigatedTo(NavigationParameters parameters)
        {

            if (parameters.ContainsKey("newBusStop"))
            {
                AddBusStopToLists(parameters["newBusStop"] as BusStopQueued);
                if (BusStops.Count > 1)
                {
                    SaveButtonEnabled = true;
                }
            }

            if (parameters.ContainsKey("busStopFromEdit"))
            {
                var busStopFromEdit = parameters["busStopFromEdit"] as BusStopQueued;
                AddEditedBusStopToLists(busStopFromEdit, ref _editingElementIndex);
            }

            if (parameters.ContainsKey("removeBusStopAddress") && parameters.ContainsKey("removeBusStopLabel"))
            {
                var busStopToRemoveLabel = parameters["removeBusStopLabel"] as string;
                var busStopToRemoveAddress = parameters["removeBusStopAddress"] as string;
                await RemoveBusStop(busStopToRemoveAddress, busStopToRemoveLabel);
                if (BusStops.Count < 2)
                {
                    SaveButtonEnabled = false;
                }
            }

            if (parameters.ContainsKey("days"))
            {
                _weekDays = parameters["days"] as List<SelectableItem<DayOfWeek>>;
                WeekDaysString = DaysToString(_weekDays);
            }

        }

        private bool CheckIfCarrierIsEntered()
        {
            if (string.IsNullOrEmpty(AutoSuggestText) || AutoSuggestText.Length < 3)
            {
                MessagingHelper.Toast("Please enter carrier (Minimum 3 characters).", ToastTime.LongTime);
                return false;
            }

            return true;
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
