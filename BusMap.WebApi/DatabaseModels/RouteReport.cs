using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace BusMap.WebApi.DatabaseModels
{
    public class RouteReport
    {
        public int Id { get; set; }
        public string Description { get; set; }
        public DateTime Date { get; set; }
        public int RouteId { get; set; }
        public Route Route { get; set; }
    }
}
