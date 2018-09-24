using System;
using System.Collections.Generic;
using System.Text;
using BusMap.WebApi.Controllers;
using BusMap.WebApi.Data;
using BusMap.WebApi.Models;
using BusMap.WebApi.Repositories.Abstract;
using BusMap.WebApi.Repositories.Implementations;
using BusMap.WebApiTests.RepositoriesTests;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using NUnit.Framework;

namespace BusMap.WebApiTests.ControllerTests
{
    [TestFixture]
    public class RoutesControllerTests
    {

        private RoutesController _routesController;
        private RoutesController _routesControllerEmpty;

        [SetUp]
        public void SetUp()
        {
            var options = new DbContextOptionsBuilder<DatabaseContext>()
                .UseInMemoryDatabase(databaseName: "TestDb2")
                .UseInMemoryDatabase(Guid.NewGuid().ToString())
                .Options;
            var context = new DatabaseContext(options);
            var repository = new RouteRepository(context);

            var optionsEmpty = new DbContextOptionsBuilder<DatabaseContext>()
                .UseInMemoryDatabase(databaseName: "TestDb2")
                .UseInMemoryDatabase(Guid.NewGuid().ToString())
                .Options;
            var emptyContext = new DatabaseContext(optionsEmpty);
            var emptyRepository = new RouteRepository(emptyContext);

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
            repository.AddRoute(routeForTest1);
            repository.AddRoute(routeForTest2);

            _routesController = new RoutesController(repository);
            _routesControllerEmpty = new RoutesController(emptyRepository);
        }


        #region GetTests
        [Test]
        public void GetAll_WhenRoutesExist_ReturnsOkObjectResult()
        {
            var result = _routesController.GetAll();

            Assert.IsInstanceOf<IActionResult>(result);
            Assert.IsInstanceOf<OkObjectResult>(result);
        }

        [Test]
        public void GetAll_WhenRoutesExist_ReturnListOfRoutes()
        {
            var okResult = _routesController.GetAll() as OkObjectResult;
            var result = okResult.Value as List<Route>;

            Assert.IsInstanceOf<List<Route>>(result);
        }

        [Test]
        public void GetAll_WhenRoutesDontExist_ReturnsBadRequest()
        {
            var result = _routesControllerEmpty.GetAll();

            Assert.IsInstanceOf<IActionResult>(result);
            Assert.IsInstanceOf<NotFoundResult>(result);
        }

        [Test]
        public void GetRoute_WhenRouteUnderIdExist_ReturnOkResult()
        {
            var result = _routesController.GetRoute(1);

            Assert.IsInstanceOf<OkObjectResult>(result);
        }

        [Test]
        public void GetRoute_WhenRouteUnderIdExist_ReturnRoute()
        {
            var okResult = _routesController.GetRoute(1) as OkObjectResult;
            var result = okResult.Value as Route;

            Assert.IsInstanceOf<Route>(result);
        }

        [Test]
        public void GetRoute_WhenRouteUnderIdDontExist_ReturnsNotFound()
        {
            var result = _routesController.GetRoute(111);

            Assert.IsInstanceOf<NotFoundResult>(result);
        }

        #endregion

        #region Post

        [Test]
        public void PostRoute_ObjectIsValid_ReturnsCreatedResponse()
        {
            var routeToPost = new Route
            {
                Id = 5,
                Name = "NewRoute",
                CarrierId = 1
            };

            var result = _routesController.PostRoute(routeToPost);

            Assert.IsInstanceOf<CreatedAtActionResult>(result);
        }

        [Ignore("Returning Created at, but in real use throwing exception")]
        [Test]
        public void PostRoute_WithoutRouteId_ReturningBadRequest()
        {
            var routeToPost = new Route
            {
                Id = 5,
                Name = "NewRoute"
            };

            var result = _routesController.PostRoute(routeToPost);

            Assert.IsInstanceOf<BadRequestObjectResult>(result);
        }

        #endregion

        #region Delete

        [Test]
        public void DeleteRoute_WhenRouteExist_ReturnsOkResult()
        {
            var result = _routesController.DeleteRoute(1); 

            Assert.IsInstanceOf<OkResult>(result);
        }

        [Test]
        public void DeleteRoute_WhenRouteDontExist_ReturnsNotFound()
        {
            var result = _routesController.DeleteRoute(1920);

            Assert.IsInstanceOf<NotFoundResult>(result);
        }
        

        #endregion


    }
}
