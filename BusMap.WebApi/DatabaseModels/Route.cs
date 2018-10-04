using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace BusMap.WebApi.DatabaseModels
{
    public class Route
    {
        public int Id { get; set; }

        [Required]
        [MaxLength(100)]
        public string Name { get; set; }

        public int CarrierId { get; set; }

        public Carrier Carrier { get; set; }

        public ICollection<BusStop> BusStops { get; set; }

        

    }
}
