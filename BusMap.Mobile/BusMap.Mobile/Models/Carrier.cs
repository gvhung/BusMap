using System;
using System.Collections.Generic;
using System.Text;

namespace BusMap.Mobile.Models
{
    public class Carrier
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public IList<Route> Routes { get; set; }
    }
}
