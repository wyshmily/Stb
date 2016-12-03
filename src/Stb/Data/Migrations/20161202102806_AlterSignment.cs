using System;
using System.Collections.Generic;
using Microsoft.EntityFrameworkCore.Migrations;

namespace Stb.Data.Migrations
{
    public partial class AlterSignment : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Signment_AspNetUsers_WorkerId",
                table: "Signment");

            migrationBuilder.RenameColumn(
                name: "WorkerId",
                table: "Signment",
                newName: "EndUserId");

            migrationBuilder.RenameColumn(
                name: "Date",
                table: "Signment",
                newName: "Time");

            migrationBuilder.RenameIndex(
                name: "IX_Signment_WorkerId",
                table: "Signment",
                newName: "IX_Signment_EndUserId");

            migrationBuilder.AddColumn<string>(
                name: "Address",
                table: "Signment",
                nullable: true);

            migrationBuilder.AddColumn<bool>(
                name: "InOut",
                table: "Signment",
                nullable: false,
                defaultValue: false);

            migrationBuilder.AddColumn<string>(
                name: "Location",
                table: "Signment",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "Pics",
                table: "Signment",
                nullable: true);

            migrationBuilder.AddForeignKey(
                name: "FK_Signment_AspNetUsers_EndUserId",
                table: "Signment",
                column: "EndUserId",
                principalTable: "AspNetUsers",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Signment_AspNetUsers_EndUserId",
                table: "Signment");

            migrationBuilder.DropColumn(
                name: "Address",
                table: "Signment");

            migrationBuilder.DropColumn(
                name: "InOut",
                table: "Signment");

            migrationBuilder.DropColumn(
                name: "Location",
                table: "Signment");

            migrationBuilder.DropColumn(
                name: "Pics",
                table: "Signment");

            migrationBuilder.RenameColumn(
                name: "Time",
                table: "Signment",
                newName: "Date");

            migrationBuilder.RenameColumn(
                name: "EndUserId",
                table: "Signment",
                newName: "WorkerId");

            migrationBuilder.RenameIndex(
                name: "IX_Signment_EndUserId",
                table: "Signment",
                newName: "IX_Signment_WorkerId");

            migrationBuilder.AddForeignKey(
                name: "FK_Signment_AspNetUsers_WorkerId",
                table: "Signment",
                column: "WorkerId",
                principalTable: "AspNetUsers",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
