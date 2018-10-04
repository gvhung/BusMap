﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using BusMap.WebApi.DatabaseModels;

namespace BusMap.WebApi.Repositories.Abstract
{
    public interface ICarrierRepository
    {
        Carrier GetCarrier(int id);
        IEnumerable<Carrier> GetAllCarriers();
        void AddCarrier(Carrier carrier);
        void AddCArrierRange(IEnumerable<Carrier> carriers);
        void RemoveCarrier(Carrier carrier);
    }
}
