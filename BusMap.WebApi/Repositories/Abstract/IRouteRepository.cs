using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using BusMap.WebApi.Dto;
using BusMap.WebApi.DatabaseModels;
using BusMap.WebApi.Models;

namespace BusMap.WebApi.Repositories.Abstract
{
    public interface IRouteRepository
    {
        Route GetRoute(int id);
        RouteModel GetRouteIncludeBusStops(int id);
        RouteModel GetRouteIncludeBusStopsCarrier(int id);
        IEnumerable<RouteModel> GetAllRoutes();
        void AddRoute(Route route);
        void AddRouteRange(IEnumerable<Route> routes);
        void RemoveRoute(Route route);
    }
}
