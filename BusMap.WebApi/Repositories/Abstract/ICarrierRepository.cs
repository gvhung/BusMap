﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using BusMap.WebApi.DatabaseModels;

namespace BusMap.WebApi.Repositories.Abstract
{
    public interface ICarrierRepository
    {
        Task<Carrier> GetCarrierAsync(int id);
        Task<IEnumerable<Carrier>> GetAllCarriers();
        Task AddCarrierAsync(Carrier carrier);
        Task AddCarrierRangeAsync(IEnumerable<Carrier> carriers);
        Task RemoveCarrier(Carrier carrier);
    }
}
