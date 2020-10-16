using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace Pharmacy.Migrations
{
    public partial class full_items : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {

            migrationBuilder.CreateTable(
                name: "Items",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    CreationTime = table.Column<DateTime>(nullable: false),
                    CreatorUserId = table.Column<long>(nullable: true),
                    LastModificationTime = table.Column<DateTime>(nullable: true),
                    LastModifierUserId = table.Column<long>(nullable: true),
                    IsDeleted = table.Column<bool>(nullable: false),
                    DeleterUserId = table.Column<long>(nullable: true),
                    DeletionTime = table.Column<DateTime>(nullable: true),
                    TenantId = table.Column<int>(nullable: true),
                    IsActive = table.Column<bool>(nullable: false),
                    ItemNumber = table.Column<string>(nullable: true),
                    Name = table.Column<string>(nullable: true),
                    Description = table.Column<string>(nullable: true),
                    ItemClassId = table.Column<int>(nullable: false),
                    CategoryId = table.Column<int>(nullable: false),
                    SubCategoryId = table.Column<int>(nullable: false),
                    BarCode = table.Column<string>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Items", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Items_Categories_CategoryId",
                        column: x => x.CategoryId,
                        principalTable: "Categories",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_Items_ItemClasses_ItemClassId",
                        column: x => x.ItemClassId,
                        principalTable: "ItemClasses",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_Items_SubCategories_SubCategoryId",
                        column: x => x.SubCategoryId,
                        principalTable: "SubCategories",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "ItemBarCodes",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    CreationTime = table.Column<DateTime>(nullable: false),
                    CreatorUserId = table.Column<long>(nullable: true),
                    LastModificationTime = table.Column<DateTime>(nullable: true),
                    LastModifierUserId = table.Column<long>(nullable: true),
                    IsDeleted = table.Column<bool>(nullable: false),
                    DeleterUserId = table.Column<long>(nullable: true),
                    DeletionTime = table.Column<DateTime>(nullable: true),
                    TenantId = table.Column<int>(nullable: false),
                    IsActive = table.Column<bool>(nullable: false),
                    ItemId = table.Column<int>(nullable: false),
                    BarCode = table.Column<string>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_ItemBarCodes", x => x.Id);
                    table.ForeignKey(
                        name: "FK_ItemBarCodes_Items_ItemId",
                        column: x => x.ItemId,
                        principalTable: "Items",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "ItemPrices",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    CreationTime = table.Column<DateTime>(nullable: false),
                    CreatorUserId = table.Column<long>(nullable: true),
                    LastModificationTime = table.Column<DateTime>(nullable: true),
                    LastModifierUserId = table.Column<long>(nullable: true),
                    IsDeleted = table.Column<bool>(nullable: false),
                    DeleterUserId = table.Column<long>(nullable: true),
                    DeletionTime = table.Column<DateTime>(nullable: true),
                    TenantId = table.Column<int>(nullable: false),
                    IsActive = table.Column<bool>(nullable: false),
                    ItemId = table.Column<int>(nullable: false),
                    ProductionDate = table.Column<DateTime>(nullable: false),
                    ExpiringDate = table.Column<DateTime>(nullable: false),
                    Price = table.Column<decimal>(nullable: false),
                    CorporateId = table.Column<int>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_ItemPrices", x => x.Id);
                    table.ForeignKey(
                        name: "FK_ItemPrices_Corporates_CorporateId",
                        column: x => x.CorporateId,
                        principalTable: "Corporates",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_ItemPrices_Items_ItemId",
                        column: x => x.ItemId,
                        principalTable: "Items",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "ItemQuantities",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    CreationTime = table.Column<DateTime>(nullable: false),
                    CreatorUserId = table.Column<long>(nullable: true),
                    LastModificationTime = table.Column<DateTime>(nullable: true),
                    LastModifierUserId = table.Column<long>(nullable: true),
                    IsDeleted = table.Column<bool>(nullable: false),
                    DeleterUserId = table.Column<long>(nullable: true),
                    DeletionTime = table.Column<DateTime>(nullable: true),
                    BranchId = table.Column<int>(nullable: false),
                    ItemPriceId = table.Column<int>(nullable: false),
                    Quantity = table.Column<decimal>(nullable: false),
                    UnitId = table.Column<int>(nullable: false),
                    IsActive = table.Column<bool>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_ItemQuantities", x => x.Id);
                    table.ForeignKey(
                        name: "FK_ItemQuantities_Branches_BranchId",
                        column: x => x.BranchId,
                        principalTable: "Branches",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_ItemQuantities_ItemPrices_ItemPriceId",
                        column: x => x.ItemPriceId,
                        principalTable: "ItemPrices",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_ItemQuantities_Lookups_UnitId",
                        column: x => x.UnitId,
                        principalTable: "Lookups",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_ItemBarCodes_ItemId",
                table: "ItemBarCodes",
                column: "ItemId");

            migrationBuilder.CreateIndex(
                name: "IX_ItemPrices_CorporateId",
                table: "ItemPrices",
                column: "CorporateId");

            migrationBuilder.CreateIndex(
                name: "IX_ItemPrices_ItemId",
                table: "ItemPrices",
                column: "ItemId");

            migrationBuilder.CreateIndex(
                name: "IX_ItemQuantities_BranchId",
                table: "ItemQuantities",
                column: "BranchId");

            migrationBuilder.CreateIndex(
                name: "IX_ItemQuantities_ItemPriceId",
                table: "ItemQuantities",
                column: "ItemPriceId");

            migrationBuilder.CreateIndex(
                name: "IX_ItemQuantities_UnitId",
                table: "ItemQuantities",
                column: "UnitId");

            migrationBuilder.CreateIndex(
                name: "IX_Items_CategoryId",
                table: "Items",
                column: "CategoryId");

            migrationBuilder.CreateIndex(
                name: "IX_Items_ItemClassId",
                table: "Items",
                column: "ItemClassId");

            migrationBuilder.CreateIndex(
                name: "IX_Items_SubCategoryId",
                table: "Items",
                column: "SubCategoryId");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "ItemBarCodes");

            migrationBuilder.DropTable(
                name: "ItemQuantities");

            migrationBuilder.DropTable(
                name: "ItemPrices");

            migrationBuilder.DropTable(
                name: "Items");
        }
    }
}
