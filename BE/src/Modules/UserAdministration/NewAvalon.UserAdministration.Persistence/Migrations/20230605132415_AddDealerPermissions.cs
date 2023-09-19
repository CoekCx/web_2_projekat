using Microsoft.EntityFrameworkCore.Migrations;

namespace NewAvalon.UserAdministration.Persistence.Migrations
{
    public partial class AddDealerPermissions : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.InsertData(
                table: "Permissions",
                columns: new[] { "Id", "Description", "Name" },
                values: new object[,]
                {
                    { 200, "Can create products", "ProductCreate" },
                    { 201, "Can update products", "ProductUpdate" },
                    { 202, "Can delete products", "ProductDelete" },
                    { 300, "Can read dealer", "DealerRead" },
                    { 301, "Can update dealer", "DealerUpdate" }
                });

            migrationBuilder.InsertData(
                table: "PermissionRole",
                columns: new[] { "PermissionsId", "RolesId" },
                values: new object[,]
                {
                    { 200, 1 },
                    { 201, 1 },
                    { 202, 1 },
                    { 300, 2 },
                    { 301, 2 }
                });
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DeleteData(
                table: "PermissionRole",
                keyColumns: new[] { "PermissionsId", "RolesId" },
                keyValues: new object[] { 200, 1 });

            migrationBuilder.DeleteData(
                table: "PermissionRole",
                keyColumns: new[] { "PermissionsId", "RolesId" },
                keyValues: new object[] { 201, 1 });

            migrationBuilder.DeleteData(
                table: "PermissionRole",
                keyColumns: new[] { "PermissionsId", "RolesId" },
                keyValues: new object[] { 202, 1 });

            migrationBuilder.DeleteData(
                table: "PermissionRole",
                keyColumns: new[] { "PermissionsId", "RolesId" },
                keyValues: new object[] { 300, 2 });

            migrationBuilder.DeleteData(
                table: "PermissionRole",
                keyColumns: new[] { "PermissionsId", "RolesId" },
                keyValues: new object[] { 301, 2 });

            migrationBuilder.DeleteData(
                table: "Permissions",
                keyColumn: "Id",
                keyValue: 200);

            migrationBuilder.DeleteData(
                table: "Permissions",
                keyColumn: "Id",
                keyValue: 201);

            migrationBuilder.DeleteData(
                table: "Permissions",
                keyColumn: "Id",
                keyValue: 202);

            migrationBuilder.DeleteData(
                table: "Permissions",
                keyColumn: "Id",
                keyValue: 300);

            migrationBuilder.DeleteData(
                table: "Permissions",
                keyColumn: "Id",
                keyValue: 301);
        }
    }
}
