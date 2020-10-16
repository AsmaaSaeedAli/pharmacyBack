using Microsoft.EntityFrameworkCore.Migrations;

namespace Pharmacy.Migrations
{
    public partial class Add_Market_Manf_PreferStok : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<int>(
                name: "ManufactureId",
                table: "Items",
                nullable: true);

            migrationBuilder.AddColumn<int>(
                name: "MarketId",
                table: "Items",
                nullable: true);

            migrationBuilder.AddColumn<int>(
                name: "PreferedStockId",
                table: "Items",
                nullable: true);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "ManufactureId",
                table: "Items");

            migrationBuilder.DropColumn(
                name: "MarketId",
                table: "Items");

            migrationBuilder.DropColumn(
                name: "PreferedStockId",
                table: "Items");
        }
    }
}
