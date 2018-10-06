using BusMap.WebApi.DatabaseModels;
using BusMap.WebApi.Dto;
using BusMap.WebApi.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace BusMap.WebApi.Helpers
{
    public static class DtoConversionExtensions
    {
        public static RouteDto ToRouteDto(this RouteModel route)
        {
            if (route == null)
                return null;

            return new RouteDto
            {
                Id = route.Id,
                Name = route.Name,
                //BusStops = route?.BusStops
            };
        }
    }
}
