using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace NewAvalon.UserAdministration.Persistence.Migrations
{
    public partial class AddSuperAdminPasswordUser : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DeleteData(
                table: "RoleUser",
                keyColumns: new[] { "RolesId", "UsersId" },
                keyValues: new object[] { 2, new Guid("e486bbf4-f94b-4e61-9d9f-01ce92cccb2d") });

            migrationBuilder.DeleteData(
                table: "Users",
                keyColumn: "Id",
                keyValue: new Guid("e486bbf4-f94b-4e61-9d9f-01ce92cccb2d"));

            migrationBuilder.InsertData(
                table: "Users",
                columns: new[] { "Id", "Address", "CreatedOnUtc", "DateOfBirth", "Email", "FirstName", "IdentityProviderId", "LastName", "ModifiedOnUtc", "Password", "UserName" },
                values: new object[] { new Guid("cb15aa3b-be03-426c-a620-0e9bc9e51d00"), "Admin address", new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), "admin@admin.com", "Admin", null, "Admin", null, "]C�?Z����ϼ����eA��,���_��", "Admin" });

            migrationBuilder.InsertData(
                table: "RoleUser",
                columns: new[] { "RolesId", "UsersId" },
                values: new object[] { 2, new Guid("cb15aa3b-be03-426c-a620-0e9bc9e51d00") });
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DeleteData(
                table: "RoleUser",
                keyColumns: new[] { "RolesId", "UsersId" },
                keyValues: new object[] { 2, new Guid("cb15aa3b-be03-426c-a620-0e9bc9e51d00") });

            migrationBuilder.DeleteData(
                table: "Users",
                keyColumn: "Id",
                keyValue: new Guid("cb15aa3b-be03-426c-a620-0e9bc9e51d00"));

            migrationBuilder.InsertData(
                table: "Users",
                columns: new[] { "Id", "Address", "CreatedOnUtc", "DateOfBirth", "Email", "FirstName", "IdentityProviderId", "LastName", "ModifiedOnUtc", "Password", "UserName" },
                values: new object[] { new Guid("e486bbf4-f94b-4e61-9d9f-01ce92cccb2d"), "Admin address", new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), "admin@admin.com", "Admin", null, "Admin", null, null, "Admin" });

            migrationBuilder.InsertData(
                table: "RoleUser",
                columns: new[] { "RolesId", "UsersId" },
                values: new object[] { 2, new Guid("e486bbf4-f94b-4e61-9d9f-01ce92cccb2d") });
        }
    }
}
