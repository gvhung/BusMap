using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Diagnostics.CodeAnalysis;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;
using BusMap.Mobile.Helpers;
using BusMap.Mobile.Models;
using Xamarin.Forms.Internals;
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
            _routes = new List<Route>();
            _carriers = new List<Carrier>();

            SeedData();
        }

        [SuppressMessage("ReSharper", "StringLiteralTypo")]
        private void SeedData()
        {
            var route1BusStops = new List<BusStop>
            {
                new BusStop
                {
                    Id = 1,
                    Address = "Gorlice",
                    Label = "Gorlice DA",
                    Latitude = 49.662932,
                    Longitude = 21.159447
                },
                new BusStop
                {
                    Id = 2,
                    Address = "Jasło",
                    Label = "Jasło DA",
                    Latitude = 49.743750,
                    Longitude = 21.473399
                },
                new BusStop
                {
                    Id = 3,
                    Address = "Frysztak",
                    Latitude = 49.845480,
                    Longitude = 21.612531
                },
                new BusStop
                {
                    Id = 4,
                    Address = "Wiśniowa",
                    Latitude = 49.869611,
                    Longitude = 21.659950
                },
                new BusStop
                {
                    Id = 5,
                    Address = "Dobrzechów",
                    Latitude = 49.876198,
                    Longitude = 21.753990
                },
                new BusStop
                {
                    Id = 6,
                    Address = "Strzyżów",
                    Label = "Strzyżów DA",
                    Latitude = 49.869992,
                    Longitude = 21.800657
                },
                new BusStop
                {
                    Id = 7,
                    Address = "Czudec",
                    Label = "Czudec DA",
                    Latitude = 49.945855,
                    Longitude = 21.837562
                },
                new BusStop
                {
                    Id = 8,
                    Address = "Boguchwała",
                    Latitude = 49.983775,
                    Longitude = 21.942793
                },
                new BusStop
                {
                    Id = 9,
                    Address = "Rzeszów",
                    Label = "Podkarpacka",
                    Latitude = 50.020076,
                    Longitude = 21.980312
                },
                new BusStop
                {
                    Id = 10,
                    Address = "Rzeszów",
                    Label = "Rzeszów DA",
                    Latitude = 50.042131,
                    Longitude = 22.003429
                },
                new BusStop
                {
                    Id = 11,
                    Address = "Rzeszów",
                    Label = "Rejtana",
                    Latitude = 50.031346,
                    Longitude = 22.016653
                }
            };

            var route2BusStops = new List<BusStop>
            {
                new BusStop
                {
                    Id = 12,
                    Address = "Rzeszów",
                    Label = "Rejtana",
                    Latitude = 50.030767,
                    Longitude = 22.017088
                },
                new BusStop
                {
                    Id = 13,
                    Address = "Rzeszów",
                    Label = "Rzeszów DA",
                    Latitude = 50.042131,
                    Longitude = 22.003429
                },
                new BusStop
                {
                    Id = 14,
                    Address = "Rzeszów",
                    Label = "Podkarpacka",
                    Latitude = 50.020076,
                    Longitude = 21.980312
                },
                new BusStop
                {
                    Id = 15,
                    Address = "Boguchwała",
                    Latitude = 49.983775,
                    Longitude = 21.942793
                },
                new BusStop
                {
                    Id = 16,
                    Address = "Czudec",
                    Label = "Czudec DA",
                    Latitude = 49.945855,
                    Longitude = 21.837562
                },
                new BusStop
                {
                    Id = 17,
                    Address = "Strzyżów",
                    Label = "Strzyżów DA",
                    Latitude = 49.869992,
                    Longitude = 21.800657
                },
                new BusStop
                {
                    Id = 18,
                    Address = "Dobrzechów",
                    Latitude = 49.876198,
                    Longitude = 21.753990
                },
                new BusStop
                {
                    Id = 19,
                    Address = "Wiśniowa",
                    Latitude = 49.869611,
                    Longitude = 21.659950
                },
                new BusStop
                {
                    Id = 20,
                    Address = "Frysztak",
                    Latitude = 49.845480,
                    Longitude = 21.612531
                },
                new BusStop
                {
                    Id = 21,
                    Address = "Jasło",
                    Label = "Jasło DA",
                    Latitude = 49.743750,
                    Longitude = 21.473399
                },
                new BusStop
                {
                    Id = 22,
                    Address = "Gorlice",
                    Label = "Gorlice DA",
                    Latitude = 49.662932,
                    Longitude = 21.159447
                }
            };

            var route3BusStops = new List<BusStop>
            {
                new BusStop
                {
                    Id = 23,
                    Address = "Frysztak",
                    Latitude = 49.845480,
                    Longitude = 21.612531
                },
                new BusStop
                {
                    Id = 24,
                    Address = "Wiśniowa",
                    Latitude = 49.869611,
                    Longitude = 21.659950
                },
                new BusStop
                {
                    Id = 25,
                    Address = "Dobrzechów",
                    Latitude = 49.876198,
                    Longitude = 21.753990
                },
                new BusStop
                {
                    Id = 26,
                    Address = "Strzyżów",
                    Label = "Strzyżów DA",
                    Latitude = 49.869992,
                    Longitude = 21.800657
                },
                new BusStop
                {
                    Id = 27,
                    Address = "Zaborów",
                    Latitude = 49.914127,
                    Longitude = 21.827073
                },
                new BusStop
                {
                    Id = 28,
                    Address = "Czudec",
                    Label = "Czudec DA",
                    Latitude = 49.945855,
                    Longitude = 21.837562
                },
                new BusStop
                {
                    Id = 29,
                    Address = "Boguchwała",
                    Latitude = 49.983775,
                    Longitude = 21.942793
                },
                new BusStop
                {
                    Id = 30,
                    Address = "Rzeszów",
                    Label = "Podkarpacka",
                    Latitude = 50.020076,
                    Longitude = 21.980312
                },
                new BusStop
                {
                    Id = 31,
                    Address = "Rzeszów",
                    Label = "Rzeszów DA",
                    Latitude = 50.042131,
                    Longitude = 22.003429
                }
            };

            var carrier1 = new Carrier
            {
                Id = 1,
                Name = "Nowex Transport"
            };
            var carrier2 = new Carrier
            {
                Id = 2,
                Name = "Kolos"
            };
            var route1 = new Route
            {
                Id = 1,
                Name = "Gorlice - Rzeszów",
                Carrier = carrier1,
                BusStops = route1BusStops
            };
            var route2 = new Route
            {
                Id = 2,
                Name = "Rzeszów - Gorlice",
                Carrier = carrier1,
                BusStops = route2BusStops
            };
            var route3 = new Route
            {
                Id = 3,
                Name = "Frysztak - Rzeszów",
                Carrier = carrier2, BusStops = route3BusStops
            };

            var busStopsLocal = new List<BusStop>();
            busStopsLocal.AddRange(route1BusStops);
            busStopsLocal.AddRange(route2BusStops);
            busStopsLocal.AddRange(route3BusStops);
            _busStops = busStopsLocal.ToObservableCollection();
            _carriers.AddRange(new [] {carrier1, carrier2});
            _routes.AddRange(new [] {route1, route2, route3});
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

        public async Task<Route> GetRoute(int routeId)
        {
            await Task.Delay(500);
            return _routes.Find(x => x.Id == routeId);
        }

        public Task<List<Route>> FindRoute(Expression<Func<Route, bool>> predicate)
        {
            throw new NotImplementedException();
        }
    }
}
