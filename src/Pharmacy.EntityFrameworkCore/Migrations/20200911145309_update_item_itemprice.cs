using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace Pharmacy.Migrations
{
    public partial class update_item_itemprice : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_ItemPrices_Corporates_CorporateFavoriteId",
                table: "ItemPrices");

            migrationBuilder.DropForeignKey(
                name: "FK_ItemPrices_Corporates_CorporateId",
                table: "ItemPrices");

            migrationBuilder.DropIndex(
                name: "IX_ItemPrices_CorporateFavoriteId",
                table: "ItemPrices");

            migrationBuilder.DropIndex(
                name: "IX_ItemPrices_CorporateId",
                table: "ItemPrices");

            migrationBuilder.DropColumn(
                name: "CorporateFavoriteId",
                table: "ItemPrices");

            migrationBuilder.DropColumn(
                name: "CorporateId",
                table: "ItemPrices");

            migrationBuilder.DropColumn(
                name: "ExpiringDate",
                table: "ItemPrices");

            migrationBuilder.DropColumn(
                name: "ProductionDate",
                table: "ItemPrices");

            migrationBuilder.AddColumn<int>(
                name: "CorporateFavoriteId",
                table: "Items",
                nullable: true);

            migrationBuilder.AlterColumn<int>(
                name: "TenantId",
                table: "ItemPrices",
                nullable: true,
                oldClrType: typeof(int),
                oldType: "int");

            migrationBuilder.CreateIndex(
                name: "IX_Items_CorporateFavoriteId",
                table: "Items",
                column: "CorporateFavoriteId");

            migrationBuilder.AddForeignKey(
                name: "FK_Items_Corporates_CorporateFavoriteId",
                table: "Items",
                column: "CorporateFavoriteId",
                principalTable: "Corporates",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Items_Corporates_CorporateFavoriteId",
                table: "Items");

            migrationBuilder.DropIndex(
                name: "IX_Items_CorporateFavoriteId",
                table: "Items");

            migrationBuilder.DropColumn(
                name: "CorporateFavoriteId",
                table: "Items");

            migrationBuilder.AlterColumn<int>(
                name: "TenantId",
                table: "ItemPrices",
                type: "int",
                nullable: false,
                oldClrType: typeof(int),
                oldNullable: true);

            migrationBuilder.AddColumn<int>(
                name: "CorporateFavoriteId",
                table: "ItemPrices",
                type: "int",
                nullable: true);

            migrationBuilder.AddColumn<int>(
                name: "CorporateId",
                table: "ItemPrices",
                type: "int",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddColumn<DateTime>(
                name: "ExpiringDate",
                table: "ItemPrices",
                type: "datetime2",
                nullable: false,
                defaultValue: new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified));

            migrationBuilder.AddColumn<DateTime>(
                name: "ProductionDate",
                table: "ItemPrices",
                type: "datetime2",
                nullable: false,
                defaultValue: new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified));

            migrationBuilder.CreateIndex(
                name: "IX_ItemPrices_CorporateFavoriteId",
                table: "ItemPrices",
                column: "CorporateFavoriteId");

            migrationBuilder.CreateIndex(
                name: "IX_ItemPrices_CorporateId",
                table: "ItemPrices",
                column: "CorporateId");

            migrationBuilder.AddForeignKey(
                name: "FK_ItemPrices_Corporates_CorporateFavoriteId",
                table: "ItemPrices",
                column: "CorporateFavoriteId",
                principalTable: "Corporates",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_ItemPrices_Corporates_CorporateId",
                table: "ItemPrices",
                column: "CorporateId",
                principalTable: "Corporates",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
