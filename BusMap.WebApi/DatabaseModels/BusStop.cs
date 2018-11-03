using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace BusMap.WebApi.DatabaseModels
{
    public class BusStop : BusStopBase
    {
        public int RouteId { get; set; }

        public Route Route { get; set; }
    }
}
