using System;
using Microsoft.EntityFrameworkCore.Migrations;
using Npgsql.EntityFrameworkCore.PostgreSQL.Metadata;

#nullable disable

namespace SmartHomeBackend.Migrations
{
    /// <inheritdoc />
    public partial class AlarmsMigration : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "Alarms",
                columns: table => new
                {
                    Alarm_Id = table.Column<string>(type: "text", nullable: false),
                    System_Id = table.Column<string>(type: "text", nullable: false),
                    IsActive = table.Column<int>(type: "integer", nullable: false),
                    IsTriggered = table.Column<int>(type: "integer", nullable: false),
                    Name = table.Column<string>(type: "text", nullable: false),
                    Details = table.Column<string>(type: "text", nullable: false),
                    AccessCode = table.Column<string>(type: "text", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Alarms", x => x.Alarm_Id);
                });

            migrationBuilder.CreateTable(
                name: "AlarmTriggers",
                columns: table => new
                {
                    AlarmTrigger_Id = table.Column<string>(type: "text", nullable: false),
                    Alarm_Id = table.Column<string>(type: "text", nullable: false),
                    DateTime = table.Column<DateTime>(type: "timestamp with time zone", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_AlarmTriggers", x => x.AlarmTrigger_Id);
                });

            migrationBuilder.CreateTable(
                name: "Humidity_Sensors",
                columns: table => new
                {
                    Sensor_Id = table.Column<string>(type: "text", nullable: false),
                    System_Id = table.Column<string>(type: "text", nullable: false),
                    Name = table.Column<string>(type: "text", nullable: false),
                    Details = table.Column<string>(type: "text", nullable: false),
                    Value = table.Column<decimal>(type: "numeric", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Humidity_Sensors", x => x.Sensor_Id);
                });

            migrationBuilder.CreateTable(
                name: "Humidity_Sensors_Logs",
                columns: table => new
                {
                    Log_Id = table.Column<int>(type: "integer", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    Humidity = table.Column<double>(type: "double precision", nullable: false),
                    DateTime = table.Column<DateTime>(type: "timestamp with time zone", nullable: false),
                    SensorId = table.Column<int>(type: "integer", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Humidity_Sensors_Logs", x => x.Log_Id);
                });

            migrationBuilder.CreateTable(
                name: "Light_Sensors",
                columns: table => new
                {
                    Sensor_Id = table.Column<string>(type: "text", nullable: false),
                    System_Id = table.Column<string>(type: "text", nullable: false),
                    Name = table.Column<string>(type: "text", nullable: false),
                    Details = table.Column<string>(type: "text", nullable: false),
                    Value = table.Column<decimal>(type: "numeric", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Light_Sensors", x => x.Sensor_Id);
                });

            migrationBuilder.CreateTable(
                name: "SunlightSensorsLogs",
                columns: table => new
                {
                    Log_Id = table.Column<int>(type: "integer", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    LightValue = table.Column<double>(type: "double precision", nullable: false),
                    DateTime = table.Column<DateTime>(type: "timestamp with time zone", nullable: false),
                    SensorId = table.Column<int>(type: "integer", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_SunlightSensorsLogs", x => x.Log_Id);
                });

            migrationBuilder.CreateTable(
                name: "Switchable_Lights",
                columns: table => new
                {
                    Switchable_Light_Id = table.Column<string>(type: "text", nullable: false),
                    System_Id = table.Column<string>(type: "text", nullable: false),
                    Name = table.Column<string>(type: "text", nullable: false),
                    Details = table.Column<string>(type: "text", nullable: false),
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
                    Details = table.Column<string>(type: "text", nullable: false),
                    Value = table.Column<decimal>(type: "numeric", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Temperature_Sensors", x => x.Sensor_Id);
                });

            migrationBuilder.CreateTable(
                name: "TemperatureSensorsLogs",
                columns: table => new
                {
                    Log_Id = table.Column<int>(type: "integer", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    Temperature = table.Column<double>(type: "double precision", nullable: false),
                    DateTime = table.Column<DateTime>(type: "timestamp with time zone", nullable: false),
                    SensorId = table.Column<int>(type: "integer", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_TemperatureSensorsLogs", x => x.Log_Id);
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
                name: "Alarms");

            migrationBuilder.DropTable(
                name: "AlarmTriggers");

            migrationBuilder.DropTable(
                name: "Humidity_Sensors");

            migrationBuilder.DropTable(
                name: "Humidity_Sensors_Logs");

            migrationBuilder.DropTable(
                name: "Light_Sensors");

            migrationBuilder.DropTable(
                name: "SunlightSensorsLogs");

            migrationBuilder.DropTable(
                name: "Switchable_Lights");

            migrationBuilder.DropTable(
                name: "Systems");

            migrationBuilder.DropTable(
                name: "Temperature_Sensors");

            migrationBuilder.DropTable(
                name: "TemperatureSensorsLogs");

            migrationBuilder.DropTable(
                name: "Users");
        }
    }
}
