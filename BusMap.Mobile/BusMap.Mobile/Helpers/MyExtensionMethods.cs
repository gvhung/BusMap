using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Text;
using BusMap.Mobile.Models;

namespace BusMap.Mobile.Helpers
{
    public static class MyExtensionMethods
    {
        public static ObservableCollection<Xamarin.Forms.Maps.Pin> ConvertToMapPins(this IEnumerable<Pin> list)
        {
            var result = new ObservableCollection<Xamarin.Forms.Maps.Pin>();
            foreach (var item in list)
            {
                result.Add(item.ConvertToFormsMapsPin());
            }

            return result;
        }
    }
}
