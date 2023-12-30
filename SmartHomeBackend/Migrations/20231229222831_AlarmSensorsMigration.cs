using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace SmartHomeBackend.Migrations
{
    /// <inheritdoc />
    public partial class AlarmSensorsMigration : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "AlarmSensors",
                columns: table => new
                {
                    Alarm_Sensor_Id = table.Column<string>(type: "text", nullable: false),
                    Alarm_Id = table.Column<string>(type: "text", nullable: false),
                    Name = table.Column<string>(type: "text", nullable: false),
                    Details = table.Column<string>(type: "text", nullable: false),
                    Is_On = table.Column<int>(type: "integer", nullable: false),
                    Movement_Detected = table.Column<int>(type: "integer", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_AlarmSensors", x => x.Alarm_Sensor_Id);
                });
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "AlarmSensors");
        }
    }
}
