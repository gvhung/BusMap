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

        public async Task AddRouteToQueueAsync(RouteQueued routeQueued)
        {
            await _context.RoutesQueued.AddAsync(routeQueued);
            await _context.SaveChangesAsync();
        }

        public async Task UpdateRouteAsync(int id, RouteQueued routeQueued)
        {
            var routeFromDb = await _context
                .RoutesQueued
                .Include(r => r.CarrierQueued)
                .SingleOrDefaultAsync(x => x.Id == id);

            routeQueued.Id = routeFromDb.Id;

            if (routeFromDb.CarrierQueued == null && routeQueued.CarrierQueued != null)
            {
                _context.CarriersQueued.Add(routeQueued.CarrierQueued);
                _context.Entry(routeFromDb).CurrentValues.SetValues(routeQueued);
                routeFromDb.CarrierQueuedId = routeQueued.CarrierQueued.Id;
            }
            
            if (routeFromDb.CarrierQueued != null && routeQueued.CarrierQueued != null)
            {

                var carrierFromDb = _context.CarriersQueued
                    .SingleOrDefault(c => c.Id == routeFromDb.CarrierQueued.Id);

                //_context.Entry(carrierFromDb).CurrentValues.SetValues(routeQueued.CarrierQueued);
                carrierFromDb.Name = routeQueued.CarrierQueued.Name;
                _context.Entry(carrierFromDb).State = EntityState.Modified;

                //_context.Entry(routeFromDb).CurrentValues.SetValues(routeQueued);
                //routeFromDb.CarrierQueued.Name = routeQueued.CarrierQueued.Name;
            }


            

            _context.Entry(routeFromDb).State = EntityState.Modified;
            await _context.SaveChangesAsync();
        }

    }
}
