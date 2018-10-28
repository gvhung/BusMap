using System;
using System.Collections.Generic;
using System.Text;

namespace BusMap.Mobile.Models
{
    public class RouteQueued
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public CarrierQueued CarrierQueued { get; set; }
        public int CarrierId { get; set; }
        public List<BusStopQueued> BusStopsQueued { get; set; }

        public DateTime CreatedDatetime { get; set; }
        public DateTime? VotingStartedDatetime { get; set; }
        public DateTime? VotingEndedDateTime { get; set; }
        public int PositiveVotes { get; set; }
        public int NegativeVotes { get; set; }
    }
}
