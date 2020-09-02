using System;
using Microsoft.EntityFrameworkCore.Migrations;
using Npgsql.EntityFrameworkCore.PostgreSQL.Metadata;

namespace QMPT.Data.Migrations
{
    public partial class OrganizationNotesTableCreating : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "OrganizationNotes",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    CreationTime = table.Column<DateTime>(nullable: false),
                    Note = table.Column<string>(maxLength: 500, nullable: false),
                    OrganizationId = table.Column<int>(nullable: false),
                    CreatorId = table.Column<int>(nullable: false),
                    EditorId = table.Column<int>(nullable: true),
                    EditingTime = table.Column<DateTime>(nullable: true),
                    IsEdited = table.Column<bool>(nullable: false),
                    Version = table.Column<int>(nullable: false),
                    OriginalId = table.Column<int>(nullable: true),
                    RemoverId = table.Column<int>(nullable: true),
                    RemovalTime = table.Column<DateTime>(nullable: true),
                    IsRemoved = table.Column<bool>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_OrganizationNotes", x => x.Id);
                    table.ForeignKey(
                        name: "FK_OrganizationNotes_Users_CreatorId",
                        column: x => x.CreatorId,
                        principalTable: "Users",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_OrganizationNotes_Users_EditorId",
                        column: x => x.EditorId,
                        principalTable: "Users",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_OrganizationNotes_Organizations_OrganizationId",
                        column: x => x.OrganizationId,
                        principalTable: "Organizations",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_OrganizationNotes_OrganizationNotes_OriginalId",
                        column: x => x.OriginalId,
                        principalTable: "OrganizationNotes",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_OrganizationNotes_Users_RemoverId",
                        column: x => x.RemoverId,
                        principalTable: "Users",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateIndex(
                name: "IX_OrganizationNotes_CreatorId",
                table: "OrganizationNotes",
                column: "CreatorId");

            migrationBuilder.CreateIndex(
                name: "IX_OrganizationNotes_EditorId",
                table: "OrganizationNotes",
                column: "EditorId");

            migrationBuilder.CreateIndex(
                name: "IX_OrganizationNotes_OrganizationId",
                table: "OrganizationNotes",
                column: "OrganizationId");

            migrationBuilder.CreateIndex(
                name: "IX_OrganizationNotes_OriginalId",
                table: "OrganizationNotes",
                column: "OriginalId");

            migrationBuilder.CreateIndex(
                name: "IX_OrganizationNotes_RemoverId",
                table: "OrganizationNotes",
                column: "RemoverId");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "OrganizationNotes");
        }
    }
}
