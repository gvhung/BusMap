using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using BusMap.WebApi.Data;
using BusMap.WebApi.Models;
using BusMap.WebApi.Repositories.Implementations;
using Microsoft.EntityFrameworkCore;
using NUnit.Framework;

namespace BusMap.WebApiTests.RepositoriesTests
{
    [TestFixture]
    public abstract class RepositoryTestAbstractClass
    {
        protected  BusStopRepository repository;
        protected DatabaseContext context;

        [SetUp]
        public void SetUp()
        {
            var options = new DbContextOptionsBuilder<DatabaseContext>()
                .UseInMemoryDatabase(databaseName: "TestDb")
                .UseInMemoryDatabase(Guid.NewGuid().ToString())
                .Options;
            context = new DatabaseContext(options);
            repository = new BusStopRepository(context);

            var routeForTest = new Route
            {
                Id = 1,
                Name = "RouteName",
                Carrier = new Carrier
                {
                    Id = 1,
                    Name = "CarrierName"
                }
            };

            repository.AddRange(new List<BusStop>
            {
                new BusStop
                {
                    Id = 1,
                    Latitude = 5.0,
                    Longitude = 10.0,
                    Address = "TestAddress1",
                    Label = "TestLabel1",
                    Route = routeForTest
                },
                new BusStop
                {
                    Id = 2,
                    Latitude = 15.0,
                    Longitude = 20.0,
                    Address = "TestAddress2",
                    Label = "TestLabel2",
                    Route = routeForTest
                },
                new BusStop
                {
                    Id = 3,
                    Latitude = 25.0,
                    Longitude = 30.0,
                    Address = "TestAddress3",
                    Label = "TestLabel3",
                    Route = routeForTest
                }
            });
        }

        [Test]
        public void InMemoryDatabaseInitializationTest()
        {
            var result = repository.GetAll().Count();
            Assert.IsTrue(repository.GetAll().Count() == 3);
        }

        [Test]
        public void InMemoryDatabaseInitializationTest2()
        {
            Assert.IsTrue(repository.GetAll().Count() == 3);
            Assert.AreEqual("TestLabel1", repository.Get(1).Label);
        }


    }
}
