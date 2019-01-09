using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using BusMap.WebApi.Data;
using BusMap.WebApi.DatabaseModels;
using BusMap.WebApi.Helpers;
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

            var carrierQueued1 = new CarrierQueued
            {
                Id = 1,
                Name = "CarrierQueued"
            };
            var carrierQueued2 = new CarrierQueued
            {
                Id = 2,
                Name = "CarrierQueued2"
            };
            var carrierQueued3 = new CarrierQueued
            {
                Id = 3,
                Name = "CarrierQueued3"
            };
            var carrierQueued4 = new CarrierQueued
            {
                Id = 4,
                Name = "CarrierQueued4"
            };
            var carrierQueued5 = new CarrierQueued
            {
                Id = 5,
                Name = "CarrierQueued4"
            };

            var route1 = new RouteQueued
            {
                Id = 1,
                Name = "routeQueuedName",
                CarrierQueued = carrierQueued1,
                CreatedDatetime = new DateTime(1998, 1, 1),
                VotingStartedDatetime = new DateTime(1998, 1, 1),
                VotingEndedDateTime = new DateTime(1998, 1, 15),
                DayOfTheWeek = "1,2,3",
                PositiveVotes = 10,
                NegativeVotes = 2,
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
                CarrierQueued = carrierQueued2,
                CreatedDatetime = new DateTime(1998, 1, 1),
                VotingStartedDatetime = new DateTime(1998, 1, 2),
                VotingEndedDateTime = new DateTime(1998, 1, 16),
                DayOfTheWeek = "1,2,3",
                PositiveVotes = 6,
                NegativeVotes = 2,
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
            var route3 = new RouteQueued
            {
                Id = 3,
                Name = "routeQueuedName3",
                CarrierQueued = carrierQueued3,
                CreatedDatetime = new DateTime(1998, 1, 1),
                VotingStartedDatetime = new DateTime(1998, 1, 3),
                VotingEndedDateTime = new DateTime(1998, 1, 16),
                DayOfTheWeek = "1,2,3",
                PositiveVotes = 22,
                NegativeVotes = 2,
                BusStopsQueued = new List<BusStopQueued>
                {
                    new BusStopQueued //150km
                    {
                        Id = 6,
                        Address = "Address6",
                        Label = "Label1",
                        Latitude = 53.553832,
                        Longitude = 21.422401
                    },
                    new BusStopQueued //170km
                    {
                        Id = 7,
                        Address = "Address7",
                        Label = "Label2",
                        Latitude = 53.733411,
                        Longitude = 21.451130
                    },
                    new BusStopQueued //10.3km
                    {
                        Id = 8,
                        Address = "Address8",
                        Label = "Label3",
                        Latitude = 52.215682,
                        Longitude = 20.855198
                    }
                }
            };
            var route4 = new RouteQueued
            {
                Id = 4,
                Name = "routeQueuedName4",
                CarrierQueued = carrierQueued4,
                CreatedDatetime = new DateTime(1998, 1, 5),
                VotingStartedDatetime = new DateTime(1998, 1, 6),
                VotingEndedDateTime = new DateTime(1998, 1, 20),
                DayOfTheWeek = "1,2,3",
                PositiveVotes = 22,
                NegativeVotes = 2,
                BusStopsQueued = new List<BusStopQueued>
                {
                    new BusStopQueued //150km
                    {
                        Id = 9,
                        Address = "Address6",
                        Label = "Label1",
                        Latitude = 53.553832,
                        Longitude = 21.422401
                    },
                    new BusStopQueued //170km
                    {
                        Id = 10,
                        Address = "Address7",
                        Label = "Label2",
                        Latitude = 53.733411,
                        Longitude = 21.451130
                    },
                    new BusStopQueued //10.3km
                    {
                        Id = 11,
                        Address = "Address8",
                        Label = "Label3",
                        Latitude = 52.215682,
                        Longitude = 20.855198
                    }
                }
            };
            var route5 = new RouteQueued
            {
                Id = 5,
                Name = "routeQueuedName5",
                CarrierQueued = carrierQueued5,
                CreatedDatetime = new DateTime(1998, 1, 5),
                VotingStartedDatetime = new DateTime(1998, 1, 2),
                VotingEndedDateTime = new DateTime(1998, 1, 15),
                DayOfTheWeek = "1,2,3",
                PositiveVotes = 15,
                NegativeVotes = 15,
                BusStopsQueued = new List<BusStopQueued>
                {
                    new BusStopQueued //150km
                    {
                        Id = 12,
                        Address = "Address6",
                        Label = "Label1",
                        Latitude = 53.553832,
                        Longitude = 21.422401
                    },
                    new BusStopQueued //170km
                    {
                        Id = 13,
                        Address = "Address7",
                        Label = "Label2",
                        Latitude = 53.733411,
                        Longitude = 21.451130
                    },
                    new BusStopQueued //10.3km
                    {
                        Id = 14,
                        Address = "Address8",
                        Label = "Label3",
                        Latitude = 52.215682,
                        Longitude = 20.855198
                    }
                }
            };

            var routes = new List<RouteQueued>()
            {
                route1,
                route2,
                route3,
                route4,
                route5
            };

            await _context.RoutesQueued.AddRangeAsync(routes);
            await _context.SaveChangesAsync();
        }


        [Test]
        public async Task GetRoutesInRange_WhenOneBusStopInOneRouteIsInRange_ReturningOneRoute()
        {
            var currentPositionString = "52.231247,21.004107";
            
            var result = await _queueRepository.GetRoutesInRangeAsync(currentPositionString, 10);

            Assert.AreEqual(1, result.Count());
            Assert.AreEqual("routeQueuedName", result.First().Name);
            Assert.AreEqual(2, result.ToList()[0].BusStopsQueued.Count);
        }

        [Test]
        public async Task GetRouteQueuedAsync_WhenRouteUnderIdExist_ReturningRouteQueued()
        {
            var result = await _queueRepository.GetRouteQueuedAsync(1);

            Assert.IsNotNull(result);
            Assert.IsInstanceOf<RouteQueued>(result);
            Assert.AreEqual("routeQueuedName", result.Name);
        }

        [Test]
        public async Task GetRouteQueuedAsync_WhenRouteUnderIdExist_ReturningRouteIncludeCarrierQueuedAndBusStopsQueued()
        {
            var result = await _queueRepository.GetRouteQueuedAsync(1);

            Assert.IsNotNull(result.CarrierQueued);
            Assert.IsNotNull(result.BusStopsQueued);
            Assert.IsTrue(result.BusStopsQueued.Count > 0);
        }

        [Test]
        public async Task GetRouteQueuedAsync_WhenRouteUnderIdNotExist_ReturningNull()
        {
            var result = await _queueRepository.GetRouteQueuedAsync(10);

            Assert.IsNull(result);
        }

        [Test]
        public async Task GetRoutesQueueAsync_WhenRoutesExist_ReturningRoutesQueued()
        {
            var result = await _queueRepository.GetRoutesQueueAsync();

            Assert.IsInstanceOf<IEnumerable<RouteQueued>>(result);
            Assert.IsTrue(result.Count() > 0);
        }

        [Test]
        public async Task GetRoutesQueueAsync_WhenRoutesNotExist_ReturningEmptyCollection()
        {
            _context.RoutesQueued.RemoveRange(_context.RoutesQueued.ToList());
            await _context.SaveChangesAsync();

            var result = await _queueRepository.GetRoutesQueueAsync();

            Assert.IsInstanceOf<IEnumerable<RouteQueued>>(result);
            Assert.IsTrue(result.Count() == 0);
        }



        [Test]
        public async Task MoveQueuedRoutesToMainTableAsync_WhenFinishDateInAllRoutesAreBefore_NotMovingAnyRoute()
        {
            DateAndTime.NowImpl = () => new DateTime(1998, 1, 13);

            var routesInRoutesTableBefore = await _context.Routes.ToListAsync();
            var routesInRoutesQueuedTableBefore = await _context.RoutesQueued.ToListAsync();

            await _queueRepository.MoveQueuedRoutesToMainTableAsync();

            var routesInRoutesTableAfter = await _context.Routes.ToListAsync();
            var routesInRoutesQueuedTableAfter = await _context.RoutesQueued.ToListAsync();

            Assert.AreEqual(routesInRoutesQueuedTableBefore.Count, routesInRoutesQueuedTableAfter.Count);
            Assert.AreEqual(0, routesInRoutesTableAfter.Count);
            Assert.AreEqual(routesInRoutesQueuedTableBefore, routesInRoutesQueuedTableAfter);
            Assert.AreEqual(routesInRoutesTableBefore, routesInRoutesTableAfter);
        }

        [Test]
        public async Task MoveQueuedRoutesToMainTableAsync_WhenFinishDateInOneRouteIsEqualToday_Targeting1Route_MovingSingleRoute()
        {
            DateAndTime.NowImpl = () => new DateTime(1998, 1, 15);

            var routesInRoutesTableBefore = await _context.Routes.ToListAsync();
            var routesInRoutesQueuedTableBefore = await _context.RoutesQueued.ToListAsync();

            await _queueRepository.MoveQueuedRoutesToMainTableAsync();

            var routesInRoutesTableAfter = await _context.Routes.ToListAsync();
            var routesInRoutesQueuedTableAfter = await _context.RoutesQueued.ToListAsync();
            var resultRoute = _context.Routes.First();

            Assert.AreEqual(routesInRoutesQueuedTableBefore.Count - 1, routesInRoutesQueuedTableAfter.Count);
            Assert.AreEqual(1, routesInRoutesTableAfter.Count);

            Assert.AreEqual(2, resultRoute.BusStops.Count);
            Assert.AreEqual("CarrierQueued", resultRoute.Carrier.Name);
        }

        [Test]
        public async Task MoveQueuedRoutesToMainTableAsync_WhenFinishDateInOneRouteIsEqualToday_Targeting2Routes_MovingThreeRoutes()
        {
            DateAndTime.NowImpl = () => new DateTime(1998, 1, 16);

            var routesInRoutesTableBefore = await _context.Routes.ToListAsync();
            var routesInRoutesQueuedTableBefore = await _context.RoutesQueued.ToListAsync();

            await _queueRepository.MoveQueuedRoutesToMainTableAsync();

            var routesInRoutesTableAfter = await _context.Routes.ToListAsync();
            var routesInRoutesQueuedTableAfter = await _context.RoutesQueued.ToListAsync();

            Assert.AreEqual(2, routesInRoutesQueuedTableAfter.Count);
            Assert.AreEqual(3, routesInRoutesTableAfter.Count);
        }

        [Test]
        public async Task MoveQueuedRoutesToMainTableAsync_WhenRouteHaveNormallCarrier_MovingRouteAndSettingCorrectCarrier()
        {
            DateAndTime.NowImpl = () => new DateTime(1995, 1, 1);
            await _context.Carriers.AddAsync(new Carrier
            {
                Id = 1,
                Name = "NormalAddedCarrier"
            });
            await _context.RoutesQueued.AddAsync(new RouteQueued
            {
                Id = 100,
                Name = "newRouteQueuedForTest",
                CarrierId = 1,
                CreatedDatetime = new DateTime(1994, 12, 2),
                VotingStartedDatetime = new DateTime(1994, 12, 17),
                VotingEndedDateTime = new DateTime(1995, 1, 1),
                DayOfTheWeek = "1,2,3",
                PositiveVotes = 20,
                NegativeVotes = 1,
                BusStopsQueued = new List<BusStopQueued>
                {
                    new BusStopQueued //150km
                    {
                        Id = 100,
                        Address = "AddressTest1",
                        Label = "Label1",
                        Latitude = 53.553832,
                        Longitude = 21.422401
                    },
                    new BusStopQueued //170km
                    {
                        Id = 101,
                        Address = "AddressTest2",
                        Label = "Label2",
                        Latitude = 53.733411,
                        Longitude = 21.451130
                    },
                    new BusStopQueued //10.3km
                    {
                        Id = 102,
                        Address = "AddressTest3",
                        Label = "Label3",
                        Latitude = 52.215682,
                        Longitude = 20.855198
                    }
                }
            });
            await _context.SaveChangesAsync();

            var routesInRoutesTableBefore = await _context.Routes.ToListAsync();
            var routesInRoutesQueuedTableBefore = await _context.RoutesQueued.ToListAsync();
            var carriersBefore = await _context.Carriers.ToListAsync();

            await _queueRepository.MoveQueuedRoutesToMainTableAsync();

            var routesInRoutesTableAfter = await _context.Routes.ToListAsync();
            var routesInRoutesQueuedTableAfter = await _context.RoutesQueued.ToListAsync();
            var carriersAfter = await _context.Carriers.ToListAsync();
            var resultRoute = _context.Routes.First();


            Assert.AreEqual(carriersBefore.Count , carriersAfter.Count);
            Assert.AreEqual(routesInRoutesQueuedTableBefore.Count - 1, routesInRoutesQueuedTableAfter.Count);
            Assert.AreEqual(routesInRoutesTableBefore.Count + 1, routesInRoutesTableAfter.Count);

            Assert.AreEqual(3, resultRoute.BusStops.Count);
            Assert.AreEqual("NormalAddedCarrier", resultRoute.Carrier.Name);
        }

        [Test]
        public async Task RemoveRejectedQueuedRoutesAsync_WhenSingleRouteQueuedNotPassingVoting_RemoveThisRouteQueued()
        {
            DateAndTime.NowImpl = () => new DateTime(1995, 1, 1);
            await _context.RoutesQueued.AddAsync(new RouteQueued
            {
                Id = 100,
                Name = "newRouteQueuedForTest",
                CarrierQueued = new CarrierQueued
                {
                    Id = 1001,
                    Name = "CarrierQueuedName"
                },
                CreatedDatetime = new DateTime(1994, 12, 2),
                VotingStartedDatetime = new DateTime(1994, 12, 17),
                VotingEndedDateTime = new DateTime(1995, 1, 1),
                DayOfTheWeek = "1,2,3",
                PositiveVotes = 20,
                NegativeVotes = 18,
                BusStopsQueued = new List<BusStopQueued>
                {
                    new BusStopQueued //150km
                    {
                        Id = 100,
                        Address = "AddressTest1",
                        Label = "Label1",
                        Latitude = 53.553832,
                        Longitude = 21.422401
                    },
                    new BusStopQueued //170km
                    {
                        Id = 101,
                        Address = "AddressTest2",
                        Label = "Label2",
                        Latitude = 53.733411,
                        Longitude = 21.451130
                    },
                    new BusStopQueued //10.3km
                    {
                        Id = 102,
                        Address = "AddressTest3",
                        Label = "Label3",
                        Latitude = 52.215682,
                        Longitude = 20.855198
                    }
                }
            });
            await _context.SaveChangesAsync();

            var nOfQueuedRoutesBefore = _context.RoutesQueued.Count();
            await _queueRepository.RemoveRejectedQueuedRoutesAsync();
            var nOfQueuedRoutesAfter = _context.RoutesQueued.Count();

            Assert.AreEqual(nOfQueuedRoutesBefore - 1, nOfQueuedRoutesAfter);
        }

        [Test]
        public async Task RemoveRejectedQueuedRoutesAsync_WhenNoneRouteNotPassingVoting_RemovingNothing()
        {
            DateAndTime.NowImpl = () => new DateTime(1995, 1, 20);

            var nOfQueuedRoutesBefore = _context.RoutesQueued.Count();
            await _queueRepository.RemoveRejectedQueuedRoutesAsync();
            var nOfQueuedRoutesAfter = _context.RoutesQueued.Count();

            Assert.AreEqual(nOfQueuedRoutesBefore, nOfQueuedRoutesAfter);
        }

    }
}
