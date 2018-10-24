using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using BusMap.WebApi.DatabaseModels;
using Microsoft.EntityFrameworkCore;
using NUnit.Framework;

namespace BusMap.WebApiTests.RepositoriesTests
{
    [TestFixture]
    public class CarrierRepositoryInMemoryTests : RepositoryTestAbstractClass
    {

        #region GetCarrierTests    
        [Test]
        public async Task GetCarrier_WhenIdExists_ReturnsCarrier()
        {
            var result1 = await carrierRepository.GetCarrierAsync(1);
            var result2 = await carrierRepository.GetCarrierAsync(2);

            Assert.IsNotNull(result1);
            Assert.IsNotNull(result2);
            Assert.AreEqual("CarrierName1", result1.Name);
            Assert.AreEqual("CarrierName2", result2.Name);
        }

        [Test]
        public async Task GetCarrier_GettingRoutesWhenHaveSome_IsPossibleToGetRoute()
        {
            var result = await carrierRepository.GetCarrierAsync(1);
            var resultRoutes = result.Routes.ToList();

            Assert.IsNotNull(resultRoutes);
            Assert.AreNotEqual(resultRoutes[0], resultRoutes[1]);
            Assert.AreEqual("TestLabel1", resultRoutes[0].BusStops.ToList()[0].Label);
        }

        [Test]
        public async Task GetCarrier_WhenIdNotExists_ReturnsNull()
        {
            var result = await carrierRepository.GetCarrierAsync(1280);

            Assert.IsNull(result);
        }

        [Test]
        public async Task GetAllCarriers_ReturningAllCarriers()
        {
            var nOfCarriersFromContext = context.Carriers.ToList().Count;
            var result = await carrierRepository.GetAllCarriersAsync();

            Assert.NotNull(result);
            Assert.AreEqual(nOfCarriersFromContext, result.ToList().Count);
        }
        #endregion

        #region AddCarrierTests

        [Test]
        public async Task AddCarrier_WithoutRoutes_IsPossibleToAddCarrierWithoutRoutes()
        {
            var carrierToAdd = new Carrier
            {
                Id = 10,
                Name = "JustTest",
            };

            await carrierRepository.AddCarrierAsync(carrierToAdd);
            var result = context.Carriers
                .Include(x => x.Routes)
                .First(x => x.Id == 10);


            Assert.IsTrue(context.Carriers.Contains(carrierToAdd));
            Assert.AreEqual("JustTest", result.Name);
            Assert.IsEmpty(result.Routes);
        }

        [Test]
        public async Task AddCarrier_WithRoute_IsPossibleToAddCarrierWithRoute()
        {
            var busStop1 = new BusStop
            {
                Id = 21,
                Latitude = 500.0,
                Longitude = 700.0,
                Address = "TestRouteAddress1",
                Label = "TestRouteLabel1"
            };
            var busStop2 = new BusStop
            {
                Id = 22,
                Latitude = 1000.0,
                Longitude = 2000.0,
                Address = "TestRouteAddress2",
                Label = "TestRouteLabel2"
            };
            var routeToAdd = new Route
            {
                Id = 10,
                Name = "TestRoute",
                Carrier = context.Carriers.First(),
                BusStops = new List<BusStop>
                {
                    busStop1,
                    busStop2
                }
            };
            var carrierToAdd = new Carrier
            {
                Id = 10,
                Name = "JustTest",
                Routes = new List<Route>() { routeToAdd }
            };

            await carrierRepository.AddCarrierAsync(carrierToAdd);
            var result = context.Carriers
                .Include(x => x.Routes)
                .First(x => x.Id == 10);

            Assert.IsNotNull(result);
            Assert.IsTrue(result.Routes.Contains(routeToAdd));
            Assert.IsTrue(result.Routes.ToList()[0]
                .BusStops.ToList()[0]
                .Label.Equals("TestRouteLabel1"));
        }
        #endregion

        #region RemoveCarrierTests

        [Test]
        public async Task RemoveCarrier_WhenCarrierUnderIdExists_RemovingCarrier()
        {
            var nOfCarriersBefore = context.Carriers.ToList().Count;
            var carrierToRemove = await carrierRepository.GetCarrierAsync(1);
            await carrierRepository.RemoveCarrierAsync(carrierToRemove);
            var nOfCarriersAfter = context.Carriers.ToList().Count;

            Assert.AreEqual(nOfCarriersBefore - 1, nOfCarriersAfter);
            Assert.IsFalse(context.Carriers.Contains(carrierToRemove));
        }

        [Test]
        public async Task RemoveCarrier_WhenCarrierUnderIdDontExists_ThrowingException()
        {
            var carrierToRemove = await carrierRepository.GetCarrierAsync(1920);

            Assert.ThrowsAsync<ArgumentNullException>(async () =>
                await carrierRepository.RemoveCarrierAsync(carrierToRemove));
        }

        [Test]
        public async Task RemoveCarrier_WhenCarrierUnderIdExists_AlsoRemovingRoutes()
        {
            var nOfRoutesBefore = context.Routes.ToList().Count;
            var carrierToRemove = await carrierRepository.GetCarrierAsync(1);
            await carrierRepository.RemoveCarrierAsync(carrierToRemove);
            var resultRoutes = context.Routes.ToList();
            var nOfRoutesAfter = resultRoutes.Count;

            Assert.AreEqual(nOfRoutesBefore - 2, nOfRoutesAfter);
        }
        #endregion

    }
}
