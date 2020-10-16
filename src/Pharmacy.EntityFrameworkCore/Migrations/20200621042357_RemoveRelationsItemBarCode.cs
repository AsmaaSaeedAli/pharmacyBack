using Microsoft.EntityFrameworkCore.Migrations;

namespace Pharmacy.Migrations
{
    public partial class RemoveRelationsItemBarCode : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_ItemBarCodes_Items_ItemId",
                table: "ItemBarCodes");

            migrationBuilder.DropIndex(
                name: "IX_ItemBarCodes_ItemId",
                table: "ItemBarCodes");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateIndex(
                name: "IX_ItemBarCodes_ItemId",
                table: "ItemBarCodes",
                column: "ItemId");

            migrationBuilder.AddForeignKey(
                name: "FK_ItemBarCodes_Items_ItemId",
                table: "ItemBarCodes",
                column: "ItemId",
                principalTable: "Items",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
