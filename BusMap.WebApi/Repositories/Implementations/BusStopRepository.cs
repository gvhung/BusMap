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
            => _context.BusStops.FirstOrDefault(x => x.Id == id);

        public IEnumerable<BusStop> GetAllBusStops()
            => _context.BusStops;

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

        public void RemoveBusStop(BusStop busStop)
        {
            _context.BusStops.Remove(busStop);
            _context.SaveChanges();
        }
    }
}
