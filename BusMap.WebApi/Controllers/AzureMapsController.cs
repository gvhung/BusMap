using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Text.RegularExpressions;
using System.Threading.Tasks;
using BusMap.WebApi.Data;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json.Linq;

namespace BusMap.WebApi.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class AzureMapsController : ControllerBase
    {
        public async Task<IActionResult> GetCityNameForLatLong([FromQuery] string query)
        {
            if (!Regex.Match(query, @"^(\-?\d+(\.\d+)?),(\-?\d+(\.\d+)?)$").Success)
            {
                return BadRequest("Qury must contain two double/int values, representing coordinates"); //When query doesn't contain two double/int digits
            }

            var splittedQuery = query.Split(",");
            string latitude = splittedQuery[0],
                longitude = splittedQuery[1],
                subscriptionKey = Connections.GetAzureMapsKey(),
                result;

            if (!ModelState.IsValid)
                return BadRequest(ModelState);

            string requestUri = "https://atlas.microsoft.com/search/fuzzy/json?api-version=1" +
                                $"&subscription-key={subscriptionKey}&query={latitude},{longitude}";

            using (var client = new HttpClient())
            {
                var json = await client.GetStringAsync(requestUri);

                var jo = JObject.Parse(json);
                result = jo["results"].First["address"]["municipality"].ToString();
            }

            return Ok(result);
        }
    }
}
        
