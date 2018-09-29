using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;
using System.Windows.Input;
using BusMap.Mobile.Annotations;
using Xamarin.Forms;

namespace BusMap.Mobile.Models
{
    public class Route : INotifyPropertyChanged
    {
        private bool _busStopsStringIsVisible;

        public int Id { get; set; }
        public string Name { get; set; }
        public Carrier Carrier { get; set; }
        public List<BusStop> BusStops { get; set; }
        public BusStop StartingBusStop => BusStops.First();
        public BusStop DestinationBusStop => BusStops.Last();



        public string BusStopsString => ToString();

        public bool BusStopsStringIsVisible
        {
            get => _busStopsStringIsVisible;
            set
            {
                _busStopsStringIsVisible = value;
                OnPropertyChanged();
            }
        }


        public override string ToString()
        {
            var stringBuilder = new StringBuilder();
            foreach (var stop in BusStops)
            {
                stringBuilder.Append(string.IsNullOrEmpty(stop.Label)
                    ? $"{stop.Address}\n"
                    : $"{stop.Address}, {stop.Label}\n");
            }

            return stringBuilder.ToString();
        }

        public ICommand LabelCommand => new Command(async () =>
        {
            BusStopsStringIsVisible = !BusStopsStringIsVisible;
        });




        public event PropertyChangedEventHandler PropertyChanged;

        [NotifyPropertyChangedInvocator]
        protected virtual void OnPropertyChanged([CallerMemberName] string propertyName = null)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }
    }
}
