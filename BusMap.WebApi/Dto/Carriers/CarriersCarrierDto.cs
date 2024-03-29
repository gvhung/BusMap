﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using BusMap.WebApi.DatabaseModels;

namespace BusMap.WebApi.Dto.Carriers
{
    public class CarriersCarrierDto
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public IList<CarriersRouteDto> Routes { get; set; }
    }
}
