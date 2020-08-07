using Microsoft.EntityFrameworkCore.Migrations;

namespace WebApp_Core.Migrations
{
    public partial class AddedOtpEntity : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "Email",
                table: "Users",
                nullable: true);

            migrationBuilder.AddColumn<bool>(
                name: "OTPAuth",
                table: "Users",
                nullable: false,
                defaultValue: false);

            migrationBuilder.AddColumn<string>(
                name: "OtpToCheckWith",
                table: "Users",
                nullable: true);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Email",
                table: "Users");

            migrationBuilder.DropColumn(
                name: "OTPAuth",
                table: "Users");

            migrationBuilder.DropColumn(
                name: "OtpToCheckWith",
                table: "Users");
        }
    }
}
