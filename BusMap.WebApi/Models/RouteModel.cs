using BusMap.WebApi.DatabaseModels;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace BusMap.WebApi.Models
{
    public class RouteModel
    {
        public int Id { get; set; }

        [Required]
        [MaxLength(100)]
        public string Name { get; set; }

        public int CarrierId { get; set; }

        public CarrierModel Carrier { get; set; }

        public ICollection<BusStopModel> BusStops { get; set; }

    }
}
