using System;
using System.Collections.Generic;
using Microsoft.EntityFrameworkCore.Migrations;

namespace Stb.Data.Migrations
{
    public partial class AlterTableProject : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "ContractNo",
                table: "Project",
                maxLength: 64,
                nullable: true);

            migrationBuilder.AlterColumn<string>(
                name: "Name",
                table: "Project",
                maxLength: 64,
                nullable: true);

            migrationBuilder.AlterColumn<decimal>(
                name: "ContractAmount",
                table: "Project",
                nullable: true);

            migrationBuilder.AlterColumn<string>(
                name: "Contract",
                table: "Project",
                maxLength: 64,
                nullable: true);

            migrationBuilder.AlterColumn<string>(
                name: "Client",
                table: "Project",
                maxLength: 64,
                nullable: true);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "ContractNo",
                table: "Project");

            migrationBuilder.AlterColumn<string>(
                name: "Name",
                table: "Project",
                nullable: true);

            migrationBuilder.AlterColumn<decimal>(
                name: "ContractAmount",
                table: "Project",
                nullable: false);

            migrationBuilder.AlterColumn<string>(
                name: "Contract",
                table: "Project",
                nullable: true);

            migrationBuilder.AlterColumn<string>(
                name: "Client",
                table: "Project",
                nullable: true);
        }
    }
}
