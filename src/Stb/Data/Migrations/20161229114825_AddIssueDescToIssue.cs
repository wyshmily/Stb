using System;
using System.Collections.Generic;
using Microsoft.EntityFrameworkCore.Migrations;

namespace Stb.Data.Migrations
{
    public partial class AddIssueDescToIssue : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "IsuueDesc",
                table: "Issue",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "SolutionDesc",
                table: "Issue",
                nullable: true);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "IsuueDesc",
                table: "Issue");

            migrationBuilder.DropColumn(
                name: "SolutionDesc",
                table: "Issue");
        }
    }
}
