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
        public DbSet<RouteDelay> RouteDelays { get; set; }



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

            modelBuilder.Entity<BusStopTrace>().HasData(
                new BusStopTrace
                {
                    Id = 1,
                    BusStopId = 1,
                    Date = new DateTime(2018, 12, 17),
                    Hour = new TimeSpan(10, 5, 0)
                },
                new BusStopTrace
                {
                    Id = 2,
                    BusStopId = 1,
                    Date = new DateTime(2018, 12, 17),
                    Hour = new TimeSpan(10, 13, 0)
                },
                new BusStopTrace
                {
                    Id = 3,
                    BusStopId = 1,
                    Date = new DateTime(2018, 12, 17),
                    Hour = new TimeSpan(10, 09, 0)
                },
                new BusStopTrace
                {
                    Id = 4,
                    BusStopId = 1,
                    Date = new DateTime(2018, 12, 17),
                    Hour = new TimeSpan(09, 52, 0)
                },
                new BusStopTrace
                {
                    Id = 5,
                    BusStopId = 1,
                    Date = new DateTime(2018, 12, 17),
                    Hour = new TimeSpan(09, 47, 0)
                },
                new BusStopTrace
                {
                    Id = 6,
                    BusStopId = 2,
                    Date = new DateTime(2018, 12, 17),
                    Hour = new TimeSpan(10, 13, 0)
                },
                new BusStopTrace
                {
                    Id = 7,
                    BusStopId = 2,
                    Date = new DateTime(2018, 12, 17),
                    Hour = new TimeSpan(10, 09, 0)
                },
                new BusStopTrace
                {
                    Id = 8,
                    BusStopId = 2,
                    Date = new DateTime(2018, 12, 17),
                    Hour = new TimeSpan(10, 32, 0)
                },
                new BusStopTrace
                {
                    Id = 9,
                    BusStopId = 2,
                    Date = new DateTime(2018, 12, 17),
                    Hour = new TimeSpan(10, 16, 0)
                },
                new BusStopTrace
                {
                    Id = 10,
                    BusStopId = 2,
                    Date = new DateTime(2018, 12, 17),
                    Hour = new TimeSpan(10, 43, 0)
                },
                new BusStopTrace
                {
                    Id = 11,
                    BusStopId = 3,
                    Date = new DateTime(2018, 12, 17),
                    Hour = new TimeSpan(10, 16, 0)
                },
                new BusStopTrace
                {
                    Id = 12,
                    BusStopId = 3,
                    Date = new DateTime(2018, 12, 17),
                    Hour = new TimeSpan(10, 21, 0)
                },
                new BusStopTrace
                {
                    Id = 13,
                    BusStopId = 3,
                    Date = new DateTime(2018, 12, 17),
                    Hour = new TimeSpan(10, 41, 0)
                },
                new BusStopTrace
                {
                    Id = 14,
                    BusStopId = 3,
                    Date = new DateTime(2018, 12, 17),
                    Hour = new TimeSpan(10, 38, 0)
                },
                new BusStopTrace
                {
                    Id = 15,
                    BusStopId = 3,
                    Date = new DateTime(2018, 12, 17),
                    Hour = new TimeSpan(10, 33, 0)
                },
                new BusStopTrace
                {
                    Id = 16,
                    BusStopId = 4,
                    Date = new DateTime(2018, 12, 17),
                    Hour = new TimeSpan(10, 37, 0)
                },
                new BusStopTrace
                {
                    Id = 17,
                    BusStopId = 4,
                    Date = new DateTime(2018, 12, 17),
                    Hour = new TimeSpan(10, 45, 0)
                },
                new BusStopTrace
                {
                    Id = 18,
                    BusStopId = 4,
                    Date = new DateTime(2018, 12, 17),
                    Hour = new TimeSpan(10, 51, 0)
                },
                new BusStopTrace
                {
                    Id = 19,
                    BusStopId = 4,
                    Date = new DateTime(2018, 12, 17),
                    Hour = new TimeSpan(10, 31, 0)
                },
                new BusStopTrace
                {
                    Id = 20,
                    BusStopId = 4,
                    Date = new DateTime(2018, 12, 17),
                    Hour = new TimeSpan(10, 43, 0)
                },
                new BusStopTrace
                {
                    Id = 21,
                    BusStopId = 5,
                    Date = new DateTime(2018, 12, 17),
                    Hour = new TimeSpan(10, 46, 0)
                },
                new BusStopTrace
                {
                    Id = 22,
                    BusStopId = 5,
                    Date = new DateTime(2018, 12, 17),
                    Hour = new TimeSpan(10, 57, 0)
                },
                new BusStopTrace
                {
                    Id = 23,
                    BusStopId = 5,
                    Date = new DateTime(2018, 12, 17),
                    Hour = new TimeSpan(11, 08, 0)
                },
                new BusStopTrace
                {
                    Id = 24,
                    BusStopId = 5,
                    Date = new DateTime(2018, 12, 17),
                    Hour = new TimeSpan(10, 38, 0)
                },
                new BusStopTrace
                {
                    Id = 25,
                    BusStopId = 5,
                    Date = new DateTime(2018, 12, 17),
                    Hour = new TimeSpan(11, 13, 0)
                },
                new BusStopTrace
                {
                    Id = 26,
                    BusStopId = 6,
                    Date = new DateTime(2018, 12, 17),
                    Hour = new TimeSpan(10, 53, 0)
                },
                new BusStopTrace
                {
                    Id = 27,
                    BusStopId = 6,
                    Date = new DateTime(2018, 12, 17),
                    Hour = new TimeSpan(10, 51, 0)
                },
                new BusStopTrace
                {
                    Id = 28,
                    BusStopId = 6,
                    Date = new DateTime(2018, 12, 17),
                    Hour = new TimeSpan(11, 10, 0)
                },
                new BusStopTrace
                {
                    Id = 29,
                    BusStopId = 6,
                    Date = new DateTime(2018, 12, 17),
                    Hour = new TimeSpan(11, 14, 0)
                },
                new BusStopTrace
                {
                    Id = 30,
                    BusStopId = 6,
                    Date = new DateTime(2018, 12, 17),
                    Hour = new TimeSpan(10, 55, 0)
                },
                new BusStopTrace
                {
                    Id = 31,
                    BusStopId = 7,
                    Date = new DateTime(2018, 12, 17),
                    Hour = new TimeSpan(11, 13, 0)
                },
                new BusStopTrace
                {
                    Id = 32,
                    BusStopId = 7,
                    Date = new DateTime(2018, 12, 17),
                    Hour = new TimeSpan(11, 06, 0)
                },
                new BusStopTrace
                {
                    Id = 33,
                    BusStopId = 7,
                    Date = new DateTime(2018, 12, 17),
                    Hour = new TimeSpan(10, 53, 0)
                },
                new BusStopTrace
                {
                    Id = 34,
                    BusStopId = 7,
                    Date = new DateTime(2018, 12, 17),
                    Hour = new TimeSpan(11, 21, 0)
                },
                new BusStopTrace
                {
                    Id = 35,
                    BusStopId = 7,
                    Date = new DateTime(2018, 12, 17),
                    Hour = new TimeSpan(11, 18, 0)
                },
                new BusStopTrace
                {
                    Id = 36,
                    BusStopId = 8,
                    Date = new DateTime(2018, 12, 17),
                    Hour = new TimeSpan(11, 12, 0)
                },
                new BusStopTrace
                {
                    Id = 37,
                    BusStopId = 8,
                    Date = new DateTime(2018, 12, 17),
                    Hour = new TimeSpan(11, 07, 0)
                },
                new BusStopTrace
                {
                    Id = 38,
                    BusStopId = 8,
                    Date = new DateTime(2018, 12, 17),
                    Hour = new TimeSpan(11, 24, 0)
                },
                new BusStopTrace
                {
                    Id = 39,
                    BusStopId = 8,
                    Date = new DateTime(2018, 12, 17),
                    Hour = new TimeSpan(11, 28, 0)
                },
                new BusStopTrace
                {
                    Id = 40,
                    BusStopId = 8,
                    Date = new DateTime(2018, 12, 17),
                    Hour = new TimeSpan(11, 21, 0)
                },
                new BusStopTrace
                {
                    Id = 41,
                    BusStopId = 9,
                    Date = new DateTime(2018, 12, 17),
                    Hour = new TimeSpan(11, 14, 0)
                },
                new BusStopTrace
                {
                    Id = 42,
                    BusStopId = 9,
                    Date = new DateTime(2018, 12, 17),
                    Hour = new TimeSpan(11, 18, 0)
                },
                new BusStopTrace
                {
                    Id = 43,
                    BusStopId = 9,
                    Date = new DateTime(2018, 12, 17),
                    Hour = new TimeSpan(11, 21, 0)
                },
                new BusStopTrace
                {
                    Id = 44,
                    BusStopId = 9,
                    Date = new DateTime(2018, 12, 17),
                    Hour = new TimeSpan(11, 13, 0)
                },
                new BusStopTrace
                {
                    Id = 45,
                    BusStopId = 9,
                    Date = new DateTime(2018, 12, 17),
                    Hour = new TimeSpan(11, 38, 0)
                },
                new BusStopTrace
                {
                    Id = 46,
                    BusStopId = 10,
                    Date = new DateTime(2018, 12, 17),
                    Hour = new TimeSpan(11, 33, 0)
                },
                new BusStopTrace
                {
                    Id = 47,
                    BusStopId = 10,
                    Date = new DateTime(2018, 12, 17),
                    Hour = new TimeSpan(11, 27, 0)
                },
                new BusStopTrace
                {
                    Id = 48,
                    BusStopId = 10,
                    Date = new DateTime(2018, 12, 17),
                    Hour = new TimeSpan(11, 17, 0)
                },
                new BusStopTrace
                {
                    Id = 49,
                    BusStopId = 10,
                    Date = new DateTime(2018, 12, 17),
                    Hour = new TimeSpan(11, 46, 0)
                },
                new BusStopTrace
                {
                    Id = 50,
                    BusStopId = 10,
                    Date = new DateTime(2018, 12, 17),
                    Hour = new TimeSpan(11, 51, 0)
                },
                new BusStopTrace
                {
                    Id = 51,
                    BusStopId = 11,
                    Date = new DateTime(2018, 12, 17),
                    Hour = new TimeSpan(11, 46, 0)
                },
                new BusStopTrace
                {
                    Id = 52,
                    BusStopId = 11,
                    Date = new DateTime(2018, 12, 17),
                    Hour = new TimeSpan(11, 38, 0)
                },
                new BusStopTrace
                {
                    Id = 53,
                    BusStopId = 11,
                    Date = new DateTime(2018, 12, 17),
                    Hour = new TimeSpan(11, 31, 0)
                },
                new BusStopTrace
                {
                    Id = 54,
                    BusStopId = 11,
                    Date = new DateTime(2018, 12, 17),
                    Hour = new TimeSpan(11, 53, 0)
                },
                new BusStopTrace
                {
                    Id = 55,
                    BusStopId = 11,
                    Date = new DateTime(2018, 12, 17),
                    Hour = new TimeSpan(11, 58, 0)
                },
                new BusStopTrace
                {
                    Id = 56,
                    BusStopId = 12,
                    Date = new DateTime(2018, 12, 17),
                    Hour = new TimeSpan(11, 07, 0)
                },
                new BusStopTrace
                {
                    Id = 57,
                    BusStopId = 12,
                    Date = new DateTime(2018, 12, 17),
                    Hour = new TimeSpan(11, 12, 0)
                },
                new BusStopTrace
                {
                    Id = 58,
                    BusStopId = 12,
                    Date = new DateTime(2018, 12, 17),
                    Hour = new TimeSpan(10, 51, 0)
                },
                new BusStopTrace
                {
                    Id = 59,
                    BusStopId = 12,
                    Date = new DateTime(2018, 12, 17),
                    Hour = new TimeSpan(10, 46, 0)
                },
                new BusStopTrace
                {
                    Id = 60,
                    BusStopId = 12,
                    Date = new DateTime(2018, 12, 17),
                    Hour = new TimeSpan(11, 09, 0)
                },
                new BusStopTrace
                {
                    Id = 61,
                    BusStopId = 13,
                    Date = new DateTime(2018, 12, 17),
                    Hour = new TimeSpan(11, 07, 0)
                },
                new BusStopTrace
                {
                    Id = 62,
                    BusStopId = 13,
                    Date = new DateTime(2018, 12, 17),
                    Hour = new TimeSpan(11, 02, 0)
                },
                new BusStopTrace
                {
                    Id = 63,
                    BusStopId = 13,
                    Date = new DateTime(2018, 12, 17),
                    Hour = new TimeSpan(10, 55, 0)
                },
                new BusStopTrace
                {
                    Id = 64,
                    BusStopId = 13,
                    Date = new DateTime(2018, 12, 17),
                    Hour = new TimeSpan(10, 47, 0)
                },
                new BusStopTrace
                {
                    Id = 65,
                    BusStopId = 13,
                    Date = new DateTime(2018, 12, 17),
                    Hour = new TimeSpan(11, 19, 0)
                },
                new BusStopTrace
                {
                    Id = 66,
                    BusStopId = 14,
                    Date = new DateTime(2018, 12, 17),
                    Hour = new TimeSpan(11, 18, 0)
                },
                new BusStopTrace
                {
                    Id = 67,
                    BusStopId = 14,
                    Date = new DateTime(2018, 12, 17),
                    Hour = new TimeSpan(11, 14, 0)
                },
                new BusStopTrace
                {
                    Id = 68,
                    BusStopId = 14,
                    Date = new DateTime(2018, 12, 17),
                    Hour = new TimeSpan(11, 08, 0)
                },
                new BusStopTrace
                {
                    Id = 69,
                    BusStopId = 14,
                    Date = new DateTime(2018, 12, 17),
                    Hour = new TimeSpan(11, 23, 0)
                },
                new BusStopTrace
                {
                    Id = 70,
                    BusStopId = 14,
                    Date = new DateTime(2018, 12, 17),
                    Hour = new TimeSpan(11, 26, 0)
                },
                new BusStopTrace
                {
                    Id = 71,
                    BusStopId = 15,
                    Date = new DateTime(2018, 12, 17),
                    Hour = new TimeSpan(11, 21, 0)
                },
                new BusStopTrace
                {
                    Id = 72,
                    BusStopId = 15,
                    Date = new DateTime(2018, 12, 17),
                    Hour = new TimeSpan(11, 16, 0)
                },
                new BusStopTrace
                {
                    Id = 73,
                    BusStopId = 15,
                    Date = new DateTime(2018, 12, 17),
                    Hour = new TimeSpan(11, 13, 0)
                },
                new BusStopTrace
                {
                    Id = 74,
                    BusStopId = 15,
                    Date = new DateTime(2018, 12, 17),
                    Hour = new TimeSpan(11, 32, 0)
                },
                new BusStopTrace
                {
                    Id = 75,
                    BusStopId = 15,
                    Date = new DateTime(2018, 12, 17),
                    Hour = new TimeSpan(11, 37, 0)
                },
                new BusStopTrace
                {
                    Id = 76,
                    BusStopId = 16,
                    Date = new DateTime(2018, 12, 17),
                    Hour = new TimeSpan(11, 38, 0)
                },
                new BusStopTrace
                {
                    Id = 77,
                    BusStopId = 16,
                    Date = new DateTime(2018, 12, 17),
                    Hour = new TimeSpan(11, 35, 0)
                },
                new BusStopTrace
                {
                    Id = 78,
                    BusStopId = 16,
                    Date = new DateTime(2018, 12, 17),
                    Hour = new TimeSpan(11, 31, 0)
                },
                new BusStopTrace
                {
                    Id = 79,
                    BusStopId = 16,
                    Date = new DateTime(2018, 12, 17),
                    Hour = new TimeSpan(11, 43, 0)
                },
                new BusStopTrace
                {
                    Id = 80,
                    BusStopId = 16,
                    Date = new DateTime(2018, 12, 17),
                    Hour = new TimeSpan(11, 44, 0)
                },
                new BusStopTrace
                {
                    Id = 81,
                    BusStopId = 17,
                    Date = new DateTime(2018, 12, 17),
                    Hour = new TimeSpan(11, 47, 0)
                },
                new BusStopTrace
                {
                    Id = 82,
                    BusStopId = 17,
                    Date = new DateTime(2018, 12, 17),
                    Hour = new TimeSpan(11, 45, 0)
                },
                new BusStopTrace
                {
                    Id = 83,
                    BusStopId = 17,
                    Date = new DateTime(2018, 12, 17),
                    Hour = new TimeSpan(11, 52, 0)
                },
                new BusStopTrace
                {
                    Id = 84,
                    BusStopId = 17,
                    Date = new DateTime(2018, 12, 17),
                    Hour = new TimeSpan(11, 57, 0)
                },
                new BusStopTrace
                {
                    Id = 85,
                    BusStopId = 17,
                    Date = new DateTime(2018, 12, 17),
                    Hour = new TimeSpan(12, 00, 0)
                },
                new BusStopTrace
                {
                    Id = 86,
                    BusStopId = 18,
                    Date = new DateTime(2018, 12, 17),
                    Hour = new TimeSpan(11, 45, 0)
                },
                new BusStopTrace
                {
                    Id = 87,
                    BusStopId = 18,
                    Date = new DateTime(2018, 12, 17),
                    Hour = new TimeSpan(11, 48, 0)
                },
                new BusStopTrace
                {
                    Id = 88,
                    BusStopId = 18,
                    Date = new DateTime(2018, 12, 17),
                    Hour = new TimeSpan(11, 54, 0)
                },
                new BusStopTrace
                {
                    Id = 89,
                    BusStopId = 18,
                    Date = new DateTime(2018, 12, 17),
                    Hour = new TimeSpan(12, 02, 0)
                },
                new BusStopTrace
                {
                    Id = 90,
                    BusStopId = 18,
                    Date = new DateTime(2018, 12, 17),
                    Hour = new TimeSpan(12, 08, 0)
                },
                new BusStopTrace
                {
                    Id = 91,
                    BusStopId = 19,
                    Date = new DateTime(2018, 12, 17),
                    Hour = new TimeSpan(12, 02, 0)
                },
                new BusStopTrace
                {
                    Id = 92,
                    BusStopId = 19,
                    Date = new DateTime(2018, 12, 17),
                    Hour = new TimeSpan(12, 08, 0)
                },
                new BusStopTrace
                {
                    Id = 93,
                    BusStopId = 19,
                    Date = new DateTime(2018, 12, 17),
                    Hour = new TimeSpan(12, 12, 0)
                },
                new BusStopTrace
                {
                    Id = 94,
                    BusStopId = 19,
                    Date = new DateTime(2018, 12, 17),
                    Hour = new TimeSpan(12, 15, 0)
                },
                new BusStopTrace
                {
                    Id = 95,
                    BusStopId = 19,
                    Date = new DateTime(2018, 12, 17),
                    Hour = new TimeSpan(12, 10, 0)
                },
                new BusStopTrace
                {
                    Id = 96,
                    BusStopId = 20,
                    Date = new DateTime(2018, 12, 17),
                    Hour = new TimeSpan(12, 17, 0)
                },
                new BusStopTrace
                {
                    Id = 97,
                    BusStopId = 20,
                    Date = new DateTime(2018, 12, 17),
                    Hour = new TimeSpan(12, 14, 0)
                },
                new BusStopTrace
                {
                    Id = 98,
                    BusStopId = 20,
                    Date = new DateTime(2018, 12, 17),
                    Hour = new TimeSpan(12, 21, 0)
                },
                new BusStopTrace
                {
                    Id = 99,
                    BusStopId = 20,
                    Date = new DateTime(2018, 12, 17),
                    Hour = new TimeSpan(12, 25, 0)
                },
                new BusStopTrace
                {
                    Id = 100,
                    BusStopId = 20,
                    Date = new DateTime(2018, 12, 17),
                    Hour = new TimeSpan(12, 27, 0)
                },
                new BusStopTrace
                {
                    Id = 101,
                    BusStopId = 21,
                    Date = new DateTime(2018, 12, 17),
                    Hour = new TimeSpan(12, 19, 0)
                },
                new BusStopTrace
                {
                    Id = 102,
                    BusStopId = 21,
                    Date = new DateTime(2018, 12, 17),
                    Hour = new TimeSpan(12, 22, 0)
                },
                new BusStopTrace
                {
                    Id = 103,
                    BusStopId = 21,
                    Date = new DateTime(2018, 12, 17),
                    Hour = new TimeSpan(12, 28, 0)
                },
                new BusStopTrace
                {
                    Id = 104,
                    BusStopId = 21,
                    Date = new DateTime(2018, 12, 17),
                    Hour = new TimeSpan(12, 32, 0)
                },
                new BusStopTrace
                {
                    Id = 105,
                    BusStopId = 21,
                    Date = new DateTime(2018, 12, 17),
                    Hour = new TimeSpan(12, 37, 0)
                },
                new BusStopTrace
                {
                    Id = 106,
                    BusStopId = 22,
                    Date = new DateTime(2018, 12, 17),
                    Hour = new TimeSpan(12, 37, 0)
                },
                new BusStopTrace
                {
                    Id = 107,
                    BusStopId = 22,
                    Date = new DateTime(2018, 12, 17),
                    Hour = new TimeSpan(12, 39, 0)
                },
                new BusStopTrace
                {
                    Id = 108,
                    BusStopId = 22,
                    Date = new DateTime(2018, 12, 17),
                    Hour = new TimeSpan(12, 41, 0)
                },
                new BusStopTrace
                {
                    Id = 109,
                    BusStopId = 22,
                    Date = new DateTime(2018, 12, 17),
                    Hour = new TimeSpan(12, 46, 0)
                },
                new BusStopTrace
                {
                    Id = 110,
                    BusStopId = 22,
                    Date = new DateTime(2018, 12, 17),
                    Hour = new TimeSpan(12, 51, 0)
                },
                new BusStopTrace
                {
                    Id = 111,
                    BusStopId = 23,
                    Date = new DateTime(2018, 12, 17),
                    Hour = new TimeSpan(08, 06, 0)
                },
                new BusStopTrace
                {
                    Id = 112,
                    BusStopId = 23,
                    Date = new DateTime(2018, 12, 17),
                    Hour = new TimeSpan(08, 10, 0)
                },
                new BusStopTrace
                {
                    Id = 113,
                    BusStopId = 23,
                    Date = new DateTime(2018, 12, 17),
                    Hour = new TimeSpan(07, 56, 0)
                },
                new BusStopTrace
                {
                    Id = 114,
                    BusStopId = 23,
                    Date = new DateTime(2018, 12, 17),
                    Hour = new TimeSpan(07, 51, 0)
                },
                new BusStopTrace
                {
                    Id = 115,
                    BusStopId = 23,
                    Date = new DateTime(2018, 12, 17),
                    Hour = new TimeSpan(08, 04, 0)
                },
                new BusStopTrace
                {
                    Id = 116,
                    BusStopId = 24,
                    Date = new DateTime(2018, 12, 17),
                    Hour = new TimeSpan(08, 07, 0)
                },
                new BusStopTrace
                {
                    Id = 117,
                    BusStopId = 24,
                    Date = new DateTime(2018, 12, 17),
                    Hour = new TimeSpan(08, 06, 0)
                },
                new BusStopTrace
                {
                    Id = 118,
                    BusStopId = 24,
                    Date = new DateTime(2018, 12, 17),
                    Hour = new TimeSpan(08, 02, 0)
                },
                new BusStopTrace
                {
                    Id = 119,
                    BusStopId = 24,
                    Date = new DateTime(2018, 12, 17),
                    Hour = new TimeSpan(08, 12, 0)
                },
                new BusStopTrace
                {
                    Id = 120,
                    BusStopId = 24,
                    Date = new DateTime(2018, 12, 17),
                    Hour = new TimeSpan(08, 14, 0)
                },
                new BusStopTrace
                {
                    Id = 121,
                    BusStopId = 25,
                    Date = new DateTime(2018, 12, 17),
                    Hour = new TimeSpan(08, 17, 0)
                },
                new BusStopTrace
                {
                    Id = 122,
                    BusStopId = 25,
                    Date = new DateTime(2018, 12, 17),
                    Hour = new TimeSpan(08, 15, 0)
                },
                new BusStopTrace
                {
                    Id = 123,
                    BusStopId = 25,
                    Date = new DateTime(2018, 12, 17),
                    Hour = new TimeSpan(08, 21, 0)
                },
                new BusStopTrace
                {
                    Id = 124,
                    BusStopId = 25,
                    Date = new DateTime(2018, 12, 17),
                    Hour = new TimeSpan(08, 24, 0)
                },
                new BusStopTrace
                {
                    Id = 125,
                    BusStopId = 25,
                    Date = new DateTime(2018, 12, 17),
                    Hour = new TimeSpan(08, 27, 0)
                },
                new BusStopTrace
                {
                    Id = 126,
                    BusStopId = 26,
                    Date = new DateTime(2018, 12, 17),
                    Hour = new TimeSpan(08, 26, 0)
                },
                new BusStopTrace
                {
                    Id = 127,
                    BusStopId = 26,
                    Date = new DateTime(2018, 12, 17),
                    Hour = new TimeSpan(08, 28, 0)
                },
                new BusStopTrace
                {
                    Id = 128,
                    BusStopId = 26,
                    Date = new DateTime(2018, 12, 17),
                    Hour = new TimeSpan(08, 32, 0)
                },
                new BusStopTrace
                {
                    Id = 129,
                    BusStopId = 26,
                    Date = new DateTime(2018, 12, 17),
                    Hour = new TimeSpan(08, 35, 0)
                },
                new BusStopTrace
                {
                    Id = 130,
                    BusStopId = 26,
                    Date = new DateTime(2018, 12, 17),
                    Hour = new TimeSpan(08, 38, 0)
                },
                new BusStopTrace
                {
                    Id = 131,
                    BusStopId = 27,
                    Date = new DateTime(2018, 12, 17),
                    Hour = new TimeSpan(08, 32, 0)
                },
                new BusStopTrace
                {
                    Id = 132,
                    BusStopId = 27,
                    Date = new DateTime(2018, 12, 17),
                    Hour = new TimeSpan(08, 37, 0)
                },
                new BusStopTrace
                {
                    Id = 133,
                    BusStopId = 27,
                    Date = new DateTime(2018, 12, 17),
                    Hour = new TimeSpan(08, 41, 0)
                },
                new BusStopTrace
                {
                    Id = 134,
                    BusStopId = 27,
                    Date = new DateTime(2018, 12, 17),
                    Hour = new TimeSpan(08, 45, 0)
                },
                new BusStopTrace
                {
                    Id = 135,
                    BusStopId = 27,
                    Date = new DateTime(2018, 12, 17),
                    Hour = new TimeSpan(08, 48, 0)
                },
                new BusStopTrace
                {
                    Id = 136,
                    BusStopId = 28,
                    Date = new DateTime(2018, 12, 17),
                    Hour = new TimeSpan(08, 46, 0)
                },
                new BusStopTrace
                {
                    Id = 137,
                    BusStopId = 28,
                    Date = new DateTime(2018, 12, 17),
                    Hour = new TimeSpan(08, 50, 0)
                },
                new BusStopTrace
                {
                    Id = 138,
                    BusStopId = 28,
                    Date = new DateTime(2018, 12, 17),
                    Hour = new TimeSpan(08, 54, 0)
                },
                new BusStopTrace
                {
                    Id = 139,
                    BusStopId = 28,
                    Date = new DateTime(2018, 12, 17),
                    Hour = new TimeSpan(08, 58, 0)
                },
                new BusStopTrace
                {
                    Id = 140,
                    BusStopId = 28,
                    Date = new DateTime(2018, 12, 17),
                    Hour = new TimeSpan(09, 08, 0)
                },
                new BusStopTrace
                {
                    Id = 141,
                    BusStopId = 29,
                    Date = new DateTime(2018, 12, 17),
                    Hour = new TimeSpan(08, 51, 0)
                },
                new BusStopTrace
                {
                    Id = 142,
                    BusStopId = 29,
                    Date = new DateTime(2018, 12, 17),
                    Hour = new TimeSpan(08, 59, 0)
                },
                new BusStopTrace
                {
                    Id = 143,
                    BusStopId = 29,
                    Date = new DateTime(2018, 12, 17),
                    Hour = new TimeSpan(09, 03, 0)
                },
                new BusStopTrace
                {
                    Id = 144,
                    BusStopId = 29,
                    Date = new DateTime(2018, 12, 17),
                    Hour = new TimeSpan(09, 12, 0)
                },
                new BusStopTrace
                {
                    Id = 145,
                    BusStopId = 29,
                    Date = new DateTime(2018, 12, 17),
                    Hour = new TimeSpan(09, 07, 0)
                },
                new BusStopTrace
                {
                    Id = 146,
                    BusStopId = 30,
                    Date = new DateTime(2018, 12, 17),
                    Hour = new TimeSpan(09, 04, 0)
                },
                new BusStopTrace
                {
                    Id = 147,
                    BusStopId = 30,
                    Date = new DateTime(2018, 12, 17),
                    Hour = new TimeSpan(09, 08, 0)
                },
                new BusStopTrace
                {
                    Id = 148,
                    BusStopId = 30,
                    Date = new DateTime(2018, 12, 17),
                    Hour = new TimeSpan(09, 11, 0)
                },
                new BusStopTrace
                {
                    Id = 149,
                    BusStopId = 30,
                    Date = new DateTime(2018, 12, 17),
                    Hour = new TimeSpan(09, 15, 0)
                },
                new BusStopTrace
                {
                    Id = 150,
                    BusStopId = 30,
                    Date = new DateTime(2018, 12, 17),
                    Hour = new TimeSpan(09, 17, 0)
                },
                new BusStopTrace
                {
                    Id = 151,
                    BusStopId = 31,
                    Date = new DateTime(2018, 12, 17),
                    Hour = new TimeSpan(09, 14, 0)
                },
                new BusStopTrace
                {
                    Id = 152,
                    BusStopId = 31,
                    Date = new DateTime(2018, 12, 17),
                    Hour = new TimeSpan(09, 11, 0)
                },
                new BusStopTrace
                {
                    Id = 153,
                    BusStopId = 31,
                    Date = new DateTime(2018, 12, 17),
                    Hour = new TimeSpan(09, 23, 0)
                },
                new BusStopTrace
                {
                    Id = 154,
                    BusStopId = 31,
                    Date = new DateTime(2018, 12, 17),
                    Hour = new TimeSpan(09, 28, 0)
                },
                new BusStopTrace
                {
                    Id = 155,
                    BusStopId = 31,
                    Date = new DateTime(2018, 12, 17),
                    Hour = new TimeSpan(09, 39, 0)
                }
            );

        }

    }
}
