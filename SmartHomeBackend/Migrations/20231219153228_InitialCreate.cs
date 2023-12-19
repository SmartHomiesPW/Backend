using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace SmartHomeBackend.Migrations
{
    /// <inheritdoc />
    public partial class InitialCreate : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "Humidity_Sensors",
                columns: table => new
                {
                    Sensor_Id = table.Column<string>(type: "text", nullable: false),
                    System_Id = table.Column<string>(type: "text", nullable: false),
                    Name = table.Column<string>(type: "text", nullable: false),
                    Value = table.Column<decimal>(type: "numeric", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Humidity_Sensors", x => x.Sensor_Id);
                });

            migrationBuilder.CreateTable(
                name: "Light_Sensors",
                columns: table => new
                {
                    Sensor_Id = table.Column<string>(type: "text", nullable: false),
                    System_Id = table.Column<string>(type: "text", nullable: false),
                    Name = table.Column<string>(type: "text", nullable: false),
                    Value = table.Column<decimal>(type: "numeric", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Light_Sensors", x => x.Sensor_Id);
                });

            migrationBuilder.CreateTable(
                name: "Switchable_Lights",
                columns: table => new
                {
                    Switchable_Light_Id = table.Column<string>(type: "text", nullable: false),
                    System_Id = table.Column<string>(type: "text", nullable: false),
                    Value = table.Column<int>(type: "integer", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Switchable_Lights", x => x.Switchable_Light_Id);
                });

            migrationBuilder.CreateTable(
                name: "Systems",
                columns: table => new
                {
                    System_Id = table.Column<string>(type: "text", nullable: false),
                    Name = table.Column<string>(type: "text", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Systems", x => x.System_Id);
                });

            migrationBuilder.CreateTable(
                name: "Temperature_Sensors",
                columns: table => new
                {
                    Sensor_Id = table.Column<string>(type: "text", nullable: false),
                    System_Id = table.Column<string>(type: "text", nullable: false),
                    Name = table.Column<string>(type: "text", nullable: false),
                    Value = table.Column<decimal>(type: "numeric", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Temperature_Sensors", x => x.Sensor_Id);
                });

            migrationBuilder.CreateTable(
                name: "Users",
                columns: table => new
                {
                    User_Id = table.Column<string>(type: "text", nullable: false),
                    Email = table.Column<string>(type: "text", nullable: false),
                    Password = table.Column<string>(type: "text", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Users", x => x.User_Id);
                });
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "Humidity_Sensors");

            migrationBuilder.DropTable(
                name: "Light_Sensors");

            migrationBuilder.DropTable(
                name: "Switchable_Lights");

            migrationBuilder.DropTable(
                name: "Systems");

            migrationBuilder.DropTable(
                name: "Temperature_Sensors");

            migrationBuilder.DropTable(
                name: "Users");
        }
    }
}
