using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Linq.Expressions;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Text;
using System.Threading.Tasks;
using BusMap.Mobile.Helpers;
using BusMap.Mobile.Models;
using Newtonsoft.Json;
using Xamarin.Forms.Maps;

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

        public async Task<Route> GetRoute(int routeId)
        {
            throw new NotImplementedException();
        }

        public async Task<List<Route>> FindRoutesAsync(string startCity, string destinationCity)
        {
            var result = new List<Route>();

            var httpClient = new HttpClient();

            var json = await httpClient.GetStringAsync(Uri + "/routes/busStopsCarrier");
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
            stringContent.Headers.ContentType = new MediaTypeHeaderValue("application/json");

            var result = await httpClient.PostAsync(Uri + "/routes", stringContent);
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
            stringContent.Headers.ContentType = new MediaTypeHeaderValue("application/json");

            var result = await httpClient.PostAsync(Uri + "carriers", stringContent);
            var resultObject = await result.Content.ReadAsStringAsync();
            var resultResponse = JsonConvert.DeserializeObject<Carrier>(resultObject);

            return resultResponse;
        }
    }
}
