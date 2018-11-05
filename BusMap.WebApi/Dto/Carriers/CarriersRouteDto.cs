using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using BusMap.WebApi.DatabaseModels;

namespace BusMap.WebApi.Dto.Carriers
{
    public class CarriersRouteDto
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public int DayOfTheWeek { get; set; }
        public ICollection<CarriersBusStopDto> BusStops { get; set; }


    }
}
