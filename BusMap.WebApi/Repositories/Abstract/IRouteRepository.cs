using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using BusMap.WebApi.Models;

namespace BusMap.WebApi.Repositories.Abstract
{
    public interface IRouteRepository
    {
        Route GetRoute(int id);
        IEnumerable<Route> GetAllRoutes();
        void AddRoute(Route route);
        void AddRouteRange(IEnumerable<Route> routes);
        void RemoveRoute(int id);
    }
}
