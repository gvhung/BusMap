using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace BusMap.WebApi.Migrations
{
    public partial class CarrierQueuedVoting : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<DateTime>(
                name: "CreatedDatetime",
                table: "CarriersQueued",
                nullable: false,
                defaultValueSql: "FORMAT(GetDate(), 'yyyy-MM-dd')");

            migrationBuilder.AddColumn<int>(
                name: "NegativeVotes",
                table: "CarriersQueued",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddColumn<int>(
                name: "PositiveVotes",
                table: "CarriersQueued",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddColumn<DateTime>(
                name: "VotingEndedDateTime",
                table: "CarriersQueued",
                nullable: true);

            migrationBuilder.AddColumn<DateTime>(
                name: "VotingStartedDatetime",
                table: "CarriersQueued",
                nullable: true);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "CreatedDatetime",
                table: "CarriersQueued");

            migrationBuilder.DropColumn(
                name: "NegativeVotes",
                table: "CarriersQueued");

            migrationBuilder.DropColumn(
                name: "PositiveVotes",
                table: "CarriersQueued");

            migrationBuilder.DropColumn(
                name: "VotingEndedDateTime",
                table: "CarriersQueued");

            migrationBuilder.DropColumn(
                name: "VotingStartedDatetime",
                table: "CarriersQueued");
        }
    }
}
