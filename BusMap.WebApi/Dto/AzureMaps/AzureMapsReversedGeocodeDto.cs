using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace BusMap.WebApi.Dto.AzureMaps
{
    public class AzureMapsReversedGeocodeDto
    {
        public string City { get; set; }
        public string Street { get; set; }
    }
}
