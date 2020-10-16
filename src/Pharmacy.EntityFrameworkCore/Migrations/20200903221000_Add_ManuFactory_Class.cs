using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace Pharmacy.Migrations
{
    public partial class Add_ManuFactory_Class : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<int>(
                name: "ManuFactoryId",
                table: "Items",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.CreateTable(
                name: "ManuFactories",
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
                    Name = table.Column<string>(nullable: true),
                    ContactName = table.Column<string>(nullable: true),
                    ContactPhone = table.Column<string>(nullable: false),
                    ContactEmail = table.Column<string>(nullable: false),
                    Notes = table.Column<string>(nullable: true),
                    IsActive = table.Column<bool>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_ManuFactories", x => x.Id);
                });

            migrationBuilder.CreateIndex(
                name: "IX_Items_ManuFactoryId",
                table: "Items",
                column: "ManuFactoryId");

            migrationBuilder.AddForeignKey(
                name: "FK_Items_ManuFactories_ManuFactoryId",
                table: "Items",
                column: "ManuFactoryId",
                principalTable: "ManuFactories",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Items_ManuFactories_ManuFactoryId",
                table: "Items");

            migrationBuilder.DropTable(
                name: "ManuFactories");

            migrationBuilder.DropIndex(
                name: "IX_Items_ManuFactoryId",
                table: "Items");

            migrationBuilder.DropColumn(
                name: "ManuFactoryId",
                table: "Items");
        }
    }
}
