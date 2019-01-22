using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using BusMap.WebApi.DatabaseModels;
using BusMap.WebApi.Dto.Reports;

namespace BusMap.WebApi.Services.Abstract
{
    public interface IReportService
    {
        Task<IEnumerable<ReportsRouteReportDto>> GetRouteReportsForRouteAsync(int routeId);
        Task AddRouteReportAsync(RouteReport routeReport);

        Task<IEnumerable<ReportsRouteDelayDto>> GetLatestRouteDelaysAsync(int routeId);
        Task AddRouteDelayAsync(RouteDelay routeDelay);
    }
}
