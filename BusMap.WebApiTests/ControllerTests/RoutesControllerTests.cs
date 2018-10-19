using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using BusMap.WebApi.Controllers;
using BusMap.WebApi.Data;
using BusMap.WebApi.DatabaseModels;
using BusMap.WebApi.Dto.Routes;
using BusMap.WebApi.Repositories.Abstract;
using BusMap.WebApi.Repositories.Implementations;
using BusMap.WebApi.Services.Implementations;
using BusMap.WebApiTests.RepositoriesTests;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using NUnit.Framework;

namespace BusMap.WebApiTests.ControllerTests
{
    [TestFixture]
    public class RoutesControllerTests : ControllerTestAbstractClass
    {

        private RoutesController _routesController;
        private RoutesController _routesControllerEmpty;

        [SetUp]
        public void SetUp()
        {
            var repository = new RouteRepository(Context);
            var repositoryEmpty = new RouteRepository(ContextEmpty);

            var service = new RouteService(repository, Mapper);
            var serviceEmpty = new RouteService(repositoryEmpty, Mapper);

            _routesController = new RoutesController(service);
            _routesControllerEmpty = new RoutesController(serviceEmpty);
        }


        #region GetTests
        [Test]
        public async Task GetAll_WhenRoutesExist_ReturnsOkObjectResult()
        {
            var result = await _routesController.GetAllRoutes();

            Assert.IsInstanceOf<IActionResult>(result);
            Assert.IsInstanceOf<OkObjectResult>(result);
        }

        [Test]
        public async Task GetAll_WhenRoutesExist_ReturnListOfRoutes()
        {
            var okResult = await _routesController.GetAllRoutes() as OkObjectResult;
            var result = okResult.Value as List<RoutesRouteDto>;

            Assert.IsInstanceOf<List<RoutesRouteDto>>(result);
        }

        [Test]
        public async Task GetAll_WhenRoutesDontExist_ReturnsBadRequest()
        {
            var result = await _routesControllerEmpty.GetAllRoutes();

            Assert.IsInstanceOf<IActionResult>(result);
            Assert.IsInstanceOf<NotFoundResult>(result);
        }

        [Test]
        public async Task GetRoute_WhenRouteUnderIdExist_ReturnOkResult()
        {
            var result = await _routesController.GetRoute(1);

            Assert.IsInstanceOf<OkObjectResult>(result);
        }

        [Test]
        public async Task GetRoute_WhenRouteUnderIdExist_ReturnRoute()
        {
            var okResult = await _routesController.GetRoute(1) as OkObjectResult;
            var result = okResult.Value as RoutesRouteDto;

            Assert.IsInstanceOf<RoutesRouteDto>(result);
        }

        [Test]
        public async Task GetRoute_WhenRouteUnderIdDontExist_ReturnsNotFound()
        {
            var result = await _routesController.GetRoute(111);

            Assert.IsInstanceOf<NotFoundResult>(result);
        }

        #endregion

        #region Post

        [Test]
        public async Task PostRoute_ObjectIsValid_ReturnsCreatedResponse()
        {
            var routeToPost = new Route
            {
                Id = 5,
                Name = "NewRoute",
                CarrierId = 1
            };

            var result = await _routesController.PostRoute(routeToPost);

            Assert.IsInstanceOf<CreatedAtActionResult>(result);
        }

        [Ignore("Returning Created at, but in real use throwing exception")]
        [Test]
        public async Task PostRoute_WithoutRouteId_ReturningBadRequest()
        {
            var routeToPost = new Route
            {
                Id = 5,
                Name = "NewRoute"
            };

            var result = await _routesController.PostRoute(routeToPost);

            Assert.IsInstanceOf<BadRequestObjectResult>(result);
        }

        #endregion

        #region Delete

        [Test]
        public async Task DeleteRoute_WhenRouteExist_ReturnsOkResult()
        {
            var result = await _routesController.DeleteRoute(1);

            Assert.IsInstanceOf<OkResult>(result);
        }

        [Test]
        public async Task DeleteRoute_WhenRouteDontExist_ReturnsNotFound()
        {
            var result = await _routesController.DeleteRoute(1920);

            Assert.IsInstanceOf<NotFoundResult>(result);
        }


        #endregion


    }
}
