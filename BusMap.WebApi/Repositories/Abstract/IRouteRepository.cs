﻿using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using BusMap.WebApi.DatabaseModels;

namespace BusMap.WebApi.Repositories.Abstract
{
    public interface IRouteRepository
    {
        Task<Route> GetRouteAsync(int id);
        Task<Route> GetRouteIncludeBusStopsAsync(int id);
        Task<Route> GetRouteIncludeCarrierAsync(int id);
        Task<Route> GetRouteIncludeBusStopsCarrierAsync(int id);
        Task<Route> GetRouteIncludeAllAsync(int id);


        Task<IEnumerable<Route>> GetAllRoutesAsync();
        Task<IEnumerable<Route>> GetAllRoutesIncludeBusStopsAsync();
        Task<IEnumerable<Route>> GetAllRoutesIncludeCarrierAsync();
        Task<IEnumerable<Route>> GetAllRoutesIncludeBusStopsCarrierAsync();
        Task<IEnumerable<Route>> GetAllRoutesIncludeAllAsync();
        Task<IEnumerable<Route>> GetAllFavoriteRoutesAsync(IEnumerable<int> routesIds);

        Task AddRouteAsync(Route route);
        Task AddRouteRangeAsync(IEnumerable<Route> routes);
        Task RemoveRouteAsync(Route route);

        Task<int> GetRouteCurrentLatencyAsync(int routeId);
        Task<BusStop> GetRouteRecentBusStopAsync(int routeId);

        Task<IEnumerable<Route>> FindRoutesAsync(string startCity, string destinationCity, string days = null,
            TimeSpan hourFrom = default(TimeSpan), TimeSpan hourTo = default(TimeSpan),
            DateTime date = default(DateTime));
    }
}
