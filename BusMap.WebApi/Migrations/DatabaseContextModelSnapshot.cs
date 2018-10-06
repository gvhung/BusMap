﻿// <auto-generated />
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
                .HasAnnotation("ProductVersion", "2.1.3-rtm-32065")
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

                    b.Property<string>("Label")
                        .HasMaxLength(50);

                    b.Property<double>("Latitude");

                    b.Property<double>("Longitude");

                    b.Property<int>("RouteId");

                    b.HasKey("Id");

                    b.HasIndex("RouteId");

                    b.ToTable("BusStops");

                    b.HasData(
                        new { Id = 1, Address = "Gorlice", Label = "Gorlice DA", Latitude = 49.662932, Longitude = 21.159447, RouteId = 1 },
                        new { Id = 2, Address = "Jasło", Label = "Jasło DA", Latitude = 49.74375, Longitude = 21.473399, RouteId = 1 },
                        new { Id = 3, Address = "Frysztak", Latitude = 49.84548, Longitude = 21.612531, RouteId = 1 },
                        new { Id = 4, Address = "Wiśniowa", Latitude = 49.869611, Longitude = 21.65995, RouteId = 1 },
                        new { Id = 5, Address = "Dobrzechów", Latitude = 49.876198, Longitude = 21.75399, RouteId = 1 },
                        new { Id = 6, Address = "Strzyżów", Label = "Strzyżów DA", Latitude = 49.869992, Longitude = 21.800657, RouteId = 1 },
                        new { Id = 7, Address = "Czudec", Label = "Czudec DA", Latitude = 49.945855, Longitude = 21.837562, RouteId = 1 },
                        new { Id = 8, Address = "Boguchwała", Latitude = 49.983775, Longitude = 21.942793, RouteId = 1 },
                        new { Id = 9, Address = "Rzeszów", Label = "Podkarpacka", Latitude = 50.020076, Longitude = 21.980312, RouteId = 1 },
                        new { Id = 10, Address = "Rzeszów", Label = "Rzeszów DA", Latitude = 50.042131, Longitude = 22.003429, RouteId = 1 },
                        new { Id = 11, Address = "Rzeszów", Label = "Rejtana", Latitude = 50.031346, Longitude = 22.016653, RouteId = 1 },
                        new { Id = 12, Address = "Rzeszów", Label = "Rejtana", Latitude = 50.030767, Longitude = 22.017088, RouteId = 2 },
                        new { Id = 13, Address = "Rzeszów", Label = "Rzeszów DA", Latitude = 50.042131, Longitude = 22.003429, RouteId = 2 },
                        new { Id = 14, Address = "Rzeszów", Label = "Podkarpacka", Latitude = 50.020076, Longitude = 21.980312, RouteId = 2 },
                        new { Id = 15, Address = "Boguchwała", Latitude = 49.983775, Longitude = 21.942793, RouteId = 2 },
                        new { Id = 16, Address = "Czudec", Label = "Czudec DA", Latitude = 49.945855, Longitude = 21.837562, RouteId = 2 },
                        new { Id = 17, Address = "Strzyżów", Label = "Strzyżów DA", Latitude = 49.869992, Longitude = 21.800657, RouteId = 2 },
                        new { Id = 18, Address = "Dobrzechów", Latitude = 49.876198, Longitude = 21.75399, RouteId = 2 },
                        new { Id = 19, Address = "Wiśniowa", Latitude = 49.869611, Longitude = 21.65995, RouteId = 2 },
                        new { Id = 20, Address = "Frysztak", Latitude = 49.84548, Longitude = 21.612531, RouteId = 2 },
                        new { Id = 21, Address = "Jasło", Label = "Jasło DA", Latitude = 49.74375, Longitude = 21.473399, RouteId = 2 },
                        new { Id = 22, Address = "Gorlice", Label = "Gorlice DA", Latitude = 49.662932, Longitude = 21.159447, RouteId = 2 },
                        new { Id = 23, Address = "Frysztak", Latitude = 49.84548, Longitude = 21.612531, RouteId = 3 },
                        new { Id = 24, Address = "Wiśniowa", Latitude = 49.869611, Longitude = 21.65995, RouteId = 3 },
                        new { Id = 25, Address = "Dobrzechów", Latitude = 49.876198, Longitude = 21.75399, RouteId = 3 },
                        new { Id = 26, Address = "Strzyżów", Label = "Strzyżów DA", Latitude = 49.869992, Longitude = 21.800657, RouteId = 3 },
                        new { Id = 27, Address = "Zaborów", Latitude = 49.914127, Longitude = 21.827073, RouteId = 3 },
                        new { Id = 28, Address = "Czudec", Label = "Czudec DA", Latitude = 49.945855, Longitude = 21.837562, RouteId = 3 },
                        new { Id = 29, Address = "Boguchwała", Latitude = 49.983775, Longitude = 21.942793, RouteId = 3 },
                        new { Id = 30, Address = "Rzeszów", Label = "Podkarpacka", Latitude = 50.020076, Longitude = 21.980312, RouteId = 3 },
                        new { Id = 31, Address = "Rzeszów", Label = "Rzeszów DA", Latitude = 50.042131, Longitude = 22.003429, RouteId = 3 }
                    );
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

            modelBuilder.Entity("BusMap.WebApi.DatabaseModels.Route", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

                    b.Property<int>("CarrierId");

                    b.Property<string>("Name")
                        .IsRequired()
                        .HasMaxLength(100);

                    b.HasKey("Id");

                    b.HasIndex("CarrierId");

                    b.ToTable("Routes");

                    b.HasData(
                        new { Id = 1, CarrierId = 1, Name = "Gorlice - Rzeszów" },
                        new { Id = 2, CarrierId = 1, Name = "Rzeszów - Gorlice" },
                        new { Id = 3, CarrierId = 2, Name = "Frysztak - Rzeszów" }
                    );
                });

            modelBuilder.Entity("BusMap.WebApi.DatabaseModels.BusStop", b =>
                {
                    b.HasOne("BusMap.WebApi.DatabaseModels.Route", "Route")
                        .WithMany("BusStops")
                        .HasForeignKey("RouteId")
                        .OnDelete(DeleteBehavior.Cascade);
                });

            modelBuilder.Entity("BusMap.WebApi.DatabaseModels.Route", b =>
                {
                    b.HasOne("BusMap.WebApi.DatabaseModels.Carrier", "Carrier")
                        .WithMany("Routes")
                        .HasForeignKey("CarrierId")
                        .OnDelete(DeleteBehavior.Cascade);
                });
#pragma warning restore 612, 618
        }
    }
}
