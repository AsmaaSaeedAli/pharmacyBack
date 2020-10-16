using Microsoft.EntityFrameworkCore.Migrations;

namespace Pharmacy.Migrations
{
    public partial class Update_Itemprice_Discount2 : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "DiscountPrice",
                table: "ItemPrices");

            migrationBuilder.AddColumn<decimal>(
                name: "Discount",
                table: "ItemPrices",
                nullable: false,
                defaultValue: 0m);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Discount",
                table: "ItemPrices");

            migrationBuilder.AddColumn<double>(
                name: "DiscountPrice",
                table: "ItemPrices",
                type: "float",
                nullable: false,
                defaultValue: 0.0);
        }
    }
}
