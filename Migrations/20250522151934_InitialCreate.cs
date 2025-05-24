using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace First_MVC_webApp.Migrations
{
    /// <inheritdoc />
    public partial class InitialCreate : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropPrimaryKey(
                name: "PK_chatmesage",
                table: "chatmesage");

            migrationBuilder.RenameTable(
                name: "chatmesage",
                newName: "ChatMessages");

            migrationBuilder.AddPrimaryKey(
                name: "PK_ChatMessages",
                table: "ChatMessages",
                column: "Id");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropPrimaryKey(
                name: "PK_ChatMessages",
                table: "ChatMessages");

            migrationBuilder.RenameTable(
                name: "ChatMessages",
                newName: "chatmesage");

            migrationBuilder.AddPrimaryKey(
                name: "PK_chatmesage",
                table: "chatmesage",
                column: "Id");
        }
    }
}
