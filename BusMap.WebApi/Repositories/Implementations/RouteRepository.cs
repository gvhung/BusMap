using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using BusMap.WebApi.Data;
using BusMap.WebApi.Models;
using BusMap.WebApi.Repositories.Abstract;
using Microsoft.EntityFrameworkCore;

namespace BusMap.WebApi.Repositories.Implementations
{
    public class RouteRepository : IRouteRepository
    {
        private readonly DatabaseContext _context;

        public RouteRepository(DatabaseContext context)
        {
            _context = context;
        }


        public Route GetRoute(int id)
            => _context.Routes.FirstOrDefault(x => x.Id == id);

        public IEnumerable<Route> GetAllRoutes()
            => _context.Routes;

        public void AddRoute(Route route)
        {
            _context.Routes.Add(route);
            _context.SaveChanges();
        }

        public void AddRouteRange(IEnumerable<Route> routes)
        {
            _context.Routes.AddRange(routes);
            _context.SaveChanges();
        }

        public void RemoveRoute(Route route)
        {
            _context.Routes.Remove(route);
            _context.SaveChanges();
        }
    }
}
