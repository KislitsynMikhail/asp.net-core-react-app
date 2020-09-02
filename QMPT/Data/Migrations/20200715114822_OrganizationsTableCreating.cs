using System;
using Microsoft.EntityFrameworkCore.Migrations;
using Npgsql.EntityFrameworkCore.PostgreSQL.Metadata;

namespace QMPT.Data.Migrations
{
    public partial class OrganizationsTableCreating : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "Organizations",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    CreationTime = table.Column<DateTime>(nullable: false),
                    Name = table.Column<string>(maxLength: 200, nullable: false),
                    INN = table.Column<string>(maxLength: 12, nullable: false),
                    KPP = table.Column<string>(maxLength: 9, nullable: false),
                    OGRN = table.Column<string>(maxLength: 13, nullable: false),
                    OKPO = table.Column<string>(maxLength: 10, nullable: false),
                    LegalAddress = table.Column<string>(maxLength: 200, nullable: false),
                    Email = table.Column<string>(maxLength: 100, nullable: false),
                    PhoneNumber = table.Column<string>(maxLength: 20, nullable: false),
                    SettlementAccount = table.Column<string>(maxLength: 100, nullable: false),
                    CorporateAccount = table.Column<string>(maxLength: 100, nullable: false),
                    BIK = table.Column<string>(maxLength: 9, nullable: false),
                    ManagerPosition = table.Column<string>(maxLength: 100, nullable: false),
                    Base = table.Column<string>(maxLength: 100, nullable: false),
                    SupervisorFIO = table.Column<string>(maxLength: 100, nullable: false),
                    ChiefAccountant = table.Column<string>(maxLength: 100, nullable: false),
                    IsUSN = table.Column<bool>(nullable: false),
                    IsRelevant = table.Column<bool>(nullable: false),
                    CreatorId = table.Column<int>(nullable: false),
                    EditorId = table.Column<int>(nullable: true),
                    EditingTime = table.Column<DateTime>(nullable: true),
                    IsEdited = table.Column<bool>(nullable: false),
                    Version = table.Column<int>(nullable: false),
                    OriginalId = table.Column<int>(nullable: true),
                    RemoverId = table.Column<int>(nullable: true),
                    RemovalTime = table.Column<DateTime>(nullable: true),
                    IsRemoved = table.Column<bool>(nullable: false),
                    Discriminator = table.Column<string>(maxLength: 8, nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Organizations", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Organizations_Users_CreatorId",
                        column: x => x.CreatorId,
                        principalTable: "Users",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_Organizations_Users_EditorId",
                        column: x => x.EditorId,
                        principalTable: "Users",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_Organizations_Organizations_OriginalId",
                        column: x => x.OriginalId,
                        principalTable: "Organizations",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_Organizations_Users_RemoverId",
                        column: x => x.RemoverId,
                        principalTable: "Users",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateIndex(
                name: "IX_Organizations_CreatorId",
                table: "Organizations",
                column: "CreatorId");

            migrationBuilder.CreateIndex(
                name: "IX_Organizations_EditorId",
                table: "Organizations",
                column: "EditorId");

            migrationBuilder.CreateIndex(
                name: "IX_Organizations_OriginalId",
                table: "Organizations",
                column: "OriginalId");

            migrationBuilder.CreateIndex(
                name: "IX_Organizations_RemoverId",
                table: "Organizations",
                column: "RemoverId");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "Organizations");
        }
    }
}
