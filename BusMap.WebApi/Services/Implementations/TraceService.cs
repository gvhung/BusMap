using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using BusMap.WebApi.DatabaseModels;
using BusMap.WebApi.Repositories.Abstract;
using BusMap.WebApi.Services.Abstract;

namespace BusMap.WebApi.Services.Implementations
{
    public class TraceService : ITraceService
    {
        private readonly ITraceRepository _repository;

        public TraceService(ITraceRepository repository)
        {
            _repository = repository;
        }

        public async Task AddBusStopTraceAsync(BusStopTrace busStopTrace)
            => await _repository.AddBusStopTraceAsync(busStopTrace);
    }
}
