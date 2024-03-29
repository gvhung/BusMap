﻿using System;
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

        [Test]
        public void BusStopPunctualityHourMode_WhenTracesExist_ReturnsModeAsTimespan()
        {
            _busStops[0].BusStopTraces.Add(new BusStopTrace //two traces at 11:55
            {
                Id = 10000,
                Hour = new TimeSpan(11, 55, 0),
                BusStopId = 1
            });

            var result = PunctualityConverter.BusStopPunctualityHourMode(_busStops[0]);

            Assert.IsInstanceOf<TimeSpan>(result);
            Assert.AreEqual(new TimeSpan(11, 55, 0), result);
        }

        [Test]
        public void BusStopPunctualityHourMode_WhenTracesDontExistOrNull_ReturnBusStopHour()
        {
            var busStop1 = new BusStop()
            {
                Id = 1,
                Address = "Address",
                Hour = new TimeSpan(12,0,0)
            };
            var busStop2 = new BusStop()
            {
                Id = 1,
                Address = "Address",
                BusStopTraces = new List<BusStopTrace>(),
                Hour = new TimeSpan(14, 0, 0)
            };

            var result1 = PunctualityConverter.BusStopPunctualityHourMode(busStop1);
            var result2 = PunctualityConverter.BusStopPunctualityHourMode(busStop2);

            Assert.AreEqual(new TimeSpan(12, 0, 0), result1);
            Assert.AreEqual(new TimeSpan(14, 0, 0), result2);
        }

        [Test]
        public void AverageTimespan_WhenListIsNotEmptyAndCorrect_ReturnsTimespan()
        {
            var list = new List<TimeSpan>
            {
                new TimeSpan(12,0,0),
                new TimeSpan(12,30,0),
            };

            var result = list.AverageTimespan();

            Assert.AreEqual(new TimeSpan(12,15,0), result);
        }

        [Test]
        public void AverageTimespan_WhenListIsEmpty_ThrowsInvalidOperationException()
        {
            var list = new List<TimeSpan>();
            Assert.Throws<InvalidOperationException>(() => list.AverageTimespan());
        }

        [Test]
        public void AverageTimespan_WhenListIsNull_ThrowsArgumentNullException()
        {
            List<TimeSpan> list = null;
            Assert.Throws<ArgumentNullException>(() => list.AverageTimespan());
        }

        [Test]
        public void BusStopPunctualityHourAvgBeforeAvgAfterTime_WhenBusStopHaveTraces_ReturnsTuple()
        {
            var result = PunctualityConverter.BusStopPunctualityHourAvgBeforeAvgAfterTime(_busStops[0]);
            
            Assert.AreEqual((4, 9), result);
        }

        [Test]
        public void BusStopPunctualityHourAvgBeforeAvgAfterTime_WhenBusStopHaveNotTraces_ReturnsTuple00()
        {
            var busStop1 = new BusStop()
            {
                BusStopTraces = new List<BusStopTrace>()
            };
            var busStop2 = new BusStop()
            {
                BusStopTraces = new List<BusStopTrace>()
            };

            var result1 = PunctualityConverter.BusStopPunctualityHourAvgBeforeAvgAfterTime(busStop1);
            var result2 = PunctualityConverter.BusStopPunctualityHourAvgBeforeAvgAfterTime(busStop2);

            Assert.AreEqual((0, 0), result1);
            Assert.AreEqual((0, 0), result2);
        }

        [Test]
        public void BusStopPunctualityHourAvgBeforeAvgAfterTime_WhenHaveOnlyAfterTraces_ReturnsTuple03()
        {
            var busStop = new BusStop()
            {
                Hour = new TimeSpan(12, 0, 0),
                BusStopTraces = new List<BusStopTrace>
                {
                    new BusStopTrace()
                    {
                        Hour = new TimeSpan(12, 2, 0)
                    },
                    new BusStopTrace()
                    {
                        Hour = new TimeSpan(12, 5, 0)
                    }
                }
            };

            var result = PunctualityConverter.BusStopPunctualityHourAvgBeforeAvgAfterTime(busStop);
            Assert.AreEqual((0,3), result);
        }

        [Test]
        public void BusStopPunctualityHourAvgBeforeAvgAfterTime_WhenHaveOnlyAfterTraces_ReturnsTuple30()
        {
            var busStop = new BusStop
            {
                Hour = new TimeSpan(12, 0, 0),
                BusStopTraces = new List<BusStopTrace>
                {
                    new BusStopTrace
                    {
                        Hour = new TimeSpan(11, 58, 0)
                    },
                    new BusStopTrace
                    {
                        Hour = new TimeSpan(11, 55, 0)
                    }
                }
            };

            var result = PunctualityConverter.BusStopPunctualityHourAvgBeforeAvgAfterTime(busStop);
            Assert.AreEqual((3, 0), result);
        }

        [Test]
        public void RoutePunctualityHourAvgBeforeAvgAfterTime_WhenRouteHaveTraces_ReturnsTuple()
        {
            var result = PunctualityConverter.RoutePunctualityHourAvgBeforeAvgAfterTime(_routes[0]);

            Assert.AreEqual((4,8), result);
        }

        [Test]
        public void RoutePunctualityHourAvgBeforeAvgAfterTime_WhenRouteHaveNotTraces_ReturnsTuple00()
        {
            var route = new Route
            {
                BusStops = new List<BusStop>
                {
                    new BusStop
                    {
                        BusStopTraces = new List<BusStopTrace>()
                    },
                    new BusStop
                    {
                        BusStopTraces = new List<BusStopTrace>()
                    }
                }
            };

            var result = PunctualityConverter.RoutePunctualityHourAvgBeforeAvgAfterTime(route);

            Assert.AreEqual((0, 0), result);
        }


    }
}
