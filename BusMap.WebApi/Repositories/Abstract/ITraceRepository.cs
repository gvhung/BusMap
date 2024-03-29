﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using BusMap.WebApi.DatabaseModels;

namespace BusMap.WebApi.Repositories.Abstract
{
    public interface ITraceRepository
    {
        Task AddBusStopTraceAsync(BusStopTrace busStopTrace);
    }
}
