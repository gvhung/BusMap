using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using BusMap.WebApi.DatabaseModels;
using BusMap.WebApi.Dto.Queues;
using BusMap.WebApi.Dto.Routes;

namespace BusMap.WebApi.Services.Abstract
{
    public interface IQueueService
    {
        Task<IEnumerable<QueuesRouteDto>> GetRoutesQueueAsync();
        Task<int> GetNumberOfQueuedRoutesAsync();
        Task AddRouteToQueueAsync(RouteQueued routeQueued);
        Task UpdateRouteAsync(int id, RouteQueued routeQueued);
    }
}
