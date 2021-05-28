using Microsoft.EntityFrameworkCore.Migrations;

namespace BlazorBattles.Server.Migrations
{
    public partial class UserBattleStatistics : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<int>(
                name: "Battles",
                table: "Users",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddColumn<int>(
                name: "Defeats",
                table: "Users",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddColumn<int>(
                name: "Victories",
                table: "Users",
                nullable: false,
                defaultValue: 0);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Battles",
                table: "Users");

            migrationBuilder.DropColumn(
                name: "Defeats",
                table: "Users");

            migrationBuilder.DropColumn(
                name: "Victories",
                table: "Users");
        }
    }
}
