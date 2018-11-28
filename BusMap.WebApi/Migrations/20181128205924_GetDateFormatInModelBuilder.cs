using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace BusMap.WebApi.Migrations
{
    public partial class GetDateFormatInModelBuilder : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AlterColumn<DateTime>(
                name: "CreatedDatetime",
                table: "RoutesQueued",
                nullable: false,
                defaultValueSql: "FORMAT(GetDate(), 'yyyy-MM-dd')",
                oldClrType: typeof(DateTime),
                oldDefaultValueSql: "GETDATE()");

            migrationBuilder.AlterColumn<DateTime>(
                name: "Date",
                table: "RouteReports",
                nullable: false,
                defaultValueSql: "FORMAT(GetDate(), 'yyyy-MM-dd')",
                oldClrType: typeof(DateTime),
                oldDefaultValueSql: "GETDATE()");

            migrationBuilder.AlterColumn<DateTime>(
                name: "Date",
                table: "BusStopTraces",
                nullable: false,
                defaultValueSql: "FORMAT(GetDate(), 'yyyy-MM-dd')",
                oldClrType: typeof(DateTime),
                oldDefaultValueSql: "GETDATE()");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AlterColumn<DateTime>(
                name: "CreatedDatetime",
                table: "RoutesQueued",
                nullable: false,
                defaultValueSql: "GETDATE()",
                oldClrType: typeof(DateTime),
                oldDefaultValueSql: "FORMAT(GetDate(), 'yyyy-MM-dd')");

            migrationBuilder.AlterColumn<DateTime>(
                name: "Date",
                table: "RouteReports",
                nullable: false,
                defaultValueSql: "GETDATE()",
                oldClrType: typeof(DateTime),
                oldDefaultValueSql: "FORMAT(GetDate(), 'yyyy-MM-dd')");

            migrationBuilder.AlterColumn<DateTime>(
                name: "Date",
                table: "BusStopTraces",
                nullable: false,
                defaultValueSql: "GETDATE()",
                oldClrType: typeof(DateTime),
                oldDefaultValueSql: "FORMAT(GetDate(), 'yyyy-MM-dd')");
        }
    }
}
