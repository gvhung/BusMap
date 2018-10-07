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
    public class CarrierRepository : ICarrierRepository
    {
        private readonly DatabaseContext _context;

        public CarrierRepository(DatabaseContext context)
        {
            _context = context;
        }


        public async Task<Carrier> GetCarrierAsync(int id)
            => await _context.Carriers
                .FirstOrDefaultAsync(x => x.Id == id);

        public async Task<Carrier> GetCarrierIncludeRoutesAsync(int id)
            => await _context.Carriers
                .Include(x => x.Routes)
                .FirstOrDefaultAsync(x => x.Id == id);

        public async Task<Carrier> GetCarrierIncludeRoutesBusStopsAsync(int id)
            => await _context.Carriers
                .Include(x => x.Routes)
                .ThenInclude(x => x.BusStops)
                .FirstOrDefaultAsync(x => x.Id == id);

        public async Task<IEnumerable<Carrier>> GetAllCarriersAsync()
            => await _context.Carriers
                .ToListAsync();

        public async Task AddCarrierAsync(Carrier carrier)
        {
            await _context.AddAsync(carrier);
            await _context.SaveChangesAsync();
        }

        public async Task AddCarrierRangeAsync(IEnumerable<Carrier> carriers)
        {
            await _context.Carriers.AddRangeAsync(carriers);
            await _context.SaveChangesAsync();
        }

        public async Task RemoveCarrierAsync(Carrier carrier)
        {
            _context.Carriers.Remove(carrier);
            await _context.SaveChangesAsync();
        }
    }
}
