using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using BusMap.WebApi.DatabaseModels;
using BusMap.WebApi.Models;

namespace BusMap.WebApi.Dto.BusStops
{
    public class BusStopsRouteDto
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public int DayOfTheWeek { get; set; }
        public BusStopsCarrierDto Carrier { get; set; }

    }
}
