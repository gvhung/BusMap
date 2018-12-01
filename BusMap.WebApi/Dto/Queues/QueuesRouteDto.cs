using System;
using System.Collections.Generic;

namespace BusMap.WebApi.Dto.Queues
{
    public class QueuesRouteDto
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string DayOfTheWeek { get; set; }
        public int CarrierQueuedId { get; set; }
        public QueuesCarrierDto CarrierQueued { get; set; }

        public ICollection<QueuesBusStopDto> BusStopsQueued { get; set; }

        public DateTime CreatedDatetime { get; set; }
        public DateTime? VotingStartedDatetime { get; set; }
        public DateTime? VotingEndedDateTime { get; set; }
        public int PositiveVotes { get; set; }
        public int NegativeVotes { get; set; }
        
    }
}
