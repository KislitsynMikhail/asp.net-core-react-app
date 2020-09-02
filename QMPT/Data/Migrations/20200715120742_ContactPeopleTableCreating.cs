using System;
using Microsoft.EntityFrameworkCore.Migrations;
using Npgsql.EntityFrameworkCore.PostgreSQL.Metadata;

namespace QMPT.Data.Migrations
{
    public partial class ContactPeopleTableCreating : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "ContactPeople",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    CreationTime = table.Column<DateTime>(nullable: false),
                    FIO = table.Column<string>(maxLength: 100, nullable: false),
                    Position = table.Column<string>(maxLength: 100, nullable: false),
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
                    table.PrimaryKey("PK_ContactPeople", x => x.Id);
                    table.ForeignKey(
                        name: "FK_ContactPeople_Users_CreatorId",
                        column: x => x.CreatorId,
                        principalTable: "Users",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_ContactPeople_Users_EditorId",
                        column: x => x.EditorId,
                        principalTable: "Users",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_ContactPeople_Organizations_OrganizationId",
                        column: x => x.OrganizationId,
                        principalTable: "Organizations",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_ContactPeople_ContactPeople_OriginalId",
                        column: x => x.OriginalId,
                        principalTable: "ContactPeople",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_ContactPeople_Users_RemoverId",
                        column: x => x.RemoverId,
                        principalTable: "Users",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateIndex(
                name: "IX_ContactPeople_CreatorId",
                table: "ContactPeople",
                column: "CreatorId");

            migrationBuilder.CreateIndex(
                name: "IX_ContactPeople_EditorId",
                table: "ContactPeople",
                column: "EditorId");

            migrationBuilder.CreateIndex(
                name: "IX_ContactPeople_OrganizationId",
                table: "ContactPeople",
                column: "OrganizationId");

            migrationBuilder.CreateIndex(
                name: "IX_ContactPeople_OriginalId",
                table: "ContactPeople",
                column: "OriginalId");

            migrationBuilder.CreateIndex(
                name: "IX_ContactPeople_RemoverId",
                table: "ContactPeople",
                column: "RemoverId");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "ContactPeople");
        }
    }
}
