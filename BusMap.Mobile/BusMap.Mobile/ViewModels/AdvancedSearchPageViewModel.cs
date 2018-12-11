using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Diagnostics;
using System.Net.Http;
using System.Text;
using System.Windows.Input;
using BusMap.Mobile.Helpers;
using BusMap.Mobile.Models;
using BusMap.Mobile.Services;
using BusMap.Mobile.Views;
using Prism.Commands;
using Prism.Navigation;

namespace BusMap.Mobile.ViewModels
{
    public class AdvancedSearchPageViewModel : ViewModelBase
    {
        private readonly IDataService _dataService;

        private string _startCityText;
        private string _destinationCityText;
        private DayOfWeek _selectedDayOfWeek;
        private TimeSpan _hourFromTime;
        private TimeSpan _hourToTime;
        private DateTime _date;
        private bool _isDateSwitchToggled;
        private bool _isDaysSwitchToggled;
        private ObservableCollection<SelectableItem<DayOfWeek>> _selectableDays;

        public string StartCityText
        {
            get => _startCityText;
            set => SetProperty(ref _startCityText, value);
        }

        public string DestinationCityText
        {
            get => _destinationCityText;
            set => SetProperty(ref _destinationCityText, value);
        }

        public List<DayOfWeek> DaysList { get; set; }

        public DayOfWeek SelectedDayOfWeek
        {
            get => _selectedDayOfWeek;
            set => SetProperty(ref _selectedDayOfWeek, value);
        }

        public TimeSpan HourFromTime
        {
            get => _hourFromTime;
            set => SetProperty(ref _hourFromTime, value);
        }

        public TimeSpan HourToTime
        {
            get => _hourToTime;
            set => SetProperty(ref _hourToTime, value);
        }

        public DateTime Date
        {
            get => _date;
            set => SetProperty(ref _date, value);
        }

        public bool IsMultiDaySelectSwitchToggled
        {
            get => _isDaysSwitchToggled;
            set => SetProperty(ref _isDaysSwitchToggled, value);
        }

        public bool IsDateSwitchToggled
        {
            get => _isDateSwitchToggled;
            set => SetProperty(ref _isDateSwitchToggled, value);
        }

        public ObservableCollection<SelectableItem<DayOfWeek>> SelectableDays
        {
            get => _selectableDays;
            set => SetProperty(ref _selectableDays, value);
        }


        public AdvancedSearchPageViewModel(INavigationService navigationService, IDataService dataService) 
            : base(navigationService)
        {
            _dataService = dataService;

            DaysList = CreateDaysList();
            SelectedDayOfWeek = DaysList.Find(d => d == DateTime.Now.DayOfWeek);
            HourFromTime = DateTime.Now.TimeOfDay;
            HourToTime = new TimeSpan(23,59,0);
            Date = DateTime.Now;
            SelectableDays = CreateSelectableDaysList();
        } 


        public ICommand SearchButtonCommand => new DelegateCommand(async () =>
        {
            if (!Validate())
                return;

            var navParams = new NavigationParameters();
            var days = ConvertSwitchesToDays();
            var date = default(DateTime);
            var destinationString = DestinationCityText;
            var optionalInTitle = new StringBuilder();
            if (IsDateSwitchToggled)
            {
                date = Date;
                optionalInTitle.Append(date.DayOfWeek);
                optionalInTitle.Append(date.Date.ToString("dd.MM.yyyy,"));  //Todo: display optional just as label of text on RoutesListPage as parameters list
            }

            optionalInTitle.Append($"{HourFromTime.ToString("hh\\:mm")}-{HourToTime.ToString("hh\\:mm")}");

            navParams.Add("startBusStopName", StartCityText);
            navParams.Add("destinationBusStopName", destinationString);
            navParams.Add("optionalInTitle", optionalInTitle.ToString());

            try
            {
                var foundRoutes = await _dataService.FindRoutesAsync(StartCityText, DestinationCityText,
                    days, HourFromTime, HourToTime, date);
                navParams.Add("foundedRoutes", foundRoutes);
                await NavigationService.NavigateAsync(nameof(RoutesListPage), navParams);
            }
            catch (HttpRequestException ex)
            {
                MessagingHelper.Toast("No routes was found.", ToastTime.LongTime);
            }
        });


        private List<DayOfWeek> CreateDaysList()
            => new List<DayOfWeek>
            {
                DayOfWeek.Monday,
                DayOfWeek.Tuesday,
                DayOfWeek.Wednesday,
                DayOfWeek.Thursday,
                DayOfWeek.Friday,
                DayOfWeek.Saturday,
                DayOfWeek.Sunday
            };

        private ObservableCollection<SelectableItem<DayOfWeek>> CreateSelectableDaysList()
            => new ObservableCollection<SelectableItem<DayOfWeek>>
            {
                new SelectableItem<DayOfWeek>(DayOfWeek.Monday),
                new SelectableItem<DayOfWeek>(DayOfWeek.Tuesday),
                new SelectableItem<DayOfWeek>(DayOfWeek.Wednesday),
                new SelectableItem<DayOfWeek>(DayOfWeek.Thursday),
                new SelectableItem<DayOfWeek>(DayOfWeek.Friday),
                new SelectableItem<DayOfWeek>(DayOfWeek.Saturday),
                new SelectableItem<DayOfWeek>(DayOfWeek.Sunday)
            };

        private string ConvertSwitchesToDays()
        {
            var stringBuilder = new StringBuilder();

            if (!IsMultiDaySelectSwitchToggled)
            {
                stringBuilder.Append(((int) SelectedDayOfWeek).ToString());
            }
            else
            {
                foreach (var selectableDay in SelectableDays)
                {
                    if (selectableDay.IsChecked)
                        stringBuilder.Append($"{(int) selectableDay.TObject},");
                }

                stringBuilder.Length--;
            }

            return stringBuilder.ToString();
        }

        private bool Validate()
        {
            var cities = AreCitiesFilled();
            var hours = IsHourFromNotAfterHourTo();

            var result = cities && hours;
            return result;
        }

        private bool AreCitiesFilled()
        {
            if (String.IsNullOrEmpty(StartCityText)
                || String.IsNullOrEmpty(DestinationCityText)
                || String.IsNullOrWhiteSpace(StartCityText)
                || String.IsNullOrWhiteSpace(DestinationCityText))
            {
                MessagingHelper.Toast("Please enter bus stops in both entries.", ToastTime.ShortTime);
                return false;
            }

            return true;
        }

        private bool IsHourFromNotAfterHourTo()
        {
            if (HourFromTime > HourToTime)
            {
                MessagingHelper.Toast("Hour from cannot be after hour to.", ToastTime.ShortTime);
                return false;
            }
                
            return true;
        }

    }
}
