using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Text;
using BusMap.Mobile.Models;
using Xamarin.Forms.GoogleMaps;

namespace BusMap.Mobile.Helpers
{
    public static class GoogleMapsExtensionMethods
    {
        public static ObservableCollection<Pin> ToGoogleMapsPins(
            this IEnumerable<BusStop> busStops)
        {
            var result = new ObservableCollection<Pin>();
            foreach (var busStop in busStops)
            {
                if (string.IsNullOrEmpty(busStop.Label))
                    busStop.Label = " ";
                result.Add(new Pin
                {
                    Label = busStop.Label,
                    Address = busStop.Address,
                    Position = new Position(busStop.Latitude, busStop.Longitude),
                });
            }

            return result;
        }

        public static Position ToGoogleMapsPosition(
            this Plugin.Geolocator.Abstractions.Position geolocatorPosition)
            => new Position(geolocatorPosition.Latitude, geolocatorPosition.Longitude);

        public static MapSpan ToMapSpan(this Plugin.Geolocator.Abstractions.Position position, Distance radius)
            => MapSpan.FromCenterAndRadius(position.ToGoogleMapsPosition(), radius);

        public static Pin ToGoogleMapsPin(this BusStop busStop)
            => new Pin
            {
                Label = busStop.Label,
                Address = busStop.Address,
                Position = new Position(busStop.Latitude, busStop.Longitude)
            };

    }
}
