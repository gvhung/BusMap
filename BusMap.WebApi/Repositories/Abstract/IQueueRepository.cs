using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using BusMap.WebApi.Data;
using BusMap.WebApi.DatabaseModels;

namespace BusMap.WebApi.Repositories.Abstract
{
    public interface IQueueRepository
    {
        Task<RouteQueued> GetRouteQueuedAsync(int routeQueuedId);
        Task<IEnumerable<RouteQueued>> GetRoutesQueueAsync();
        Task<int> GetNumberOfQueuedRoutesAsync();
        Task<CarrierQueued> GetCarrierQueued(int id);

        Task AddRouteToQueueAsync(RouteQueued routeQueued);
        Task AddCarrierToQueueAsync(CarrierQueued carrierQueued);
        Task UpdateRouteAsync(int id, RouteQueued routeQueued);

        Task<IEnumerable<RouteQueued>> GetRoutesInRangeAsync(string yourLocation, int range);
        Task MoveQueuedRoutesToMainTableAsync();
        Task RemoveRejectedQueuedRoutesAsync();
    }
}
