using System;
using System.Collections.Generic;
using System.Text;
using Prism.Mvvm;

namespace BusMap.Mobile.Models
{
    public class Carrier : BindableBase
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public IList<Route> Routes { get; set; }
    }
}
