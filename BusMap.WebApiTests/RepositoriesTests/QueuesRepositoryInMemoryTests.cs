using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using BusMap.WebApi.Data;
using BusMap.WebApi.DatabaseModels;
using BusMap.WebApi.Models;
using BusMap.WebApi.Repositories.Abstract;
using BusMap.WebApi.Repositories.Implementations;
using Microsoft.EntityFrameworkCore;
using Moq;
using NUnit.Framework;

namespace BusMap.WebApiTests.RepositoriesTests
{
    [TestFixture]
    public class QueuesRepositoryInMemoryTests
    {
        private IQueueRepository _queueRepository;
        private DatabaseContext _context;

        [SetUp]
        public async Task SetUp()
        {
            var options = new DbContextOptionsBuilder<DatabaseContext>()
                .UseInMemoryDatabase(databaseName: "TestDb")
                .UseInMemoryDatabase(Guid.NewGuid().ToString())
                .Options;
            _context = new DatabaseContext(options);

            _queueRepository = new QueueRepository(_context);

            var carrierQueued = new CarrierQueued
            {
                Id = 1,
                Name = "CarrierQueued"
            };

            var route1 = new RouteQueued
            {
                Id = 1,
                Name = "routeQueuedName",
                CarrierQueued = carrierQueued,
                BusStopsQueued = new List<BusStopQueued>
                {
                    new BusStopQueued //150km
                    {
                        Id = 1,
                        Address = "Address1",
                        Label = "Label1",
                        Latitude = 53.553832,
                        Longitude = 21.422401
                    },
                    new BusStopQueued //8km
                    {
                        Id = 2,
                        Address = "Address2",
                        Label = "Label2",
                        Latitude = 52.295105,
                        Longitude = 20.944934
                    }
                }
            };
            var route2 = new RouteQueued
            {
                Id = 2,
                Name = "routeQueuedName2",
                CarrierQueued = carrierQueued,
                BusStopsQueued = new List<BusStopQueued>
                {
                    new BusStopQueued //150km
                    {
                        Id = 3,
                        Address = "Address1",
                        Label = "Label1",
                        Latitude = 53.553832,
                        Longitude = 21.422401
                    },
                    new BusStopQueued //170km
                    {
                        Id = 4,
                        Address = "Address2",
                        Label = "Label2",
                        Latitude = 53.733411,
                        Longitude = 21.451130
                    },
                    new BusStopQueued //10.3km
                    {
                        Id = 5,
                        Address = "Address3",
                        Label = "Label3",
                        Latitude = 52.215682,
                        Longitude = 20.855198
                    }
                }
            };
            var routes = new List<RouteQueued>()
            {
                route1,
                route2
            };

            await _queueRepository.AddRouteToQueueAsync(route1);
            await _queueRepository.AddRouteToQueueAsync(route2);
        }


        [Test]
        public async Task GetRoutesInRange_WhenOneBusStopInOneRouteIsInRange_ReturningOneRoute()
        {
            var currentPositionString = "52.231247,21.004107";
            
            var result = await _queueRepository.GetRoutesInRangeAsync(currentPositionString, 10);

            Assert.AreEqual(1, result.Count());
            Assert.AreEqual(2, result.ToList()[0].BusStopsQueued.Count);
        }

        //TODO: Other test, eg. Then is null //after merge with tests update branch

    }
}
