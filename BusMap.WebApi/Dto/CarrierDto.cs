using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using BusMap.WebApi.DatabaseModels;

namespace BusMap.WebApi.Dto
{
    public class CarrierDto
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public IList<RouteDto> Routes { get; set; }
    }
}
