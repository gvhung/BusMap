using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using BusMap.WebApi.DatabaseModels;

namespace BusMap.WebApi.Services.Abstract
{
    public interface ITraceService
    {
        Task AddBusStopTraceAsync(BusStopTrace busStopTrace);
    }
}
