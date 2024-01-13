using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace SmartHomeBackend.Migrations
{
    /// <inheritdoc />
    public partial class DoorLockMigration : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "DoorLocks",
                columns: table => new
                {
                    DoorLock_Id = table.Column<string>(type: "text", nullable: false),
                    System_Id = table.Column<string>(type: "text", nullable: false),
                    IsOn = table.Column<int>(type: "integer", nullable: false),
                    Name = table.Column<string>(type: "text", nullable: false),
                    Details = table.Column<string>(type: "text", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_DoorLocks", x => x.DoorLock_Id);
                });
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "DoorLocks");
        }
    }
}
