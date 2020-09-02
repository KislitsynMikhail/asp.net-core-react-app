using System;
using Microsoft.EntityFrameworkCore.Migrations;
using Npgsql.EntityFrameworkCore.PostgreSQL.Metadata;

namespace QMPT.Data.Migrations
{
    public partial class ContactPersonEmailsTableCreating : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "ContactPersonEmails",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    CreationTime = table.Column<DateTime>(nullable: false),
                    Email = table.Column<string>(maxLength: 100, nullable: false),
                    ContactPersonId = table.Column<int>(nullable: false),
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
                    table.PrimaryKey("PK_ContactPersonEmails", x => x.Id);
                    table.ForeignKey(
                        name: "FK_ContactPersonEmails_ContactPeople_ContactPersonId",
                        column: x => x.ContactPersonId,
                        principalTable: "ContactPeople",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_ContactPersonEmails_Users_CreatorId",
                        column: x => x.CreatorId,
                        principalTable: "Users",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_ContactPersonEmails_Users_EditorId",
                        column: x => x.EditorId,
                        principalTable: "Users",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_ContactPersonEmails_ContactPersonEmails_OriginalId",
                        column: x => x.OriginalId,
                        principalTable: "ContactPersonEmails",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_ContactPersonEmails_Users_RemoverId",
                        column: x => x.RemoverId,
                        principalTable: "Users",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateIndex(
                name: "IX_ContactPersonEmails_ContactPersonId",
                table: "ContactPersonEmails",
                column: "ContactPersonId");

            migrationBuilder.CreateIndex(
                name: "IX_ContactPersonEmails_CreatorId",
                table: "ContactPersonEmails",
                column: "CreatorId");

            migrationBuilder.CreateIndex(
                name: "IX_ContactPersonEmails_EditorId",
                table: "ContactPersonEmails",
                column: "EditorId");

            migrationBuilder.CreateIndex(
                name: "IX_ContactPersonEmails_OriginalId",
                table: "ContactPersonEmails",
                column: "OriginalId");

            migrationBuilder.CreateIndex(
                name: "IX_ContactPersonEmails_RemoverId",
                table: "ContactPersonEmails",
                column: "RemoverId");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "ContactPersonEmails");
        }
    }
}
