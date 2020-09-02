using System;
using Microsoft.EntityFrameworkCore.Migrations;
using Npgsql.EntityFrameworkCore.PostgreSQL.Metadata;

namespace QMPT.Data.Migrations
{
    public partial class DevicesTableCreating : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "Devices",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    CreationTime = table.Column<DateTime>(nullable: false),
                    Number = table.Column<string>(maxLength: 50, nullable: false),
                    MeasurementRange = table.Column<string>(maxLength: 50, nullable: false),
                    PermissibleSystematicErrorMax = table.Column<string>(maxLength: 50, nullable: false),
                    AdmissibleRandomErrorsMaxJson = table.Column<string>(maxLength: 1000, nullable: false),
                    MagnetometerReadingsVariation = table.Column<string>(maxLength: 50, nullable: false),
                    GradientResistence = table.Column<string>(maxLength: 50, nullable: false),
                    SignalAmplitude = table.Column<string>(maxLength: 50, nullable: false),
                    RelaxtionTime = table.Column<string>(maxLength: 50, nullable: false),
                    OptimalCycle = table.Column<string>(maxLength: 50, nullable: false),
                    CreatorId = table.Column<int>(nullable: false),
                    OriginalId = table.Column<int>(nullable: true),
                    EditorId = table.Column<int>(nullable: true),
                    EditingTime = table.Column<DateTime>(nullable: true),
                    IsEdited = table.Column<bool>(nullable: false),
                    Version = table.Column<int>(nullable: false),
                    RemoverId = table.Column<int>(nullable: true),
                    RemovalTime = table.Column<DateTime>(nullable: true),
                    IsRemoved = table.Column<bool>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Devices", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Devices_Users_CreatorId",
                        column: x => x.CreatorId,
                        principalTable: "Users",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_Devices_Users_EditorId",
                        column: x => x.EditorId,
                        principalTable: "Users",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_Devices_Devices_OriginalId",
                        column: x => x.OriginalId,
                        principalTable: "Devices",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_Devices_Users_RemoverId",
                        column: x => x.RemoverId,
                        principalTable: "Users",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateIndex(
                name: "IX_Devices_CreatorId",
                table: "Devices",
                column: "CreatorId");

            migrationBuilder.CreateIndex(
                name: "IX_Devices_EditorId",
                table: "Devices",
                column: "EditorId");

            migrationBuilder.CreateIndex(
                name: "IX_Devices_OriginalId",
                table: "Devices",
                column: "OriginalId");

            migrationBuilder.CreateIndex(
                name: "IX_Devices_RemoverId",
                table: "Devices",
                column: "RemoverId");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "Devices");
        }
    }
}
