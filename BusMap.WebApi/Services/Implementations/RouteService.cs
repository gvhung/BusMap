using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using AutoMapper;
using BusMap.WebApi.DatabaseModels;
using BusMap.WebApi.Dto.Routes;
using BusMap.WebApi.Helpers;
using BusMap.WebApi.Repositories.Abstract;
using BusMap.WebApi.Services.Abstract;

namespace BusMap.WebApi.Services.Implementations
{
    public class RouteService : IRouteService
    {
        private readonly IRouteRepository _repository;
        private readonly IMapper _mapper;

        public RouteService(IRouteRepository repository, IMapper mapper)
        {
            _repository = repository;
            _mapper = mapper;
        }


        public async Task<RoutesRouteDto> GetRouteAsync(int id)
        {
            var route = await _repository.GetRouteAsync(id);
            return _mapper.Map<Route, RoutesRouteDto>(route);
        }

        public async Task<RoutesRouteDto> GetRouteIncludeBusStopsAsync(int id)
        {
            var route = await _repository.GetRouteIncludeBusStopsAsync(id);
            return _mapper.Map<Route, RoutesRouteDto>(route);
        }

        public async Task<RoutesRouteDto> GetRouteIncludeCarrierAsync(int id)
        {
            var route = await _repository.GetRouteIncludeCarrierAsync(id);
            return _mapper.Map<Route, RoutesRouteDto>(route);
        }

        public async Task<RoutesRouteDto> GetRouteIncludeBusStopsCarrierAsync(int id)
        {
            var route = await _repository.GetRouteIncludeBusStopsCarrierAsync(id);
            return _mapper.Map<Route, RoutesRouteDto>(route);
        }

        public async Task<RoutesRouteDto> GetRouteIncludeAllAsync(int id)
        {
            var route = await _repository.GetRouteIncludeAllAsync(id);
            var punctuality = PunctualityConverter.BusStopsTracesPunctualityPercentage(route.BusStops);
            var routeDto = new RoutesRouteDto
            {
                Punctuality = punctuality
            };
            return _mapper.Map<Route, RoutesRouteDto>(route, routeDto);
        }

        public async Task<IEnumerable<RoutesRouteDto>> GetAllRoutesAsync()
        {
            var routes = await _repository.GetAllRoutesAsync();
            return _mapper.Map<IEnumerable<Route>, IEnumerable<RoutesRouteDto>>(routes);
        }

        public async Task<IEnumerable<RoutesRouteDto>> GetAllRoutesIncludeBusStopsAsync()
        {
            var routes = await _repository.GetAllRoutesIncludeBusStopsAsync();
            return _mapper.Map<IEnumerable<Route>, IEnumerable<RoutesRouteDto>>(routes);
        }

        public async Task<IEnumerable<RoutesRouteDto>> GetAllRoutesIncludeCarrierAsync()
        {
            var routes = await _repository.GetAllRoutesIncludeCarrierAsync();
            return _mapper.Map<IEnumerable<Route>, IEnumerable<RoutesRouteDto>>(routes);
        }

        public async Task<IEnumerable<RoutesRouteDto>> GetAllRoutesIncludeBusStopsCarrierAsync()
        {
            var routes = await _repository.GetAllRoutesIncludeBusStopsCarrierAsync();
            return _mapper.Map<IEnumerable<Route>, IEnumerable<RoutesRouteDto>>(routes);
        }

        public async Task<IEnumerable<RoutesRouteDto>> GetAllRoutesIncludeAllAsync()
        {
            var routes = await _repository.GetAllRoutesIncludeAllAsync();
            return _mapper.Map<IEnumerable<Route>, IEnumerable<RoutesRouteDto>>(routes);
        }

        public async Task AddRouteAsync(Route route)
            => await _repository.AddRouteAsync(route);

        public async Task AddRouteRangeAsync(IEnumerable<Route> routes)
            => await _repository.AddRouteRangeAsync(routes);

        public async Task RemoveRouteAsync(RoutesRouteDto route)
        {
            var routeToRemove = await _repository.GetRouteAsync(route.Id);
            await _repository.RemoveRouteAsync(routeToRemove);
        }
    }
}
