using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace BusMap.WebApi.Dto.Routes
{
    public class BusStopDto
    {
        public int Id { get; set; }

        public double Latitude { get; set; }

        public double Longitude { get; set; }

        public string Address { get; set; }

        public string Label { get; set; }



        public int RouteId { get; set; }

        public RouteDto Route { get; set; }
    }
}
