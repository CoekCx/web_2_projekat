using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace NewAvalon.UserAdministration.Persistence.Migrations
{
    public partial class SeedSuperAdminUser : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.InsertData(
                table: "Users",
                columns: new[] { "Id", "Address", "CreatedOnUtc", "DateOfBirth", "Email", "FirstName", "IdentityProviderId", "LastName", "ModifiedOnUtc", "Password", "UserName" },
                values: new object[] { new Guid("e486bbf4-f94b-4e61-9d9f-01ce92cccb2d"), "Admin address", new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), "admin@admin.com", "Admin", null, "Admin", null, null, "Admin" });

            migrationBuilder.InsertData(
                table: "RoleUser",
                columns: new[] { "RolesId", "UsersId" },
                values: new object[] { 2, new Guid("e486bbf4-f94b-4e61-9d9f-01ce92cccb2d") });
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DeleteData(
                table: "RoleUser",
                keyColumns: new[] { "RolesId", "UsersId" },
                keyValues: new object[] { 2, new Guid("e486bbf4-f94b-4e61-9d9f-01ce92cccb2d") });

            migrationBuilder.DeleteData(
                table: "Users",
                keyColumn: "Id",
                keyValue: new Guid("e486bbf4-f94b-4e61-9d9f-01ce92cccb2d"));
        }
    }
}
