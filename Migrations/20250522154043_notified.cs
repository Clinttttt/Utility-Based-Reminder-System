using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace First_MVC_webApp.Migrations
{
    /// <inheritdoc />
    public partial class notified : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<bool>(
                name: "IsNotified",
                table: "reminder",
                type: "bit",
                nullable: false,
                defaultValue: false);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "IsNotified",
                table: "reminder");
        }
    }
}
