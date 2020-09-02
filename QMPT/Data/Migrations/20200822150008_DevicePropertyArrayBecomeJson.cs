using System.Collections.Generic;
using Microsoft.EntityFrameworkCore.Migrations;
using QMPT.Data.Models.Devices;

namespace QMPT.Data.Migrations
{
    public partial class DevicePropertyArrayBecomeJson : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "AdmissibleRandomErrorsMaxJson",
                table: "Devices");

            migrationBuilder.AddColumn<List<AdmissibleRandomErrorMax>>(
                name: "AdmissibleRandomErrorsMax",
                table: "Devices",
                type: "jsonb",
                nullable: true);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "AdmissibleRandomErrorsMax",
                table: "Devices");

            migrationBuilder.AddColumn<string>(
                name: "AdmissibleRandomErrorsMaxJson",
                table: "Devices",
                type: "character varying(1000)",
                maxLength: 1000,
                nullable: true);
        }
    }
}
