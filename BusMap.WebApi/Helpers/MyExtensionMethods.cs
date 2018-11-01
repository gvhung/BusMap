using BusMap.WebApi.DatabaseModels;
using BusMap.WebApi.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace BusMap.WebApi.Helpers
{
    public static class MyExtensionMethods
    {

        public static bool AnyInRange(this IEnumerable<BusStopQueued> busStopsQueued, GeoPosition geoPosition,
            int range)
        {
            if (busStopsQueued.Any(b => b.IsInRange(geoPosition, range)))
            {
                return true;
            }

            return false;
        }

        public static bool IsInRange(this BusStopQueued busStopQueued, GeoPosition geoPosition, int range)
        {
            var busStopGeoPosition = new GeoPosition(busStopQueued.Latitude, busStopQueued.Longitude);
            if (CalcDistance(geoPosition, busStopGeoPosition) < range)
            {
                return true;
            }

            return false;
        }


        private static double CalcDistance(GeoPosition point1, GeoPosition point2)
        {
            var r = 6.371;   //earth radius
            var deltaLat = ConvertToRadians(point2.Latitude) - ConvertToRadians(point1.Latitude);
            var deltaLon = ConvertToRadians(point2.Longitude) - ConvertToRadians(point1.Longitude);

            var a = Math.Sin(deltaLat / 2) * Math.Sin(deltaLat / 2)
                    + Math.Cos(ConvertToRadians(point1.Latitude))
                    * Math.Cos(ConvertToRadians(point2.Latitude))
                    * Math.Sin(deltaLon / 2) * Math.Sin(deltaLon / 2);

            var c = 2 * Math.Atan2(Math.Sqrt(a), Math.Sqrt(1 - a));

            var result = r * c * 1000;
            return Math.Round(result, 2);
        }

        private static double ConvertToRadians(double angle)
            => (Math.PI / 180) * angle;
    }
}
