using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using AutoMapper;
using BusMap.WebApi.Automapper;
using BusMap.WebApi.Controllers;
using BusMap.WebApi.Data;
using BusMap.WebApi.DatabaseModels;
using BusMap.WebApi.Dto.BusStops;
using BusMap.WebApi.Repositories.Abstract;
using BusMap.WebApi.Repositories.Implementations;
using BusMap.WebApi.Services.Implementations;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using NUnit.Framework;

namespace BusMap.WebApiTests
{
    [TestFixture]
    public class BusStopsControllerTests
    {
        private BusStopsController _busStopsController;
        private BusStopsController _busStopsControllerEmpty;
        private IMapper mapper;
        
        

        public BusStopsControllerTests()
        {
            AutoMapperSetup();
        }

        private void AutoMapperSetup()
        {
            var config = new AutoMapper.MapperConfiguration(cfg =>
            {
                cfg.AddProfile(new AutomapperProfile());
            });
            mapper = config.CreateMapper();
        }

        [SetUp]
        public async Task Setup()
        { 
            var options = new DbContextOptionsBuilder<DatabaseContext>()
                .UseInMemoryDatabase(databaseName: "TestDb")
                .UseInMemoryDatabase(Guid.NewGuid().ToString())
                .Options;
            var context = new DatabaseContext(options);
            var repository = new BusStopRepository(context);
            
            var optionsEmpty = new DbContextOptionsBuilder<DatabaseContext>()
                .UseInMemoryDatabase(databaseName: "TestDb")
                .UseInMemoryDatabase(Guid.NewGuid().ToString())
                .Options;
            var emptyContext = new DatabaseContext(optionsEmpty);
            var emptyRepository = new BusStopRepository(emptyContext);

            var service = new BusStopService(repository, mapper);
            var emptyService = new BusStopService(emptyRepository, mapper);

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
            await repository.AddBusStopsRangeAsync(new List<BusStop>
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

            _busStopsController = new BusStopsController(service);
            _busStopsControllerEmpty = new BusStopsController(emptyService);
        }


        #region GetAll
        [Test]
        public async Task GetAll_WhenBusStopsExists_ReturnsOkResult()
        {
            var okResult = await _busStopsController.GetAllBusStops();

            Assert.IsInstanceOf<IActionResult>(okResult);
            Assert.IsInstanceOf<OkObjectResult>(okResult);
        }

        [Test]
        public async Task GetAll_WhenBusStopsExists_ReturnsListOfPins()
        {
            var result = await _busStopsController.GetAllBusStops() as OkObjectResult;
            var resultStops = result?.Value as List<BusStopsBusStopDto>;

            Assert.IsInstanceOf<List<BusStopsBusStopDto>>(resultStops);
            Assert.IsTrue(resultStops.Count == 3);
        }

        [Test]
        public async Task GetAll_WhenBusStopsExists_ReturningUniquePins()
        {
            IActionResult actionResult = await _busStopsController.GetAllBusStops();
            OkObjectResult result = actionResult as OkObjectResult;
            List<BusStopsBusStopDto> resultStops = result.Value as List<BusStopsBusStopDto>;

            Assert.IsInstanceOf<IEnumerable<BusStopsBusStopDto>>(resultStops);
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

        [Test]
        public async Task GetAll_WhenBusStopsDontExist_ReturnsNotFound()    
        {
            var result = await _busStopsControllerEmpty.GetAllBusStops();

            Assert.IsInstanceOf<NotFoundResult>(result);
        }

        [Test]
        public async Task GetBusStop_WhenBusStopUnderIdExist_ReturnOkObjectResult()
        {
            var result = await _busStopsController.GetBusStop(2);

            Assert.IsInstanceOf<OkObjectResult>(result);
        }

        [Test]
        public async Task GetBusStop_WhenBusStopUnderIdExist_ReturnBusStop()
        {
            var okResult = await _busStopsController.GetBusStop(2) as OkObjectResult;
            var result = okResult.Value as BusStopsBusStopDto;

            Assert.IsInstanceOf<BusStopsBusStopDto>(result);
            Assert.AreEqual("TestLabel2", result.Label);
        }

        [Test]
        public async Task GetBusStop_WhenBusStopUnderIdDontExist_ReturnsNotFound()
        {
            var result = await _busStopsController.GetBusStop(1920);

            Assert.IsInstanceOf<NotFoundResult>(result);
        }

        #endregion

        #region PostBusStop

        [Test]
        public async Task PostBusStop_ValidObjectPassed_ReturnsCreatedResponse()
        {
            var busStop = new BusStop()
            {
                Id = 24,
                Label = "AdditionTest",
                Address = "Test",
                Longitude = 23,
                Latitude = -20
            };

            var result = await _busStopsController.PostBusStop(busStop);

            Assert.IsInstanceOf<CreatedAtActionResult>(result);
        }

        [Test]
        public async Task PostBusStop_ValidObjectPassed_ReturnsCreatedPinObject()
        {
            var pin = new BusStop()
            {
                Id = 23,
                Label = "AdditionTest",
                Address = "Test",
                Longitude = 23,
                Latitude = -20
            };

            var result = await _busStopsController.PostBusStop(pin) as CreatedAtActionResult;
            var resultBusStop = result.Value as BusStop;

            Assert.IsInstanceOf<BusStop>(resultBusStop);
            Assert.AreEqual(-20, resultBusStop?.Latitude);
            Assert.AreEqual("AdditionTest", resultBusStop?.Label);
        }



        //TODO: Add case with null Label after dbUpdate. Invalid post

        [Test]
        public async Task PostBusStopAndGetBusStop_ValidObjectPassed_ReturnsBiggerBy1Collection()
        {
            var pin = new BusStop()
            {
                Id = 23,
                Label = "AdditionTest",
                Address = "Test",
                Longitude = 23,
                Latitude = -20
            };

            await _busStopsController.PostBusStop(pin);
            var okResult = await _busStopsController.GetAllBusStops() as OkObjectResult;
            var resultBusStop = okResult?.Value as List<BusStopsBusStopDto>;

            Assert.IsInstanceOf<List<BusStopsBusStopDto>>(resultBusStop);
            Assert.AreEqual(4, resultBusStop?.Count);
        }

        #endregion

        #region DeleteBusStop

        [Test]
        public async Task DeleteBusStop_WhenBusStopUnderIdExist_ReturnsOkResult()
        {
            var result = await _busStopsController.DeleteBusStop(3);

            Assert.IsInstanceOf<OkResult>(result);
        }

        [Test]
        public async Task DeleteBusStop_WhenBusStopUnderIdDontExist_ReturnsNotFound()
        {
            var result = await _busStopsController.DeleteBusStop(1980);

            Assert.IsInstanceOf<NotFoundResult>(result);
        }


        #endregion


    }
}
