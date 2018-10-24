using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;
using System.Windows.Input;
using BusMap.Mobile.Annotations;
using BusMap.Mobile.Views;
using Prism.Commands;
using Prism.Mvvm;
using Prism.Navigation;
using Prism.Services;
using Xamarin.Forms;

namespace BusMap.Mobile.Models
{
    public class Route : BindableBase
    {
        private bool _busStopsStringIsVisible;

        public int Id { get; set; }
        public string Name { get; set; }
        public Carrier Carrier { get; set; }
        public int CarrierId { get; set; }
        public List<BusStop> BusStops { get; set; }
        public BusStop StartingBusStop => BusStops.First();
        public BusStop DestinationBusStop => BusStops.Last();

        public string BusStopsString => ToString();

        public bool BusStopsStringIsVisible
        {
            get => _busStopsStringIsVisible;
            set => SetProperty(ref _busStopsStringIsVisible, value);
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


        //Todo: Find how to convert to behaviour/put to viewModel
        public ICommand LabelCommand => new DelegateCommand(() =>
        {
            BusStopsStringIsVisible = !BusStopsStringIsVisible;
        });



    }
}
