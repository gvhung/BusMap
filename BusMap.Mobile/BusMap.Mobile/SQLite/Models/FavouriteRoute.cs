using System;
using System.Collections.Generic;
using System.Text;
using SQLite;

namespace BusMap.Mobile.SQLite.Models
{
    public class FavoriteRoute
    {
        [PrimaryKey]
        public int Id { get; set; }
        public DateTime AddedDate { get; set; }
    }
}
