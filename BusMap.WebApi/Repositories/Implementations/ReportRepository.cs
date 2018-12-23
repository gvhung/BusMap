using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Threading.Tasks;
using BusMap.WebApi.Data;
using BusMap.WebApi.DatabaseModels;
using BusMap.WebApi.Repositories.Abstract;
using Microsoft.EntityFrameworkCore;

namespace BusMap.WebApi.Repositories.Implementations
{
    public class ReportRepository : IReportRepository
    {
        private readonly DatabaseContext _context;

        public ReportRepository(DatabaseContext context)
        {
            _context = context;
        }


        public async Task<IEnumerable<RouteReport>> GetRouteReportsForRouteAsync(int routeId)
            => await _context.RouteReports
                .Where(r => r.RouteId == routeId)
                .ToListAsync();

        public async Task AddRouteReportAsync(RouteReport routeReport)
        {
            await _context.AddAsync(routeReport);
            await _context.SaveChangesAsync();
        }


        public async Task<IEnumerable<RouteDelay>> GetLatestRouteDelaysAsync(int routeId)
        {
            var route = await _context.Routes
                .Include(r => r.BusStops)
                .FirstOrDefaultAsync(r => r.Id == routeId);

            //var currentDate = = new DateTime(2018, 12, 23, 13, 0, 0);
            var currentDateTime = DateTime.Now;
            try
            {
                var routeFin = _context.BusStopTraces
                    .Include(t => t.BusStop)
                    .Where(t => t.BusStop.RouteId == routeId)
                    .Last(t => t.Date == currentDateTime.Date)
                    .BusStop.Equals(route.BusStops.Last());

                if (routeFin)
                    return new List<RouteDelay>();
            }
            catch (InvalidOperationException)   //When any busStop today haven't trace
            {
                return new List<RouteDelay>();
            }

            var delays = await _context.RouteDelays
                .Where(d => d.RouteId == routeId)
                .Where(d => d.DateTime.Date == currentDateTime.Date)
                .OrderByDescending(d => d.DateTime)
                .ToListAsync();
            return delays;
        }

        public async Task AddRouteDelayAsync(RouteDelay routeDelay)
        {
            await _context.RouteDelays.AddAsync(routeDelay);
            await _context.SaveChangesAsync();
        }
    }
}
