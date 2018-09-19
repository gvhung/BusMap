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
        public ICollection<BusStop> BusStops { get; set; }

        public string BusStopsString => BusStopsToString();



        private string BusStopsToString()
        {
            var stringBuilder = new StringBuilder();
            foreach (var stop in BusStops)
            {
                stringBuilder.Append($"{stop.Label} {stop.Address}\n");
            }

            return stringBuilder.ToString();
        }
    }
}
