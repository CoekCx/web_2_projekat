using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace NewAvalon.Order.Persistence.Migrations
{
    public partial class AddDeliverOnUtcOrder : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<DateTime>(
                name: "DeliveryOnUtc",
                table: "Orders",
                type: "timestamp without time zone",
                nullable: false,
                defaultValue: new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified));
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "DeliveryOnUtc",
                table: "Orders");
        }
    }
}
