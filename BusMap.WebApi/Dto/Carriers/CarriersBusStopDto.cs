using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace BusMap.WebApi.Dto.Carriers
{
    public class CarriersBusStopDto
    {
        public int Id { get; set; }

        public double Latitude { get; set; }

        public double Longitude { get; set; }

        public string Address { get; set; }

        public string Label { get; set; }
        public TimeSpan Hour { get; set; }

    }
}
