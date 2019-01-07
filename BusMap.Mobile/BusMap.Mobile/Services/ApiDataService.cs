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
using BusMap.Mobile.SQLite.Models;
using BusMap.Mobile.SQLite.Repositories;
using Newtonsoft.Json;
using Xamarin.Forms.Maps;
using Position = Plugin.Geolocator.Abstractions.Position;

namespace BusMap.Mobile.Services
{
    public class ApiDataService : IDataService
    {
        //private const string Uri = "http://192.168.0.129:5003/api/";
        private const string Uri = "https://busmapwebapi.azurewebsites.net/api/";
        private readonly IRecentSearchRepository _recentSearchRepository;

        public ApiDataService(IRecentSearchRepository recentSearchRepository)
        {
            _recentSearchRepository = recentSearchRepository;
        }




        public event EventHandler<string> HttpClientFindEvent;

        public async Task<List<BusStop>> GetBusStopsAsync()
        {
            var httpClient = new HttpClient();

            var json = await httpClient.GetStringAsync(Uri + "/busStops/");
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

        public async Task<List<Carrier>> GetAllCarriersAsync()
        {
            var httpClient = new HttpClient();
            var json = await httpClient.GetStringAsync(Uri + "carriers");
            var result = JsonConvert.DeserializeObject<List<Carrier>>(json);
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

        public async Task<List<RouteQueued>> GetQueuedRoutesInRange(Position currentPosition, int range)
        {
            var httpClient = new HttpClient();
            var currentPositionString = $"{currentPosition.Latitude},{currentPosition.Longitude}";
            var json = "";
            try
            {
                json = await httpClient.GetStringAsync(
                    $"{Uri}queues/routes/range?yourLocation={currentPositionString}&range={range}");
            }
            catch (HttpRequestException)
            {
                return new List<RouteQueued>();
            }

            var queuedRoutesInRange = JsonConvert.DeserializeObject<List<RouteQueued>>(json);
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

        public async Task<IEnumerable<Route>> GetFavoriteRoutes(IEnumerable<int> ids)
        {
            var httpClient = new HttpClient();
            var idQuery = IdArrayToStringQuery(ids);
            var json = await httpClient.GetStringAsync($"{Uri}routes/favorites?{idQuery}");

            var routes = JsonConvert.DeserializeObject<IEnumerable<Route>>(json);
            return routes;
        }

        public async Task<int> GetRouteCurrentLatency(int routeId)
        {
            var httpClient = new HttpClient();
            var json = await httpClient.GetStringAsync($"{Uri}routes/{routeId}/currentLatency");
            var latency = JsonConvert.DeserializeObject<int>(json);
            return latency;
        }

        public async Task<BusStop> GetRouteRecentBusStop(int routeId)
        {
            var httpClient = new HttpClient();
            var json = await httpClient.GetStringAsync($"{Uri}routes/{routeId}/recentBusStop");
            var recentBusStop = JsonConvert.DeserializeObject<BusStop>(json);
            return recentBusStop;
        }

        public async Task<List<Route>> FindRoutesAsync(string startCity, string destinationCity)
        {
            var httpClient = new HttpClient();
            var apiQuery = $"{Uri}routes/find?startCity={startCity}" +
                          $"&destinationCity={destinationCity}";

            var json = await httpClient.GetStringAsync(apiQuery);
            HttpClientFindEvent?.Invoke(this, apiQuery);            
            _recentSearchRepository.AddSearch(startCity, destinationCity);

            var foundRoutes = JsonConvert.DeserializeObject<List<Route>>(json);
            return foundRoutes;
        }

        public async Task<List<Route>> FindRoutesAsync(string startCity, string destinationCity, string days,
            TimeSpan hourFrom, TimeSpan hourTo, DateTime date)
        {
            var httpClient = new HttpClient();
            var apiQuery = $"{Uri}routes/find?startCity={startCity}" +
                           $"&destinationCity={destinationCity}&days={days}" +
                           $"&hourFrom={hourFrom}&hourTo={hourTo}&date={date}";

            var json = await httpClient.GetStringAsync(apiQuery);
            HttpClientFindEvent?.Invoke(this, apiQuery);
            _recentSearchRepository.AddSearch(startCity, destinationCity);

            var foundRoutes = JsonConvert.DeserializeObject<List<Route>>(json);
            return foundRoutes;
        }

        public async Task<T> GetObjectFromQueryStringAsync<T>(string query)
        {
            var httpClient = new HttpClient();
            var json = await httpClient.GetStringAsync(query);
            var resultObject = JsonConvert.DeserializeObject<T>(json);
            return resultObject;
        }

        public async Task<List<RouteDelay>> GetRouteDelays(int routeId)
        {
            var httpClient = new HttpClient();
            var json = await httpClient.GetStringAsync($"{Uri}reports/delay/{routeId}");
            var routeDelays = JsonConvert.DeserializeObject<List<RouteDelay>>(json);
            return routeDelays;
        }

        public async Task<bool> PostRouteDelay(RouteDelay routeDelay)
        {
            var httpClient = new HttpClient();
            var json = JsonConvert.SerializeObject(routeDelay);
            var result = await httpClient.PostAsync(Uri + "reports/delay", AddMediaTypeHeaderValueToJson(json));
            return result.StatusCode.Equals(HttpStatusCode.Created);
        }


        private string IdArrayToStringQuery(IEnumerable<int> ids)
        {
            //?id=1&id=3
            var result = new StringBuilder();
            foreach (var id in ids)
            {
                result.Append($"id={id}&");
            }

            result.Length--;
            //result.Remove(result.Length - 2, 1);
            return result.ToString();
        }


        private StringContent AddMediaTypeHeaderValueToJson(string json)
        {
            var stringContent = new StringContent(json);
            stringContent.Headers.ContentType = new MediaTypeHeaderValue("application/json");
            return stringContent;
        }

    }
}
