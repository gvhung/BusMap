using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using AutoMapper;
using BusMap.WebApi.DatabaseModels;
using BusMap.WebApi.Dto.Queues;
using BusMap.WebApi.Dto.Routes;
using BusMap.WebApi.Repositories.Abstract;
using BusMap.WebApi.Services.Abstract;

namespace BusMap.WebApi.Services.Implementations
{
    public class QueueService : IQueueService
    {
        private readonly IQueueRepository _repository;
        private readonly IMapper _mapper;

        public QueueService(IQueueRepository repository, IMapper mapper)
        {
            _repository = repository;
            _mapper = mapper;
        }

        public async Task<IEnumerable<QueuesRouteDto>> GetRoutesQueueAsync()
        {
            var routes = await _repository.GetRoutesQueueAsync();
            var result = _mapper.Map<IEnumerable<RouteQueued>, IEnumerable<QueuesRouteDto>>(routes);
            return result;
        }

        public async Task<int> GetNumberOfQueuedRoutesAsync()
        {
            var nOfRoutes = await _repository.GetNumberOfQueuedRoutesAsync();
            return nOfRoutes;
        }

        public async Task AddRouteToQueueAsync(RouteQueued routeQueued)
            => await _repository.AddRouteToQueueAsync(routeQueued);

        public async Task UpdateRouteAsync(int id, RouteQueued routeQueued)
            => await _repository.UpdateRouteAsync(id, routeQueued);
    }
}
