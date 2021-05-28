using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace BlazorBattles.Server.Migrations
{
    public partial class Battles : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "Battles",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false)
                        .Annotation("Sqlite:Autoincrement", true),
                    AttackerId = table.Column<int>(nullable: false),
                    OpponentId = table.Column<int>(nullable: false),
                    WinnerId = table.Column<int>(nullable: false),
                    WinnerDamage = table.Column<int>(nullable: false),
                    RoundsFought = table.Column<int>(nullable: false),
                    BattleDate = table.Column<DateTime>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Battles", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Battles_Users_AttackerId",
                        column: x => x.AttackerId,
                        principalTable: "Users",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_Battles_Users_OpponentId",
                        column: x => x.OpponentId,
                        principalTable: "Users",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_Battles_Users_WinnerId",
                        column: x => x.WinnerId,
                        principalTable: "Users",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_Battles_AttackerId",
                table: "Battles",
                column: "AttackerId");

            migrationBuilder.CreateIndex(
                name: "IX_Battles_OpponentId",
                table: "Battles",
                column: "OpponentId");

            migrationBuilder.CreateIndex(
                name: "IX_Battles_WinnerId",
                table: "Battles",
                column: "WinnerId");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "Battles");
        }
    }
}
