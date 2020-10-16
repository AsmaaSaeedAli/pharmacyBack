using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace Pharmacy.Migrations
{
    public partial class _Update_Category_Entity : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<int>(
                name: "ItemClassId",
                table: "Categories",
                nullable: true);

            migrationBuilder.CreateTable(
                name: "ItemClasses",
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
                    IsActive = table.Column<bool>(nullable: false),
                    Name = table.Column<string>(nullable: true),
                    Code = table.Column<string>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_ItemClasses", x => x.Id);
                });

            migrationBuilder.CreateIndex(
                name: "IX_Categories_ItemClassId",
                table: "Categories",
                column: "ItemClassId");

            migrationBuilder.AddForeignKey(
                name: "FK_Categories_ItemClasses_ItemClassId",
                table: "Categories",
                column: "ItemClassId",
                principalTable: "ItemClasses",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Categories_ItemClasses_ItemClassId",
                table: "Categories");

            migrationBuilder.DropTable(
                name: "ItemClasses");

            migrationBuilder.DropIndex(
                name: "IX_Categories_ItemClassId",
                table: "Categories");

            migrationBuilder.DropColumn(
                name: "ItemClassId",
                table: "Categories");
        }
    }
}
