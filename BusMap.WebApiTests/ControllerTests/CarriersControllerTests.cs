//using System;
//using System.Collections.Generic;
//using System.Text;
//using BusMap.WebApi.Controllers;
//using BusMap.WebApi.Data;
//using BusMap.WebApi.DatabaseModels;
//using BusMap.WebApi.Repositories.Implementations;
//using Microsoft.AspNetCore.Mvc;
//using Microsoft.EntityFrameworkCore;
//using NUnit.Framework;

//namespace BusMap.WebApiTests.ControllerTests
//{
//    [TestFixture]
//    public class CarriersControllerTests
//    {
//        private CarriersController _carriersController;
//        private CarriersController _carriersControllerEmpty;

//        [SetUp]
//        public void SetUp()
//        {
//            var options = new DbContextOptionsBuilder<DatabaseContext>()
//                .UseInMemoryDatabase(databaseName: "TestDb")
//                .UseInMemoryDatabase(Guid.NewGuid().ToString())
//                .Options;
//            var context = new DatabaseContext(options);
//            var repository = new CarrierRepository(context);

//            var optionsEmpty = new DbContextOptionsBuilder<DatabaseContext>()
//                .UseInMemoryDatabase(databaseName: "TestDb")
//                .UseInMemoryDatabase(Guid.NewGuid().ToString())
//                .Options;
//            var emptyContext = new DatabaseContext(optionsEmpty);
//            var emptyRepository = new CarrierRepository(emptyContext);

//            repository.AddCarrierRangeAsync(new List<Carrier>
//            {
//                new Carrier
//                {
//                    Id = 1,
//                    Name = "Carrier1"
//                },
//                new Carrier
//                {
//                    Id = 2,
//                    Name = "Carrier2"
//                }
//            });


//            //_carriersController = new CarriersController(repository);
//            //_carriersControllerEmpty = new CarriersController(emptyRepository);
//        }


//        #region GetTests
//        [Test]
//        public void GetAll_WhenCarriersExist_ReturnsOkObjectResult()
//        {
//            var result = _carriersController.GetAll();

//            Assert.IsInstanceOf<OkObjectResult>(result);
//        }

//        [Ignore("missing async implementation")]
//        [Test]
//        public void GetAll_WhenCarriersExist_ReturnsListOfCarriers()
//        {
//            //var okResult = _carriersController.GetAll() as OkObjectResult;
//            //var result = okResult.Value as List<Carrier>;

//            //Assert.IsInstanceOf<List<Carrier>>(result);
//        }

//        [Test]
//        public void GetAll_WhenCarriersDontExist_ReturnsNotFound()
//        {
//            var result = _carriersControllerEmpty.GetAll();

//            Assert.IsInstanceOf<IActionResult>(result);
//            Assert.IsInstanceOf<NotFoundResult>(result);
//        }

//        [Test]
//        public void GetCarrier_WhenCarrierUnderIdExist_ReturnsOkObjectResult()
//        {
//            var result = _carriersController.GetCarrier(2);

//            Assert.IsInstanceOf<OkObjectResult>(result);
//        }

//        [Test]
//        public async void GetCarrier_WhenCarrierUnderIdExist_ReturnsCarrier()
//        {
//            var okResult = await _carriersController.GetCarrier(1) as OkObjectResult;
//            var result = okResult.Value as Carrier;

//            Assert.IsInstanceOf<Carrier>(result);
//        }

//        [Test]
//        public void GetCarrier_WhenCarrierUnderIdDontExist_ReturnsNotFound()
//        {
//            var result = _carriersController.GetCarrier(1920);

//            Assert.IsInstanceOf<NotFoundResult>(result);
//        }

//        #endregion

//        #region PostTests
//        //TODO:Post tests 


//        #endregion

//        #region DeleteTests

//        [Test]
//        public void DeleteCarrier_WhenCarrierExist_ReturnsOkResult()
//        {
//            var result = _carriersController.DeleteCarrier(2);

//            Assert.IsInstanceOf<OkResult>(result);
//        }

//        [Test]
//        public void DeleteCarrier_WhenCarrierNotExist_ReturnsNotFound()
//        {
//            var result = _carriersController.DeleteCarrier(1920);

//            Assert.IsInstanceOf<NotFoundResult>(result);
//        }

//        #endregion



//    }
//}
