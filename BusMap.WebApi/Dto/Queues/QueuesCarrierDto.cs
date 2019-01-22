using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using BusMap.WebApi.DatabaseModels;

namespace BusMap.WebApi.Dto.Queues
{
    public class QueuesCarrierDto
    {
        public int Id { get; set; }
        public string Name { get; set; }

        public DateTime CreatedDatetime { get; set; }
        public DateTime? VotingStartedDatetime { get; set; }
        public DateTime? VotingEndedDateTime { get; set; }
        public int PositiveVotes { get; set; }
        public int NegativeVotes { get; set; }
    }
}
