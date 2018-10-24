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
    }
}
