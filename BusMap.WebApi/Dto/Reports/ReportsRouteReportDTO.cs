using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace BusMap.WebApi.Dto.Reports
{
    public class ReportsRouteReportDto
    {
        public int Id { get; set; }
        public string Description { get; set; }
        public int RouteId { get; set; }
    }
}
