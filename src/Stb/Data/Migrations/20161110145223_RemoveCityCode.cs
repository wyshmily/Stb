using System;
using System.Collections.Generic;
using Microsoft.EntityFrameworkCore.Migrations;

namespace Stb.Data.Migrations
{
    public partial class RemoveCityCode : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "AdCode",
                table: "Province");

            migrationBuilder.DropColumn(
                name: "AdCode",
                table: "City");

            migrationBuilder.DropColumn(
                name: "CityCode",
                table: "City");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<int>(
                name: "AdCode",
                table: "Province",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddColumn<int>(
                name: "AdCode",
                table: "City",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddColumn<int>(
                name: "CityCode",
                table: "City",
                nullable: false,
                defaultValue: 0);
        }
    }
}
