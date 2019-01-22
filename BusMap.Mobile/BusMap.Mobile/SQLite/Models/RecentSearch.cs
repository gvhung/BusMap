using System;
using System.Collections.Generic;
using System.Text;
using SQLite;

namespace BusMap.Mobile.SQLite.Models
{
    public class RecentSearch
    {
        [PrimaryKey]
        [AutoIncrement]
        public int  Id { get; set; }

        public string Start { get; set; }

        public string Destination { get; set; }

        public override string ToString()
            => $"{Start} - {Destination}";
    }
}
