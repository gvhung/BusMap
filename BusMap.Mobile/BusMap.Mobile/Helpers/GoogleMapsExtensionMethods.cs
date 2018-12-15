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
        private static readonly string[] _mapIcons = 
        {
            "MapIcons/mapIconBad.png",
            "MapIcons/mapIconAverage.png",
            "MapIcons/mapIconGood.png"
        };

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
                    Icon = BitmapDescriptorFactory.FromBundle(_mapIcons[1])
                });
            }

            return result;
        }

        public static ObservableCollection<Pin> ToGoogleMapsPins(
            this IEnumerable<BusStopQueued> busStops)
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
                    Icon = BitmapDescriptorFactory.FromBundle(_mapIcons[1])
                });
            }

            return result;
        }


        public static ObservableCollection<Pin> ToGoogleMapsPinsWithPunctualityColorsPins(
            this IEnumerable<BusStop> busStops)
        {
            var result = new ObservableCollection<Pin>();
            foreach (var busStop in busStops)
            {
                var punctualityPercentage = int.Parse(busStop.PunctualityPercentage.TrimEnd('%'));
                string iconName = "MapIcons/mapIconAverage.png";

                if (punctualityPercentage < 50)
                    iconName = _mapIcons[0];
                else if (punctualityPercentage < 80)
                    iconName = _mapIcons[1];
                else if (punctualityPercentage >= 80)
                    iconName = _mapIcons[2];

                if (string.IsNullOrEmpty(busStop.Label))
                    busStop.Label = " ";
                result.Add(new Pin
                {
                    Label = busStop.Label,
                    Address = busStop.Address,
                    Position = new Position(busStop.Latitude, busStop.Longitude),
                    Icon = BitmapDescriptorFactory.FromBundle(iconName)
                });
            }

            return result;
        }

        public static ObservableCollection<Pin> ToCustomIconPins(
            this IEnumerable<Pin> pins)
        {
            var result = new ObservableCollection<Pin>();
            foreach (var pin in pins)
            {
                pin.Icon = BitmapDescriptorFactory.FromBundle(_mapIcons[1]);
                result.Add(pin);
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

        public static Pin ToGoogleMapsPin(this BusStopQueued busStopQueued)
            => new Pin
            {
                Label = busStopQueued.Label,
                Address = busStopQueued.Address,
                Position = new Position(busStopQueued.Latitude, busStopQueued.Longitude),
                Icon = BitmapDescriptorFactory.FromBundle(_mapIcons[1])
            };

    }
}
