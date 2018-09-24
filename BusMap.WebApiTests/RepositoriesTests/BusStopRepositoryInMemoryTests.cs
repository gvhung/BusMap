using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using BusMap.WebApi.Data;
using BusMap.WebApi.Models;
using BusMap.WebApi.Repositories.Abstract;
using BusMap.WebApi.Repositories.Implementations;
using Microsoft.EntityFrameworkCore;
using Moq;
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
            var result = busStopRepository.GetBusStop(2);

            Assert.IsNotNull(result);
            Assert.AreEqual("TestAddress2", result.Address);
            Assert.AreEqual(15.0, result.Latitude);
        }

        [Test]
        public void Get_WhenIdNotExists_ReturnsNull()
        {
            var result = busStopRepository.GetBusStop(7);

            Assert.IsNull(result);
        }

        [Test]
        public void Get_IncludeRoute_IsPossibleToGetRoute()
        {
            var busStop = busStopRepository.GetBusStop(1);
            var result = busStop.Route;

            Assert.IsNotNull(result);
            Assert.AreEqual("RouteName1", result.Name);
        }

        [Test]
        public void Get_IncludeRouteCarrier_IsPossibleToGetCarrier()
        {
            var busStop = busStopRepository.GetBusStop(1);
            var result = busStop.Route.Carrier;

            Assert.IsNotNull(result);
            Assert.AreEqual("CarrierName1", result.Name);
        }

        [Test]
        public void GetAll_ReturningALlBusStops()
        {
            var nOfStopsFromContext = context.BusStops.ToList().Count;
            var result = busStopRepository.GetAllBusStops();

            Assert.AreEqual(nOfStopsFromContext, result.Count());
        }

        [Test]
        public void GetAll_WhenRepositoryHavntAnyBusStops_ReturnsIsEmpty()
        {
            var busStopRepositoryMock = new Mock<IBusStopRepository>();

            var result = busStopRepositoryMock.Object.GetAllBusStops();
            Assert.IsEmpty(result);
        }
        #endregion


        #region AddTets  
        [Test]
        public void Add_WhenDataIsCorrectAndCarrierAndRouteExist_AddingBusStopWithoutChangingRoutesAndCarriers()
        {
            var nOfBusStopsBefore = context.BusStops.ToList().Count;
            var nOfCarriersBefore = context.Carriers.ToList().Count;
            var nOfRoutesBefore = context.Routes.ToList().Count;

            var busStopToAdd = new BusStop
            {
                Id = 8,
                Latitude = 100.0,
                Longitude = 200.0,
                Address = "TestAddressAdd",
                Label = "TestLabelAdd",
                Route = new Route
                {
                    Id = 1,
                    Name = "RouteName1",
                    Carrier = new Carrier
                    {
                        Id = 1,
                        Name = "CarrierName1"
                    }
                }
            };

            busStopRepository.AddBusStop(busStopToAdd);
            var result = context.BusStops.Last();
            var nOfBusStopsAfter = context.BusStops.ToList().Count;
            var nOfCarriersAfter = context.Carriers.ToList().Count;
            var nOfRoutesAfter = context.Routes.ToList().Count;

            Assert.IsTrue(context.BusStops.Contains(busStopToAdd));
            Assert.IsTrue(nOfBusStopsAfter == nOfBusStopsBefore + 1);
            Assert.AreEqual(nOfRoutesBefore, nOfRoutesAfter);
            Assert.AreEqual(nOfCarriersBefore, nOfCarriersAfter);

            Assert.AreEqual(200.0, result.Longitude);
            Assert.AreEqual(busStopToAdd, result);
        }

        [Test]
        public void Add_WhenDataIsCorrectAndCarrierAndRouteDontExist_AddingBusStopCarrierAndRoute()
        {
            var nOfBusStopsBefore = context.BusStops.ToList().Count;
            var nOfCarriersBefore = context.Carriers.ToList().Count;
            var nOfRoutesBefore = context.Routes.ToList().Count;
            var busStopToAdd = new BusStop
            {
                Id = 8,
                Latitude = 100.0,
                Longitude = 200.0,
                Address = "TestAddressAdd",
                Label = "TestLabelAdd",
                Route = new Route
                {
                    Id = 10,
                    Name = "RouteNameTest",
                    Carrier = new Carrier
                    {
                        Id = 10,
                        Name = "CarrierNameTest"
                    }
                }
            };

            busStopRepository.AddBusStop(busStopToAdd);
            var resultBusStop = context.BusStops.Last();
            var resultCarrierList = context.Carriers.ToList();
            var resultRoutesList = context.Routes.ToList();
            var nOfBusStopsAfter = context.BusStops.ToList().Count;
            var nOfCarriersAfter = context.Carriers.ToList().Count;
            var nOfRoutesAfter = context.Routes.ToList().Count;

            Assert.IsTrue(context.BusStops.Contains(busStopToAdd));
            Assert.AreEqual(busStopToAdd, resultBusStop);

            Assert.IsTrue(nOfBusStopsAfter == nOfBusStopsBefore + 1);
            Assert.AreEqual(nOfRoutesBefore + 1, nOfRoutesAfter);
            Assert.AreEqual(nOfCarriersBefore + 1, nOfCarriersAfter);
            
            Assert.AreEqual("RouteNameTest", resultRoutesList.Last().Name);
            Assert.AreEqual("CarrierNameTest", resultCarrierList.Last().Name);
        }

        [Test]
        public void Add_AddingBusStopWithoutRoute_ThrowingException()
            => Assert.Throws<InvalidOperationException>(() => 
                busStopRepository.AddBusStop(new BusStop
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
            busStopRepository.AddBusStop(busStop);

            Assert.IsTrue(context.BusStops.Count() == 4);
        }
        #endregion

        #region Remove
        [Test]
        public void Remove_WhenBusStopExists_RemovingBusStop()
        {
            var busStop = busStopRepository.GetBusStop(2);

            busStopRepository.RemoveBusStop(busStop);
            var result = context.BusStops;

            Assert.IsFalse(result.Contains(busStop));
        }

        [Test]
        public void Remove_WhenBusStopUnderIdNotExist_ThrowingException()
        {
            var busStopToRemove = busStopRepository.GetBusStop(7);

            Assert.Throws<ArgumentNullException>(() => busStopRepository.RemoveBusStop(busStopToRemove));
        }
            //=> Assert.Throws<InvalidOperationException>(() => 
            //    busStopRepository.RemoveBusStop(7));

        [Test]
        public void Remove_WhenBusStopExists_DontRemovingCarrier()
        {
            var nOfBusStops = context.BusStops.ToList().Count;
            var before = context.Carriers.ToList();
            var busStopToRemove = busStopRepository.GetBusStop(3);
            busStopRepository.RemoveBusStop(busStopToRemove);
            var result = context.Carriers.ToList();

            Assert.AreEqual(nOfBusStops - 1, context.BusStops.ToList().Count);
            Assert.IsTrue(before.Count == result.Count);
        }

        [Test]
        public void Remove_WhenBusStopExists_DontRemovingRoute()
        {
            var nOfBusStops = context.BusStops.ToList().Count;
            var before = context.Routes.ToList();
            var busStopToRemove = busStopRepository.GetBusStop(3);
            busStopRepository.RemoveBusStop(busStopToRemove);
            var result = context.Routes.ToList();

            Assert.AreEqual(nOfBusStops - 1, context.BusStops.ToList().Count);
            Assert.IsTrue(before.Count == result.Count);
        }
        #endregion


    }
}
