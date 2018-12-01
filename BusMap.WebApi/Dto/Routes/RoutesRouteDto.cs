using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using BusMap.WebApi.DatabaseModels;

namespace BusMap.WebApi.Dto.Routes
{
    public class RoutesRouteDto
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string DayOfTheWeek { get; set; }
        public RoutesCarrierDto Carrier { get; set; }
        public ICollection<RoutesBusStopDto> BusStops { get; set; }

        public string PunctualityPercentage { get; set; }
        public string PunctualityAvgBeforeTime { get; set; }
        public string PunctualityAvgAfterTime { get; set; }

    }
}
