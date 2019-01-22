using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using BusMap.Mobile.Models;

namespace BusMap.Mobile.Models
{
    public class BusStopTrace
    {
        public int Id { get; set; }
        public TimeSpan Hour { get; set; }

        public int BusStopId { get; set; }
        //public BusStop BusStop { get; set; }
    }
}
