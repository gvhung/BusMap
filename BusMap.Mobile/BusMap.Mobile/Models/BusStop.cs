using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Prism.Mvvm;
using Xamarin.Forms.Maps;

namespace BusMap.Mobile.Models
{
    public class BusStop : BindableBase
    {
        public int Id { get; set; }
        public double Latitude { get; set; }
        public double Longitude { get; set; }
        public string Address { get; set; }
        public string Label { get; set; }
        public TimeSpan Hour { get; set; }


        public Route Route { get; set; }

        //[Obsolete("Don't use this", true)]
        //public new Position Position { get; set; }

        public Xamarin.Forms.Maps.Pin ConvertToFormsMapsPin()
        {
            if (string.IsNullOrEmpty(Label))    //Todo: Change after map renderer customization
                Label = " ";

            var result = new Xamarin.Forms.Maps.Pin()
            {
                Id = Id,
                Position = new Position(Latitude, Longitude),
                Address = Address,
                Label = Label
            };

            return result;
        }

    }
}
