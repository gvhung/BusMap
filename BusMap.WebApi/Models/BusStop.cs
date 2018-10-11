﻿using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace BusMap.WebApi.Models
{
    public class BusStop
    {
        public int Id { get; set; }

        [Required]
        public double Latitude { get; set; }

        [Required]
        public double Longitude { get; set; }

        [Required]
        [MaxLength(50)]
        public string Address { get; set; }

        [MaxLength(50)]
        public string Label { get; set; }



        public int RouteId { get; set; }

        public Route Route { get; set; }
    }
}
