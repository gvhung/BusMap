using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using BusMap.WebApi.DatabaseModels;
using BusMap.WebApi.Helpers;
using Microsoft.EntityFrameworkCore;
using NUnit.Framework;

namespace BusMap.WebApiTests.RepositoriesTests
{
    [TestFixture]
    public class RouteRepositoryInMemoryTests : RepositoryTestAbstractClass
    {

        [SetUp]
        public async Task SetUpFixture()
        {
            //Data initialization for Find method tests. Other tests still using base-type setUp data.
            var routeForFindTest1 = new Route
            {
                Id = 100,
                Name = "Test WWA - Krk",
                DayOfTheWeek = "1,2,3",
                Carrier = new Carrier
                {
                    Id = 10,
                    Name = "WaniaTrans"
                },
                BusStops = new List<BusStop>
                {
                    new BusStop
                    {
                        Id = 100,
                        Address = "Warszawa",
                        Label = "Wschodnia",
                        Hour = new TimeSpan(12, 0, 0),
                        Latitude = 1,
                        Longitude = 2
                    },
                    new BusStop
                    {
                        Id = 101,
                        Address = "Krakow",
                        Label = "Wawel",
                        Hour = new TimeSpan(15, 0, 0),
                        Latitude = 3,
                        Longitude = 4
                    }
                }
            };

            var routeForFindTest2 = new Route
            {
                Id = 101,
                Name = "Test WWA - Kato",
                DayOfTheWeek = "1,3,4",
                Carrier = new Carrier
                {
                    Id = 11,
                    Name = "LeszkoTrans"
                },
                BusStops = new List<BusStop>
                {
                    new BusStop
                    {
                        Id = 102,
                        Address = "Warszawa",
                        Label = "Zachodnia",
                        Hour = new TimeSpan(12, 0, 0),
                        Latitude = 1,
                        Longitude = 2
                    },
                    new BusStop
                    {
                        Id = 103,
                        Address = "Krakow",
                        Label = "Wawel",
                        Hour = new TimeSpan(15, 0, 0),
                        Latitude = 3,
                        Longitude = 4
                    },
                    new BusStop
                    {
                        Id = 104,
                        Address = "Katowice",
                        Label = "Spodek",
                        Hour = new TimeSpan(17, 0, 0),
                        Latitude = 3,
                        Longitude = 4
                    },
                }
            };

            var routeForFindTest3 = new Route
            {
                Id = 102,
                Name = "Test Gdynia - Warszwawa",
                DayOfTheWeek = "1,2,3",
                Carrier = new Carrier
                {
                    Id = 12,
                    Name = "MieszkoTrans"
                },
                BusStops = new List<BusStop>
                {
                    new BusStop
                    {
                        Id = 105,
                        Address = "Gdynia",
                        Label = "Centralna",
                        Hour = new TimeSpan(8, 0, 0),
                        Latitude = 1,
                        Longitude = 2
                    },
                    new BusStop
                    {
                        Id = 106,
                        Address = "Warszawa",
                        Label = "Zlote tarasy",
                        Hour = new TimeSpan(14, 0, 0),
                        Latitude = 3,
                        Longitude = 4
                    }
                }
            };

            await context.Routes.AddAsync(routeForFindTest1);
            await context.Routes.AddAsync(routeForFindTest2);
            await context.Routes.AddAsync(routeForFindTest3);
            await context.SaveChangesAsync();
        }

        #region GetRouteTests   
        [Test]
        public async Task GetRoute_WhenIdExists_ReturnsRoute()
        {
            var result1 = await routeRepository.GetRouteAsync(1);
            var result2 = await routeRepository.GetRouteAsync(2);

            Assert.IsNotNull(result1);
            Assert.AreEqual("RouteName1", result1.Name);

            Assert.IsNotNull(result2);
            Assert.AreEqual("RouteName2", result2.Name);
        }

        [Test]
        public async Task GetRoute_IncludeBusStops_InPossibleToGetBusStops()
        {
            var result1 = await routeRepository.GetRouteAsync(1);
            var result2 = await routeRepository.GetRouteAsync(2);
            var result1BusStops = result1.BusStops.ToList();
            var result2BusStops = result2.BusStops.ToList();

            Assert.IsNotNull(result1);
            Assert.AreEqual(3, result1BusStops.Count);
            Assert.AreEqual(result1BusStops, context.BusStops.Where(x => x.Route.Equals(result1)).ToList());
            Assert.AreEqual("TestLabel2", result1BusStops[1].Label);

            Assert.IsNotNull(result2);
            Assert.AreEqual(3, result2BusStops.Count);
            Assert.AreEqual(result2BusStops, context.BusStops.Where(x => x.Route.Equals(result2)));
            Assert.AreEqual("TestLabel6", result2BusStops[2].Label);
        }

        [Test]
        public async Task GetRoute_IncludeCarrier_IsPossibleToGetCarrier()
        {
            var result1 = await routeRepository.GetRouteAsync(1);
            var result2 = await routeRepository.GetRouteAsync(2);
            var result1Carrier = result1.Carrier;
            var result2Carrier = result2.Carrier;

            Assert.IsNotNull(result1);
            Assert.AreEqual("CarrierName1", result1Carrier.Name);

            Assert.IsNotNull(result2);
            Assert.AreEqual("CarrierName1", result2Carrier.Name);
        }

        [Test]
        public async Task GetRoute_WhenIdNotExists_ReturnsNull()
        {
            var route = await routeRepository.GetRouteAsync(190);

            Assert.IsNull(route);
        }

        [Test]
        public async Task GetAllRoutes_ReturningAllRoutes()
        {
            var nOfRoutesFromContext = context.Routes.ToList().Count;
            var result = await routeRepository.GetAllRoutesAsync();

            Assert.AreEqual(nOfRoutesFromContext, result.ToList().Count);
        }

        #endregion

        #region AddRouteTests

        [Test]
        public async Task AddRoute_WhenCarrierExistBusStopsAreNot_AddingNewRouteNewBusStopsWithoutNewCarrier()
        {
            var nOfBusStopsBefore = context.BusStops.ToList().Count;
            var nOfCarriersBefore = context.Carriers.ToList().Count;
            var nOfRoutesBefore = context.Routes.ToList().Count;

            var busStop1 = new BusStop
            {
                Id = 21,
                Latitude = 500.0,
                Longitude = 700.0,
                Address = "TestRouteAddress1",
                Label = "TestRouteLabel1"
            };
            var busStop2 = new BusStop
            {
                Id = 22,
                Latitude = 1000.0,
                Longitude = 2000.0,
                Address = "TestRouteAddress2",
                Label = "TestRouteLabel2"
            };
            var routeToAdd = new Route
            {
                Id = 10,
                Name = "TestRoute",
                Carrier = context.Carriers.First(),
                BusStops = new List<BusStop>
                {
                   busStop1,
                   busStop2
                }
            };

            await routeRepository.AddRouteAsync(routeToAdd);

            var result = context.Routes.Last();
            var resultList = context.Routes.ToList();
            var nOfBusStopsAfter = context.BusStops.ToList().Count;
            var nOfCarriersAfter = context.Carriers.ToList().Count;
            var nOfRoutesAfter = context.Routes.ToList().Count;

            Assert.IsTrue(resultList.Contains(routeToAdd));
            Assert.IsTrue(result.BusStops.Contains(busStop1));
            Assert.IsTrue(result.BusStops.Contains(busStop2));

            Assert.AreEqual(nOfRoutesBefore + 1, nOfRoutesAfter);
            Assert.AreEqual(nOfBusStopsBefore + 2, nOfBusStopsAfter);
            Assert.AreEqual(nOfCarriersBefore, nOfCarriersAfter);
        }

        [Ignore("Wait for implementation in EF")]
        [Test]
        public void AddRoute_AddingWithoutBusStops_ThrowingException()
        {
            var routeToAdd = new Route
            {
                Id = 15,
                Name = "Name",
                Carrier = new Carrier
                {
                    Id = 12,
                    Name = "CarrierName"
                }
            };

            Assert.ThrowsAsync<InvalidOperationException>(async () =>
                await routeRepository.AddRouteAsync(routeToAdd));
        }

        [Ignore("Wait for implementation.")]
        [Test]
        public void AddRoute_WhenNameNotSet_ThrowingException()
        {
            var routeToAdd = new Route
            {
                Id = 15,
                Carrier = new Carrier
                {
                    Id = 12,
                    Name = "CarrierName"
                }
            };

            Assert.ThrowsAsync<InvalidOperationException>(async () =>
                await routeRepository.AddRouteAsync(routeToAdd));
        }

        [Ignore("Wait for implementation.")]
        [Test]
        public void AddRoute_WhenCarrierIsNotSet_ThrowingException()
        {
            var routeToAdd = new Route
            {
                Id = 15,
                Name = "NameTest"
            };

            Assert.Throws<InvalidOperationException>(() =>
                routeRepository.AddRouteAsync(routeToAdd));
        }
        #endregion

        #region RemoveRouteTests

        [Test]
        public async Task RemoveRoute_WhenRouteUnderIdExists_RemovingRoute()
        {
            var nOfRoutesBefore = context.Routes.ToList().Count;
            var routeToRemove = await routeRepository.GetRouteAsync(2);
            await routeRepository.RemoveRouteAsync(routeToRemove);
            var nOfRoutesAfter = context.Routes.ToList().Count;

            Assert.AreEqual(nOfRoutesBefore - 1, nOfRoutesAfter);
            Assert.IsFalse(context.Routes.Contains(routeToRemove));
        }

        [Test]
        public async Task RemoveRoute_WhenRouteUnderIdDontExists_ThrowingException()
        {
            var routeToRemove = await routeRepository.GetRouteAsync(1290);

            Assert.ThrowsAsync<ArgumentNullException>(async () =>
                await routeRepository.RemoveRouteAsync(routeToRemove));
        }


        [Test]
        public async Task RemoveRoute_WhenRouteUnderIdExists_AlsoRemovingBusStops()
        {
            var nOfBusStopsBefore = context.BusStops.ToList().Count;
            var routeToRemove = await routeRepository.GetRouteAsync(1);
            await routeRepository.RemoveRouteAsync(routeToRemove);
            var nOfBusStopsAfter = context.BusStops.ToList().Count;

            Assert.AreEqual(nOfBusStopsBefore - 3, nOfBusStopsAfter);
        }

        [Test]
        public async Task RemoveRoute_WhenRouteUnderIdExists_DontRemovingCarrier()
        {
            var nOfCarriersBefore = context.Carriers.ToList().Count;
            var routeToRemove = await routeRepository.GetRouteAsync(1);
            await routeRepository.RemoveRouteAsync(routeToRemove);
            var nOfCarriersAfter = context.Carriers.ToList().Count;

            Assert.AreEqual(nOfCarriersBefore, nOfCarriersAfter);
        }

        #endregion

        #region RouteLatency_RouteRecentBusStop

        [Test]
        public async Task GetRouteCurrentLatencyAsync_WhenTraceExistAndIsLate_ReturnsInt()
        {
            var dateTime = DateAndTime.NowImpl = () => new DateTime(1980, 1, 1, 12, 40, 0);
            await context.BusStopTraces.AddAsync(new BusStopTrace
            {
                Id = 1,
                BusStopId = 1,
                Date = new DateTime(1980, 1, 1),
                Hour = new TimeSpan(12, 10, 0)
            });
            await context.BusStopTraces.AddAsync(new BusStopTrace
            {
                Id = 2,
                BusStopId = 2,
                Date = new DateTime(1980, 1, 1),
                Hour = new TimeSpan(12, 35, 0)
            });
            await context.SaveChangesAsync();

            //Bus was on second stop with 5 minutes delay
            var result = await routeRepository.GetRouteCurrentLatencyAsync(1);
            Assert.AreEqual(5, result);
        }

        [Test]
        public async Task GetRouteCurrentLatencyAsync_WhenTraceExistAndIsBeforeTime_ReturnsNegativeInt()
        {
            var dateTime = DateAndTime.NowImpl = () => new DateTime(1980, 1, 1, 12, 40, 0);
            await context.BusStopTraces.AddAsync(new BusStopTrace
            {
                Id = 1,
                BusStopId = 1,
                Date = new DateTime(1980, 1, 1),
                Hour = new TimeSpan(12, 10, 0)
            });
            await context.BusStopTraces.AddAsync(new BusStopTrace
            {
                Id = 2,
                BusStopId = 2,
                Date = new DateTime(1980, 1, 1),
                Hour = new TimeSpan(12, 25, 0)
            });
            await context.SaveChangesAsync();

            //Bus was on second stop with 5 minutes delay
            var result = await routeRepository.GetRouteCurrentLatencyAsync(1);
            Assert.AreEqual(-5, result);
        }

        [Test]
        public async Task GetRouteCurrentLatencyAsync_WhenTraceExistFromYesterday_Returns9999()
        {
            var dateTime = DateAndTime.NowImpl = () => new DateTime(1980, 1, 2, 12, 40, 0);
            await context.BusStopTraces.AddAsync(new BusStopTrace
            {
                Id = 1,
                BusStopId = 1,
                Date = new DateTime(1980, 1, 1),
                Hour = new TimeSpan(12, 10, 0)
            });
            await context.BusStopTraces.AddAsync(new BusStopTrace
            {
                Id = 2,
                BusStopId = 2,
                Date = new DateTime(1980, 1, 1),
                Hour = new TimeSpan(12, 25, 0)
            });
            await context.SaveChangesAsync();

            //Bus was on second stop with 5 minutes delay
            var result = await routeRepository.GetRouteCurrentLatencyAsync(1);
            Assert.AreEqual(9999, result);
        }

        [Test]
        public async Task GetRouteCurrentLatencyAsync_WhenRouteHaventEverAnyTraces_Returns9999()
        {
            var dateTime = DateAndTime.NowImpl = () => new DateTime(1980, 1, 2, 12, 40, 0);

            var result = await routeRepository.GetRouteCurrentLatencyAsync(1);
            Assert.AreEqual(9999, result);
        }




        [Test]
        public async Task GetRouteRecentBusStopAsync_WhenTraceExistAndIsLate_ReturnsBusStop()
        {
            var dateTime = DateAndTime.NowImpl = () => new DateTime(1980, 1, 1, 12, 40, 0);
            await context.BusStopTraces.AddAsync(new BusStopTrace
            {
                Id = 1,
                BusStopId = 1,
                Date = new DateTime(1980, 1, 1),
                Hour = new TimeSpan(12, 10, 0)
            });
            await context.BusStopTraces.AddAsync(new BusStopTrace
            {
                Id = 2,
                BusStopId = 2,
                Date = new DateTime(1980, 1, 1),
                Hour = new TimeSpan(12, 35, 0)
            });
            await context.SaveChangesAsync();

            var result = await routeRepository.GetRouteRecentBusStopAsync(1);
            Assert.IsInstanceOf<BusStop>(result);
            Assert.AreEqual(2, result.Id);
        }

        [Test]
        public async Task GetRouteRecentBusStopAsync_WhenTraceExistFromYesterday_ReturnsNull()
        {
            var dateTime = DateAndTime.NowImpl = () => new DateTime(1980, 1, 2, 12, 40, 0);
            await context.BusStopTraces.AddAsync(new BusStopTrace
            {
                Id = 1,
                BusStopId = 1,
                Date = new DateTime(1980, 1, 1),
                Hour = new TimeSpan(12, 10, 0)
            });
            await context.BusStopTraces.AddAsync(new BusStopTrace
            {
                Id = 2,
                BusStopId = 2,
                Date = new DateTime(1980, 1, 1),
                Hour = new TimeSpan(12, 35, 0)
            });
            await context.SaveChangesAsync();

            var result = await routeRepository.GetRouteRecentBusStopAsync(1);
            Assert.IsNull(result);
        }

        #endregion

        #region FindRoutes

        [Test]
        public async Task FindRoutesAsync_UsingOnlyCities_ReturnsRoutes()
        {
            var result = await routeRepository.FindRoutesAsync("War", "Krak");

            Assert.AreEqual(2, result.Count());
            Assert.IsTrue(!result.Any(r => r.Name == "Test Gdynia - Warszwawa"));
        }

        [Test]
        public async Task FindRoutesAsync_WhenRouteNotFound_ReturnsEmptyList()
        {
            var result = await routeRepository.FindRoutesAsync("Raw", "Kark");

            Assert.IsInstanceOf<IEnumerable<Route>>(result);
            Assert.AreEqual(0, result.Count());
        }

        [Test]
        public async Task FindRoutesAsync_TwoRoutesWithReversedOrder_ReturnsOnly1Route()
        {
            var route1ReversedOrder = new Route
            {
                Id = 110,
                Name = "Test KrK - WWA",
                DayOfTheWeek = "1,2,3",
                CarrierId = 10,
                BusStops = new List<BusStop>
                {
                    new BusStop
                    {
                        Id = 107,
                        Address = "Krakow",
                        Label = "Wawel",
                        Hour = new TimeSpan(12, 0, 0),
                        Latitude = 3,
                        Longitude = 4
                    },
                    new BusStop
                    {
                        Id = 108,
                        Address = "Warszawa",
                        Label = "Wschodnia",
                        Hour = new TimeSpan(15, 0, 0),
                        Latitude = 1,
                        Longitude = 2
                    }
                }

            };
            await context.Routes.AddAsync(route1ReversedOrder);
            await context.SaveChangesAsync();


            var result = await routeRepository.FindRoutesAsync("Krak", "War");

            Assert.AreEqual(1, result.Count());
            Assert.IsTrue(!result.Any(r => r.Name == "Test WWA - Krk"));
            Assert.AreEqual(route1ReversedOrder.Name, result.First().Name);
        }

        [Test]
        public async Task FindRoutesAsync_UsingCitiesDays_Returns2Routes()
        {
            var result = await routeRepository.FindRoutesAsync("War", "Krak", "1,3");

            Assert.AreEqual(2, result.Count());
        }

        [Test]
        public async Task FindRoutesAsync_UsingCitiesDays_Returns1Routes()
        {
            var result = await routeRepository.FindRoutesAsync("War", "Krak", "2");

            Assert.AreEqual(1, result.Count());
            Assert.AreEqual("Test WWA - Krk", result.First().Name);
        }

        [Test]
        public async Task FindRoutesAsync_UsingCitiesDaysHoursFromTo_ReturnsRoutesInRange()
        {
            var routeDifferentHours = new Route
            {
                Id = 110,
                Name = "WWA - Krk 2 diff hrs",
                DayOfTheWeek = "1,2,3",
                CarrierId = 10,
                BusStops = new List<BusStop>
                {
                    new BusStop
                    {
                        Id = 107,
                        Address = "Warszawa",
                        Label = "Wschodnia",
                        Hour = new TimeSpan(10, 0, 0),
                        Latitude = 1,
                        Longitude = 2
                    },
                    new BusStop
                    {
                        Id = 108,
                        Address = "Krakow",
                        Label = "Wawel",
                        Hour = new TimeSpan(13, 0, 0),
                        Latitude = 3,
                        Longitude = 4
                    }
                }

            };
            await context.Routes.AddAsync(routeDifferentHours);
            await context.SaveChangesAsync();

            var resultRoutes = await routeRepository.FindRoutesAsync("Warsz", "Krak", "1,2,3",
                new TimeSpan(9, 0, 0), new TimeSpan(11, 30, 0));

            Assert.AreEqual(1, resultRoutes.Count());
            Assert.AreEqual(routeDifferentHours.Name, resultRoutes.First().Name);
        }

        [Test]
        public async Task FindRoutesAsync_UsingCitiesDaysHoursFromTo_Returns3RoutesInRange()
        {
            var routeDifferentHours = new Route
            {
                Id = 110,
                Name = "WWA - Krk 2 diff hrs",
                DayOfTheWeek = "1,2,3",
                CarrierId = 10,
                BusStops = new List<BusStop>
                {
                    new BusStop
                    {
                        Id = 107,
                        Address = "Warszawa",
                        Label = "Wschodnia",
                        Hour = new TimeSpan(10, 0, 0),
                        Latitude = 1,
                        Longitude = 2
                    },
                    new BusStop
                    {
                        Id = 108,
                        Address = "Krakow",
                        Label = "Wawel",
                        Hour = new TimeSpan(13, 0, 0),
                        Latitude = 3,
                        Longitude = 4
                    }
                }

            };
            await context.Routes.AddAsync(routeDifferentHours);
            await context.SaveChangesAsync();

            var resultRoutes = await routeRepository.FindRoutesAsync("Warsz", "Krak", "1,2,3",
                new TimeSpan(9, 0, 0), new TimeSpan(13, 30, 0));

            Assert.AreEqual(3, resultRoutes.Count());
        }

        [Test]
        public async Task FindRoutesAsync_UsingCitiesDate_ReturnsRoutesAtSpecificDate()
        {
            DateAndTime.NowImpl = () => new DateTime(1980, 1, 1);
            var routeAtSunday = new Route //Only this route will course in sunday (0)
            {
                Id = 110,
                Name = "Lublin Poznan Test Route",
                DayOfTheWeek = "0",
                CarrierId = 10,
                BusStops = new List<BusStop>
                {
                    new BusStop
                    {
                        Id = 107,
                        Address = "Lublin",
                        Label = "Dworzec",
                        Hour = new TimeSpan(10, 0, 0),
                        Latitude = 1,
                        Longitude = 2
                    },
                    new BusStop
                    {
                        Id = 108,
                        Address = "Poznan",
                        Label = "Dworzec",
                        Hour = new TimeSpan(13, 0, 0),
                        Latitude = 3,
                        Longitude = 4
                    }
                }

            };
            await context.Routes.AddAsync(routeAtSunday);
            await context.SaveChangesAsync();

            var resultRoutes = await routeRepository.FindRoutesAsync("Lublin", "Poznan",
                date: new DateTime(1980, 2, 3));

            Assert.AreEqual(1, resultRoutes.Count());
            Assert.AreEqual(routeAtSunday.Name, resultRoutes.First().Name);
        }

        [Test]
        public async Task FindRoutesAsync_WhenFromAndToCitiesAreSame_ReturnsRoutesInThisCity()
        {
            var carrier = new Carrier
            {
                Id = 60,
                Name = "TestTrans"
            };
            var route1= new Route
            {
                Id = 1040,
                Name = "Rzeszow - Rzeszow",
                DayOfTheWeek = "1,3,4",
                Carrier = carrier,
                BusStops = new List<BusStop>
                {
                    new BusStop
                    {
                        Id = 151,
                        Address = "Rzeszow",
                        Label = "One",
                    },
                    new BusStop
                    {
                        Id = 152,
                        Address = "Rzeszow",
                        Label = "Two",
                    },
                    new BusStop
                    {
                        Id = 153,
                        Address = "Rzeszow",
                        Label = "Three",
                    }
                }
            };
            var route2 = new Route
            {
                Id = 1041,
                Name = "Rzeszow - Rzeszow",
                DayOfTheWeek = "1,3,4",
                Carrier = carrier,
                BusStops = new List<BusStop>
                {
                    new BusStop
                    {
                        Id = 154,
                        Address = "Rzeszow",
                        Label = "Three",
                    },
                    new BusStop
                    {
                        Id = 155,
                        Address = "Rzeszow",
                        Label = "Two",
                    },
                    new BusStop
                    {
                        Id = 156,
                        Address = "Rzeszow",
                        Label = "One",
                    }                   
                }
            };

            await context.Routes.AddRangeAsync(new []{route1, route2});
            await context.SaveChangesAsync();
            var test = await context.Routes.ToListAsync();
            var result = await routeRepository.FindRoutesAsync("Rzeszow", "Rzeszow");

            Assert.AreEqual(2, result.ToList().Count);
        }

        [Test]
        public async Task FindRoutes_WhenStartAndEndPointAreSame_ReturnsSingleRoute()
        {
            var carrier = new Carrier
            {
                Id = 60,
                Name = "TestTrans"
            };
            var route1 = new Route
            {
                Id = 121,
                Carrier = carrier,
                DayOfTheWeek = "1,2,3,4,5,6,0",
                Name = "Rzeszow - Warszawa",
                BusStops = new List<BusStop>
                {
                    new BusStop
                    {
                        Id = 1030,
                        Address = "Rzeszow",
                        Label = "DA",
                        Hour = new TimeSpan(10,0,0),
                        Latitude = 1,
                        Longitude = 1
                    },
                    new BusStop
                    {
                        Id = 1031,
                        Address = "Lodz",
                        Label = "Glowna",
                        Hour = new TimeSpan(13,0,0),
                        Latitude = 2,
                        Longitude = 2
                    },
                    new BusStop
                    {
                        Id = 1032,
                        Address = "Warszawa",
                        Label = "Dworzec glowny",
                        Hour = new TimeSpan(16,0,0),
                        Latitude = 3,
                        Longitude = 3
                    }
                }
            };
            var route2 = new Route
            {
                Id = 122,
                Carrier = carrier,
                DayOfTheWeek = "1,2,3,4,5,6,0",
                Name = "Rzeszow - Rzeszow",
                BusStops = new List<BusStop>
                {
                    new BusStop
                    {
                        Id = 1035,
                        Address = "Rzeszow",
                        Label = "DA",
                        Hour = new TimeSpan(10,0,0),
                        Latitude = 1,
                        Longitude = 1
                    },
                    new BusStop
                    {
                        Id = 1036,
                        Address = "Rzeszow",
                        Label = "Dabrowskiego",
                        Hour = new TimeSpan(10,30,0),
                        Latitude = 2,
                        Longitude = 2
                    },
                    new BusStop
                    {
                        Id = 1037,
                        Address = "Rzeszow",
                        Label = "Lisa Kuli",
                        Hour = new TimeSpan(11,0,0),
                        Latitude = 3,
                        Longitude = 3
                    }
                }
            };

            await context.Routes.AddRangeAsync(new[] {route1, route2});
            await context.SaveChangesAsync();

            var result = await routeRepository.FindRoutesAsync("Rzeszow", "Rzeszow");

            Assert.AreEqual(1, result.Count());
            //Assert.IsFalse(result.Any(r => r.Name.Equals(route1.Name)));
        }

        #endregion


    }
}
