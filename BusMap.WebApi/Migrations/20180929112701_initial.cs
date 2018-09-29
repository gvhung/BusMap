using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Migrations;

namespace BusMap.WebApi.Migrations
{
    public partial class initial : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "Carriers",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn),
                    Name = table.Column<string>(maxLength: 100, nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Carriers", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "Routes",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn),
                    Name = table.Column<string>(maxLength: 100, nullable: false),
                    CarrierId = table.Column<int>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Routes", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Routes_Carriers_CarrierId",
                        column: x => x.CarrierId,
                        principalTable: "Carriers",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "BusStops",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn),
                    Latitude = table.Column<double>(nullable: false),
                    Longitude = table.Column<double>(nullable: false),
                    Address = table.Column<string>(maxLength: 50, nullable: false),
                    Label = table.Column<string>(maxLength: 50, nullable: true),
                    RouteId = table.Column<int>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_BusStops", x => x.Id);
                    table.ForeignKey(
                        name: "FK_BusStops_Routes_RouteId",
                        column: x => x.RouteId,
                        principalTable: "Routes",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.InsertData(
                table: "Carriers",
                columns: new[] { "Id", "Name" },
                values: new object[] { 1, "Nowex Transport" });

            migrationBuilder.InsertData(
                table: "Carriers",
                columns: new[] { "Id", "Name" },
                values: new object[] { 2, "Kolos" });

            migrationBuilder.InsertData(
                table: "Routes",
                columns: new[] { "Id", "CarrierId", "Name" },
                values: new object[] { 1, 1, "Gorlice - Rzeszów" });

            migrationBuilder.InsertData(
                table: "Routes",
                columns: new[] { "Id", "CarrierId", "Name" },
                values: new object[] { 2, 1, "Rzeszów - Gorlice" });

            migrationBuilder.InsertData(
                table: "Routes",
                columns: new[] { "Id", "CarrierId", "Name" },
                values: new object[] { 3, 2, "Frysztak - Rzeszów" });

            migrationBuilder.InsertData(
                table: "BusStops",
                columns: new[] { "Id", "Address", "Label", "Latitude", "Longitude", "RouteId" },
                values: new object[,]
                {
                    { 1, "Gorlice", "Gorlice DA", 49.662932, 21.159447, 1 },
                    { 29, "Boguchwała", null, 49.983775, 21.942793, 3 },
                    { 28, "Czudec", "Czudec DA", 49.945855, 21.837562, 3 },
                    { 27, "Zaborów", null, 49.914127, 21.827073, 3 },
                    { 26, "Strzyżów", "Strzyżów DA", 49.869992, 21.800657, 3 },
                    { 25, "Dobrzechów", null, 49.876198, 21.75399, 3 },
                    { 24, "Wiśniowa", null, 49.869611, 21.65995, 3 },
                    { 23, "Frysztak", null, 49.84548, 21.612531, 3 },
                    { 22, "Gorlice", "Gorlice DA", 49.662932, 21.159447, 2 },
                    { 21, "Jasło", "Jasło DA", 49.74375, 21.473399, 2 },
                    { 20, "Frysztak", null, 49.84548, 21.612531, 2 },
                    { 19, "Wiśniowa", null, 49.869611, 21.65995, 2 },
                    { 18, "Dobrzechów", null, 49.876198, 21.75399, 2 },
                    { 17, "Strzyżów", "Strzyżów DA", 49.869992, 21.800657, 2 },
                    { 30, "Rzeszów", "Podkarpacka", 50.020076, 21.980312, 3 },
                    { 16, "Czudec", "Czudec DA", 49.945855, 21.837562, 2 },
                    { 14, "Rzeszów", "Podkarpacka", 50.020076, 21.980312, 2 },
                    { 13, "Rzeszów", "Rzeszów DA", 50.042131, 22.003429, 2 },
                    { 12, "Rzeszów", "Rejtana", 50.030767, 22.017088, 2 },
                    { 11, "Rzeszów", "Rejtana", 50.031346, 22.016653, 1 },
                    { 10, "Rzeszów", "Rzeszów DA", 50.042131, 22.003429, 1 },
                    { 9, "Rzeszów", "Podkarpacka", 50.020076, 21.980312, 1 },
                    { 8, "Boguchwała", null, 49.983775, 21.942793, 1 },
                    { 7, "Czudec", "Czudec DA", 49.945855, 21.837562, 1 },
                    { 6, "Strzyżów", "Strzyżów DA", 49.869992, 21.800657, 1 },
                    { 5, "Dobrzechów", null, 49.876198, 21.75399, 1 },
                    { 4, "Wiśniowa", null, 49.869611, 21.65995, 1 },
                    { 3, "Frysztak", null, 49.84548, 21.612531, 1 },
                    { 2, "Jasło", "Jasło DA", 49.74375, 21.473399, 1 },
                    { 15, "Boguchwała", null, 49.983775, 21.942793, 2 },
                    { 31, "Rzeszów", "Rzeszów DA", 50.042131, 22.003429, 3 }
                });

            migrationBuilder.CreateIndex(
                name: "IX_BusStops_RouteId",
                table: "BusStops",
                column: "RouteId");

            migrationBuilder.CreateIndex(
                name: "IX_Routes_CarrierId",
                table: "Routes",
                column: "CarrierId");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "BusStops");

            migrationBuilder.DropTable(
                name: "Routes");

            migrationBuilder.DropTable(
                name: "Carriers");
        }
    }
}
