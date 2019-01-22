using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace BusMap.WebApi.DatabaseModels
{
    public class BusStopTrace
    {
        public int Id { get; set; }
        public TimeSpan Hour { get; set; }  //AutoSet
        public DateTime Date { get; set; }  //AutoSet

        public int BusStopId { get; set; }
        public BusStop BusStop { get; set; }
    }
}
