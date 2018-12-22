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
        public DbSet<BusStopQueued> BusStopsQueued { get; set; }
        public DbSet<RouteQueued> RoutesQueued { get; set; }
        public DbSet<CarrierQueued> CarriersQueued { get; set; }
        public DbSet<BusStopTrace> BusStopTraces { get; set; }
        public DbSet<RouteReport> RouteReports { get; set; }



        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<RouteQueued>()
                .HasOne(r => r.CarrierQueued)
                .WithMany(c => c.RoutesQueued)
                .IsRequired(false);

            modelBuilder.Entity<RouteQueued>()
                .HasOne(r => r.CarrierQueued)
                .WithMany(c => c.RoutesQueued)
                .OnDelete(DeleteBehavior.Cascade);
            modelBuilder.Entity<RouteQueued>()
                .HasMany(r => r.BusStopsQueued)
                .WithOne(b => b.RouteQueued)
                .OnDelete(DeleteBehavior.Cascade);

            modelBuilder.Entity<RouteQueued>()
                .Property(r => r.PositiveVotes)
                .HasDefaultValue(0);
            modelBuilder.Entity<RouteQueued>()
                .Property(r => r.NegativeVotes)
                .HasDefaultValue(0);

            modelBuilder.Entity<CarrierQueued>()
                .Property(c => c.PositiveVotes)
                .HasDefaultValue(0);
            modelBuilder.Entity<CarrierQueued>()
                .Property(c => c.NegativeVotes)
                .HasDefaultValue(0);

            modelBuilder.Entity<RouteQueued>()
                .Property(r => r.CreatedDatetime)
                .HasDefaultValueSql("FORMAT(GetDate(), 'yyyy-MM-dd')");
            modelBuilder.Entity<CarrierQueued>()
                .Property(c => c.CreatedDatetime)
                .HasDefaultValueSql("FORMAT(GetDate(), 'yyyy-MM-dd')");
            modelBuilder.Entity<RouteReport>()
                .Property(r => r.Date)
                .HasDefaultValueSql("FORMAT(GetDate(), 'yyyy-MM-dd')");

            //Todo: These 2 schould be from device, not db
            modelBuilder.Entity<BusStopTrace>()
                .Property(t => t.Date)
                .HasDefaultValueSql("FORMAT(GetDate(), 'yyyy-MM-dd')");
            modelBuilder.Entity<BusStopTrace>()
                .Property(t => t.Hour)
                .HasDefaultValueSql("Format(GETDATE(),'hh:mm')");


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
                    CarrierId = 1,
                    DayOfTheWeek = "1,2,3"
                },
                new Route
                {
                    Id = 2,
                    Name = "Rzeszów - Gorlice",
                    CarrierId = 1,
                    DayOfTheWeek = "1,2,3"
                },
                new Route
                {
                    Id = 3,
                    Name = "Frysztak - Rzeszów",
                    CarrierId = 2,
                    DayOfTheWeek = "2"
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
                    Longitude = 21.159447,
                    Hour = new TimeSpan(10,00,00)
                },
                new BusStop
                {
                    Id = 2,
                    RouteId = 1,
                    Address = "Jasło",
                    Label = "Jasło DA",
                    Latitude = 49.743750,
                    Longitude = 21.473399,
                    Hour = new TimeSpan(10, 20, 00)
                },
                new BusStop
                {
                    Id = 3,
                    RouteId = 1,
                    Address = "Frysztak",
                    Latitude = 49.845480,
                    Longitude = 21.612531,
                    Hour = new TimeSpan(10, 30, 00)
                },
                new BusStop
                {
                    Id = 4,
                    RouteId = 1,
                    Address = "Wiśniowa",
                    Latitude = 49.869611,
                    Longitude = 21.659950,
                    Hour = new TimeSpan(10, 40, 00)
                },
                new BusStop
                {
                    Id = 5,
                    RouteId = 1,
                    Address = "Dobrzechów",
                    Latitude = 49.876198,
                    Longitude = 21.753990,
                    Hour = new TimeSpan(10, 50, 00)
                },
                new BusStop
                {
                    Id = 6,
                    RouteId = 1,
                    Address = "Strzyżów",
                    Label = "Strzyżów DA",
                    Latitude = 49.869992,
                    Longitude = 21.800657,
                    Hour = new TimeSpan(11, 00, 00)
                },
                new BusStop
                {
                    Id = 7,
                    RouteId = 1,
                    Address = "Czudec",
                    Label = "Czudec DA",
                    Latitude = 49.945855,
                    Longitude = 21.837562,
                    Hour = new TimeSpan(11, 10, 00)
                },
                new BusStop
                {
                    Id = 8,
                    RouteId = 1,
                    Address = "Boguchwała",
                    Latitude = 49.983775,
                    Longitude = 21.942793,
                    Hour = new TimeSpan(11, 20, 00)
                },
                new BusStop
                {
                    Id = 9,
                    RouteId = 1,
                    Address = "Rzeszów",
                    Label = "Podkarpacka",
                    Latitude = 50.020076,
                    Longitude = 21.980312,
                    Hour = new TimeSpan(11, 30, 00)
                },
                new BusStop
                {
                    Id = 10,
                    RouteId = 1,
                    Address = "Rzeszów",
                    Label = "Rzeszów DA",
                    Latitude = 50.042131,
                    Longitude = 22.003429,
                    Hour = new TimeSpan(11, 40, 00)
                },
                new BusStop
                {
                    Id = 11,
                    RouteId = 1,
                    Address = "Rzeszów",
                    Label = "Rejtana",
                    Latitude = 50.031346,
                    Longitude = 22.016653,
                    Hour = new TimeSpan(11, 50, 00)
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
                    Longitude = 22.017088,
                    Hour = new TimeSpan(11, 00, 00)
                },
                new BusStop
                {
                    Id = 13,
                    RouteId = 2,
                    Address = "Rzeszów",
                    Label = "Rzeszów DA",
                    Latitude = 50.042131,
                    Longitude = 22.003429,
                    Hour = new TimeSpan(11, 10, 00)
                },
                new BusStop
                {
                    Id = 14,
                    RouteId = 2,
                    Address = "Rzeszów",
                    Label = "Podkarpacka",
                    Latitude = 50.020076,
                    Longitude = 21.980312,
                    Hour = new TimeSpan(11, 20, 00)
                },
                new BusStop
                {
                    Id = 15,
                    RouteId = 2,
                    Address = "Boguchwała",
                    Latitude = 49.983775,
                    Longitude = 21.942793,
                    Hour = new TimeSpan(11, 30, 00)
                },
                new BusStop
                {
                    Id = 16,
                    RouteId = 2,
                    Address = "Czudec",
                    Label = "Czudec DA",
                    Latitude = 49.945855,
                    Longitude = 21.837562,
                    Hour = new TimeSpan(11, 40, 00)
                },
                new BusStop
                {
                    Id = 17,
                    RouteId = 2,
                    Address = "Strzyżów",
                    Label = "Strzyżów DA",
                    Latitude = 49.869992,
                    Longitude = 21.800657,
                    Hour = new TimeSpan(11, 50, 00)
                },
                new BusStop
                {
                    Id = 18,
                    RouteId = 2,
                    Address = "Dobrzechów",
                    Latitude = 49.876198,
                    Longitude = 21.753990,
                    Hour = new TimeSpan(12, 00, 00)
                },
                new BusStop
                {
                    Id = 19,
                    RouteId = 2,
                    Address = "Wiśniowa",
                    Latitude = 49.869611,
                    Longitude = 21.659950,
                    Hour = new TimeSpan(12, 10, 00)
                },
                new BusStop
                {
                    Id = 20,
                    RouteId = 2,
                    Address = "Frysztak",
                    Latitude = 49.845480,
                    Longitude = 21.612531,
                    Hour = new TimeSpan(12, 20, 00)
                },
                new BusStop
                {
                    Id = 21,
                    RouteId = 2,
                    Address = "Jasło",
                    Label = "Jasło DA",
                    Latitude = 49.743750,
                    Longitude = 21.473399,
                    Hour = new TimeSpan(12, 30, 00)
                },
                new BusStop
                {
                    Id = 22,
                    RouteId = 2,
                    Address = "Gorlice",
                    Label = "Gorlice DA",
                    Latitude = 49.662932,
                    Longitude = 21.159447,
                    Hour = new TimeSpan(12, 40, 00)
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
                    Longitude = 21.612531,
                    Hour = new TimeSpan(8, 00, 00)
                },
                new BusStop
                {
                    Id = 24,
                    RouteId = 3,
                    Address = "Wiśniowa",
                    Latitude = 49.869611,
                    Longitude = 21.659950,
                    Hour = new TimeSpan(8, 10, 00)
                },
                new BusStop
                {
                    Id = 25,
                    RouteId = 3,
                    Address = "Dobrzechów",
                    Latitude = 49.876198,
                    Longitude = 21.753990,
                    Hour = new TimeSpan(8, 20, 00)
                },
                new BusStop
                {
                    Id = 26,
                    RouteId = 3,
                    Address = "Strzyżów",
                    Label = "Strzyżów DA",
                    Latitude = 49.869992,
                    Longitude = 21.800657,
                    Hour = new TimeSpan(8, 30, 00)
                },
                new BusStop
                {
                    Id = 27,
                    RouteId = 3,
                    Address = "Zaborów",
                    Latitude = 49.914127,
                    Longitude = 21.827073,
                    Hour = new TimeSpan(8, 40, 00)
                },
                new BusStop
                {
                    Id = 28,
                    RouteId = 3,
                    Address = "Czudec",
                    Label = "Czudec DA",
                    Latitude = 49.945855,
                    Longitude = 21.837562,
                    Hour = new TimeSpan(8, 50, 00)
                },
                new BusStop
                {
                    Id = 29,
                    RouteId = 3,
                    Address = "Boguchwała",
                    Latitude = 49.983775,
                    Longitude = 21.942793,
                    Hour = new TimeSpan(9, 00, 00)
                },
                new BusStop
                {
                    Id = 30,
                    RouteId = 3,
                    Address = "Rzeszów",
                    Label = "Podkarpacka",
                    Latitude = 50.020076,
                    Longitude = 21.980312,
                    Hour = new TimeSpan(9, 10, 00)
                },
                new BusStop
                {
                    Id = 31,
                    RouteId = 3,
                    Address = "Rzeszów",
                    Label = "Rzeszów DA",
                    Latitude = 50.042131,
                    Longitude = 22.003429,
                    Hour = new TimeSpan(9, 20, 00)
                }
            );

        }

    }
}
