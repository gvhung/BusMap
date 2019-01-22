using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using BusMap.WebApi.Data;
using BusMap.WebApi.DatabaseModels;
using BusMap.WebApi.Repositories.Abstract;

namespace BusMap.WebApi.Repositories.Implementations
{
    public class TraceRepository : ITraceRepository
    {
        private readonly DatabaseContext _context;

        public TraceRepository(DatabaseContext context)
        {
            _context = context;
        }

        public async Task AddBusStopTraceAsync(BusStopTrace busStopTrace)
        {
            await _context.BusStopTraces.AddAsync(busStopTrace);
            await _context.SaveChangesAsync();
        }
    }
}
