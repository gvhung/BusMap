using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace BusMap.WebApi.Migrations
{
    public partial class TraceDateAdded_DateAndHourAutoSet : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AlterColumn<TimeSpan>(
                name: "Hour",
                table: "BusStopTraces",
                nullable: false,
                defaultValueSql: "Format(GETDATE(),'hh:mm')",
                oldClrType: typeof(TimeSpan));

            migrationBuilder.AddColumn<DateTime>(
                name: "Date",
                table: "BusStopTraces",
                nullable: false,
                defaultValueSql: "GETDATE()");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Date",
                table: "BusStopTraces");

            migrationBuilder.AlterColumn<TimeSpan>(
                name: "Hour",
                table: "BusStopTraces",
                nullable: false,
                oldClrType: typeof(TimeSpan),
                oldDefaultValueSql: "Format(GETDATE(),'hh:mm')");
        }
    }
}
