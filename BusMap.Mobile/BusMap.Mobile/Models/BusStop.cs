﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Prism.Mvvm;
using Xamarin.Forms.Maps;

namespace BusMap.Mobile.Models
{
    public class BusStop : BindableBase
    {
        private bool _isRecentBusStop;
        public int Id { get; set; }
        public double Latitude { get; set; }
        public double Longitude { get; set; }
        public string Address { get; set; }
        public string Label { get; set; }
        public TimeSpan Hour { get; set; }


        public Route Route { get; set; }

        public string PunctualityPercentage { get; set; }
        public string PunctualityMode { get; set; }
        public string PunctualityAvgBeforeTime { get; set; }
        public string PunctualityAvgAfterTime { get; set; }

        public bool IsRecentBusStop
        {
            get => _isRecentBusStop;
            set => SetProperty(ref _isRecentBusStop, value);
        }

        //[Obsolete("Don't use this", true)]
        //public new Position Position { get; set; }

        public Xamarin.Forms.Maps.Pin ConvertToFormsMapsPin()
        {
            if (string.IsNullOrEmpty(Label))    //Todo: Change after map renderer customization
                Label = " ";

            var result = new Xamarin.Forms.Maps.Pin()
            {
                Id = Id,
                Position = new Position(Latitude, Longitude),
                Address = Address,
                Label = Label
            };

            return result;
        }

        public override string ToString()
        {
            if (string.IsNullOrWhiteSpace(Label))
            {
                return Address;
            }

            return $"{Address}, {Label}";
        }
    }
}
