using System;
using System.Collections;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;
using BusMap.Mobile.Models;
using BusMap.Mobile.SQLite.Models;
using Xamarin.Forms.Maps;
using Position = Plugin.Geolocator.Abstractions.Position;

namespace BusMap.Mobile.Services
{
    public interface IDataService
    {
        event EventHandler<string> HttpClientFindEvent;

        Task<List<BusStop>> GetBusStopsAsync();
        Task<Route> GetRouteAsync(int routeId);
        Task<List<Carrier>> GetAllCarriersAsync();

        Task<List<RouteQueued>> GetQueuedRoutesInRange(Position currentPosition, int range);
        Task<int> GetNumberOfQueuedRoutesInRangeAsync(Position currentPosition, int range);
        Task<bool> PostRouteQueuedAsync(RouteQueued routeQueued);
        Task<bool> UpdateQueuedRoute(int id, RouteQueued updatedRouteQueued);

        Task<bool> PostBusStopTraceAsync(BusStopTrace busStopTrace);
        Task<bool> PostRouteReportAsync(RouteReport routeReport);
        Task<IEnumerable<Route>> GetFavoriteRoutes(IEnumerable<int> ids);
        Task<int> GetRouteCurrentLatency(int routeId);
        Task<BusStop> GetRouteRecentBusStop(int routeId);

        Task<List<Route>> FindRoutesAsync(string startCity, string destinationCity);
        Task<List<Route>> FindRoutesAsync(string startCity, string destinationCity, string days,
            TimeSpan hourFrom, TimeSpan hourTo, DateTime date);

        Task<T> GetObjectFromQueryStringAsync<T>(string query);

        Task<List<RouteDelay>> GetRouteDelays(int routeId);
        Task<bool> PostRouteDelay(RouteDelay routeDelay);
        Task<ReversedGeocode> GetReversedGeocodeForLatLngAsync(Position position);
    }
}
