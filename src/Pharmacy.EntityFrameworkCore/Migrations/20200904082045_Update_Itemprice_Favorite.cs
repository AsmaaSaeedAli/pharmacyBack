using Microsoft.EntityFrameworkCore.Migrations;

namespace Pharmacy.Migrations
{
    public partial class Update_Itemprice_Favorite : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<int>(
                name: "CorporateFavoriteId",
                table: "ItemPrices",
                nullable: true);

            migrationBuilder.CreateIndex(
                name: "IX_ItemPrices_CorporateFavoriteId",
                table: "ItemPrices",
                column: "CorporateFavoriteId");

            migrationBuilder.AddForeignKey(
                name: "FK_ItemPrices_Corporates_CorporateFavoriteId",
                table: "ItemPrices",
                column: "CorporateFavoriteId",
                principalTable: "Corporates",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_ItemPrices_Corporates_CorporateFavoriteId",
                table: "ItemPrices");

            migrationBuilder.DropIndex(
                name: "IX_ItemPrices_CorporateFavoriteId",
                table: "ItemPrices");

            migrationBuilder.DropColumn(
                name: "CorporateFavoriteId",
                table: "ItemPrices");
        }
    }
}
