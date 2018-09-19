using System;
using System.Collections.Generic;
using System.ComponentModel;
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
        public ICollection<BusStop> BusStops { get; set; }

        public string BusStopsString => BusStopsToString();

        public bool BusStopsStringIsVisible
        {
            get => _busStopsStringIsVisible;
            set
            {
                _busStopsStringIsVisible = value;
                OnPropertyChanged();
            }
        }


        private string BusStopsToString()
        {
            var stringBuilder = new StringBuilder();
            foreach (var stop in BusStops)
            {
                stringBuilder.Append($"{stop.Label}, {stop.Address}\n");
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
