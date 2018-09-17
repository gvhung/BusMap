using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Text;
using System.Threading.Tasks;
using BusMap.Mobile.Helpers;
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
            var pins = JsonConvert.DeserializeObject<List<Models.Pin>>(json);

            ObservableCollection<Pin> result = pins.ConvertToMapPins();

            return result;
        }

        public async Task PostPins(Models.Pin pin)
        {
            var httpClient = new HttpClient();
            var json = JsonConvert.SerializeObject(pin);
            StringContent content = new StringContent(json);
            content.Headers.ContentType = new MediaTypeHeaderValue("application/json");

            var result = await httpClient.PostAsync(Uri, content);
            if (result.IsSuccessStatusCode)
                MessagingHelper.Toast("Posted successfully", ToastTime.LongTime);
        }
    }
}
