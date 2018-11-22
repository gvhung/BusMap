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
    }
}
