using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace NewAvalon.Notification.Persistence.Migrations
{
    public partial class AddInitialMigration : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "Notifications",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uuid", nullable: false),
                    UserId = table.Column<Guid>(type: "uuid", nullable: true),
                    Email = table.Column<string>(type: "character varying(256)", maxLength: 256, nullable: true),
                    DeliveryMechanism = table.Column<int>(type: "integer", nullable: false),
                    Type = table.Column<int>(type: "integer", nullable: false),
                    Title = table.Column<string>(type: "character varying(300)", maxLength: 300, nullable: true),
                    Content = table.Column<string>(type: "character varying(1000)", maxLength: 1000, nullable: true),
                    Details = table.Column<string>(type: "json", nullable: true),
                    Published = table.Column<bool>(type: "boolean", nullable: false),
                    PublishedOnUtc = table.Column<DateTime>(type: "timestamp without time zone", nullable: true),
                    Failed = table.Column<bool>(type: "boolean", nullable: false),
                    FailedOnUtc = table.Column<DateTime>(type: "timestamp without time zone", nullable: true),
                    CreatedOnUtc = table.Column<DateTime>(type: "timestamp without time zone", nullable: false),
                    ModifiedOnUtc = table.Column<DateTime>(type: "timestamp without time zone", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Notifications", x => x.Id);
                });

            migrationBuilder.CreateIndex(
                name: "IX_Notifications_Published_Failed_DeliveryMechanism_CreatedOnU~",
                table: "Notifications",
                columns: new[] { "Published", "Failed", "DeliveryMechanism", "CreatedOnUtc" },
                filter: "NOT (\"Published\") AND NOT (\"Failed\")");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "Notifications");
        }
    }
}
