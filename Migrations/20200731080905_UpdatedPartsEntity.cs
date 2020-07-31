using Microsoft.EntityFrameworkCore.Migrations;

namespace WebApp_Core.Migrations
{
    public partial class UpdatedPartsEntity : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<int>(
                name: "Count",
                table: "PartsAndUsers",
                nullable: false,
                defaultValue: 0);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Count",
                table: "PartsAndUsers");
        }
    }
}
