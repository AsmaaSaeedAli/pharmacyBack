using Microsoft.EntityFrameworkCore.Migrations;

namespace Pharmacy.Migrations
{
    public partial class Vat_For_Items : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<bool>(
                name: "HasVat",
                table: "Items",
                nullable: false,
                defaultValue: true);

            migrationBuilder.AddColumn<decimal>(
                name: "Vat",
                table: "Items",
                nullable: false,
                defaultValue: 5m);

            migrationBuilder.AddColumn<decimal>(
                name: "Vat",
                table: "InvoiceItems",
                nullable: false,
                defaultValue: 0m);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "HasVat",
                table: "Items");

            migrationBuilder.DropColumn(
                name: "Vat",
                table: "Items");

            migrationBuilder.DropColumn(
                name: "Vat",
                table: "InvoiceItems");
        }
    }
}
