using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
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
        private const string Uri = "http://192.168.0.110:5003/api/pins";
        public async Task<ObservableCollection<Pin>> GetPins()
        {
            HttpClient httpClient = new HttpClient();

            var json = await httpClient.GetStringAsync(Uri);
            var busStops = JsonConvert.DeserializeObject<List<BusStop>>(json);

            ObservableCollection<Pin> result = busStops.ConvertToMapPins();

            return result;
        }

        public async Task PostPins(BusStop busStop)
        {
            var httpClient = new HttpClient();
            var json = JsonConvert.SerializeObject(busStop);
            StringContent content = new StringContent(json);
            content.Headers.ContentType = new MediaTypeHeaderValue("application/json");

            var result = await httpClient.PostAsync(Uri, content);
            if (result.IsSuccessStatusCode)
                MessagingHelper.Toast("Posted successfully", ToastTime.LongTime);
        }

        public Task<ObservableCollection<Pin>> GetPinsForRoute(int routeId)
        {
            throw new NotImplementedException();
        }

        public Task<List<Route>> GetRoutes()
        {
            throw new NotImplementedException();
        }

        public Task<Route> GetRoute(int routeId)
        {
            throw new NotImplementedException();
        }

        public Task<List<Route>> FindRoutes(string startCity, string destinationCity)
        {
            throw new NotImplementedException();
        }

        public Task<List<Route>> FindRoute(Expression<Func<Route, bool>> predicate)
        {
            throw new NotImplementedException();
        }
    }
}
