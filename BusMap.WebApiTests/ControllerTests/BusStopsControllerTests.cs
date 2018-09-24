using System;
using System.Collections.Generic;
using BusMap.WebApi.Controllers;
using BusMap.WebApi.Data;
using BusMap.WebApi.Models;
using BusMap.WebApi.Repositories.Abstract;
using BusMap.WebApi.Repositories.Implementations;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using NUnit.Framework;

namespace BusMap.WebApiTests
{
    [TestFixture]
    public class BusStopsControllerTests
    {
        private  BusStopsController _busStopsController;
        //private  IBusStopRepository _busStopRepository;

        [SetUp]
        public void Setup()
        {
            //_busStopRepository = new BusStopRepositoryForTests();

            var options = new DbContextOptionsBuilder<DatabaseContext>()
                .UseInMemoryDatabase(databaseName: "TestDb")
                .UseInMemoryDatabase(Guid.NewGuid().ToString())
                .Options;
            var context = new DatabaseContext(options);
            var repository = new BusStopRepository(context);

            var routeForTest = new Route
            {
                Id = 1,
                Name = "RouteName",
                Carrier = new Carrier
                {
                    Id = 1,
                    Name = "CarrierName"
                }
            };
            repository.AddBusStopsRange(new List<BusStop>
            {
                new BusStop
                {
                    Id = 1,
                    Latitude = 5.0,
                    Longitude = 10.0,
                    Address = "TestAddress1",
                    Label = "TestLabel1",
                    Route = routeForTest
                },
                new BusStop
                {
                    Id = 2,
                    Latitude = 15.0,
                    Longitude = 20.0,
                    Address = "TestAddress2",
                    Label = "TestLabel2",
                    Route = routeForTest
                },
                new BusStop
                {
                    Id = 3,
                    Latitude = 25.0,
                    Longitude = 30.0,
                    Address = "TestAddress3",
                    Label = "TestLabel3",
                    Route = routeForTest
                }

            });

            _busStopsController = new BusStopsController(repository);
        }

        #region GetAll

        [Test]
        public void GetAll_WhenCalled_ReturnsOkResult()
        {
            var okResult = _busStopsController.GetAll();

            Assert.IsInstanceOf<IActionResult>(okResult);
            Assert.IsInstanceOf<OkObjectResult>(okResult);
        }

        [Test]
        public void GetAll_WhenCalled_ReturnsListOfPinsObjectType()
        {
            var result = _busStopsController.GetAll() as OkObjectResult;
            var resultStops = result.Value as List<BusStop>;

            Assert.IsInstanceOf<List<BusStop>>(resultStops);
        }

        [Test]
        public void GetAll_WhenCalled_ReturningAllPins()
        {
            IActionResult actionResult = _busStopsController.GetAll();
            OkObjectResult result = actionResult as OkObjectResult;
            List<BusStop> resultStops = result.Value as List<BusStop>;

            Assert.IsInstanceOf<IEnumerable<BusStop>>(resultStops);
            Assert.IsTrue(resultStops.Count == 3);

            int nOfStops = resultStops.Count;
            for (int i = 0; i < nOfStops; i++)
            {
                for (int j = 0; j < nOfStops; j++)
                {
                    if (i != j)
                    {
                        Assert.AreNotEqual(resultStops[i], resultStops[j]);
                    }
                }
            }
        }

        #endregion

        #region PostBusStop

        [Test]
        public void PostBusStop_ValidObjectPassed_ReturnsCreatedResponse()
        {
            var busStop = new BusStop()
            {
                Label = "AdditionTest",
                Address = "Test",
                Longitude = 23,
                Latitude = -20
            };

            var result = _busStopsController.PostPin(busStop);

            Assert.IsInstanceOf<CreatedAtActionResult>(result);
        }

        [Test]
        public void PostBusStop_ValidObjectPassed_ReturnsCreatedPinObject()
        {
            var pin = new BusStop()
            {
                Label = "AdditionTest",
                Address = "Test",
                Longitude = 23,
                Latitude = -20
            };

            var result = _busStopsController.PostPin(pin) as CreatedAtActionResult;
            var resultBusStop = result.Value as BusStop;

            Assert.IsInstanceOf<BusStop>(resultBusStop);
            Assert.AreEqual(-20, resultBusStop?.Latitude);
            Assert.AreEqual("AdditionTest", resultBusStop?.Label);
        }



        //TODO: Add case with null Label after dbUpdate

        #endregion



        [Test]
        public void PostPinAndGetPins_ValidObjectPassed_ReturnsBiggerBy1Collection()
        {
            var pin = new BusStop()
            {
                Label = "AdditionTest",
                Address = "Test",
                Longitude = 23,
                Latitude = -20
            };

            _busStopsController.PostPin(pin);
            var okResult = _busStopsController.GetAll() as OkObjectResult;
            var resultBusStop = okResult?.Value as List<BusStop>;

            Assert.IsInstanceOf<List<BusStop>>(resultBusStop);
            Assert.AreEqual(4, resultBusStop?.Count);
        }


    }
}
