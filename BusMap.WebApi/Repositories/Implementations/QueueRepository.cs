using System;
using System.Collections.Generic;
using System.Globalization;
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

        public async Task<RouteQueued> GetRouteQueuedAsync(int routeQueuedId) 
            => await _context.RoutesQueued
                .Include(r => r.BusStopsQueued)
                .Include(r => r.CarrierQueued)
                .FirstOrDefaultAsync(r => r.Id == routeQueuedId);

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
            var orderedBusStops = routeQueued.BusStopsQueued.OrderBy(b => b.Hour).ToList();
            routeQueued.BusStopsQueued = orderedBusStops;
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

        public async Task MoveQueuedRoutesToMainTableAsync()
        {
            var queuedRoutesToReplace = await _context.RoutesQueued
                .Include(r => r.BusStopsQueued)
                .Include(r => r.CarrierQueued)
                .Where(r => r.VotingStartedDatetime != null)
                .Where(r => r.PositiveVotes > 0 || r.NegativeVotes > 0)
                .Where(r => r.VotingEndedDateTime.Value.Date <= DateAndTime.Now.Date)
                .Where(r => Convert.ToDouble(r.PositiveVotes * 100 / (r.NegativeVotes + r.PositiveVotes), CultureInfo.InvariantCulture) >= 75)
                .ToListAsync();

            MoveRouteQueuedToMainTable(queuedRoutesToReplace);
        }

        public async Task RemoveRejectedQueuedRoutesAsync()
        {
            var routesToRemove = await _context.RoutesQueued
                .Include(r => r.BusStopsQueued)
                .Include(r => r.CarrierQueued)
                .Where(r => r.VotingStartedDatetime != null)
                .Where(r => r.VotingEndedDateTime.Value.Date <= DateAndTime.Now.Date)
                .Where(r => Convert.ToDouble(r.PositiveVotes * 100 / (r.NegativeVotes + r.PositiveVotes), CultureInfo.InvariantCulture) < 75)
                .ToListAsync();

            _context.RoutesQueued.RemoveRange(routesToRemove);
            await _context.SaveChangesAsync();
        }



        private void MoveRouteQueuedToMainTable(List<RouteQueued> queuedRoutesToReplace)
        {
            foreach (var routeQueued in queuedRoutesToReplace)
            {
                Move(routeQueued);
                DeleteAfterMove(routeQueued);
            }
            
        }

        private void Move(RouteQueued routeQueued)
        {
            var busStops = routeQueued.BusStopsQueued.Select(q => new BusStop
            {
                Address = q.Address,
                Hour = q.Hour,
                Label = q.Label,
                Latitude = q.Latitude,
                Longitude = q.Longitude
            }).ToList();

            var route = new Route
            {
                Name = routeQueued.Name,
                BusStops = busStops,
                DayOfTheWeek = routeQueued.DayOfTheWeek,
            };

            if (routeQueued.CarrierQueued != null && routeQueued.CarrierId == null)
            {
                route.Carrier = new Carrier
                {
                    Name = routeQueued.CarrierQueued.Name
                };
            }
            else
            {
                route.CarrierId = (int)routeQueued.CarrierId;
            }

            _context.Routes.Add(route);
            _context.SaveChanges();
        }

        private void DeleteAfterMove(RouteQueued routeQueuedToRemove)
        {
            if (routeQueuedToRemove.CarrierQueued != null)
            {
                _context.CarriersQueued.Remove(routeQueuedToRemove.CarrierQueued);
            }
            else
            {
                _context.RoutesQueued.Remove(routeQueuedToRemove);
            }
                
            _context.SaveChanges();
        }

    }
}
