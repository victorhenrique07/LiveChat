using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace LiveChat.Infraestructure.Migrations
{
    /// <inheritdoc />
    public partial class mappingUserdomain : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Users_Guilds_GuildId",
                table: "Users");

            migrationBuilder.DropIndex(
                name: "IX_Users_GuildId",
                table: "Users");

            migrationBuilder.DropColumn(
                name: "GuildId",
                table: "Users");

            migrationBuilder.AddColumn<int>(
                name: "SenderId",
                table: "Messages",
                type: "integer",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.CreateTable(
                name: "GuildMembers",
                columns: table => new
                {
                    GuildId = table.Column<int>(type: "integer", nullable: false),
                    UserId = table.Column<int>(type: "integer", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_GuildMembers", x => new { x.GuildId, x.UserId });
                    table.ForeignKey(
                        name: "FK_GuildMembers_Guilds_GuildId",
                        column: x => x.GuildId,
                        principalTable: "Guilds",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_GuildMembers_Users_UserId",
                        column: x => x.UserId,
                        principalTable: "Users",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_GuildMembers_UserId",
                table: "GuildMembers",
                column: "UserId");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "GuildMembers");

            migrationBuilder.DropColumn(
                name: "SenderId",
                table: "Messages");

            migrationBuilder.AddColumn<int>(
                name: "GuildId",
                table: "Users",
                type: "integer",
                nullable: true);

            migrationBuilder.CreateIndex(
                name: "IX_Users_GuildId",
                table: "Users",
                column: "GuildId");

            migrationBuilder.AddForeignKey(
                name: "FK_Users_Guilds_GuildId",
                table: "Users",
                column: "GuildId",
                principalTable: "Guilds",
                principalColumn: "Id");
        }
    }
}
