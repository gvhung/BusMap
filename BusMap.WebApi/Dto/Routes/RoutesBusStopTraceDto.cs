using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace BusMap.WebApi.Dto.Routes
{
    public class RoutesBusStopTraceDto
    {
        public int Id { get; set; }
        public TimeSpan Hour { get; set; }
    }
}
