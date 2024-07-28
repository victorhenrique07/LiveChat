using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace LiveChat.Infraestructure.Migrations
{
    /// <inheritdoc />
    public partial class addingrelationships : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "Content",
                table: "Messages",
                type: "text",
                nullable: false,
                defaultValue: "");

            migrationBuilder.AddColumn<int>(
                name: "OwnerId",
                table: "Guilds",
                type: "integer",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.CreateIndex(
                name: "IX_Guilds_OwnerId",
                table: "Guilds",
                column: "OwnerId");

            migrationBuilder.AddForeignKey(
                name: "FK_Guilds_Users_OwnerId",
                table: "Guilds",
                column: "OwnerId",
                principalTable: "Users",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Guilds_Users_OwnerId",
                table: "Guilds");

            migrationBuilder.DropIndex(
                name: "IX_Guilds_OwnerId",
                table: "Guilds");

            migrationBuilder.DropColumn(
                name: "Content",
                table: "Messages");

            migrationBuilder.DropColumn(
                name: "OwnerId",
                table: "Guilds");
        }
    }
}
