using System;
using System.Collections.Generic;
using System.Text;
using System.Windows.Input;
using BusMap.Mobile.Helpers;
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

        public bool IsDaysSwitchToggled
        {
            get => _isDaysSwitchToggled;
            set => SetProperty(ref _isDaysSwitchToggled, value);
        }

        public bool IsDateSwitchToggled
        {
            get => _isDateSwitchToggled;
            set => SetProperty(ref _isDateSwitchToggled, value);
        }


        public AdvancedSearchPageViewModel(INavigationService navigationService, IDataService dataService) 
            : base(navigationService)
        {
            _dataService = dataService;

            DaysList = CreateDaysList();
            SelectedDayOfWeek = DaysList.Find(d => d == DateTime.Now.DayOfWeek);
            HourFromTime = DateTime.Now.TimeOfDay;
            HourToTime = new TimeSpan(23,59,0);
            Date = default(DateTime);
        }


        public ICommand SearchButtonCommand => new DelegateCommand(async () =>
        {
            var navParams = new NavigationParameters();
            var foundRoutes = await _dataService.FindRoutesAsync(StartCityText, DestinationCityText,
                "1,2", HourFromTime, HourToTime, Date);
            navParams.Add("routes", foundRoutes);
            await NavigationService.NavigateAsync(nameof(RoutesListPage), navParams);
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
    }
}
