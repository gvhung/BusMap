using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using BusMap.WebApi.Data;
using BusMap.WebApi.DatabaseModels;
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

        public async Task<BusStop> GetBusStopAsync(int id)
            => await _context.BusStops
                .FirstOrDefaultAsync(x => x.Id == id);

        public async Task<BusStop> GetBusStopIncludeRouteAsync(int id)
            => await _context.BusStops
                .Include(b => b.Route)
                .FirstOrDefaultAsync(b => b.Id == id);

        public async Task<BusStop> GetBusStopIncludeRouteCarrierAsync(int id)
            => await _context.BusStops
                .Include(b => b.Route)
                .ThenInclude(r => r.Carrier)
                .FirstOrDefaultAsync(b => b.Id == id);

        public async Task<BusStop> GetBusStopIncludeAllAsync(int id)
            => await _context.BusStops
                .Include(b => b.BusStopTraces)
                .Include(b => b.Route)
                .ThenInclude(r => r.Carrier)
                .FirstOrDefaultAsync(b => b.Id == id);

        public async Task<IEnumerable<BusStop>> GetAllBusStopsAsync()
            => await _context.BusStops.ToListAsync();

        public async Task<IEnumerable<BusStop>> GetAllBusStopsIncludeRouteAsync()
            => await _context.BusStops
                .Include(b => b.Route)
                .ToListAsync();

        public async Task<IEnumerable<BusStop>> GetAllBusStopsIncludeRouteCarrierAsync()
            => await _context.BusStops
                .Include(b => b.Route)
                .ThenInclude(r => r.Carrier)
                .ToListAsync();

        public async Task<IEnumerable<BusStop>> GetAllBusStopsIncludeAllAsync()
            => await _context.BusStops
                .Include(b => b.BusStopTraces)
                .Include(b => b.Route)
                .ThenInclude(r => r.Carrier)
                .ToListAsync();

        public async Task<int> GetLastBusStopIdAsync()
        {
            var busStop = await _context.BusStops
                .LastOrDefaultAsync();

            return busStop.Id;
        } 

        public async Task AddBusStopAsync(BusStop busStop)
        {
            await _context.BusStops.AddAsync(busStop);
            await _context.SaveChangesAsync();
        }

        public async Task AddBusStopsRangeAsync(IEnumerable<BusStop> busStops)
        {
            await _context.BusStops.AddRangeAsync(busStops);
            await _context.SaveChangesAsync();
        }

        public async Task RemoveBusStopAsync(BusStop busStop)
        {
            _context.BusStops.Remove(busStop);
            await _context.SaveChangesAsync();
        }
    }
}
