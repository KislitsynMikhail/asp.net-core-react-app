using Microsoft.EntityFrameworkCore.Migrations;

namespace QMPT.Data.Migrations
{
    public partial class CreatorIdNullableColumnAddedToEveryTable : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<int>(
                name: "CreatorId",
                table: "Users",
                nullable: true);

            migrationBuilder.AddColumn<int>(
                name: "CreatorId",
                table: "RefreshTokens",
                nullable: true);

            migrationBuilder.AlterColumn<int>(
                name: "CreatorId",
                table: "Prices",
                nullable: true,
                oldClrType: typeof(int),
                oldType: "integer");

            migrationBuilder.AlterColumn<int>(
                name: "CreatorId",
                table: "Organizations",
                nullable: true,
                oldClrType: typeof(int),
                oldType: "integer");

            migrationBuilder.AlterColumn<int>(
                name: "CreatorId",
                table: "OrganizationNotes",
                nullable: true,
                oldClrType: typeof(int),
                oldType: "integer");

            migrationBuilder.AlterColumn<int>(
                name: "CreatorId",
                table: "OrganizationFiles",
                nullable: true,
                oldClrType: typeof(int),
                oldType: "integer");

            migrationBuilder.AddColumn<int>(
                name: "CreatorId",
                table: "KeyValuePairs",
                nullable: true);

            migrationBuilder.AlterColumn<int>(
                name: "CreatorId",
                table: "Devices",
                nullable: true,
                oldClrType: typeof(int),
                oldType: "integer");

            migrationBuilder.AlterColumn<int>(
                name: "CreatorId",
                table: "ContactPersonPhoneNumbers",
                nullable: true,
                oldClrType: typeof(int),
                oldType: "integer");

            migrationBuilder.AlterColumn<int>(
                name: "CreatorId",
                table: "ContactPersonEmails",
                nullable: true,
                oldClrType: typeof(int),
                oldType: "integer");

            migrationBuilder.AlterColumn<int>(
                name: "CreatorId",
                table: "ContactPeople",
                nullable: true,
                oldClrType: typeof(int),
                oldType: "integer");

            migrationBuilder.AddColumn<int>(
                name: "CreatorId",
                table: "Accessories",
                nullable: true);

            migrationBuilder.CreateIndex(
                name: "IX_Users_CreatorId",
                table: "Users",
                column: "CreatorId");

            migrationBuilder.CreateIndex(
                name: "IX_RefreshTokens_CreatorId",
                table: "RefreshTokens",
                column: "CreatorId");

            migrationBuilder.CreateIndex(
                name: "IX_KeyValuePairs_CreatorId",
                table: "KeyValuePairs",
                column: "CreatorId");

            migrationBuilder.CreateIndex(
                name: "IX_Accessories_CreatorId",
                table: "Accessories",
                column: "CreatorId");

            migrationBuilder.AddForeignKey(
                name: "FK_Accessories_Users_CreatorId",
                table: "Accessories",
                column: "CreatorId",
                principalTable: "Users",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_KeyValuePairs_Users_CreatorId",
                table: "KeyValuePairs",
                column: "CreatorId",
                principalTable: "Users",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_RefreshTokens_Users_CreatorId",
                table: "RefreshTokens",
                column: "CreatorId",
                principalTable: "Users",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_Users_Users_CreatorId",
                table: "Users",
                column: "CreatorId",
                principalTable: "Users",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Accessories_Users_CreatorId",
                table: "Accessories");

            migrationBuilder.DropForeignKey(
                name: "FK_KeyValuePairs_Users_CreatorId",
                table: "KeyValuePairs");

            migrationBuilder.DropForeignKey(
                name: "FK_RefreshTokens_Users_CreatorId",
                table: "RefreshTokens");

            migrationBuilder.DropForeignKey(
                name: "FK_Users_Users_CreatorId",
                table: "Users");

            migrationBuilder.DropIndex(
                name: "IX_Users_CreatorId",
                table: "Users");

            migrationBuilder.DropIndex(
                name: "IX_RefreshTokens_CreatorId",
                table: "RefreshTokens");

            migrationBuilder.DropIndex(
                name: "IX_KeyValuePairs_CreatorId",
                table: "KeyValuePairs");

            migrationBuilder.DropIndex(
                name: "IX_Accessories_CreatorId",
                table: "Accessories");

            migrationBuilder.DropColumn(
                name: "CreatorId",
                table: "Users");

            migrationBuilder.DropColumn(
                name: "CreatorId",
                table: "RefreshTokens");

            migrationBuilder.DropColumn(
                name: "CreatorId",
                table: "KeyValuePairs");

            migrationBuilder.DropColumn(
                name: "CreatorId",
                table: "Accessories");

            migrationBuilder.AlterColumn<int>(
                name: "CreatorId",
                table: "Prices",
                type: "integer",
                nullable: false,
                oldClrType: typeof(int),
                oldNullable: true);

            migrationBuilder.AlterColumn<int>(
                name: "CreatorId",
                table: "Organizations",
                type: "integer",
                nullable: false,
                oldClrType: typeof(int),
                oldNullable: true);

            migrationBuilder.AlterColumn<int>(
                name: "CreatorId",
                table: "OrganizationNotes",
                type: "integer",
                nullable: false,
                oldClrType: typeof(int),
                oldNullable: true);

            migrationBuilder.AlterColumn<int>(
                name: "CreatorId",
                table: "OrganizationFiles",
                type: "integer",
                nullable: false,
                oldClrType: typeof(int),
                oldNullable: true);

            migrationBuilder.AlterColumn<int>(
                name: "CreatorId",
                table: "Devices",
                type: "integer",
                nullable: false,
                oldClrType: typeof(int),
                oldNullable: true);

            migrationBuilder.AlterColumn<int>(
                name: "CreatorId",
                table: "ContactPersonPhoneNumbers",
                type: "integer",
                nullable: false,
                oldClrType: typeof(int),
                oldNullable: true);

            migrationBuilder.AlterColumn<int>(
                name: "CreatorId",
                table: "ContactPersonEmails",
                type: "integer",
                nullable: false,
                oldClrType: typeof(int),
                oldNullable: true);

            migrationBuilder.AlterColumn<int>(
                name: "CreatorId",
                table: "ContactPeople",
                type: "integer",
                nullable: false,
                oldClrType: typeof(int),
                oldNullable: true);
        }
    }
}
