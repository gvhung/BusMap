using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;
using BusMap.WebApi.DatabaseModels.BaseModels;

namespace BusMap.WebApi.DatabaseModels
{
    public class Route : RouteBase
    {
        public int CarrierId { get; set; }
        public Carrier Carrier { get; set; }

        public ICollection<BusStop> BusStops { get; set; }

        

    }
}
