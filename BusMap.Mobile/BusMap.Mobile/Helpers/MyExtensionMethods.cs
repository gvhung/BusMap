using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Text;
using BusMap.Mobile.Models;

namespace BusMap.Mobile.Helpers
{
    public static class MyExtensionMethods
    {
        public static ObservableCollection<Xamarin.Forms.Maps.Pin> ConvertToMapPins(this IEnumerable<BusStop> list)
        {
            var result = new ObservableCollection<Xamarin.Forms.Maps.Pin>();
            foreach (var item in list)
            {
                result.Add(item.ConvertToFormsMapsPin());
            }

            return result;
        }

        public static ObservableCollection<T> ToObservableCollection<T>(this IEnumerable<T> collection)
        {
            var observableCollection = new ObservableCollection<T>();

            foreach (var item in collection)
            {
                observableCollection.Add(item);
            }

            return observableCollection;
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


    }
}
