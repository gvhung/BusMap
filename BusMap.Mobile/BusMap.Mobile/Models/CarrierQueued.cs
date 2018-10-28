using System;
using System.Collections.Generic;
using System.Text;
using Prism.Mvvm;

namespace BusMap.Mobile.Models
{
    public class CarrierQueued : BindableBase
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public IList<RouteQueued> RoutesQueued { get; set; }
    }
}
