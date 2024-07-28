using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace LiveChat.Infraestructure.Migrations
{
    /// <inheritdoc />
    public partial class adicionandocampodenomeparaChannel : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "Name",
                table: "Channels",
                type: "text",
                nullable: false,
                defaultValue: "");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Name",
                table: "Channels");
        }
    }
}
