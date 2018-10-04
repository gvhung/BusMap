using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
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
        public void GetCarrier_WhenIdExists_ReturnsCarrier()
        {
            var result1 = carrierRepository.GetCarrier(1);
            var result2 = carrierRepository.GetCarrier(2);

            Assert.IsNotNull(result1);
            Assert.IsNotNull(result2);
            Assert.AreEqual("CarrierName1", result1.Name);
            Assert.AreEqual("CarrierName2", result2.Name);
        }

        [Test]
        public void GetCarrier_GettingRoutesWhenHaveSome_IsPossibleToGetRoute()
        {
            var result = carrierRepository.GetCarrier(1);
            var resultRoutes = result.Routes.ToList();

            Assert.IsNotNull(resultRoutes);
            Assert.AreNotEqual(resultRoutes[0], resultRoutes[1]);
            Assert.AreEqual("TestLabel1", resultRoutes[0].BusStops.ToList()[0].Label);
        }

        [Test]
        public void GetCarrier_GettingRoutesWhenHaventThem_ThrowsException()
            => Assert.Throws<ArgumentNullException>(() => carrierRepository.GetCarrier(2).Routes.ToList());

        [Test]
        public void GetCarrier_WhenIdNotExists_ReturnsNull()
        {
            var result = carrierRepository.GetCarrier(1280);

            Assert.IsNull(result);
        }

        [Test]
        public void GetAllCarriers_ReturningAllCarriers()
        {
            var nOfCarriersFromContext = context.Carriers.ToList().Count;
            var result = carrierRepository.GetAllCarriers();

            Assert.NotNull(result);
            Assert.AreEqual(nOfCarriersFromContext, result.ToList().Count);
        }
        #endregion

        #region AddCarrierTests

        [Test]
        public void AddCarrier_WithoutRoutes_IsPossibleToAddCarrierWithoutRoutes()
        {
            var carrierToAdd = new Carrier
            {
                Id = 10,
                Name = "JustTest",
            };

            carrierRepository.AddCarrier(carrierToAdd);
            var result = context.Carriers
                .Include(x => x.Routes)
                .First(x => x.Id == 10);


            Assert.IsTrue(context.Carriers.Contains(carrierToAdd));
            Assert.AreEqual("JustTest", result.Name);
            Assert.IsEmpty(result.Routes);
        }

        [Test]
        public void AddCarrier_WithRoute_IsPossibleToAddCarrierWithRoute()
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

            carrierRepository.AddCarrier(carrierToAdd);
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
        public void RemoveCarrier_WhenCarrierUnderIdExists_RemovingCarrier()
        {
            var nOfCarriersBefore = context.Carriers.ToList().Count;
            var carrierToRemove = carrierRepository.GetCarrier(1);
            carrierRepository.RemoveCarrier(carrierToRemove);
            var nOfCarriersAfter = context.Carriers.ToList().Count;

            Assert.AreEqual(nOfCarriersBefore - 1, nOfCarriersAfter);
            Assert.IsFalse(context.Carriers.Contains(carrierToRemove));
        }

        [Test]
        public void RemoveCarrier_WhenCarrierUnderIdDontExists_ThrowingException()
        {
            var carrierToRemove = carrierRepository.GetCarrier(1920);

            Assert.Throws<ArgumentNullException>(() =>
                carrierRepository.RemoveCarrier(carrierToRemove));
        }

        [Test]
        public void RemoveCarrier_WhenCarrierUnderIdExists_AlsoRemovingRoutes()
        {
            var nOfRoutesBefore = context.Routes.ToList().Count;
            var carrierToRemove = carrierRepository.GetCarrier(1);
            carrierRepository.RemoveCarrier(carrierToRemove);
            var resultRoutes = context.Routes.ToList();
            var nOfRoutesAfter = resultRoutes.Count;

            Assert.AreEqual(nOfRoutesBefore - 2, nOfRoutesAfter);
        }
        #endregion

    }
}
