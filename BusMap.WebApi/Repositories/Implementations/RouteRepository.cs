using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using BusMap.WebApi.Data;
using BusMap.WebApi.DatabaseModels;
using BusMap.WebApi.Repositories.Abstract;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Internal;

namespace BusMap.WebApi.Repositories.Implementations
{
    public class RouteRepository : IRouteRepository
    {
        private readonly DatabaseContext _context;

        public RouteRepository(DatabaseContext context)
        {
            _context = context;
        }

        public async Task<Route> GetRouteAsync(int id)
            => await _context.Routes
                .FirstOrDefaultAsync(r => r.Id == id);

        

        public async Task<Route> GetRouteIncludeBusStopsAsync(int id)
            => await _context.Routes
                .Include(r => r.BusStops)
                .SingleOrDefaultAsync(r => r.Id == id);

        public async Task<Route> GetRouteIncludeCarrierAsync(int id)
            => await _context.Routes
                .Include(r => r.Carrier)
                .SingleOrDefaultAsync(r => r.Id == id);

        public async Task<Route> GetRouteIncludeBusStopsCarrierAsync(int id)
            => await _context.Routes
                .Include(r => r.BusStops)
                .Include(r => r.Carrier)
                .FirstOrDefaultAsync(r => r.Id == id);

        public async Task<Route> GetRouteIncludeAllAsync(int id)
            => await _context.Routes
                .Include(r => r.Carrier)
                .Include(r => r.BusStops)
                .ThenInclude(b => b.BusStopTraces)
                .FirstOrDefaultAsync(r => r.Id == id);


        public async Task<IEnumerable<Route>> GetAllRoutesAsync()
            => await _context.Routes.ToListAsync();

        public async Task<IEnumerable<Route>> GetAllRoutesIncludeBusStopsAsync()
            => await _context.Routes
                .Include(r => r.BusStops)
                .ToListAsync();

        public async Task<IEnumerable<Route>> GetAllRoutesIncludeCarrierAsync()
            => await _context.Routes
                .Include(r => r.Carrier)
                .ToListAsync();

        public async Task<IEnumerable<Route>> GetAllRoutesIncludeBusStopsCarrierAsync()
            => await _context.Routes
                .Include(r => r.BusStops)
                .Include(r => r.Carrier)
                .ToListAsync();

        public async Task<IEnumerable<Route>> GetAllRoutesIncludeAllAsync()
            => await _context.Routes
                .Include(r => r.Carrier)
                .Include(r => r.BusStops)
                .ThenInclude(b => b.BusStopTraces)
                .ToListAsync();

        public async Task<IEnumerable<Route>> GetAllFavoriteRoutesAsync(IEnumerable<int> routesIds)
            => await _context.Routes
                .Include(r => r.Carrier)
                .Include(r => r.BusStops)
                .ThenInclude(b => b.BusStopTraces)
                .Where(r => routesIds.Contains(r.Id))
                .ToListAsync();

        public async Task AddRouteAsync(Route route)
        {
            await _context.Routes.AddAsync(route);
            await _context.SaveChangesAsync();
        }

        public async Task AddRouteRangeAsync(IEnumerable<Route> routes)
        {
            await _context.Routes.AddRangeAsync(routes);
            await _context.SaveChangesAsync();
        }

        public async Task RemoveRouteAsync(Route route)
        {
            _context.Routes.Remove(route);
            await _context.SaveChangesAsync();
        }

        public async Task<string> GetRouteCurrentLatencyAsync(Route route)
        {
            var result = "Nan";
            var currentDate = DateTime.Now;
            var lastTrace = await _context.BusStopTraces
                .Where(x => x.Date.Equals(currentDate.Date))
                .LastOrDefaultAsync(x => x.BusStop.RouteId == route.Id);

            if (lastTrace != null)
            {
                result = (lastTrace.Hour - lastTrace.BusStop.Hour).Minutes.ToString();
            }
                

            return result;
        }

    }
}
