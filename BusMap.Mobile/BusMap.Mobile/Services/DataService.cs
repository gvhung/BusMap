using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Text;
using System.Threading.Tasks;
using Xamarin.Forms.Maps;

namespace BusMap.Mobile.Services
{
    public class DataService

    {
        public async Task<ObservableCollection<Pin>> GetPins()
        {
            var list =  new ObservableCollection<Pin>
            {

                new Pin
                {
                    Position = new Position(50.030573, 22.012914),
                    Address = "Rzeszów",
                    Label = "URz",
                    Type = PinType.SearchResult
                },
                new Pin
                {
                    Position = new Position(50.027354, 22.015306),
                    Address = "Rzeszów",
                    Label = "Millenium Hall",
                    Type = PinType.SearchResult
                },
                new Pin
                {
                    Position = new Position(50.019642, 22.019147),
                    Address = "Rzeszów",
                    Label = "Plaza",
                    Type = PinType.SearchResult
                },
                new Pin
                {
                    Position = new Position(50.016664, 22.005006),
                    Address = "Rzeszów",
                    Label = "Klub pod Palmą",
                    Type = PinType.SearchResult
                }

            };
            await Task.Delay(2000);
            return list;
        }
    }
}
