using System;
using Microsoft.EntityFrameworkCore.Migrations;
using Npgsql.EntityFrameworkCore.PostgreSQL.Metadata;

namespace QMPT.Data.Migrations
{
    public partial class ContactPersonPhoneNumbersTableCreating : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "ContactPersonPhoneNumbers",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    CreationTime = table.Column<DateTime>(nullable: false),
                    PhoneNumber = table.Column<string>(maxLength: 20, nullable: false),
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
                    table.PrimaryKey("PK_ContactPersonPhoneNumbers", x => x.Id);
                    table.ForeignKey(
                        name: "FK_ContactPersonPhoneNumbers_ContactPeople_ContactPersonId",
                        column: x => x.ContactPersonId,
                        principalTable: "ContactPeople",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_ContactPersonPhoneNumbers_Users_CreatorId",
                        column: x => x.CreatorId,
                        principalTable: "Users",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_ContactPersonPhoneNumbers_Users_EditorId",
                        column: x => x.EditorId,
                        principalTable: "Users",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_ContactPersonPhoneNumbers_ContactPersonPhoneNumbers_Origina~",
                        column: x => x.OriginalId,
                        principalTable: "ContactPersonPhoneNumbers",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_ContactPersonPhoneNumbers_Users_RemoverId",
                        column: x => x.RemoverId,
                        principalTable: "Users",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateIndex(
                name: "IX_ContactPersonPhoneNumbers_ContactPersonId",
                table: "ContactPersonPhoneNumbers",
                column: "ContactPersonId");

            migrationBuilder.CreateIndex(
                name: "IX_ContactPersonPhoneNumbers_CreatorId",
                table: "ContactPersonPhoneNumbers",
                column: "CreatorId");

            migrationBuilder.CreateIndex(
                name: "IX_ContactPersonPhoneNumbers_EditorId",
                table: "ContactPersonPhoneNumbers",
                column: "EditorId");

            migrationBuilder.CreateIndex(
                name: "IX_ContactPersonPhoneNumbers_OriginalId",
                table: "ContactPersonPhoneNumbers",
                column: "OriginalId");

            migrationBuilder.CreateIndex(
                name: "IX_ContactPersonPhoneNumbers_RemoverId",
                table: "ContactPersonPhoneNumbers",
                column: "RemoverId");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "ContactPersonPhoneNumbers");
        }
    }
}
