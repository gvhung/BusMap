using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using BusMap.WebApi.DatabaseModels;
using BusMap.WebApi.Models;

namespace BusMap.WebApi.Dto
{
    public class RouteDto
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public ICollection<BusStopModel> BusStops { get; set; }


    }
}
