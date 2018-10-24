using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace BusMap.WebApi.DatabaseModels
{
    public class BusStopQueued : BusStopBase
    {
        public RouteQueued RouteQueued { get; set; }
    }
}
