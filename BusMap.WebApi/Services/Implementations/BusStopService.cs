using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using AutoMapper;
using BusMap.WebApi.DatabaseModels;
using BusMap.WebApi.Dto.BusStops;
using BusMap.WebApi.Repositories.Abstract;
using BusMap.WebApi.Services.Abstract;

namespace BusMap.WebApi.Services.Implementations
{
    public class BusStopService : IBusStopService
    {
        private readonly IBusStopRepository _repository;
        private readonly IMapper _mapper;

        public BusStopService(IBusStopRepository repository, IMapper mapper)
        {
            _repository = repository;
            _mapper = mapper;
        }


        public async Task<BusStopsBusStopDto> GetBusStopAsync(int id)
        {
            var busStop = await _repository.GetBusStopAsync(id);
            return _mapper.Map<BusStop, BusStopsBusStopDto>(busStop);
        }

        public async Task<BusStopsBusStopDto> GetBusStopIncludeRouteAsync(int id)
        {
            var busStop = await _repository.GetBusStopIncludeRouteAsync(id);
            return _mapper.Map<BusStop, BusStopsBusStopDto>(busStop);
        }

        public async Task<BusStopsBusStopDto> GetBusStopIncludeRouteCarrierAsync(int id)
        {
            var busStop = await _repository.GetBusStopIncludeRouteCarrierAsync(id);
            return _mapper.Map<BusStop, BusStopsBusStopDto>(busStop);
        }

        public async Task<IEnumerable<BusStopsBusStopDto>> GetAllBusStopsAsync()
        {
            var busStops = await _repository.GetAllBusStopsAsync();
            return _mapper.Map<IEnumerable<BusStop>, IEnumerable<BusStopsBusStopDto>>(busStops);
        }

        public async Task AddBusStopAsync(BusStop busStop)
            => await _repository.AddBusStopAsync(busStop);

        public async Task AddBusStopsRangeAsync(IEnumerable<BusStop> busStops)
            => await _repository.AddBusStopsRangeAsync(busStops);

        public async Task RemoveBusStopAsync(BusStopsBusStopDto busStop)
        {
            var busStopToRemove = await _repository.GetBusStopAsync(busStop.Id);
            await _repository.RemoveBusStopAsync(busStopToRemove);
        }
    }
}
