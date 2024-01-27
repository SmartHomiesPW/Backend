using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace SmartHomeBackend.Migrations
{
    /// <inheritdoc />
    public partial class UsersSystems : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "UsersSystems",
                columns: table => new
                {
                    User_Id = table.Column<Guid>(type: "uuid", nullable: false),
                    System_Id = table.Column<string>(type: "text", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_UsersSystems", x => x.User_Id);
                });
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "UsersSystems");
        }
    }
}
