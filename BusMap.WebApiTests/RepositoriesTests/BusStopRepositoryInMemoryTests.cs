using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using BusMap.WebApi.Data;
using BusMap.WebApi.Models;
using BusMap.WebApi.Repositories.Implementations;
using Microsoft.EntityFrameworkCore;
using NUnit.Framework;
using NUnit.Framework.Internal;

namespace BusMap.WebApiTests.RepositoriesTests
{
    [TestFixture]
    public class BusStopRepositoryInMemoryTests : RepositoryTestAbstractClass
    {
        #region GetTests   
        [Test]
        public void Get_WhenIdExists_ReturnsBusStop()
        {
            var result = repository.Get(2);

            Assert.IsNotNull(result);
            Assert.AreEqual("TestAddress2", result.Address);
            Assert.AreEqual(15.0, result.Latitude);
        }

        [Test]
        public void Get_WhenIdNotExists_ReturnsException()
            => Assert.Throws<InvalidOperationException>(() => repository.Get(4));

        [Test]
        public void Get_IncludeRoute_IsPossibleToGetRoute()
        {
            var busStop = repository.Get(1);
            var result = busStop.Route;

            Assert.IsNotNull(result);
            Assert.AreEqual("RouteName", result.Name);
        }

        [Test]
        public void Get_IncludeRouteCarrier_IsPossibleToGetCarrier()
        {
            var busStop = repository.Get(1);
            var result = busStop.Route.Carrier;

            Assert.IsNotNull(result);
            Assert.AreEqual("CarrierName", result.Name);
        }
        #endregion


        #region AddTets  
        [Test]
        public void Add_AddingBusStopWithoutRoute_ThrowingException()
            => Assert.Throws<InvalidOperationException>(() => 
                repository.Add(new BusStop
                {
                    Label = "Label",
                    Address = "Address",
                    Latitude = 1.0,
                    Longitude = 1.5,
                }));

        [Ignore("Wait for implementation")]
        [Test]
        public void Add_WhenAddressIsNull_ThrowingException()
        {
            var busStop = new BusStop
            {
                Id = 4,
                Label = "Label",
                Longitude = 0,
                Latitude = 2.0,
                Route = new Route
                {
                    Id = 2,
                    Name = "Route",
                    Carrier = new Carrier
                    {
                        Id = 2,
                        Name = "Name"
                    }
                }
            };
            repository.Add(busStop);

            Assert.IsTrue(context.BusStops.Count() == 4);
        }
        #endregion

        #region Remove
        [Test]
        public void Remove_WhenBusStopExists_RemovingBusStop()
        {
            var busStop = repository.Get(2);

            repository.Remove(busStop.Id);
            var result = context.BusStops;

            Assert.IsFalse(result.Contains(busStop));
        }

        [Test]
        public void Remove_WhenBusStopUnderIdNotExist_ThrowingException()
            => Assert.Throws<InvalidOperationException>(() => repository.Remove(4));

        #endregion


    }
}
