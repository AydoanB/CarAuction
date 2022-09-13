using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace CarAuction.Data.Migrations
{
    public partial class CylindersToHorsePower : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "VIN",
                table: "Cars");

            migrationBuilder.RenameColumn(
                name: "Cylinders",
                table: "Engines",
                newName: "HorsePower");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameColumn(
                name: "HorsePower",
                table: "Engines",
                newName: "Cylinders");

            migrationBuilder.AddColumn<string>(
                name: "VIN",
                table: "Cars",
                type: "nvarchar(17)",
                maxLength: 17,
                nullable: false,
                defaultValue: "");
        }
    }
}
