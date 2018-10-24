using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace BusMap.WebApi.DatabaseModels
{
    public class CarrierQueued : CarrierBase
    {
        public ICollection<RouteQueued> RoutesQueued { get; set; }
    }
}
