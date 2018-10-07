using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using BusMap.WebApi.DatabaseModels;
using Microsoft.EntityFrameworkCore;
// ReSharper disable All

namespace BusMap.WebApi.Data
{
    public class DatabaseContext : DbContext
    {
        public DatabaseContext(DbContextOptions<DatabaseContext> options)
            : base(options)
        {
        }

        public DatabaseContext()
        { 
        }


        public DbSet<BusStop> BusStops { get; set; }
        public DbSet<Route> Routes { get; set; }
        public DbSet<Carrier> Carriers { get; set; }



        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Carrier>().HasData(
                new Carrier
                {
                    Id = 1,
                    Name = "Nowex Transport",
                },
                new Carrier
                {
                    Id = 2,
                    Name = "Kolos"
                }
            );

            modelBuilder.Entity<Route>().HasData(
                new Route
                {
                    Id = 1,
                    Name = "Gorlice - Rzeszów",
                    CarrierId = 1
                },
                new Route
                {
                    Id = 2,
                    Name = "Rzeszów - Gorlice",
                    CarrierId = 1
                },
                new Route
                {
                    Id = 3,
                    Name = "Frysztak - Rzeszów",
                    CarrierId = 2
                }
            );

            //route1_busStops
            modelBuilder.Entity<BusStop>().HasData(
                new BusStop
                {
                    Id = 1,
                    RouteId = 1,
                    Address = "Gorlice",
                    Label = "Gorlice DA",
                    Latitude = 49.662932,
                    Longitude = 21.159447
                },
                new BusStop
                {
                    Id = 2,
                    RouteId = 1,
                    Address = "Jasło",
                    Label = "Jasło DA",
                    Latitude = 49.743750,
                    Longitude = 21.473399
                },
                new BusStop
                {
                    Id = 3,
                    RouteId = 1,
                    Address = "Frysztak",
                    Latitude = 49.845480,
                    Longitude = 21.612531
                },
                new BusStop
                {
                    Id = 4,
                    RouteId = 1,
                    Address = "Wiśniowa",
                    Latitude = 49.869611,
                    Longitude = 21.659950
                },
                new BusStop
                {
                    Id = 5,
                    RouteId = 1,
                    Address = "Dobrzechów",
                    Latitude = 49.876198,
                    Longitude = 21.753990
                },
                new BusStop
                {
                    Id = 6,
                    RouteId = 1,
                    Address = "Strzyżów",
                    Label = "Strzyżów DA",
                    Latitude = 49.869992,
                    Longitude = 21.800657
                },
                new BusStop
                {
                    Id = 7,
                    RouteId = 1,
                    Address = "Czudec",
                    Label = "Czudec DA",
                    Latitude = 49.945855,
                    Longitude = 21.837562
                },
                new BusStop
                {
                    Id = 8,
                    RouteId = 1,
                    Address = "Boguchwała",
                    Latitude = 49.983775,
                    Longitude = 21.942793
                },
                new BusStop
                {
                    Id = 9,
                    RouteId = 1,
                    Address = "Rzeszów",
                    Label = "Podkarpacka",
                    Latitude = 50.020076,
                    Longitude = 21.980312
                },
                new BusStop
                {
                    Id = 10,
                    RouteId = 1,
                    Address = "Rzeszów",
                    Label = "Rzeszów DA",
                    Latitude = 50.042131,
                    Longitude = 22.003429
                },
                new BusStop
                {
                    Id = 11,
                    RouteId = 1,
                    Address = "Rzeszów",
                    Label = "Rejtana",
                    Latitude = 50.031346,
                    Longitude = 22.016653
                }
            );

            //route2_busStops
            modelBuilder.Entity<BusStop>().HasData(
                new BusStop
                {
                    Id = 12,
                    RouteId = 2,
                    Address = "Rzeszów",
                    Label = "Rejtana",
                    Latitude = 50.030767,
                    Longitude = 22.017088
                },
                new BusStop
                {
                    Id = 13,
                    RouteId = 2,
                    Address = "Rzeszów",
                    Label = "Rzeszów DA",
                    Latitude = 50.042131,
                    Longitude = 22.003429
                },
                new BusStop
                {
                    Id = 14,
                    RouteId = 2,
                    Address = "Rzeszów",
                    Label = "Podkarpacka",
                    Latitude = 50.020076,
                    Longitude = 21.980312
                },
                new BusStop
                {
                    Id = 15,
                    RouteId = 2,
                    Address = "Boguchwała",
                    Latitude = 49.983775,
                    Longitude = 21.942793
                },
                new BusStop
                {
                    Id = 16,
                    RouteId = 2,
                    Address = "Czudec",
                    Label = "Czudec DA",
                    Latitude = 49.945855,
                    Longitude = 21.837562
                },
                new BusStop
                {
                    Id = 17,
                    RouteId = 2,
                    Address = "Strzyżów",
                    Label = "Strzyżów DA",
                    Latitude = 49.869992,
                    Longitude = 21.800657
                },
                new BusStop
                {
                    Id = 18,
                    RouteId = 2,
                    Address = "Dobrzechów",
                    Latitude = 49.876198,
                    Longitude = 21.753990
                },
                new BusStop
                {
                    Id = 19,
                    RouteId = 2,
                    Address = "Wiśniowa",
                    Latitude = 49.869611,
                    Longitude = 21.659950
                },
                new BusStop
                {
                    Id = 20,
                    RouteId = 2,
                    Address = "Frysztak",
                    Latitude = 49.845480,
                    Longitude = 21.612531
                },
                new BusStop
                {
                    Id = 21,
                    RouteId = 2,
                    Address = "Jasło",
                    Label = "Jasło DA",
                    Latitude = 49.743750,
                    Longitude = 21.473399
                },
                new BusStop
                {
                    Id = 22,
                    RouteId = 2,
                    Address = "Gorlice",
                    Label = "Gorlice DA",
                    Latitude = 49.662932,
                    Longitude = 21.159447
                }
            );

            //route3_BusStops
            modelBuilder.Entity<BusStop>().HasData(
                new BusStop
                {
                    Id = 23,
                    RouteId = 3,
                    Address = "Frysztak",
                    Latitude = 49.845480,
                    Longitude = 21.612531
                },
                new BusStop
                {
                    Id = 24,
                    RouteId = 3,
                    Address = "Wiśniowa",
                    Latitude = 49.869611,
                    Longitude = 21.659950
                },
                new BusStop
                {
                    Id = 25,
                    RouteId = 3,
                    Address = "Dobrzechów",
                    Latitude = 49.876198,
                    Longitude = 21.753990
                },
                new BusStop
                {
                    Id = 26,
                    RouteId = 3,
                    Address = "Strzyżów",
                    Label = "Strzyżów DA",
                    Latitude = 49.869992,
                    Longitude = 21.800657
                },
                new BusStop
                {
                    Id = 27,
                    RouteId = 3,
                    Address = "Zaborów",
                    Latitude = 49.914127,
                    Longitude = 21.827073
                },
                new BusStop
                {
                    Id = 28,
                    RouteId = 3,
                    Address = "Czudec",
                    Label = "Czudec DA",
                    Latitude = 49.945855,
                    Longitude = 21.837562
                },
                new BusStop
                {
                    Id = 29,
                    RouteId = 3,
                    Address = "Boguchwała",
                    Latitude = 49.983775,
                    Longitude = 21.942793
                },
                new BusStop
                {
                    Id = 30,
                    RouteId = 3,
                    Address = "Rzeszów",
                    Label = "Podkarpacka",
                    Latitude = 50.020076,
                    Longitude = 21.980312
                },
                new BusStop
                {
                    Id = 31,
                    RouteId = 3,
                    Address = "Rzeszów",
                    Label = "Rzeszów DA",
                    Latitude = 50.042131,
                    Longitude = 22.003429
                }
            );

        }

    }
}
