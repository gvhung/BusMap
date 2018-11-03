using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;
using BusMap.Mobile.Models;
using Xamarin.Forms.Maps;
using Position = Plugin.Geolocator.Abstractions.Position;

namespace BusMap.Mobile.Services
{
    public interface IDataService
    {
        //Task<ObservableCollection<Pin>> GetPins();
        //Task PostPins(BusStop busStop);

        //Task<ObservableCollection<Pin>> GetPinsForRoute(int routeId);
        //Task<List<Route>> GetRoutes();
        //Task<Route> GetRoute(int routeId);
        //Task<List<Route>> FindRoute(Expression<Func<Route, bool>> predicate);
        //Task<List<Route>> FindRoutes(string startCity, string destinationCity);
        //Task<int> GetBusStopLastIdAsync();
        //Task PostBusStop(BusStop busStop);
        //Task<List<BusStop>> GetBusStopsForRoute(int routeId);
        //Task<Route> GetRoute(int routeId);


        Task<List<BusStop>> GetBusStopsAsync();
        Task<List<Route>> GetRoutes();
        Task<List<Route>> FindRoutesAsync(string startCity, string destinationCity);
        Task<bool> PostRouteAsync(Route route);
        Task<bool> CheckIfCarrierExistAsync(string name);
        Task<Carrier> PostCarrierAsync(Carrier carrier);

        Task<IEnumerable<RouteQueued>> GetQueuedRoutesAsync();    //TODO: download range using current localization
        Task<IEnumerable<RouteQueued>> GetQueuedRoutesInRange(Position currentPosition, int range);
        Task<int> GetNumberOfQueuedRoutesAsync();    //TODO: download range using current localization
        Task<int> GetNumberOfQueuedRoutesInRangeAsync(Position currentPosition, int range);
        Task<bool> PostRouteQueuedAsync(RouteQueued routeQueued);
        Task<CarrierQueued> PostCarrierQueuedAsync(CarrierQueued carrierQueued);
        Task<bool> UpdateQueuedRoute(int id, RouteQueued updatedRouteQueued);
        
    }
}
