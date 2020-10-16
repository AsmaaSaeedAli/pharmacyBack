using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace Pharmacy.Migrations
{
    public partial class Add_Customers : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "Customer",
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
                    Code = table.Column<string>(nullable: true),
                    FullName = table.Column<string>(nullable: true),
                    PersonalPhoto = table.Column<string>(nullable: true),
                    Email = table.Column<string>(nullable: true),
                    PrimaryMobileNumber = table.Column<string>(nullable: false),
                    SecondaryMobileNumber = table.Column<string>(nullable: true),
                    GenderId = table.Column<int>(nullable: false),
                    MaritalStatusId = table.Column<int>(nullable: true),
                    NoOfDependencies = table.Column<int>(nullable: false),
                    DateOfBirth = table.Column<DateTime>(nullable: false),
                    Notes = table.Column<string>(nullable: true),
                    NationalityId = table.Column<int>(nullable: false),
                    CountryId = table.Column<int>(nullable: false),
                    CityId = table.Column<int>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Customer", x => x.Id);
                });
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "Customer");
        }
    }
}
