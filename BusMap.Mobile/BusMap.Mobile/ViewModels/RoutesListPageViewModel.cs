using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;
using BusMap.Mobile.Annotations;
using BusMap.Mobile.Models;
using BusMap.Mobile.Services;
using Xamarin.Forms;

namespace BusMap.Mobile.ViewModels
{
    public class RoutesListPageViewModel : INotifyPropertyChanged
    {
        private IDataService _dataService;
        private List<Route> _routes;
        private bool _isRefreshing;

        public string StartPoint { get; set; }
        public string DestinationPoint { get; set; }
        public string StartDestinationTitle => $"{StartPoint} - {DestinationPoint}";

        public List<Route> Routes
        {
            get => _routes;
            set
            {
                _routes = value;
                OnPropertyChanged();
            }
        }

        public bool IsRefreshing
        {
            get => _isRefreshing;
            set
            {
                _isRefreshing = value;
                OnPropertyChanged();
            } 
        }

        //public RoutesListPageViewModel(IDataService dataService)
        //{
        //    _dataService = dataService;
        //    GetRoutes();
        //}

        public RoutesListPageViewModel(IDataService dataService, IEnumerable<Route> routes, string from, string to)
        {
            _dataService = dataService;
            Routes = routes.ToList();
            StartPoint = from;
            DestinationPoint = to;
        }

        //public RoutesListPageViewModel()
        //{
        //}



        public ICommand RefreshCommand => new Command(async () =>
        {
            await GetRoutes();
        });

        private async Task GetRoutes()
        {
            IsRefreshing = true;
            Routes = await _dataService.GetRoutes();
            IsRefreshing = false;
        }







        public event PropertyChangedEventHandler PropertyChanged;

        [NotifyPropertyChangedInvocator]
        protected virtual void OnPropertyChanged([CallerMemberName] string propertyName = null)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }

    }
}
