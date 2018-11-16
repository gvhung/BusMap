using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using BusMap.WebApi.Data;
using BusMap.WebApi.DatabaseModels;
using BusMap.WebApi.Repositories.Implementations;
using Microsoft.EntityFrameworkCore;
using NUnit.Framework;

namespace BusMap.WebApiTests.RepositoriesTests
{
    [TestFixture]
    public abstract class RepositoryTestAbstractClass
    {
        protected DatabaseContext context;
        protected BusStopRepository busStopRepository;
        protected RouteRepository routeRepository;
        protected CarrierRepository carrierRepository;

        [SetUp]
        public async Task SetUp()
        {
            var options = new DbContextOptionsBuilder<DatabaseContext>()
                .UseInMemoryDatabase(databaseName: "TestDb")
                .UseInMemoryDatabase(Guid.NewGuid().ToString())
                .Options;
            context = new DatabaseContext(options);
            busStopRepository = new BusStopRepository(context);
            routeRepository = new RouteRepository(context);
            carrierRepository = new CarrierRepository(context);

            var routeForTest1 = new Route
            {
                Id = 1,
                Name = "RouteName1",
                Carrier = new Carrier
                {
                    Id = 1,
                    Name = "CarrierName1"
                }
            };
            var routeForTest2 = new Route
            {
                Id = 2,
                Name = "RouteName2",
                Carrier = new Carrier
                {
                    Id = 1,
                    Name = "CarrierName1"
                }
            };

            await busStopRepository.AddBusStopsRangeAsync(new List<BusStop>
            {
                new BusStop //FirstRoute
                {
                    Id = 1,
                    Latitude = 5.0,
                    Longitude = 10.0,
                    Address = "TestAddress1",
                    Label = "TestLabel1",
                    Route = routeForTest1,
                    Hour = new TimeSpan(12,0,0),
                    BusStopTraces = new List<BusStopTrace>  //All traces in time (under 5min)
                    {
                        new BusStopTrace
                        {
                            Id = 1,
                            BusStopId = 1,
                            Hour = new TimeSpan(12,2,0)
                        },
                        new BusStopTrace
                        {
                            Id = 2,
                            BusStopId = 1,
                            Hour = new TimeSpan(12,5,0)
                        }
                    }
                },
                new BusStop
                {
                    Id = 2,
                    Latitude = 15.0,
                    Longitude = 20.0,
                    Address = "TestAddress2",
                    Label = "TestLabel2",
                    Route = routeForTest1,
                    Hour = new TimeSpan(12,30,0),
                    BusStopTraces = new List<BusStopTrace>
                    {
                        new BusStopTrace
                        {
                            Id = 3,
                            BusStopId = 2,
                            Hour = new TimeSpan(12,35,0)
                        },
                        new BusStopTrace
                        {
                            Id = 4,
                            BusStopId = 2,
                            Hour = new TimeSpan(12,28,0)
                        }
                    }
                },
                new BusStop
                {
                    Id = 3,
                    Latitude = 25.0,
                    Longitude = 30.0,
                    Address = "TestAddress3",
                    Label = "TestLabel3",
                    Route = routeForTest1,
                    Hour = new TimeSpan(13,00,0),
                    BusStopTraces = new List<BusStopTrace>
                    {
                        new BusStopTrace
                        {
                            Id = 5,
                            BusStopId = 3,
                            Hour = new TimeSpan(13,1,0)
                        },
                        new BusStopTrace
                        {
                            Id = 6,
                            BusStopId = 3,
                            Hour = new TimeSpan(13,2,0)
                        }
                    }
                },
                new BusStop //SecondRoute
                {
                    Id = 4,
                    Latitude = 35.0,
                    Longitude = 40.0,
                    Address = "TestAddress4",
                    Label = "TestLabel4",
                    Route = routeForTest2,
                    Hour = new TimeSpan(16,0,0),
                    BusStopTraces = new List<BusStopTrace>
                    {
                        new BusStopTrace
                        {
                            Id = 7,
                            BusStopId = 4,
                            Hour = new TimeSpan(16,2,0)
                        },
                        new BusStopTrace
                        {
                            Id = 8,
                            BusStopId = 4,
                            Hour = new TimeSpan(16,10,0)
                        }
                    }
                },
                new BusStop
                {
                    Id = 5,
                    Latitude = 45.0,
                    Longitude = 50.0,
                    Address = "TestAddress5",
                    Label = "TestLabel5",
                    Route = routeForTest2,
                    Hour = new TimeSpan(17,0,0),
                    BusStopTraces = new List<BusStopTrace>
                    {
                        new BusStopTrace
                        {
                            Id = 9,
                            BusStopId = 5,
                            Hour = new TimeSpan(17,8,0)
                        },
                        new BusStopTrace
                        {
                            Id = 10,
                            BusStopId = 5,
                            Hour = new TimeSpan(17,10,0)
                        }
                    }
                },
                new BusStop
                {
                    Id = 6,
                    Latitude = 55.0,
                    Longitude = 60.0,
                    Address = "TestAddress6",
                    Label = "TestLabel6",
                    Route = routeForTest2,
                    Hour = new TimeSpan(18,0,0),
                    BusStopTraces = new List<BusStopTrace>
                    {
                        new BusStopTrace
                        {
                            Id = 11,
                            BusStopId = 6,
                            Hour = new TimeSpan(18,4,0)
                        },
                        new BusStopTrace
                        {
                            Id = 12,
                            BusStopId = 6,
                            Hour = new TimeSpan(18,2,0)
                        }
                    }
                }
            });

            await carrierRepository.AddCarrierAsync(new Carrier
            {
                Id = 2,
                Name = "CarrierName2"
            });
        }

    }
}
