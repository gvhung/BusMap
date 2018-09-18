using System;
using System.Collections.Generic;
using System.Text;

namespace BusMap.Mobile.Models
{
    public class Route
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public Carrier Carrier { get; set; }
        public ICollection<BusStop> Pins { get; set; }

    }
}
