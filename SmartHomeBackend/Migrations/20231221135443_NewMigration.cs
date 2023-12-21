using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace SmartHomeBackend.Migrations
{
    /// <inheritdoc />
    public partial class NewMigration : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "Details",
                table: "Switchable_Lights",
                type: "text",
                nullable: false,
                defaultValue: "");

            migrationBuilder.AddColumn<string>(
                name: "Name",
                table: "Switchable_Lights",
                type: "text",
                nullable: false,
                defaultValue: "");

            migrationBuilder.AddColumn<string>(
                name: "Details",
                table: "Light_Sensors",
                type: "text",
                nullable: false,
                defaultValue: "");

            migrationBuilder.AddColumn<string>(
                name: "Details",
                table: "Humidity_Sensors",
                type: "text",
                nullable: false,
                defaultValue: "");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Details",
                table: "Switchable_Lights");

            migrationBuilder.DropColumn(
                name: "Name",
                table: "Switchable_Lights");

            migrationBuilder.DropColumn(
                name: "Details",
                table: "Light_Sensors");

            migrationBuilder.DropColumn(
                name: "Details",
                table: "Humidity_Sensors");
        }
    }
}
