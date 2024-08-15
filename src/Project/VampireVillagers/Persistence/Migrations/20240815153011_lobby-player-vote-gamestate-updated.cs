using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Persistence.Migrations
{
    /// <inheritdoc />
    public partial class lobbyplayervotegamestateupdated : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Votes_Players_PlayerId",
                table: "Votes");

            migrationBuilder.DropForeignKey(
                name: "FK_Votes_Players_TargetId",
                table: "Votes");

            migrationBuilder.DropColumn(
                name: "LiveState",
                table: "Players");

            migrationBuilder.DropColumn(
                name: "Role",
                table: "Players");

            migrationBuilder.DropColumn(
                name: "Skill",
                table: "Players");

            migrationBuilder.AddColumn<DateTime>(
                name: "CreationDate",
                table: "GameSettings",
                type: "timestamp with time zone",
                nullable: false,
                defaultValue: new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified));

            migrationBuilder.CreateTable(
                name: "GameStates",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uuid", nullable: false),
                    LiveState = table.Column<bool>(type: "boolean", nullable: false),
                    Role = table.Column<int>(type: "integer", nullable: false),
                    Skill = table.Column<int>(type: "integer", nullable: false),
                    PlayerId = table.Column<Guid>(type: "uuid", nullable: false),
                    GameSettingId = table.Column<Guid>(type: "uuid", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_GameStates", x => x.Id);
                    table.ForeignKey(
                        name: "FK_GameStates_GameSettings_GameSettingId",
                        column: x => x.GameSettingId,
                        principalTable: "GameSettings",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_GameStates_Players_PlayerId",
                        column: x => x.PlayerId,
                        principalTable: "Players",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_GameStates_GameSettingId",
                table: "GameStates",
                column: "GameSettingId");

            migrationBuilder.CreateIndex(
                name: "IX_GameStates_PlayerId",
                table: "GameStates",
                column: "PlayerId");

            migrationBuilder.AddForeignKey(
                name: "FK_Votes_Players_PlayerId",
                table: "Votes",
                column: "PlayerId",
                principalTable: "Players",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_Votes_Players_TargetId",
                table: "Votes",
                column: "TargetId",
                principalTable: "Players",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Votes_Players_PlayerId",
                table: "Votes");

            migrationBuilder.DropForeignKey(
                name: "FK_Votes_Players_TargetId",
                table: "Votes");

            migrationBuilder.DropTable(
                name: "GameStates");

            migrationBuilder.DropColumn(
                name: "CreationDate",
                table: "GameSettings");

            migrationBuilder.AddColumn<bool>(
                name: "LiveState",
                table: "Players",
                type: "boolean",
                nullable: false,
                defaultValue: false);

            migrationBuilder.AddColumn<int>(
                name: "Role",
                table: "Players",
                type: "integer",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddColumn<int>(
                name: "Skill",
                table: "Players",
                type: "integer",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddForeignKey(
                name: "FK_Votes_Players_PlayerId",
                table: "Votes",
                column: "PlayerId",
                principalTable: "Players",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_Votes_Players_TargetId",
                table: "Votes",
                column: "TargetId",
                principalTable: "Players",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
