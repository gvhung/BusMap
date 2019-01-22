using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace BusMap.WebApi.DatabaseModels
{
    public class Carrier : CarrierBase
    {
        public ICollection<Route> Routes { get; set; }
    }
}
