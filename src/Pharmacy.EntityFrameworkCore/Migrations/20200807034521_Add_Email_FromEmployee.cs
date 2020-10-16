using Microsoft.EntityFrameworkCore.Migrations;

namespace Pharmacy.Migrations
{
    public partial class Add_Email_FromEmployee : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Employees_Countries_CountryId",
                table: "Employees");

            migrationBuilder.DropIndex(
                name: "IX_Employees_CountryId",
                table: "Employees");

            migrationBuilder.DropColumn(
                name: "CountryId",
                table: "Employees");

            migrationBuilder.AddColumn<string>(
                name: "Email",
                table: "Employees",
                nullable: false,
                defaultValue: "");

            migrationBuilder.CreateIndex(
                name: "IX_Employees_NationalityId",
                table: "Employees",
                column: "NationalityId");

            migrationBuilder.AddForeignKey(
                name: "FK_Employees_Countries_NationalityId",
                table: "Employees",
                column: "NationalityId",
                principalTable: "Countries",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Employees_Countries_NationalityId",
                table: "Employees");

            migrationBuilder.DropIndex(
                name: "IX_Employees_NationalityId",
                table: "Employees");

            migrationBuilder.DropColumn(
                name: "Email",
                table: "Employees");

            migrationBuilder.AddColumn<int>(
                name: "CountryId",
                table: "Employees",
                type: "int",
                nullable: true);

            migrationBuilder.CreateIndex(
                name: "IX_Employees_CountryId",
                table: "Employees",
                column: "CountryId");

            migrationBuilder.AddForeignKey(
                name: "FK_Employees_Countries_CountryId",
                table: "Employees",
                column: "CountryId",
                principalTable: "Countries",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);
        }
    }
}
