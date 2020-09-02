using Microsoft.EntityFrameworkCore.Migrations;

namespace QMPT.Data.Migrations
{
    public partial class DevicesColumnsNamesFixed : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "GradientResistence",
                table: "Devices");

            migrationBuilder.DropColumn(
                name: "RelaxtionTime",
                table: "Devices");

            migrationBuilder.AddColumn<string>(
                name: "GradientResistance",
                table: "Devices",
                maxLength: 50,
                nullable: false,
                defaultValue: "");

            migrationBuilder.AddColumn<string>(
                name: "RelaxationTime",
                table: "Devices",
                maxLength: 50,
                nullable: false,
                defaultValue: "");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "GradientResistance",
                table: "Devices");

            migrationBuilder.DropColumn(
                name: "RelaxationTime",
                table: "Devices");

            migrationBuilder.AddColumn<string>(
                name: "GradientResistence",
                table: "Devices",
                type: "character varying(50)",
                maxLength: 50,
                nullable: false,
                defaultValue: "");

            migrationBuilder.AddColumn<string>(
                name: "RelaxtionTime",
                table: "Devices",
                type: "character varying(50)",
                maxLength: 50,
                nullable: false,
                defaultValue: "");
        }
    }
}
