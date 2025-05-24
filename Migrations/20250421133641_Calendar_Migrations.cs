using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace First_MVC_webApp.Migrations
{
    /// <inheritdoc />
    public partial class Calendar_Migrations : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<bool>(
                name: "calendar",
                table: "Todolist",
                type: "bit",
                nullable: false,
                defaultValue: false);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "calendar",
                table: "Todolist");
        }
    }
}
