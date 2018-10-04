using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using BusMap.WebApi.Data;
using BusMap.WebApi.Dto;
using BusMap.WebApi.DatabaseModels;
using BusMap.WebApi.Repositories.Abstract;
using Microsoft.EntityFrameworkCore;
using BusMap.WebApi.Models;

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
            => _context.Routes

            .FirstOrDefault(r => r.Id == id);

        public RouteModel GetRouteIncludeBusStops(int id)
            => _context.Routes
                .Include(r => r.BusStops)
                .Select(r => new RouteModel
                {
                    Id = r.Id,
                    Name = r.Name,
                    CarrierId = r.CarrierId,
                    BusStops = r.BusStops.Select(b => new BusStopModel
                    {
                        Id = b.Id,
                        Address = b.Address,
                        Label = b.Label,
                        Latitude = b.Latitude,
                        Longitude = b.Longitude
                    })
                    .ToList()
                })
                .SingleOrDefault(r => r.Id == id);

        public RouteModel GetRouteIncludeBusStopsCarrier(int id)
        {
            var routeCarrier = _context.
                Carriers
                .SingleOrDefault(c => c.Id == _context.Routes.SingleOrDefault(r => r.Id == id).CarrierId);
            var carrierModel = new CarrierModel
            {
                Id = routeCarrier.Id,
                Name = routeCarrier.Name
            };

            return _context.Routes
                .Include(r => r.BusStops)
                .Include(r => r.Carrier)
                .Select(r => new RouteModel
                {
                    Id = r.Id,
                    Name = r.Name,
                    CarrierId = r.CarrierId,
                    BusStops = r.BusStops.Select(b => new BusStopModel
                    {
                        Id = b.Id,
                        Address = b.Address,
                        Label = b.Label,
                        Latitude = b.Latitude,
                        Longitude = b.Longitude
                    })
                    .ToList(),
                    Carrier = carrierModel
                })
                .FirstOrDefault();
        }


        public IEnumerable<RouteModel> GetAllRoutes()
            => _context.Routes
                .Select(r => new RouteModel
                {
                    Id = r.Id,
                    Name = r.Name,
                    CarrierId = r.CarrierId
                })
                .ToList();

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
