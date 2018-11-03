using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using BusMap.WebApi.DatabaseModels.BaseModels;

namespace BusMap.WebApi.DatabaseModels
{
    public class RouteQueued : RouteBase
    {
        public int CarrierQueuedId { get; set; }
        public CarrierQueued CarrierQueued { get; set; }

        public ICollection<BusStopQueued> BusStopsQueued { get; set; }

        public DateTime CreatedDatetime { get; set; }        
        public DateTime? VotingStartedDatetime { get; set; }
        public DateTime? VotingEndedDateTime { get; set; }
        public int PositiveVotes { get; set; }  //set to 0 in on model creating
        public int NegativeVotes { get; set; }  //set to 0 in on model creating
    }
}
