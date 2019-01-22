using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using BusMap.WebApi.Controllers;
using BusMap.WebApi.Dto.AzureMaps;
using Microsoft.AspNetCore.Mvc;
using NUnit.Framework;

namespace BusMap.WebApiTests.ControllerTests
{
    [TestFixture]
    public class AzureMapsControllerTests
    {
        private readonly AzureMapsController _controller;

        public AzureMapsControllerTests()
        {
            _controller = new AzureMapsController();
        }


        /// <summary>
        /// Testing if app can connect with azure maps api.
        /// Computer needs internet connection.
        /// </summary>
        /// <returns>Test result</returns>
        [Test]
        public async Task GetCityNameForLatLong_WhenDataIsCorrect_ReturningRzeszow()
        {
            var response = await _controller.GetCityNameForLatLong("50.042131,22.003429");
            var resultObject = response as OkObjectResult;
            var result = resultObject?.Value as AzureMapsReversedGeocodeDto;

            Assert.IsInstanceOf<OkObjectResult>(response);
            Assert.AreEqual("Rzeszów", result.City);
            Assert.AreEqual("Artura Grottgera", result.Street);
        }

        [Test]
        public async Task GetCityNameForLatLong_WhenQueryHavntSeparator_ReturnsBadRequestObjectResult()
        {
            var response = await _controller.GetCityNameForLatLong("50.04213122.003429");
            var responseObject = response as BadRequestObjectResult;
            var responseString = responseObject.Value as string;

            Assert.IsInstanceOf<BadRequestObjectResult>(response);
            Assert.AreEqual("Qury must contain two double/int values, representing coordinates", responseString);
        }

        [Test]
        public async Task GetCityNameForLatLong_WhenQueryContainsLetter_ReturnsBadRequestObjectResult()
        {
            var response = await _controller.GetCityNameForLatLong("50.04a131,22.003429");
            var responseObject = response as BadRequestObjectResult;
            var responseString = responseObject.Value as string;

            Assert.IsInstanceOf<BadRequestObjectResult>(response);
        }

        [Test]
        public async Task GetCityNameForLatLong_WhenQueryContainsCityName_ReturnsBadRequestObjectResult()
        {
            var response = await _controller.GetCityNameForLatLong("Warszawa");

            Assert.IsInstanceOf<BadRequestObjectResult>(response);
        }


    }
}
