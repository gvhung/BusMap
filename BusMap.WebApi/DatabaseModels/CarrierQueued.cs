using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace BusMap.WebApi.DatabaseModels
{
    public class CarrierQueued : CarrierBase
    {
        public DateTime CreatedDatetime { get; set; }
        public DateTime? VotingStartedDatetime { get; set; }
        public DateTime? VotingEndedDateTime { get; set; }
        public int PositiveVotes { get; set; }  //set to 0 in on model creating
        public int NegativeVotes { get; set; }  //set to 0 in on model creating

        public ICollection<RouteQueued> RoutesQueued { get; set; }
    }

}
