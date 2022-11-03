using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace CarAuction.Data.Migrations
{
    public partial class RenameEntityWatchedCarToUserWatchedCars : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Cars_WatchedCars_WatchedCarId",
                table: "Cars");

            migrationBuilder.RenameColumn(
                name: "WatchedCarId",
                table: "Cars",
                newName: "UserWatchedCarsId");

            migrationBuilder.RenameIndex(
                name: "IX_Cars_WatchedCarId",
                table: "Cars",
                newName: "IX_Cars_UserWatchedCarsId");

            migrationBuilder.AddForeignKey(
                name: "FK_Cars_WatchedCars_UserWatchedCarsId",
                table: "Cars",
                column: "UserWatchedCarsId",
                principalTable: "WatchedCars",
                principalColumn: "Id");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Cars_WatchedCars_UserWatchedCarsId",
                table: "Cars");

            migrationBuilder.RenameColumn(
                name: "UserWatchedCarsId",
                table: "Cars",
                newName: "WatchedCarId");

            migrationBuilder.RenameIndex(
                name: "IX_Cars_UserWatchedCarsId",
                table: "Cars",
                newName: "IX_Cars_WatchedCarId");

            migrationBuilder.AddForeignKey(
                name: "FK_Cars_WatchedCars_WatchedCarId",
                table: "Cars",
                column: "WatchedCarId",
                principalTable: "WatchedCars",
                principalColumn: "Id");
        }
    }
}
