using Microsoft.EntityFrameworkCore.Migrations;

namespace QMPT.Data.Migrations
{
    public partial class IsRelevantColumnAdding : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<bool>(
                name: "IsRelevant",
                table: "Prices",
                nullable: false,
                defaultValue: false);

            migrationBuilder.AddColumn<bool>(
                name: "IsRelevant",
                table: "OrganizationNotes",
                nullable: false,
                defaultValue: false);

            migrationBuilder.AddColumn<bool>(
                name: "IsRelevant",
                table: "OrganizationFiles",
                nullable: false,
                defaultValue: false);

            migrationBuilder.AddColumn<bool>(
                name: "IsRelevant",
                table: "Devices",
                nullable: false,
                defaultValue: false);

            migrationBuilder.AddColumn<bool>(
                name: "IsRelevant",
                table: "ContactPersonPhoneNumbers",
                nullable: false,
                defaultValue: false);

            migrationBuilder.AddColumn<bool>(
                name: "IsRelevant",
                table: "ContactPersonEmails",
                nullable: false,
                defaultValue: false);

            migrationBuilder.AddColumn<bool>(
                name: "IsRelevant",
                table: "ContactPeople",
                nullable: false,
                defaultValue: false);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "IsRelevant",
                table: "Prices");

            migrationBuilder.DropColumn(
                name: "IsRelevant",
                table: "OrganizationNotes");

            migrationBuilder.DropColumn(
                name: "IsRelevant",
                table: "OrganizationFiles");

            migrationBuilder.DropColumn(
                name: "IsRelevant",
                table: "Devices");

            migrationBuilder.DropColumn(
                name: "IsRelevant",
                table: "ContactPersonPhoneNumbers");

            migrationBuilder.DropColumn(
                name: "IsRelevant",
                table: "ContactPersonEmails");

            migrationBuilder.DropColumn(
                name: "IsRelevant",
                table: "ContactPeople");
        }
    }
}
