using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Linq.Expressions;
using System.Net;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Text;
using System.Threading.Tasks;
using BusMap.Mobile.Helpers;
using BusMap.Mobile.Models;
using Newtonsoft.Json;
using Xamarin.Forms.Maps;
using Position = Plugin.Geolocator.Abstractions.Position;

namespace BusMap.Mobile.Services
{
    public class ApiDataService : IDataService
    {
        private const string Uri = "http://192.168.0.108:5003/api/";


        public async Task<List<BusStop>> GetBusStopsAsync()
        {
            var httpClient = new HttpClient();

            var json = await httpClient.GetStringAsync(Uri + "/busStops/routeCarrier");
            var busStops = JsonConvert.DeserializeObject<List<BusStop>>(json);

            return busStops;
        }

        public async Task<int> GetBusStopLastIdAsync()
        {
            var httpClient = new HttpClient();

            var json = await httpClient.GetStringAsync(Uri + "/busStops/lastId");
            int lastId = JsonConvert.DeserializeObject<int>(json);

            return lastId;
        }

        public async Task PostBusStop(BusStop busStop)
        {
            throw new NotImplementedException();
        }

        public async Task<List<BusStop>> GetBusStopsForRoute(int routeId)
        {
            throw new NotImplementedException();
        }

        public async Task<List<Route>> GetRoutes()
        {
            throw new NotImplementedException();
        }

        public async Task<Route> GetRouteAsync(int routeId)
        {
            var httpClient = new HttpClient();
            var json = await httpClient.GetStringAsync($"{Uri}routes/{routeId}/all");
            var route = JsonConvert.DeserializeObject<Route>(json);

            return route;
        }

        public async Task<List<Route>> FindRoutesAsync(string startCity, string destinationCity)
        {
            var result = new List<Route>();

            var httpClient = new HttpClient();

            var json = await httpClient.GetStringAsync(Uri + "/routes/all");
            var routes = JsonConvert.DeserializeObject<List<Route>>(json);

            var routesToTest = routes
                .Where(r => r.BusStops.Any(b => b.Address
                    .ToLowerInvariant()
                    .Contains(startCity.ToLowerInvariant())))
                .Where(r => r.BusStops.Any(b => b.Address
                    .ToLowerInvariant()
                    .Contains(destinationCity.ToLowerInvariant())))
                .ToList();

            foreach (var route in routesToTest)
            {
                var start = route.BusStops.First(b => b.Address
                    .ToLowerInvariant()
                    .Contains(startCity.ToLowerInvariant()));
                var dest = route.BusStops.First(b => b.Address
                    .ToLowerInvariant()
                    .Contains(destinationCity.ToLowerInvariant()));

                if (start.Id < dest.Id)
                    result.Add(route);
            }

            return result;
        }

        public async Task<bool> PostRouteAsync(Route route)
        {
            var httpClient = new HttpClient();
            var json = JsonConvert.SerializeObject(route);
            var stringContent = new StringContent(json);

            var result = await httpClient.PostAsync(Uri + "/routes", AddMediaTypeHeaderValueToJson(json));
            return result.IsSuccessStatusCode;
        }

        public async Task<bool> CheckIfCarrierExistAsync(string name)
        {
            var httpClient = new HttpClient();
            var json = await httpClient.GetStringAsync($"{Uri}carriers/carrierExist?name={name}");
            var result = JsonConvert.DeserializeObject<bool>(json);

            return result;
        }

        public async Task<Carrier> PostCarrierAsync(Carrier carrier)
        {
            var httpClient = new HttpClient();
            var json = JsonConvert.SerializeObject(carrier);
            var stringContent = new StringContent(json);

            var result = await httpClient.PostAsync(Uri + "carriers", AddMediaTypeHeaderValueToJson(json));
            var resultObject = await result.Content.ReadAsStringAsync();
            var resultResponse = JsonConvert.DeserializeObject<Carrier>(resultObject);

            return resultResponse;
        }

        public async Task<IEnumerable<RouteQueued>> GetQueuedRoutesAsync()
        {
            var httpClient = new HttpClient();
            var json = await httpClient.GetStringAsync(Uri + "queues/routes/");
            var queuedRoutes = JsonConvert.DeserializeObject<IEnumerable<RouteQueued>>(json);
            return queuedRoutes;
        }

        public async Task<IEnumerable<RouteQueued>> GetQueuedRoutesInRange(Position currentPosition, int range)
        {
            var httpClient = new HttpClient();
            var currentPositionString = $"{currentPosition.Latitude},{currentPosition.Longitude}";
            var json = await httpClient.GetStringAsync($"{Uri}queues/routes/range?yourLocation={currentPositionString}&range={range}");
            var queuedRoutesInRange = JsonConvert.DeserializeObject<IEnumerable<RouteQueued>>(json);
            return queuedRoutesInRange;
        }

        public async Task<int> GetNumberOfQueuedRoutesAsync()
        {
            var httpClient = new HttpClient();
            var json = await httpClient.GetStringAsync(Uri + "queues/routes/count");
            var nOfQueuedRoutes = JsonConvert.DeserializeObject<int>(json);
            return nOfQueuedRoutes;
        }

        public async Task<int> GetNumberOfQueuedRoutesInRangeAsync(Position currentPosition, int range)
        {
            var httpClient = new HttpClient();
            var currentPositionString = $"{currentPosition.Latitude},{currentPosition.Longitude}";
            var json = await httpClient.GetStringAsync($"{Uri}queues/routes/range/count?yourLocation={currentPositionString}&range={range}");
            var nOfQueuedRoutesInRange = JsonConvert.DeserializeObject<int>(json);
            return nOfQueuedRoutesInRange;
        }

        public async Task<bool> PostRouteQueuedAsync(RouteQueued routeQueued)
        {
            var httpClient = new HttpClient();
            var json = JsonConvert.SerializeObject(routeQueued);

            var result = await httpClient.PostAsync(Uri + "/queues/routes", AddMediaTypeHeaderValueToJson(json));
            return result.IsSuccessStatusCode;
        }

        public async Task<CarrierQueued> PostCarrierQueuedAsync(CarrierQueued carrierQueued)
        {
            var httpClient = new HttpClient();
            var json = JsonConvert.SerializeObject(carrierQueued);

            var result = await httpClient.PostAsync(Uri + "/queues/carriers", AddMediaTypeHeaderValueToJson(json));
            var resultJson = await result.Content.ReadAsStringAsync();
            var resultObject = JsonConvert.DeserializeObject<CarrierQueued>(resultJson);

            return resultObject;
        }

        public async Task<bool> UpdateQueuedRoute(int id, RouteQueued updatedRouteQueued)
        {
            var httpClient = new HttpClient();
            var json = JsonConvert.SerializeObject(updatedRouteQueued);

            var result = await httpClient.PutAsync($"{Uri}queues/routes/{id}", AddMediaTypeHeaderValueToJson(json));
            return result.IsSuccessStatusCode;
        }

        public async Task<bool> PostBusStopTraceAsync(BusStopTrace busStopTrace)
        {
            var httpClient = new HttpClient();
            var json = JsonConvert.SerializeObject(busStopTrace);

            var result = await httpClient.PostAsync(Uri + "traces/busStop", AddMediaTypeHeaderValueToJson(json));
            return result.StatusCode.Equals(HttpStatusCode.Created);
        }

        public async Task<bool> PostRouteReportAsync(RouteReport routeReport)
        {
            var httpClient = new HttpClient();
            var json = JsonConvert.SerializeObject(routeReport);

            var result = await httpClient.PostAsync(Uri + "reports/routes", AddMediaTypeHeaderValueToJson(json));
            return result.StatusCode.Equals(HttpStatusCode.Created);
        }


        public StringContent AddMediaTypeHeaderValueToJson(string json)
        {
            var stringContent = new StringContent(json);
            stringContent.Headers.ContentType = new MediaTypeHeaderValue("application/json");
            return stringContent;
        }
    }
}
