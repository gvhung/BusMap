﻿using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using BusMap.WebApi.DatabaseModels;
using BusMap.WebApi.Dto.Routes;

namespace BusMap.WebApi.Services.Abstract
{
    public interface IRouteService 
    {
        Task<RoutesRouteDto> GetRouteAsync(int id);
        Task<RoutesRouteDto> GetRouteIncludeBusStopsAsync(int id);
        Task<RoutesRouteDto> GetRouteIncludeCarrierAsync(int id);
        Task<RoutesRouteDto> GetRouteIncludeBusStopsCarrierAsync(int id);
        Task<RoutesRouteDto> GetRouteIncludeAllAsync(int id);

        Task<IEnumerable<RoutesRouteDto>> GetAllRoutesAsync();
        Task<IEnumerable<RoutesRouteDto>> GetAllRoutesIncludeBusStopsAsync();
        Task<IEnumerable<RoutesRouteDto>> GetAllRoutesIncludeCarrierAsync();
        Task<IEnumerable<RoutesRouteDto>> GetAllRoutesIncludeBusStopsCarrierAsync();
        Task<IEnumerable<RoutesRouteDto>> GetAllRoutesIncludeAllAsync();
        Task<IEnumerable<RoutesRouteDto>> GetAllFavoriteRoutesAsync(IEnumerable<int> routesIds);

        Task AddRouteAsync(Route route);
        Task AddRouteRangeAsync(IEnumerable<Route> routes);
        Task RemoveRouteAsync(RoutesRouteDto route);

        Task<int> GetRouteCurrentLatencyAsync(int routeId);
        Task<RoutesBusStopDto> GetRouteRecentBusStopAsync(int routeId);

        Task<IEnumerable<RoutesRouteDto>> FindRoutesAsync(string startCity, string destinationCity, string days = null,
            TimeSpan hourFrom = default(TimeSpan), TimeSpan hourTo = default(TimeSpan),
            DateTime date = default(DateTime));
    }
}
