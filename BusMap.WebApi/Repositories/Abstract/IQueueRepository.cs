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
        Task<IEnumerable<RouteQueued>> GetRoutesQueueAsync();
        Task<int> GetNumberOfQueuedRoutesAsync();
        Task AddRouteToQueueAsync(RouteQueued routeQueued);
        Task UpdateRouteAsync(int id, RouteQueued routeQueued);
    }
}
