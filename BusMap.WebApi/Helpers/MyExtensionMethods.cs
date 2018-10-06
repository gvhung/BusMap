using BusMap.WebApi.DatabaseModels;
using BusMap.WebApi.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace BusMap.WebApi.Helpers
{
    public static class MyExtensionMethods
    {
        public static Route ToRoute(this RouteModel routeModel)
        {
            var result = new Route()
            {
                Id = routeModel.Id,
                Name = routeModel.Name,
                CarrierId = routeModel.CarrierId
            };

            if (routeModel.Carrier != null)
                result.Carrier = new Carrier
                {
                    Id = routeModel.Carrier.Id,
                    Name = routeModel.Carrier.Name
                };

            if (routeModel.BusStops != null)
            {
                result.BusStops = new List<BusStop>();
                foreach (var busStop in routeModel.BusStops)
                {
                    result.BusStops.Add(new BusStop
                    {
                        Id = busStop.Id,
                        Address = busStop.Address,
                        Label = busStop.Label,
                        Latitude = busStop.Latitude,
                        Longitude = busStop.Longitude
                    });
                }
            }

            return result;
        }
    }
}
