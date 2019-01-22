using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace BusMap.WebApi.Migrations
{
    public partial class BusStopHour_RouteDayOfTheWeek : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_BusStopsQueued_RoutesQueued_RouteQueuedId",
                table: "BusStopsQueued");

            migrationBuilder.DropForeignKey(
                name: "FK_RoutesQueued_CarriersQueued_CarrierQueuedId",
                table: "RoutesQueued");

            migrationBuilder.AlterColumn<int>(
                name: "CarrierQueuedId",
                table: "RoutesQueued",
                nullable: false,
                oldClrType: typeof(int),
                oldNullable: true);

            migrationBuilder.AddColumn<int>(
                name: "DayOfTheWeek",
                table: "RoutesQueued",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddColumn<int>(
                name: "DayOfTheWeek",
                table: "Routes",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AlterColumn<int>(
                name: "RouteQueuedId",
                table: "BusStopsQueued",
                nullable: false,
                oldClrType: typeof(int),
                oldNullable: true);

            migrationBuilder.AddColumn<TimeSpan>(
                name: "Hour",
                table: "BusStopsQueued",
                nullable: false,
                defaultValue: new TimeSpan(0, 0, 0, 0, 0));

            migrationBuilder.AddColumn<TimeSpan>(
                name: "Hour",
                table: "BusStops",
                nullable: false,
                defaultValue: new TimeSpan(0, 0, 0, 0, 0));

            migrationBuilder.UpdateData(
                table: "Routes",
                keyColumn: "Id",
                keyValue: 1,
                column: "DayOfTheWeek",
                value: 1);

            migrationBuilder.AddForeignKey(
                name: "FK_BusStopsQueued_RoutesQueued_RouteQueuedId",
                table: "BusStopsQueued",
                column: "RouteQueuedId",
                principalTable: "RoutesQueued",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_RoutesQueued_CarriersQueued_CarrierQueuedId",
                table: "RoutesQueued",
                column: "CarrierQueuedId",
                principalTable: "CarriersQueued",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_BusStopsQueued_RoutesQueued_RouteQueuedId",
                table: "BusStopsQueued");

            migrationBuilder.DropForeignKey(
                name: "FK_RoutesQueued_CarriersQueued_CarrierQueuedId",
                table: "RoutesQueued");

            migrationBuilder.DropColumn(
                name: "DayOfTheWeek",
                table: "RoutesQueued");

            migrationBuilder.DropColumn(
                name: "DayOfTheWeek",
                table: "Routes");

            migrationBuilder.DropColumn(
                name: "Hour",
                table: "BusStopsQueued");

            migrationBuilder.DropColumn(
                name: "Hour",
                table: "BusStops");

            migrationBuilder.AlterColumn<int>(
                name: "CarrierQueuedId",
                table: "RoutesQueued",
                nullable: true,
                oldClrType: typeof(int));

            migrationBuilder.AlterColumn<int>(
                name: "RouteQueuedId",
                table: "BusStopsQueued",
                nullable: true,
                oldClrType: typeof(int));

            migrationBuilder.AddForeignKey(
                name: "FK_BusStopsQueued_RoutesQueued_RouteQueuedId",
                table: "BusStopsQueued",
                column: "RouteQueuedId",
                principalTable: "RoutesQueued",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_RoutesQueued_CarriersQueued_CarrierQueuedId",
                table: "RoutesQueued",
                column: "CarrierQueuedId",
                principalTable: "CarriersQueued",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);
        }
    }
}
