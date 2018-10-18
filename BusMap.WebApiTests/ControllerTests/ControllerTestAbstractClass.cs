using AutoMapper;
using BusMap.WebApi.Automapper;
using BusMap.WebApi.Data;
using BusMap.WebApi.DatabaseModels;
using Microsoft.EntityFrameworkCore;
using NUnit.Framework;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace BusMap.WebApiTests.ControllerTests
{
    [TestFixture]
    public class ControllerTestAbstractClass
    {
        protected IMapper Mapper;
        protected DatabaseContext Context;
        protected DatabaseContext ContextEmpty;


        public ControllerTestAbstractClass()
        {
            AutoMapperSetup();
        }

        private void AutoMapperSetup()
        {
            var config = new AutoMapper.MapperConfiguration(cfg =>
            {
                cfg.AddProfile(new AutomapperProfile());
            });
            Mapper = config.CreateMapper();
        }

        [SetUp]
        public void SetUp()
        {
            SetupInMemoryDatabase();
            SeedInMemoryDatabase();
        }


        public void SetupInMemoryDatabase()
        {
            var options = new DbContextOptionsBuilder<DatabaseContext>()
                .UseInMemoryDatabase(databaseName: "TestDb")
                .UseInMemoryDatabase(Guid.NewGuid().ToString())
                .Options;
            Context = new DatabaseContext(options);
            
            var optionsEmpty = new DbContextOptionsBuilder<DatabaseContext>()
                .UseInMemoryDatabase(databaseName: "TestDb")
                .UseInMemoryDatabase(Guid.NewGuid().ToString())
                .Options;
            ContextEmpty = new DatabaseContext(optionsEmpty);
        }

        public void SeedInMemoryDatabase()
        {
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

            Context.BusStops.AddRange(new List<BusStop>
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

            Context.SaveChanges();
        }

    }
}
