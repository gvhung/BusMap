﻿// <auto-generated />
using System;
using BusMap.WebApi.Data;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;

namespace BusMap.WebApi.Migrations
{
    [DbContext(typeof(DatabaseContext))]
    partial class DatabaseContextModelSnapshot : ModelSnapshot
    {
        protected override void BuildModel(ModelBuilder modelBuilder)
        {
#pragma warning disable 612, 618
            modelBuilder
                .HasAnnotation("ProductVersion", "2.1.4-rtm-31024")
                .HasAnnotation("Relational:MaxIdentifierLength", 128)
                .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

            modelBuilder.Entity("BusMap.WebApi.DatabaseModels.BusStop", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

                    b.Property<string>("Address")
                        .IsRequired()
                        .HasMaxLength(50);

                    b.Property<TimeSpan>("Hour");

                    b.Property<string>("Label")
                        .HasMaxLength(50);

                    b.Property<double>("Latitude");

                    b.Property<double>("Longitude");

                    b.Property<int>("RouteId");

                    b.HasKey("Id");

                    b.HasIndex("RouteId");

                    b.ToTable("BusStops");

                    b.HasData(
                        new { Id = 1, Address = "Gorlice", Hour = new TimeSpan(0, 10, 0, 0, 0), Label = "Gorlice DA", Latitude = 49.662932, Longitude = 21.159447, RouteId = 1 },
                        new { Id = 2, Address = "Jasło", Hour = new TimeSpan(0, 10, 20, 0, 0), Label = "Jasło DA", Latitude = 49.74375, Longitude = 21.473399, RouteId = 1 },
                        new { Id = 3, Address = "Frysztak", Hour = new TimeSpan(0, 10, 30, 0, 0), Latitude = 49.84548, Longitude = 21.612531, RouteId = 1 },
                        new { Id = 4, Address = "Wiśniowa", Hour = new TimeSpan(0, 10, 40, 0, 0), Latitude = 49.869611, Longitude = 21.65995, RouteId = 1 },
                        new { Id = 5, Address = "Dobrzechów", Hour = new TimeSpan(0, 10, 50, 0, 0), Latitude = 49.876198, Longitude = 21.75399, RouteId = 1 },
                        new { Id = 6, Address = "Strzyżów", Hour = new TimeSpan(0, 11, 0, 0, 0), Label = "Strzyżów DA", Latitude = 49.869992, Longitude = 21.800657, RouteId = 1 },
                        new { Id = 7, Address = "Czudec", Hour = new TimeSpan(0, 11, 10, 0, 0), Label = "Czudec DA", Latitude = 49.945855, Longitude = 21.837562, RouteId = 1 },
                        new { Id = 8, Address = "Boguchwała", Hour = new TimeSpan(0, 11, 20, 0, 0), Latitude = 49.983775, Longitude = 21.942793, RouteId = 1 },
                        new { Id = 9, Address = "Rzeszów", Hour = new TimeSpan(0, 11, 30, 0, 0), Label = "Podkarpacka", Latitude = 50.020076, Longitude = 21.980312, RouteId = 1 },
                        new { Id = 10, Address = "Rzeszów", Hour = new TimeSpan(0, 11, 40, 0, 0), Label = "Rzeszów DA", Latitude = 50.042131, Longitude = 22.003429, RouteId = 1 },
                        new { Id = 11, Address = "Rzeszów", Hour = new TimeSpan(0, 11, 50, 0, 0), Label = "Rejtana", Latitude = 50.031346, Longitude = 22.016653, RouteId = 1 },
                        new { Id = 12, Address = "Rzeszów", Hour = new TimeSpan(0, 11, 0, 0, 0), Label = "Rejtana", Latitude = 50.030767, Longitude = 22.017088, RouteId = 2 },
                        new { Id = 13, Address = "Rzeszów", Hour = new TimeSpan(0, 11, 10, 0, 0), Label = "Rzeszów DA", Latitude = 50.042131, Longitude = 22.003429, RouteId = 2 },
                        new { Id = 14, Address = "Rzeszów", Hour = new TimeSpan(0, 11, 20, 0, 0), Label = "Podkarpacka", Latitude = 50.020076, Longitude = 21.980312, RouteId = 2 },
                        new { Id = 15, Address = "Boguchwała", Hour = new TimeSpan(0, 11, 30, 0, 0), Latitude = 49.983775, Longitude = 21.942793, RouteId = 2 },
                        new { Id = 16, Address = "Czudec", Hour = new TimeSpan(0, 11, 40, 0, 0), Label = "Czudec DA", Latitude = 49.945855, Longitude = 21.837562, RouteId = 2 },
                        new { Id = 17, Address = "Strzyżów", Hour = new TimeSpan(0, 11, 50, 0, 0), Label = "Strzyżów DA", Latitude = 49.869992, Longitude = 21.800657, RouteId = 2 },
                        new { Id = 18, Address = "Dobrzechów", Hour = new TimeSpan(0, 12, 0, 0, 0), Latitude = 49.876198, Longitude = 21.75399, RouteId = 2 },
                        new { Id = 19, Address = "Wiśniowa", Hour = new TimeSpan(0, 12, 10, 0, 0), Latitude = 49.869611, Longitude = 21.65995, RouteId = 2 },
                        new { Id = 20, Address = "Frysztak", Hour = new TimeSpan(0, 12, 20, 0, 0), Latitude = 49.84548, Longitude = 21.612531, RouteId = 2 },
                        new { Id = 21, Address = "Jasło", Hour = new TimeSpan(0, 12, 30, 0, 0), Label = "Jasło DA", Latitude = 49.74375, Longitude = 21.473399, RouteId = 2 },
                        new { Id = 22, Address = "Gorlice", Hour = new TimeSpan(0, 12, 40, 0, 0), Label = "Gorlice DA", Latitude = 49.662932, Longitude = 21.159447, RouteId = 2 },
                        new { Id = 23, Address = "Frysztak", Hour = new TimeSpan(0, 8, 0, 0, 0), Latitude = 49.84548, Longitude = 21.612531, RouteId = 3 },
                        new { Id = 24, Address = "Wiśniowa", Hour = new TimeSpan(0, 8, 10, 0, 0), Latitude = 49.869611, Longitude = 21.65995, RouteId = 3 },
                        new { Id = 25, Address = "Dobrzechów", Hour = new TimeSpan(0, 8, 20, 0, 0), Latitude = 49.876198, Longitude = 21.75399, RouteId = 3 },
                        new { Id = 26, Address = "Strzyżów", Hour = new TimeSpan(0, 8, 30, 0, 0), Label = "Strzyżów DA", Latitude = 49.869992, Longitude = 21.800657, RouteId = 3 },
                        new { Id = 27, Address = "Zaborów", Hour = new TimeSpan(0, 8, 40, 0, 0), Latitude = 49.914127, Longitude = 21.827073, RouteId = 3 },
                        new { Id = 28, Address = "Czudec", Hour = new TimeSpan(0, 8, 50, 0, 0), Label = "Czudec DA", Latitude = 49.945855, Longitude = 21.837562, RouteId = 3 },
                        new { Id = 29, Address = "Boguchwała", Hour = new TimeSpan(0, 9, 0, 0, 0), Latitude = 49.983775, Longitude = 21.942793, RouteId = 3 },
                        new { Id = 30, Address = "Rzeszów", Hour = new TimeSpan(0, 9, 10, 0, 0), Label = "Podkarpacka", Latitude = 50.020076, Longitude = 21.980312, RouteId = 3 },
                        new { Id = 31, Address = "Rzeszów", Hour = new TimeSpan(0, 9, 20, 0, 0), Label = "Rzeszów DA", Latitude = 50.042131, Longitude = 22.003429, RouteId = 3 }
                    );
                });

            modelBuilder.Entity("BusMap.WebApi.DatabaseModels.BusStopQueued", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

                    b.Property<string>("Address")
                        .IsRequired()
                        .HasMaxLength(50);

                    b.Property<TimeSpan>("Hour");

                    b.Property<string>("Label")
                        .HasMaxLength(50);

                    b.Property<double>("Latitude");

                    b.Property<double>("Longitude");

                    b.Property<int>("RouteQueuedId");

                    b.HasKey("Id");

                    b.HasIndex("RouteQueuedId");

                    b.ToTable("BusStopsQueued");
                });

            modelBuilder.Entity("BusMap.WebApi.DatabaseModels.BusStopTrace", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

                    b.Property<int>("BusStopId");

                    b.Property<DateTime>("Date")
                        .ValueGeneratedOnAdd()
                        .HasDefaultValueSql("FORMAT(GetDate(), 'yyyy-MM-dd')");

                    b.Property<TimeSpan>("Hour")
                        .ValueGeneratedOnAdd()
                        .HasDefaultValueSql("Format(GETDATE(),'hh:mm')");

                    b.HasKey("Id");

                    b.HasIndex("BusStopId");

                    b.ToTable("BusStopTraces");
                });

            modelBuilder.Entity("BusMap.WebApi.DatabaseModels.Carrier", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

                    b.Property<string>("Name")
                        .IsRequired()
                        .HasMaxLength(100);

                    b.HasKey("Id");

                    b.ToTable("Carriers");

                    b.HasData(
                        new { Id = 1, Name = "Nowex Transport" },
                        new { Id = 2, Name = "Kolos" }
                    );
                });

            modelBuilder.Entity("BusMap.WebApi.DatabaseModels.CarrierQueued", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

                    b.Property<DateTime>("CreatedDatetime")
                        .ValueGeneratedOnAdd()
                        .HasDefaultValueSql("FORMAT(GetDate(), 'yyyy-MM-dd')");

                    b.Property<string>("Name")
                        .IsRequired()
                        .HasMaxLength(100);

                    b.Property<int>("NegativeVotes")
                        .ValueGeneratedOnAdd()
                        .HasDefaultValue(0);

                    b.Property<int>("PositiveVotes")
                        .ValueGeneratedOnAdd()
                        .HasDefaultValue(0);

                    b.Property<DateTime?>("VotingEndedDateTime");

                    b.Property<DateTime?>("VotingStartedDatetime");

                    b.HasKey("Id");

                    b.ToTable("CarriersQueued");
                });

            modelBuilder.Entity("BusMap.WebApi.DatabaseModels.Route", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

                    b.Property<int>("CarrierId");

                    b.Property<string>("DayOfTheWeek");

                    b.Property<string>("Name")
                        .IsRequired()
                        .HasMaxLength(100);

                    b.HasKey("Id");

                    b.HasIndex("CarrierId");

                    b.ToTable("Routes");

                    b.HasData(
                        new { Id = 1, CarrierId = 1, DayOfTheWeek = "1,2,3", Name = "Gorlice - Rzeszów" },
                        new { Id = 2, CarrierId = 1, DayOfTheWeek = "1,2,3", Name = "Rzeszów - Gorlice" },
                        new { Id = 3, CarrierId = 2, DayOfTheWeek = "2", Name = "Frysztak - Rzeszów" }
                    );
                });

            modelBuilder.Entity("BusMap.WebApi.DatabaseModels.RouteDelay", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

                    b.Property<DateTime>("DateTime");

                    b.Property<string>("Description");

                    b.Property<int>("RouteId");

                    b.Property<string>("Title");

                    b.HasKey("Id");

                    b.HasIndex("RouteId");

                    b.ToTable("RouteDelays");
                });

            modelBuilder.Entity("BusMap.WebApi.DatabaseModels.RouteQueued", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

                    b.Property<int?>("CarrierId");

                    b.Property<int?>("CarrierQueuedId");

                    b.Property<DateTime>("CreatedDatetime")
                        .ValueGeneratedOnAdd()
                        .HasDefaultValueSql("FORMAT(GetDate(), 'yyyy-MM-dd')");

                    b.Property<string>("DayOfTheWeek");

                    b.Property<string>("Name")
                        .IsRequired()
                        .HasMaxLength(100);

                    b.Property<int>("NegativeVotes")
                        .ValueGeneratedOnAdd()
                        .HasDefaultValue(0);

                    b.Property<int>("PositiveVotes")
                        .ValueGeneratedOnAdd()
                        .HasDefaultValue(0);

                    b.Property<DateTime?>("VotingEndedDateTime");

                    b.Property<DateTime?>("VotingStartedDatetime");

                    b.HasKey("Id");

                    b.HasIndex("CarrierQueuedId");

                    b.ToTable("RoutesQueued");
                });

            modelBuilder.Entity("BusMap.WebApi.DatabaseModels.RouteReport", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

                    b.Property<DateTime>("Date")
                        .ValueGeneratedOnAdd()
                        .HasDefaultValueSql("FORMAT(GetDate(), 'yyyy-MM-dd')");

                    b.Property<string>("Description");

                    b.Property<int>("RouteId");

                    b.HasKey("Id");

                    b.HasIndex("RouteId");

                    b.ToTable("RouteReports");
                });

            modelBuilder.Entity("BusMap.WebApi.DatabaseModels.BusStop", b =>
                {
                    b.HasOne("BusMap.WebApi.DatabaseModels.Route", "Route")
                        .WithMany("BusStops")
                        .HasForeignKey("RouteId")
                        .OnDelete(DeleteBehavior.Cascade);
                });

            modelBuilder.Entity("BusMap.WebApi.DatabaseModels.BusStopQueued", b =>
                {
                    b.HasOne("BusMap.WebApi.DatabaseModels.RouteQueued", "RouteQueued")
                        .WithMany("BusStopsQueued")
                        .HasForeignKey("RouteQueuedId")
                        .OnDelete(DeleteBehavior.Cascade);
                });

            modelBuilder.Entity("BusMap.WebApi.DatabaseModels.BusStopTrace", b =>
                {
                    b.HasOne("BusMap.WebApi.DatabaseModels.BusStop", "BusStop")
                        .WithMany("BusStopTraces")
                        .HasForeignKey("BusStopId")
                        .OnDelete(DeleteBehavior.Cascade);
                });

            modelBuilder.Entity("BusMap.WebApi.DatabaseModels.Route", b =>
                {
                    b.HasOne("BusMap.WebApi.DatabaseModels.Carrier", "Carrier")
                        .WithMany("Routes")
                        .HasForeignKey("CarrierId")
                        .OnDelete(DeleteBehavior.Cascade);
                });

            modelBuilder.Entity("BusMap.WebApi.DatabaseModels.RouteDelay", b =>
                {
                    b.HasOne("BusMap.WebApi.DatabaseModels.Route", "Route")
                        .WithMany()
                        .HasForeignKey("RouteId")
                        .OnDelete(DeleteBehavior.Cascade);
                });

            modelBuilder.Entity("BusMap.WebApi.DatabaseModels.RouteQueued", b =>
                {
                    b.HasOne("BusMap.WebApi.DatabaseModels.CarrierQueued", "CarrierQueued")
                        .WithMany("RoutesQueued")
                        .HasForeignKey("CarrierQueuedId")
                        .OnDelete(DeleteBehavior.Cascade);
                });

            modelBuilder.Entity("BusMap.WebApi.DatabaseModels.RouteReport", b =>
                {
                    b.HasOne("BusMap.WebApi.DatabaseModels.Route", "Route")
                        .WithMany()
                        .HasForeignKey("RouteId")
                        .OnDelete(DeleteBehavior.Cascade);
                });
#pragma warning restore 612, 618
        }
    }
}
