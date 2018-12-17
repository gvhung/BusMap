using Microsoft.EntityFrameworkCore.Migrations;

namespace BusMap.WebApi.Migrations
{
    public partial class QueueUpdateWithOptionalRouteQueuedAndCarrierId : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_RoutesQueued_CarriersQueued_CarrierQueuedId",
                table: "RoutesQueued");

            migrationBuilder.AlterColumn<int>(
                name: "CarrierQueuedId",
                table: "RoutesQueued",
                nullable: true,
                oldClrType: typeof(int));

            migrationBuilder.AddColumn<int>(
                name: "CarrierId",
                table: "RoutesQueued",
                nullable: true);

            migrationBuilder.AddForeignKey(
                name: "FK_RoutesQueued_CarriersQueued_CarrierQueuedId",
                table: "RoutesQueued",
                column: "CarrierQueuedId",
                principalTable: "CarriersQueued",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_RoutesQueued_CarriersQueued_CarrierQueuedId",
                table: "RoutesQueued");

            migrationBuilder.DropColumn(
                name: "CarrierId",
                table: "RoutesQueued");

            migrationBuilder.AlterColumn<int>(
                name: "CarrierQueuedId",
                table: "RoutesQueued",
                nullable: false,
                oldClrType: typeof(int),
                oldNullable: true);

            migrationBuilder.AddForeignKey(
                name: "FK_RoutesQueued_CarriersQueued_CarrierQueuedId",
                table: "RoutesQueued",
                column: "CarrierQueuedId",
                principalTable: "CarriersQueued",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
