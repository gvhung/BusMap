using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using BusMap.WebApi.Data;
using BusMap.WebApi.DatabaseModels;
using BusMap.WebApi.Helpers;
using BusMap.WebApi.Models;
using BusMap.WebApi.Repositories.Abstract;
using Microsoft.EntityFrameworkCore;

namespace BusMap.WebApi.Repositories.Implementations
{
    public class QueueRepository : IQueueRepository
    {
        private readonly DatabaseContext _context;

        public QueueRepository(DatabaseContext context)
        {
            _context = context;
        }

        public async Task<IEnumerable<RouteQueued>> GetRoutesQueueAsync()
            => await _context.RoutesQueued
                .Include(r => r.CarrierQueued)
                .Include(r => r.BusStopsQueued)
                .ToListAsync();

        public async Task<int> GetNumberOfQueuedRoutesAsync()
            => await _context.RoutesQueued.CountAsync();

        public async Task<CarrierQueued> GetCarrierQueued(int id)
            => await _context.CarriersQueued
                .FirstOrDefaultAsync(c => c.Id == id);

        public async Task AddRouteToQueueAsync(RouteQueued routeQueued)
        {
            await _context.RoutesQueued.AddAsync(routeQueued);
            await _context.SaveChangesAsync();
        }

        public async Task AddCarrierToQueueAsync(CarrierQueued carrierQueued)
        {
            await _context.CarriersQueued.AddAsync(carrierQueued);
            await _context.SaveChangesAsync();
        }

        public async Task UpdateRouteAsync(int id, RouteQueued routeQueued)
        {
            var routeFromDb = await _context
                .RoutesQueued
                .Include(r => r.CarrierQueued)
                .SingleOrDefaultAsync(x => x.Id == id);

            routeQueued.Id = routeFromDb.Id;

            if (routeFromDb.VotingStartedDatetime == null)
            {

                routeQueued.VotingStartedDatetime = DateTime.Now.Date;
                routeQueued.VotingEndedDateTime = routeQueued.VotingStartedDatetime.Value.AddDays(14);
            }

            _context.Entry(routeFromDb).CurrentValues.SetValues(routeQueued);
            _context.Entry(routeFromDb).State = EntityState.Modified;

            if (routeQueued.CarrierQueued != null)
            {
                _context.Entry(routeFromDb.CarrierQueued).CurrentValues.SetValues(routeQueued.CarrierQueued);
                _context.Entry(routeFromDb.CarrierQueued).State = EntityState.Modified;
            }
            


            await _context.SaveChangesAsync();
        }


        public async Task<IEnumerable<RouteQueued>> GetRoutesInRangeAsync(string yourLocation, int range)
        {
            var result = new List<RouteQueued>();
            var routes = await GetRoutesQueueAsync();
            var geoPosition = new GeoPosition(yourLocation);

            foreach (var route in routes)
            {
                if (route.BusStopsQueued.AnyInRange(geoPosition, range))
                {
                    result.Add(route);
                }
            }

            return result;
        }

        
    }
}
