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
    public class BusStopRepository : IBusStopRepository
    {
        private readonly DatabaseContext _context;

        public BusStopRepository(DatabaseContext context)
        {
            _context = context;
        }

        public BusStop GetBusStop(int id)
            => _context.BusStops
                .Include(x => x.Route)
                .ThenInclude(x => x.Carrier)
                .First(x => x.Id == id);

        public IEnumerable<BusStop> GetAllBusStops()
            => _context.BusStops
                .Include(x => x.Route)
                .ThenInclude(x => x.Carrier);

        public void AddBusStop(BusStop busStop)
        {
            _context.BusStops.Add(busStop);
            _context.SaveChanges();
        }

        public void AddBusStopsRange(IEnumerable<BusStop> busStops)
        {
            _context.BusStops.AddRange(busStops);
            _context.SaveChanges();
        }

        public void RemoveBusStop(int id)
        {
            var busStopToRemove = _context.BusStops.First(x => x.Id == id);
            _context.BusStops.Remove(busStopToRemove);
            _context.SaveChanges();
        }
    }
}
