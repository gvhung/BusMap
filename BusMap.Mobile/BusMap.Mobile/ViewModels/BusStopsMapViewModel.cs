using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Runtime.CompilerServices;
using System.Text;
using BusMap.Mobile.Annotations;
using BusMap.Mobile.Helpers;
using BusMap.Mobile.Models;
using Xamarin.Forms.Maps;

namespace BusMap.Mobile.ViewModels
{
    public class BusStopsMapViewModel :INotifyPropertyChanged
    {
        private ObservableCollection<Pin> _pins;
        private Position _mapPosition;

        public Position MapPosition
        {
            get => _mapPosition;
            set
            {
                _mapPosition = value;
                OnPropertyChanged();
            }
        }

        public ObservableCollection<Pin> Pins
        {
            get => _pins;
            set
            {
                _pins = value;
                OnPropertyChanged();
            }
        }

        public string PageTitle { get; set; }


        public BusStopsMapViewModel(Route route)
        {
            Pins = route.BusStops.ConvertToMapPins();
            MapPosition = Pins[0].Position;
            PageTitle = route.Name;
        }

        public BusStopsMapViewModel()
        {
        }


        public event PropertyChangedEventHandler PropertyChanged;

        [NotifyPropertyChangedInvocator]
        protected virtual void OnPropertyChanged([CallerMemberName] string propertyName = null)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }
    }
}
