using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using AutoMapper;
using BusMap.WebApi.DatabaseModels;
using BusMap.WebApi.Dto.Reports;
using BusMap.WebApi.Repositories.Abstract;
using BusMap.WebApi.Services.Abstract;

namespace BusMap.WebApi.Services.Implementations
{
    public class ReportService : IReportService
    {
        private readonly IReportRepository _repository;
        private readonly IMapper _mapper;

        public ReportService(IReportRepository repository, IMapper mapper)
        {
            _repository = repository;
            _mapper = mapper;
        }

        public async Task<IEnumerable<ReportsRouteReportDto>> GetRouteReportsForRouteAsync(int routeId)
        {
            var reports = await _repository.GetRouteReportsForRouteAsync(routeId);
            return _mapper.Map<IEnumerable<RouteReport>, IEnumerable<ReportsRouteReportDto>>(reports);
        }

        public async Task AddRouteReportAsync(RouteReport routeReport)
            => await _repository.AddRouteReportAsync(routeReport);
    }
}
