using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Collections.Specialized;
using System.ComponentModel;
using System.Text;
using BusMap.Mobile.Models;


namespace BusMap.Mobile.Helpers
{
    public static class MyExtensionMethods
    {
        public static ObservableCollection<Xamarin.Forms.Maps.Pin> ToMapPins(this IEnumerable<BusStop> list)
        {
            var result = new ObservableCollection<Xamarin.Forms.Maps.Pin>();
            foreach (var item in list)
            {
                result.Add(item.ConvertToFormsMapsPin());
            }

            return result;
        }

        public static Xamarin.Forms.Maps.Position ToMapsPosition(this Plugin.Geolocator.Abstractions.Position geolocatorPosition)
            => new Xamarin.Forms.Maps.Position(geolocatorPosition.Latitude, geolocatorPosition.Longitude);


        public static ObservableCollection<T> ToObservableCollection<T>(this IEnumerable<T> collection)
        {
            var observableCollection = new ObservableCollection<T>();

            foreach (var item in collection)
            {
                observableCollection.Add(item);
            }

            return observableCollection;
        }

        public static void AddRange<T>(this ObservableCollection<T> observableCollection, IEnumerable<T> collection)
        {
            foreach (var item in collection)
            {
                observableCollection.Add(item);
            }
        }


        public static string BusStopsToString(this IEnumerable<BusStop> list)
        {
            var stringBuilder = new StringBuilder();
            foreach (var item in list)
            {
                stringBuilder.Append(item.Label);
            }

            return stringBuilder.ToString();
        }

        public static double CalculateDistance(this Plugin.Geolocator.Abstractions.Position firstPosition,
            Plugin.Geolocator.Abstractions.Position secondPosition)
        {
            var r = 6.371;   //earth radius
            var deltaLat = ConvertToRadians(secondPosition.Latitude) - ConvertToRadians(firstPosition.Latitude);
            var deltaLon = ConvertToRadians(secondPosition.Longitude) - ConvertToRadians(firstPosition.Longitude);

            var a = Math.Sin(deltaLat / 2) * Math.Sin(deltaLat / 2)
                    + Math.Cos(ConvertToRadians(firstPosition.Latitude))
                    * Math.Cos(ConvertToRadians(secondPosition.Latitude))
                    * Math.Sin(deltaLon / 2) * Math.Sin(deltaLon / 2);

            var c = 2 * Math.Atan2(Math.Sqrt(a), Math.Sqrt(1 - a));

            var result = r * c * 1000;
            return Math.Round(result, 2);
        }

        private static double ConvertToRadians(double angle)
            => (Math.PI / 180) * angle;


    }
}
