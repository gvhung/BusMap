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

        public async Task<int> GetRouteCurrentLatencyAsync(int routeId)
        {
            int result = 9999;
            var currentDate = DateTime.Now;
            //var currentDate = new DateTime(2018,11,29,10,52,0);

            var route = await _context.Routes
                .Include(r => r.BusStops)
                .FirstOrDefaultAsync(r => r.Id == routeId);
            var lastTrace = await _context.BusStopTraces
                .Include(t => t.BusStop)
                .Where(t => t.Date.Equals(currentDate.Date) 
                            && t.Hour > route.BusStops
                                .First()
                                .Hour
                                .Subtract(new TimeSpan(0, 10, 0)) 
                            && t.Hour >= currentDate.TimeOfDay.Subtract(new TimeSpan(1, 0, 0)))
                .LastOrDefaultAsync(t => t.BusStop.RouteId == routeId);


            if (lastTrace != null)
            {
                result = (lastTrace.Hour - lastTrace.BusStop.Hour).Minutes;
            }
                
            return result;
        }

        public async Task<BusStop> GetRouteRecentBusStopAsync(int routeId)
        {
            BusStop result = null;
            var currentDate = DateTime.Now;
            //var currentDate = new DateTime(2018, 11, 29, 10, 52, 0);

            var route = await _context.Routes
                .Include(r => r.BusStops)
                .FirstOrDefaultAsync(r => r.Id == routeId);
            var lastTrace = await _context.BusStopTraces
                .Include(t => t.BusStop)
                .Where(t => t.Date.Equals(currentDate.Date)
                            && t.Hour > route.BusStops
                                .First()
                                .Hour
                                .Subtract(new TimeSpan(0, 10, 0))
                            && t.Hour >= currentDate.TimeOfDay.Subtract(new TimeSpan(1, 0, 0)))
                .LastOrDefaultAsync(t => t.BusStop.RouteId == routeId);

            if (lastTrace != null)
            {
                result = lastTrace.BusStop;
            }

            return result;
        }

        public async Task<IEnumerable<Route>> FindRoutesAsync(string startCity, string destinationCity, 
            string days = "1,2,3,4,5,6,0", TimeSpan hourFrom = default(TimeSpan), 
            TimeSpan hourTo = default(TimeSpan), DateTime date = default(DateTime))
        {
            var dayOfTheWeek = date.DayOfWeek;
            if (hourTo == default(TimeSpan))
                hourTo = new TimeSpan(23,59,0);
            if (string.IsNullOrEmpty(days))
                days = "1,2,3,4,5,6,0";

            List<Route> foundedRoutes = await _context.Routes
                .Include(r => r.Carrier)
                .Include(r => r.BusStops)
                .ThenInclude(b => b.BusStopTraces)
                .Where(r => r.BusStops.Any(b => b.Address
                    .ToLowerInvariant()
                    .Contains(startCity.ToLowerInvariant())))
                .Where(r => r.BusStops.Any(b => b.Address
                    .ToLowerInvariant()
                    .Contains(destinationCity.ToLowerInvariant())))
                .Where(r => r.BusStops.Any(b => b.Hour >= hourFrom && b.Hour <= hourTo))
                .ToListAsync();

            foundedRoutes = GetRoutesForGoodDirection(foundedRoutes, startCity, destinationCity);

            if (!date.ToString().Equals("1/1/0001 12:00:00 AM") && !days.Equals("1,2,3,4,5,6,0"))
            {
                foundedRoutes = RemoveRoutesFromWrongDays(foundedRoutes, ((int)dayOfTheWeek).ToString());
            }
            else
            {
                if (!days.Equals("1,2,3,4,5,6,0"))
                    foundedRoutes = RemoveRoutesFromWrongDays(foundedRoutes, days);
            }
            return foundedRoutes;
        }


        private List<Route> GetRoutesForGoodDirection(List<Route> routes, string startCity, string destinationCity)
        {
            var result = new List<Route>();
            foreach (var route in routes)
            {
                var start = route.BusStops.First(b => b.Address
                    .ToLowerInvariant()
                    .Contains(startCity.ToLowerInvariant()));
                var dest = route.BusStops.First(b => b.Address
                    .ToLowerInvariant()
                    .Contains(destinationCity.ToLowerInvariant()));

                if (start.Id < dest.Id)
                    result.Add(route);
            }

            return result;
        }

        private List<Route> RemoveRoutesFromWrongDays(List<Route> routes, string days)
        {
            var result = new List<Route>(routes);
            var daysArray = days.Split(',');

            foreach (var route in routes)
            {
                if (route.DayOfTheWeek.Split(',').Intersect(daysArray).ToArray().Length == 0)
                    result.Remove(route);
            }

            return result;
        }
    }
}
