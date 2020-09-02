using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace QMPT.Data.Migrations
{
    public partial class UsersTableRemovableBecoming : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<bool>(
                name: "IsRemoved",
                table: "Users",
                nullable: false,
                defaultValue: false);

            migrationBuilder.AddColumn<DateTime>(
                name: "RemovalTime",
                table: "Users",
                nullable: true);

            migrationBuilder.AddColumn<int>(
                name: "RemoverId",
                table: "Users",
                nullable: true);

            migrationBuilder.CreateIndex(
                name: "IX_Users_RemoverId",
                table: "Users",
                column: "RemoverId");

            migrationBuilder.AddForeignKey(
                name: "FK_Users_Users_RemoverId",
                table: "Users",
                column: "RemoverId",
                principalTable: "Users",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Users_Users_RemoverId",
                table: "Users");

            migrationBuilder.DropIndex(
                name: "IX_Users_RemoverId",
                table: "Users");

            migrationBuilder.DropColumn(
                name: "IsRemoved",
                table: "Users");

            migrationBuilder.DropColumn(
                name: "RemovalTime",
                table: "Users");

            migrationBuilder.DropColumn(
                name: "RemoverId",
                table: "Users");
        }
    }
}
