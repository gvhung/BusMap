using System;
using System.Collections.Generic;
using System.Text;

namespace BusMap.Mobile.Models
{
    public class CarrierQueued
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public IList<RouteQueued> RoutesQueued { get; set; }
    }
}
