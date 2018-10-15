using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using BusMap.WebApi.Data;
using BusMap.WebApi.DatabaseModels;
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
        public async Task Get_WhenIdExists_ReturnsBusStop()
        {
            var result = await busStopRepository.GetBusStopAsync(2);

            Assert.IsNotNull(result);
            Assert.AreEqual("TestAddress2", result.Address);
            Assert.AreEqual(15.0, result.Latitude);
        }

        [Test]
        public async Task Get_WhenIdNotExists_ReturnsNull()
        {
            var result = await busStopRepository.GetBusStopAsync(7);

            Assert.IsNull(result);
        }

        [Test]
        public async Task Get_IncludeRoute_IsPossibleToGetRoute()
        {
            var busStop = await busStopRepository.GetBusStopAsync(1);
            var result = busStop.Route;

            Assert.IsNotNull(result);
            Assert.AreEqual("RouteName1", result.Name);
        }

        [Test]
        public async Task Get_IncludeRouteCarrier_IsPossibleToGetCarrier()
        {
            var busStop = await busStopRepository.GetBusStopAsync(1);
            var result = busStop.Route.Carrier;

            Assert.IsNotNull(result);
            Assert.AreEqual("CarrierName1", result.Name);
        }

        [Test]
        public async Task GetAll_ReturningALlBusStops()
        {
            var stopsFromContext = await context.BusStops.ToListAsync();
            var nOfStopsFromContext = stopsFromContext.Count;
            var result = await busStopRepository.GetAllBusStopsAsync();

            Assert.AreEqual(nOfStopsFromContext, result.Count());
        }

        [Test]
        public async Task GetAll_WhenRepositoryHavntAnyBusStops_ReturnsIsEmpty()
        {
            var busStopRepositoryMock = new Mock<IBusStopRepository>();

            var result = await busStopRepositoryMock.Object.GetAllBusStopsAsync();
            Assert.IsEmpty(result);
        }
        #endregion


        #region AddTets  
        [Test]
        public async Task Add_WhenDataIsCorrectAndCarrierAndRouteExist_AddingBusStopWithoutChangingRoutesAndCarriers()
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

            await busStopRepository.AddBusStopAsync(busStopToAdd);
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
        public async Task Add_WhenDataIsCorrectAndCarrierAndRouteDontExist_AddingBusStopCarrierAndRoute()
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

            await busStopRepository.AddBusStopAsync(busStopToAdd);
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
        public async Task Add_AddingBusStopWithoutRoute_ThrowingException()
            => Assert.ThrowsAsync<InvalidOperationException>(async () =>
                await busStopRepository.AddBusStopAsync(new BusStop
                {
                    Label = "Label",
                    Address = "Address",
                    Latitude = 1.0,
                    Longitude = 1.5,
                }));


        [Test]
        public async Task Add_WhenAddressIsNull_ThrowingException()
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


            Assert.ThrowsAsync<InvalidOperationException>(async () => 
                await busStopRepository.AddBusStopAsync(busStop));
        }
        #endregion

        #region Remove
        [Test]
        public async Task Remove_WhenBusStopExists_RemovingBusStop()
        {
            var busStop = await busStopRepository.GetBusStopAsync(2);

            await busStopRepository.RemoveBusStopAsync(busStop);
            var result = context.BusStops;

            Assert.IsFalse(result.Contains(busStop));
        }

        [Test]
        public async Task Remove_WhenBusStopUnderIdNotExist_ThrowingException()
        {
            var busStopToRemove = await busStopRepository.GetBusStopAsync(7);

            Assert.ThrowsAsync<ArgumentNullException>(async () => await busStopRepository.RemoveBusStopAsync(busStopToRemove));
        }
        //=> Assert.Throws<InvalidOperationException>(() => 
        //    busStopRepository.RemoveBusStop(7));

        [Test]
        public async Task Remove_WhenBusStopExists_DontRemovingCarrier()
        {
            var nOfBusStops = context.BusStops.ToList().Count;
            var before = context.Carriers.ToList();
            var busStopToRemove = await busStopRepository.GetBusStopAsync(3);
            await busStopRepository.RemoveBusStopAsync(busStopToRemove);
            var result = context.Carriers.ToList();

            Assert.AreEqual(nOfBusStops - 1, context.BusStops.ToList().Count);
            Assert.IsTrue(before.Count == result.Count);
        }

        [Test]
        public async Task Remove_WhenBusStopExists_DontRemovingRoute()
        {
            var nOfBusStops = context.BusStops.ToList().Count;
            var before = context.Routes.ToList();
            var busStopToRemove = await busStopRepository.GetBusStopAsync(3);
            await busStopRepository.RemoveBusStopAsync(busStopToRemove);
            var result = context.Routes.ToList();

            Assert.AreEqual(nOfBusStops - 1, context.BusStops.ToList().Count);
            Assert.IsTrue(before.Count == result.Count);
        }
        #endregion


    }
}
