using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using BusMap.WebApi.DatabaseModels;
using NUnit.Framework;

namespace BusMap.WebApiTests.RepositoriesTests
{
    [TestFixture]
    public class RouteRepositoryInMemoryTests : RepositoryTestAbstractClass
    {
        #region GetRouteTests   
        [Test]
        public async Task GetRoute_WhenIdExists_ReturnsRoute()
        {
            var result1 = await routeRepository.GetRouteAsync(1);
            var result2 = await routeRepository.GetRouteAsync(2);

            Assert.IsNotNull(result1);
            Assert.AreEqual("RouteName1", result1.Name);

            Assert.IsNotNull(result2);
            Assert.AreEqual("RouteName2", result2.Name);
        }

        [Test]
        public async Task GetRoute_IncludeBusStops_InPossibleToGetBusStops()
        {
            var result1 = await routeRepository.GetRouteAsync(1);
            var result2 = await routeRepository.GetRouteAsync(2);
            var result1BusStops = result1.BusStops.ToList();
            var result2BusStops = result2.BusStops.ToList();

            Assert.IsNotNull(result1);
            Assert.AreEqual(3, result1BusStops.Count);
            Assert.AreEqual(result1BusStops, context.BusStops.Where(x => x.Route.Equals(result1)).ToList());
            Assert.AreEqual("TestLabel2", result1BusStops[1].Label);

            Assert.IsNotNull(result2);
            Assert.AreEqual(3, result2BusStops.Count);
            Assert.AreEqual(result2BusStops, context.BusStops.Where(x => x.Route.Equals(result2)));
            Assert.AreEqual("TestLabel6", result2BusStops[2].Label);
        }

        [Test]
        public async Task GetRoute_IncludeCarrier_IsPossibleToGetCarrier()
        {
            var result1 = await routeRepository.GetRouteAsync(1);
            var result2 = await routeRepository.GetRouteAsync(2);
            var result1Carrier = result1.Carrier;
            var result2Carrier = result2.Carrier;

            Assert.IsNotNull(result1);
            Assert.AreEqual("CarrierName1", result1Carrier.Name);

            Assert.IsNotNull(result2);
            Assert.AreEqual("CarrierName1", result2Carrier.Name);
        }

        [Test]
        public async Task GetRoute_WhenIdNotExists_ReturnsNull()
        {
            var route = await routeRepository.GetRouteAsync(190);

            Assert.IsNull(route);
        }

        [Test]
        public async Task GetAllRoutes_ReturningAllRoutes()
        {
            var nOfRoutesFromContext = context.Routes.ToList().Count;
            var result = await routeRepository.GetAllRoutesAsync();

            Assert.AreEqual(nOfRoutesFromContext, result.ToList().Count);
        }

        [Test]
        public async Task GetRouteIncludeAllAsync_WhenTracesExist_Returning6Traces()
        {
            var route = await routeRepository.GetRouteIncludeAllAsync(1);
            var result = new List<BusStopTrace>();

            foreach (var busStop in route.BusStops)
            {
                result.AddRange(busStop.BusStopTraces);
            }

            Assert.AreEqual(6, result.Count);
        }

        #endregion

        #region AddRouteTests

        [Test]
        public async Task AddRoute_WhenCarrierExistBusStopsAreNot_AddingNewRouteNewBusStopsWithoutNewCarrier()
        {
            var nOfBusStopsBefore = context.BusStops.ToList().Count;
            var nOfCarriersBefore = context.Carriers.ToList().Count;
            var nOfRoutesBefore = context.Routes.ToList().Count;

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

            await routeRepository.AddRouteAsync(routeToAdd);

            var result = context.Routes.Last();
            var resultList = context.Routes.ToList();
            var nOfBusStopsAfter = context.BusStops.ToList().Count;
            var nOfCarriersAfter = context.Carriers.ToList().Count;
            var nOfRoutesAfter = context.Routes.ToList().Count;

            Assert.IsTrue(resultList.Contains(routeToAdd));
            Assert.IsTrue(result.BusStops.Contains(busStop1));
            Assert.IsTrue(result.BusStops.Contains(busStop2));

            Assert.AreEqual(nOfRoutesBefore + 1, nOfRoutesAfter);
            Assert.AreEqual(nOfBusStopsBefore + 2, nOfBusStopsAfter);
            Assert.AreEqual(nOfCarriersBefore, nOfCarriersAfter);
        }

        [Ignore("Wait for implementation in EF")]
        [Test]
        public void AddRoute_AddingWithoutBusStops_ThrowingException()
        {
            var routeToAdd = new Route
            {
                Id = 15,
                Name = "Name",
                Carrier = new Carrier
                {
                    Id = 12,
                    Name = "CarrierName"
                }
            };

            Assert.ThrowsAsync<InvalidOperationException>(async () =>
                await routeRepository.AddRouteAsync(routeToAdd));
        }

        [Ignore("Wait for implementation.")]
        [Test]
        public void AddRoute_WhenNameNotSet_ThrowingException()
        {
            var routeToAdd = new Route
            {
                Id = 15,
                Carrier = new Carrier
                {
                    Id = 12,
                    Name = "CarrierName"
                }
            };

            Assert.ThrowsAsync<InvalidOperationException>(async () =>
                await routeRepository.AddRouteAsync(routeToAdd));
        }

        [Ignore("Wait for implementation.")]
        [Test]
        public void AddRoute_WhenCarrierIsNotSet_ThrowingException()
        {
            var routeToAdd = new Route
            {
                Id = 15,
                Name = "NameTest"
            };

            Assert.Throws<InvalidOperationException>(() =>
                routeRepository.AddRouteAsync(routeToAdd));
        }
        #endregion

        #region RemoveRouteTests

        [Test]
        public async Task RemoveRoute_WhenRouteUnderIdExists_RemovingRoute()
        {
            var nOfRoutesBefore = context.Routes.ToList().Count;
            var routeToRemove = await routeRepository.GetRouteAsync(2);
            await routeRepository.RemoveRouteAsync(routeToRemove);
            var nOfRoutesAfter = context.Routes.ToList().Count;

            Assert.AreEqual(nOfRoutesBefore - 1, nOfRoutesAfter);
            Assert.IsFalse(context.Routes.Contains(routeToRemove));
        }

        [Test]
        public async Task RemoveRoute_WhenRouteUnderIdDontExists_ThrowingException()
        {
            var routeToRemove = await routeRepository.GetRouteAsync(1290);

            Assert.ThrowsAsync<ArgumentNullException>(async () =>
                await routeRepository.RemoveRouteAsync(routeToRemove));
        }


        [Test]
        public async Task RemoveRoute_WhenRouteUnderIdExists_AlsoRemovingBusStops()
        {
            var nOfBusStopsBefore = context.BusStops.ToList().Count;
            var routeToRemove = await routeRepository.GetRouteAsync(1);
            await routeRepository.RemoveRouteAsync(routeToRemove);
            var nOfBusStopsAfter = context.BusStops.ToList().Count;

            Assert.AreEqual(nOfBusStopsBefore - 3, nOfBusStopsAfter);
        }

        [Test]
        public async Task RemoveRoute_WhenRouteUnderIdExists_DontRemovingCarrier()
        {
            var nOfCarriersBefore = context.Carriers.ToList().Count;
            var routeToRemove = await routeRepository.GetRouteAsync(1);
            await routeRepository.RemoveRouteAsync(routeToRemove);
            var nOfCarriersAfter = context.Carriers.ToList().Count;

            Assert.AreEqual(nOfCarriersBefore, nOfCarriersAfter);
        }

        #endregion

    }
}
