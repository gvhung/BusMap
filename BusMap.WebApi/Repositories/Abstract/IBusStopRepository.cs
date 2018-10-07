using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using BusMap.WebApi.DatabaseModels;

namespace BusMap.WebApi.Repositories.Abstract
{
    public interface IBusStopRepository
    {
        Task<BusStop> GetBusStopAsync(int id);
        Task<BusStop> GetBusStopIncludeRouteAsync(int id);
        Task<BusStop> GetBusStopIncludeRouteCarrierAsync(int id);

        Task<IEnumerable<BusStop>> GetAllBusStopsAsync();
        Task<IEnumerable<BusStop>> GetAllBusStopsIncludeRouteAsync();
        Task<IEnumerable<BusStop>> GetAllBusStopsIncludeRouteCarrierAsync();

        Task AddBusStopAsync(BusStop busStop);
        Task AddBusStopsRangeAsync(IEnumerable<BusStop> busStops);

        Task RemoveBusStopAsync(BusStop busStop);
    }
}
