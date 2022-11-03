using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace CarAuction.Data.Migrations
{
    public partial class AddCarWatchlistToUser : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "WatchedCarId",
                table: "Cars",
                type: "nvarchar(450)",
                nullable: true);

            migrationBuilder.CreateTable(
                name: "WatchedCars",
                columns: table => new
                {
                    Id = table.Column<string>(type: "nvarchar(450)", nullable: false),
                    UserId = table.Column<string>(type: "nvarchar(450)", nullable: true),
                    CreatedOn = table.Column<DateTime>(type: "datetime2", nullable: false),
                    ModifiedOn = table.Column<DateTime>(type: "datetime2", nullable: true),
                    IsDeleted = table.Column<bool>(type: "bit", nullable: false),
                    DeletedOn = table.Column<DateTime>(type: "datetime2", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_WatchedCars", x => x.Id);
                    table.ForeignKey(
                        name: "FK_WatchedCars_AspNetUsers_UserId",
                        column: x => x.UserId,
                        principalTable: "AspNetUsers",
                        principalColumn: "Id");
                });

            migrationBuilder.CreateIndex(
                name: "IX_Cars_WatchedCarId",
                table: "Cars",
                column: "WatchedCarId");

            migrationBuilder.CreateIndex(
                name: "IX_WatchedCars_IsDeleted",
                table: "WatchedCars",
                column: "IsDeleted");

            migrationBuilder.CreateIndex(
                name: "IX_WatchedCars_UserId",
                table: "WatchedCars",
                column: "UserId");

            migrationBuilder.AddForeignKey(
                name: "FK_Cars_WatchedCars_WatchedCarId",
                table: "Cars",
                column: "WatchedCarId",
                principalTable: "WatchedCars",
                principalColumn: "Id");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Cars_WatchedCars_WatchedCarId",
                table: "Cars");

            migrationBuilder.DropTable(
                name: "WatchedCars");

            migrationBuilder.DropIndex(
                name: "IX_Cars_WatchedCarId",
                table: "Cars");

            migrationBuilder.DropColumn(
                name: "WatchedCarId",
                table: "Cars");
        }
    }
}
