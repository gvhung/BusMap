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
using Xamarin.Forms;
using Xamarin.Forms.Maps;

namespace BusMap.Mobile.ViewModels
{
    public class TrackNewRouteViewModel : INotifyPropertyChanged
    {
        private readonly IDataService _dataService;
        private ObservableCollection<Pin> _mapPins;
        private Position _mapPosition;
        private List<BusStop> _busStops;

        public ObservableCollection<Pin> MapPins
        {
            get => _mapPins;
            set
            {
                _mapPins = value;
                OnPropertyChanged();
            }
        }

        public Position MapPosition
        {
            get => _mapPosition;
            set
            {
                _mapPosition = value;
                OnPropertyChanged();
            } 
        }

        public List<BusStop> BusStops
        {
            get => _busStops;
            set
            {
                _busStops = value;
                OnPropertyChanged();
            }
        }


        public TrackNewRouteViewModel(IDataService dataService)
        {
            _dataService = dataService;
            MapPins = new ObservableCollection<Pin>();
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













        public event PropertyChangedEventHandler PropertyChanged;

        [NotifyPropertyChangedInvocator]
        protected virtual void OnPropertyChanged([CallerMemberName] string propertyName = null)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }
    }
}
