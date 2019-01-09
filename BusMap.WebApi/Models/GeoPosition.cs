using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Threading.Tasks;

namespace BusMap.WebApi.Models
{
    public class GeoPosition
    {
        public double Latitude { get; set; }
        public double Longitude { get; set; }


        public GeoPosition(double latitude, double longitude)
        {
            Latitude = latitude;
            Longitude = longitude;
        }

        public GeoPosition(string positionString)
        {
            string[] stringArray = positionString.Split(",");
            Latitude = Convert.ToDouble(stringArray[0], CultureInfo.InvariantCulture);
            Longitude = Convert.ToDouble(stringArray[1], CultureInfo.InvariantCulture);
        }

    }
}
