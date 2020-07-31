using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace WebApp_Core.Migrations
{
    public partial class PurchasingData : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<float>(
                name: "CashAmount",
                table: "Users",
                nullable: false,
                defaultValue: 0f);

            migrationBuilder.AddColumn<DateTime>(
                name: "DateOfPurchase",
                table: "PartsAndUsers",
                nullable: false,
                defaultValue: new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified));

            migrationBuilder.AddColumn<bool>(
                name: "purchased",
                table: "PartsAndUsers",
                nullable: false,
                defaultValue: false);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "CashAmount",
                table: "Users");

            migrationBuilder.DropColumn(
                name: "DateOfPurchase",
                table: "PartsAndUsers");

            migrationBuilder.DropColumn(
                name: "purchased",
                table: "PartsAndUsers");
        }
    }
}
