using Microsoft.EntityFrameworkCore.Migrations;

namespace QMPT.Data.Migrations
{
    public partial class ContactPeopleFIOIsRenamedToName : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "FIO",
                table: "ContactPeople");

            migrationBuilder.AddColumn<string>(
                name: "Name",
                table: "ContactPeople",
                maxLength: 100,
                nullable: true);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Name",
                table: "ContactPeople");

            migrationBuilder.AddColumn<string>(
                name: "FIO",
                table: "ContactPeople",
                type: "character varying(100)",
                maxLength: 100,
                nullable: true);
        }
    }
}
