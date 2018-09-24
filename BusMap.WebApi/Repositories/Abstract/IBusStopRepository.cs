using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using BusMap.WebApi.Models;

namespace BusMap.WebApi.Repositories.Abstract
{
    public interface IBusStopRepository
    {
        BusStop GetBusStop(int id);
        IEnumerable<BusStop> GetAllBusStops();
        void AddBusStop(BusStop busStop);
        void AddBusStopsRange(IEnumerable<BusStop> busStops);
        void RemoveBusStop(int id);
    }
}
