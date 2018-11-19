using System;
using System.Collections.Generic;
using System.Text;
using BusMap.WebApi.DatabaseModels;
using BusMap.WebApi.Helpers;
using NUnit.Framework;
using NUnit.Framework.Internal;

namespace BusMap.WebApiTests.HelpersTests
{
    [TestFixture]
    public class PunctualityConverterTests
    {
        private List<BusStop> _busStops;
        private List<Route> _routes;


        [SetUp]
        public void SetUp()
        {
            var busStop1 = new BusStop
            {
                Id = 1,
                Address = "Address1",
                Label = "Label1",
                Hour = new TimeSpan(12,0,0),
                BusStopTraces = new List<BusStopTrace>
                {
                    new BusStopTrace
                    {
                        Id = 1,
                        Hour = new TimeSpan(12,2,0),
                        BusStopId = 1
                    },
                    new BusStopTrace
                    {
                        Id = 2,
                        Hour = new TimeSpan(12,5,0),
                        BusStopId = 1
                    },
                    new BusStopTrace
                    {
                        Id = 3,
                        Hour = new TimeSpan(11,55,0),
                        BusStopId = 1
                    },
                    new BusStopTrace    //First4 ok
                    {
                        Id = 4,
                        Hour = new TimeSpan(11,59,0),
                        BusStopId = 1
                    },
                    new BusStopTrace
                    {
                        Id = 5,
                        Hour = new TimeSpan(12,7,0),
                        BusStopId = 1
                    },
                    new BusStopTrace
                    {
                        Id = 6,
                        Hour = new TimeSpan(11,52,0),
                        BusStopId = 1
                    },
                    new BusStopTrace
                    {
                        Id = 7,
                        Hour = new TimeSpan(12,22,0),
                        BusStopId = 1
                    },
                    new BusStopTrace
                    {
                        Id = 8,
                        Hour = new TimeSpan(15,22,0),
                        BusStopId = 1
                    },
                }
            };
            var busStop2 = new BusStop
            {
                Id = 2,
                Address = "Address2",
                Label = "Label2",
                Hour = new TimeSpan(13, 0, 0),
                BusStopTraces = new List<BusStopTrace>
                {
                    new BusStopTrace
                    {
                        Id = 9,
                        Hour = new TimeSpan(13,2,0),
                        BusStopId = 2
                    },
                    new BusStopTrace
                    {
                        Id = 10,
                        Hour = new TimeSpan(13,5,0),
                        BusStopId = 2
                    },
                    new BusStopTrace
                    {
                        Id = 11,
                        Hour = new TimeSpan(12,55,0),
                        BusStopId = 2
                    },
                    new BusStopTrace    
                    {
                        Id = 12,
                        Hour = new TimeSpan(12,59,0),
                        BusStopId = 2
                    },
                    new BusStopTrace    //First5 ok
                    {
                        Id = 13,
                        Hour = new TimeSpan(13,4,0),
                        BusStopId = 2
                    },
                    new BusStopTrace
                    {
                        Id = 14,
                        Hour = new TimeSpan(12,52,0),
                        BusStopId = 2
                    },
                    new BusStopTrace
                    {
                        Id = 15,
                        Hour = new TimeSpan(13,22,0),
                        BusStopId = 2
                    },
                    new BusStopTrace
                    {
                        Id = 16,
                        Hour = new TimeSpan(18,22,0),
                        BusStopId = 2
                    },
                }
            };

            _busStops = new List<BusStop>();
            _busStops.Add(busStop1);
            _busStops.Add(busStop2);
            _routes = new List<Route>
            {
                new Route
                {
                    BusStops = _busStops,
                }
            };
        }


        //[Test]
        //public void PercentageOfPunctuality_FromBusStop1_Returning50()
        //{
        //    var result = PunctualityConverter.BusStopPercentageOfPunctuality(_busStops[0]);
            
        //    Assert.AreEqual(50.0, result);
        //}

        //[Test]
        //public void PercentageOfPunctuality_FromBusStop2_Returning62()
        //{
        //    var result = PunctualityConverter.BusStopPercentageOfPunctuality(_busStops[1]);
        //    int x = 1;
        //    Assert.AreEqual(62, result);
        //}

        [Test]
        public void RoutePunctualityPercentage_Returning56Percent()

        {
            var result = PunctualityConverter.RoutePunctualityPercentage(_routes[0]);
            
            Assert.AreEqual("56%", result);
        }

        [Test]
        public void BusStopPunctualityPercentage_FromBusStops0_Returning50Percent()
        {
            var result = PunctualityConverter.BusStopPunctualityPercentage(_busStops[0]);

            Assert.AreEqual("50%", result);
        }

        [Test]
        public void BusStopPunctualityPercentage_WenBusStopHavntTraces_Returning0Percent()
        {
            var busStop1 = new BusStop
            {
                Id = 1,
                Address = "Address",
                Label = "Label"
            };

            var busStop2 = new BusStop
            {
                Id = 1,
                Address = "Address",
                Label = "Label",
                BusStopTraces = new List<BusStopTrace>()
            };

            //var result1 = PunctualityConverter.BusStopPunctualityPercentage(busStop1);
            var result2 = PunctualityConverter.BusStopPunctualityPercentage(busStop2);

            //Assert.AreEqual("0%", result1);
            Assert.AreEqual("0%", result2);
        }


        [Test]
        public void ConvertToAverageArrivedHour_FromBusStops0_Returning12_28()
        {
            var result = PunctualityConverter.BusStopAverageArrivedHour(_busStops[0]);

            Assert.AreEqual(new TimeSpan(12,28,0), result);
        }

        [Test]
        public void ConvertToAverageArrivedHour_FromTwoTraces_Returning2()
        {
            var busStop = new BusStop
            {
                Hour = new TimeSpan(12,0,0),
                BusStopTraces = new List<BusStopTrace>
                {
                    new BusStopTrace
                    {
                        Hour = new TimeSpan(12,10,0)
                    },
                    new BusStopTrace
                    {
                        Hour = new TimeSpan(12,5,0)
                    }
                }
            };

            var result = PunctualityConverter.BusStopAverageArrivedHour(busStop);

            Assert.AreEqual(new TimeSpan(12,7,30), result);
        }

        [Test]
        public void CalculateDelay_WhenBusIsHasty_Returning7()
        {
            var busStop = new BusStop
            {
                Hour = new TimeSpan(12, 0, 0),
                BusStopTraces = new List<BusStopTrace>
                {
                    new BusStopTrace
                    {
                        Hour = new TimeSpan(12,10,0)
                    },
                    new BusStopTrace
                    {
                        Hour = new TimeSpan(12,5,0)
                    }
                }
            };

            var result = PunctualityConverter.CalculateBusStopDelay(busStop);

            Assert.AreEqual(7, result);
        }

        [Test]
        public void CalculateDelay_WhenBusIsLate_ReturningMinus7()
        {
            var busStop = new BusStop
            {
                Hour = new TimeSpan(12, 0, 0),
                BusStopTraces = new List<BusStopTrace>
                {
                    new BusStopTrace
                    {
                        Hour = new TimeSpan(11,50,0)
                    },
                    new BusStopTrace
                    {
                        Hour = new TimeSpan(11,55,0)
                    }
                }
            };

            var result = PunctualityConverter.CalculateBusStopDelay(busStop);

            Assert.AreEqual(-7, result);
        }

        

    }
}
