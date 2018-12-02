using System;
using System.Collections.Generic;
using System.Text;

namespace BusMap.Mobile.Models
{
    public class BusStopQueued
    {
        public int Id { get; set; }
        public double Latitude { get; set; }
        public double Longitude { get; set; }
        public string Address { get; set; }
        public string Label { get; set; }
        public TimeSpan Hour { get; set; }


        public RouteQueued RouteQueued { get; set; }
    }
}
