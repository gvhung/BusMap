using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using BusMap.WebApi.Models;
using BusMap.WebApi.Repositories.Abstract;

namespace BusMap.WebApiTests
{
    class BusStopRepositoryForTests : IBusStopRepository
    {
        private List<BusStop> _busStops;
        private List<Carrier> _carriers;
        private List<Route> _routes;

        public BusStopRepositoryForTests()
        {
            InitializeBusStopsData();
            InitializeCarriersData();
            InitializeRoutesData();
        }

        private void InitializeBusStopsData()
        {
            _busStops = new List<BusStop>()
            {
                new BusStop
                {
                    Id = 1,
                    Latitude = 5.0,
                    Longitude = 10.0,
                    Address = "TestAddress1",
                    Label = "TestLabel1"
                },
                new BusStop
                {
                    Id = 2,
                    Latitude = 15.0,
                    Longitude = 20.0,
                    Address = "TestAddress2",
                    Label = "TestLabel2"
                },
                new BusStop
                {
                    Id = 3,
                    Latitude = 25.0,
                    Longitude = 30.0,
                    Address = "TestAddress3",
                    Label = "TestLabel3"
                }
            };
        }

        private void InitializeCarriersData()
        {
            _carriers = new List<Carrier>
            {
                new Carrier
                {
                    Id = 1,
                    Name = "Carrier1"
                },
                new Carrier
                {
                    Id = 2,
                    Name = "Carrier2"
                }

            };
        }

        private void InitializeRoutesData()
        {
            _routes = new List<Route>
            {
                new Route
                {
                    Id = 1,
                    Name = "Route1",
                    Carrier = _carriers[0],
                    BusStops = _busStops.Take(3).ToList()
                },
            };

            _routes[0].Carrier = _carriers[0];

        }

        public BusStop Get(int id)
            => _busStops.First(x => x.Id == id);

        public IEnumerable<BusStop> GetAll()
            => _busStops;

        public void Add(BusStop pin)
            => _busStops.Add(pin);

        public void AddRange(IEnumerable<BusStop> pins)
            => _busStops.AddRange(pins);

        public void Remove(int id)
            => _busStops.Remove(_busStops.Find(x => x.Id == id));
    }
}
