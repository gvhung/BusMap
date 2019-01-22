using System;
using System.Collections.Generic;
using System.Text;
using SQLite;

namespace BusMap.Mobile.SQLite.Models
{
    public class VotedQueuedRoute
    {
        [PrimaryKey]
        public int Id { get; set; }

        public bool VoteType { get; set; }
        public DateTime AddedDate { get; set; }
    }
}
