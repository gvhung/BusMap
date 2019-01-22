using Microsoft.EntityFrameworkCore.Migrations;

namespace BusMap.WebApi.Migrations
{
    public partial class FullCascadeRouteQueued : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_RoutesQueued_CarriersQueued_CarrierQueuedId",
                table: "RoutesQueued");

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
                name: "FK_RoutesQueued_CarriersQueued_CarrierQueuedId",
                table: "RoutesQueued");

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
