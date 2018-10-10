using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;
using BusMap.Mobile.Models;
using Xamarin.Forms.Maps;

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

        Task<List<BusStop>> GetBusStops();
        Task PostBusStop(BusStop busStop);
        Task<List<BusStop>> GetBusStopsForRoute(int routeId);
        Task<List<Route>> GetRoutes();
        Task<Route> GetRoute(int routeId);
        Task<List<Route>> FindRoutes(string startCity, string destinationCity);

    }
}
