using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace First_MVC_webApp.Migrations
{
    /// <inheritdoc />
    public partial class chatmessage : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "chatmesage",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Content = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    DeliveryTime = table.Column<DateTime>(type: "datetime2", nullable: false),
                    IsFromUser = table.Column<bool>(type: "bit", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_chatmesage", x => x.Id);
                });
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "chatmesage");
        }
    }
}
