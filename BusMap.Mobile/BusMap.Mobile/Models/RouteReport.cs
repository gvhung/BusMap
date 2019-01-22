using System;
using System.Collections.Generic;
using System.Text;

namespace BusMap.Mobile.Models
{
    public class RouteReport
    {
        public int Id { get; set; }
        public string Description { get; set; }
        public DateTime Date { get; set; }
        public int RouteId { get; set; }
    }
}
