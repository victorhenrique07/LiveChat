using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace LiveChat.Infraestructure.Migrations
{
    /// <inheritdoc />
    public partial class fazendoumtesteemguildmembers : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropPrimaryKey(
                name: "PK_GuildMembers",
                table: "GuildMembers");

            migrationBuilder.DropIndex(
                name: "IX_GuildMembers_UserId",
                table: "GuildMembers");

            migrationBuilder.AlterColumn<DateTime>(
                name: "CreatedAt",
                table: "Guilds",
                type: "timestamp without time zone",
                nullable: false,
                oldClrType: typeof(DateTime),
                oldType: "timestamp with time zone");

            migrationBuilder.AddPrimaryKey(
                name: "PK_GuildMembers",
                table: "GuildMembers",
                columns: new[] { "UserId", "GuildId" });

            migrationBuilder.CreateIndex(
                name: "IX_GuildMembers_GuildId",
                table: "GuildMembers",
                column: "GuildId");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropPrimaryKey(
                name: "PK_GuildMembers",
                table: "GuildMembers");

            migrationBuilder.DropIndex(
                name: "IX_GuildMembers_GuildId",
                table: "GuildMembers");

            migrationBuilder.AlterColumn<DateTime>(
                name: "CreatedAt",
                table: "Guilds",
                type: "timestamp with time zone",
                nullable: false,
                oldClrType: typeof(DateTime),
                oldType: "timestamp without time zone");

            migrationBuilder.AddPrimaryKey(
                name: "PK_GuildMembers",
                table: "GuildMembers",
                columns: new[] { "GuildId", "UserId" });

            migrationBuilder.CreateIndex(
                name: "IX_GuildMembers_UserId",
                table: "GuildMembers",
                column: "UserId");
        }
    }
}
