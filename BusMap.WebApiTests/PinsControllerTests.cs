using System;
using System.Collections.Generic;
using BusMap.WebApi.Controllers;
using BusMap.WebApi.Models;
using BusMap.WebApi.Repositories.Abstract;
using Microsoft.AspNetCore.Mvc;
using NUnit.Framework;

namespace BusMap.WebApiTests
{
    [TestFixture]
    public class PinsControllerTests
    {
        private  PinsController _pinsController;
        private  IPinRepository _pinRepository;

        [SetUp]
        public void Setup()
        {
            _pinRepository = new PinRepositoryForTests();
            _pinsController = new PinsController(_pinRepository);
        }

        #region GetAll

        [Test]
        public void GetAll_WhenCalled_ReturnsOkResult()
        {
            var okResult = _pinsController.GetAll();

            Assert.IsInstanceOf<IActionResult>(okResult);
            Assert.IsInstanceOf<OkObjectResult>(okResult);
        }

        [Test]
        public void GetAll_WhenCalled_ReturnsListOfPinsObjectType()
        {
            var result = _pinsController.GetAll() as OkObjectResult;
            var resultPins = result.Value as List<Pin>;

            Assert.IsInstanceOf<List<Pin>>(resultPins);
        }

        [Test]
        public void GetAll_WhenCalled_ReturningAllPins()
        {
            IActionResult actionResult = _pinsController.GetAll();
            OkObjectResult result = actionResult as OkObjectResult;
            List<Pin> resultPins = result.Value as List<Pin>;

            Assert.IsInstanceOf<IEnumerable<Pin>>(resultPins);
            Assert.IsTrue(resultPins.Count == 3);

            int nOfPins = resultPins.Count;
            for (int i = 0; i < nOfPins; i++)
            {
                for (int j = 0; j < nOfPins; j++)
                {
                    if (i != j)
                    {
                        Assert.AreNotEqual(resultPins[i], resultPins[j]);
                    }
                }
            }
        }

        #endregion

        #region PostPin

        [Test]
        public void PostPin_ValidObjectPassed_ReturnsCreatedResponse()
        {
            var pin = new Pin()
            {
                Label = "AdditionTest",
                Address = "Test",
                Longitude = 23,
                Latitude = -20
            };

            var result = _pinsController.PostPin(pin);

            Assert.IsInstanceOf<CreatedAtActionResult>(result);
        }

        [Test]
        public void PostPin_ValidObjectPassed_ReturnsCreatedPinObject()
        {
            var pin = new Pin()
            {
                Label = "AdditionTest",
                Address = "Test",
                Longitude = 23,
                Latitude = -20
            };

            var result = _pinsController.PostPin(pin) as CreatedAtActionResult;
            var resultPin = result.Value as Pin;

            Assert.IsInstanceOf<Pin>(resultPin);
            Assert.AreEqual(-20, resultPin.Latitude);
            Assert.AreEqual("AdditionTest", resultPin.Label);
        }



        //TODO: Add case with null Label after dbUpdate

        #endregion



        [Test]
        public void PostPinAndGetPins_ValidObjectPassed_ReturnsBiggerBy1Collection()
        {
            var pin = new Pin()
            {
                Label = "AdditionTest",
                Address = "Test",
                Longitude = 23,
                Latitude = -20
            };

            _pinsController.PostPin(pin);
            var okResult = _pinsController.GetAll() as OkObjectResult;
            var resultPins = okResult.Value as List<Pin>;

            Assert.IsInstanceOf<List<Pin>>(resultPins);
            Assert.AreEqual(4, resultPins.Count);
        }


    }
}
