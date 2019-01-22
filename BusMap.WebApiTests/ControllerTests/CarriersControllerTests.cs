using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using BusMap.WebApi.Controllers;
using BusMap.WebApi.Data;
using BusMap.WebApi.DatabaseModels;
using BusMap.WebApi.Dto.Carriers;
using BusMap.WebApi.Repositories.Implementations;
using BusMap.WebApi.Services.Implementations;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using NUnit.Framework;

namespace BusMap.WebApiTests.ControllerTests
{
    [TestFixture]
    public class CarriersControllerTests : ControllerTestAbstractClass
    {
        private CarriersController _carriersController;
        private CarriersController _carriersControllerEmpty;

        [SetUp]
        public void SetUp()
        {
            var repository = new CarrierRepository(Context);
            var repositoryEmpty = new CarrierRepository(ContextEmpty);

            var service = new CarrierService(repository, Mapper);
            var serviceEmpty = new CarrierService(repositoryEmpty, Mapper);

            _carriersController = new CarriersController(service);
            _carriersControllerEmpty = new CarriersController(serviceEmpty);
        }


        #region GetTests
        [Test]
        public async Task GetAll_WhenCarriersExist_ReturnsOkObjectResult()
        {
            var result = await _carriersController.GetAllCarriers();

            Assert.IsInstanceOf<OkObjectResult>(result);
        }

        [Ignore("missing async implementation")]
        [Test]
        public async Task GetAll_WhenCarriersExist_ReturnsListOfCarriers()
        {
            var okResult = await _carriersController.GetAllCarriers() as OkObjectResult;
            var result = okResult.Value as List<CarriersCarrierDto>;

            Assert.IsInstanceOf<List<Carrier>>(result);
        }

        [Test]
        public async Task GetAll_WhenCarriersDontExist_ReturnsNotFound()
        {
            var result = await _carriersControllerEmpty.GetAllCarriers();

            Assert.IsInstanceOf<IActionResult>(result);
            Assert.IsInstanceOf<NotFoundResult>(result);
        }

        [Test]
        public async Task GetCarrier_WhenCarrierUnderIdExist_ReturnsOkObjectResult()
        {
            var result = await _carriersController.GetCarrier(1);

            Assert.IsInstanceOf<OkObjectResult>(result);
        }

        [Test]
        public async Task GetCarrier_WhenCarrierUnderIdExist_ReturnsCarrier()
        {
            var okResult = await _carriersController.GetCarrier(1) as OkObjectResult;
            var result = okResult.Value as CarriersCarrierDto;

            Assert.IsInstanceOf<CarriersCarrierDto>(result);
        }

        [Test]
        public async Task GetCarrier_WhenCarrierUnderIdDontExist_ReturnsNotFound()
        {
            var result = await _carriersController.GetCarrier(1920);

            Assert.IsInstanceOf<NotFoundResult>(result);
        }

        #endregion

        #region PostTests
        //TODO:Post tests 


        #endregion

        #region DeleteTests

        [Test]
        public async Task DeleteCarrier_WhenCarrierExist_ReturnsOkResult()
        {
            var result = await _carriersController.DeleteCarrier(1);

            Assert.IsInstanceOf<OkResult>(result);
        }

        [Test]
        public async Task DeleteCarrier_WhenCarrierNotExist_ReturnsNotFound()
        {
            var result = await _carriersController.DeleteCarrier(1920);

            Assert.IsInstanceOf<NotFoundResult>(result);
        }

        #endregion



    }
}
