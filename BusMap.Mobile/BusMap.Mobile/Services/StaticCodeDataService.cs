using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using BusMap.Mobile.Helpers;
using BusMap.Mobile.Models;
using Xamarin.Forms.Maps;

namespace BusMap.Mobile.Services
{
    public class StaticCodeDataService : IDataService
    {
        private ObservableCollection<BusStop> _busStops;
        private List<Route> _routes;
        private List<Carrier> _carriers;

        public StaticCodeDataService()
        {
            _busStops = new ObservableCollection<BusStop>
            {

                new BusStop
                {
                    //Position = new Position(50.030573, 22.012914),
                    Latitude = 50.030573,
                    Longitude = 22.012914,
                    Address = "Rzeszów",
                    Label = "URz"
                },
                new BusStop
                {
                    //Position = new Position(50.027354, 22.015306),
                    Latitude = 50.027354,
                    Longitude = 22.015306,
                    Address = "Rzeszów",
                    Label = "Millenium Hall"
                },
                new BusStop
                {
                    //Position = new Position(50.019642, 22.019147),
                    Latitude = 50.019642,
                    Longitude = 22.019147,
                    Address = "Rzeszów",
                    Label = "Plaza"
                },
                new BusStop
                {
                    //Position = new Position(50.016664, 22.005006),
                    Latitude = 50.016664,
                    Longitude = 22.005006,
                    Address = "Rzeszów",
                    Label = "Klub pod Palmą"
                }

            };
            _carriers = new List<Carrier>
            {
                new Carrier()
                {
                    Name = "Nowex transport",
                    Id = 1
                    //Routes = new List<Route>(_routes.Where(x => x.Id == 1))
                },
                new Carrier
                {
                    Name = "Kudlik",
                    Id = 2
                },
                new Carrier
                {
                    Name = "Kolos usługi transportowe Wiesław Kut",
                    Id = 3
                }
            };
            _routes = new List<Route>
            {
                new Route
                {
                    BusStops = _busStops,
                    Id = 1,
                    Name = "Gorlice - Rzeszów",
                    Carrier = _carriers.First(x => x.Name.Equals("Nowex transport"))
                },
                new Route
                {
                    BusStops = _busStops.Take(2).ToList(),
                    Id = 2,
                    Name = "Strzyżów - Rzeszów",
                    Carrier = _carriers[1]
                },
                new Route
                {
                    BusStops = _busStops.Skip(2).ToList(),
                    Id = 3,
                    Name = "Frysztak - Rzeszów p. Strzyżów",
                    Carrier = _carriers[2]
                }
            };
        }

        public async Task<ObservableCollection<Pin>> GetPins()
        {
            
            await Task.Delay(2000);
            return _busStops.ConvertToMapPins();
        }

        public async Task PostPins(BusStop busStop)
        {
            await Task.Delay(2000);
            _busStops.Add(busStop);
            MessagingHelper.Toast($"Pins added.\nCurrent number of pins: {_busStops.Count.ToString()}", ToastTime.LongTime);
        }

        public async Task<ObservableCollection<Pin>> GetPinsForRoute(int routeId)
        {
            await Task.Delay(2000);
            return _busStops.Where(x => x.Route.Id == routeId)
                .ToObservableCollection()
                .ConvertToMapPins();
        }

        public async Task<List<Route>> GetRoutes()
        {
            await Task.Delay(500);
            return _routes;
        }
    }
}
