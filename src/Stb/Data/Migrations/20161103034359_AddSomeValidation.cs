using System;
using System.Collections.Generic;
using Microsoft.EntityFrameworkCore.Migrations;

namespace Stb.Data.Migrations
{
    public partial class AddSomeValidation : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AlterColumn<string>(
                name: "Unit",
                table: "JobMeasurement",
                maxLength: 16,
                nullable: false);

            migrationBuilder.AlterColumn<string>(
                name: "Name",
                table: "JobMeasurement",
                maxLength: 16,
                nullable: false);

            migrationBuilder.AlterColumn<string>(
                name: "Name",
                table: "JobClass",
                maxLength: 16,
                nullable: false);

            migrationBuilder.AlterColumn<string>(
                name: "Name",
                table: "JobCategory",
                maxLength: 16,
                nullable: false);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AlterColumn<string>(
                name: "Unit",
                table: "JobMeasurement",
                nullable: true);

            migrationBuilder.AlterColumn<string>(
                name: "Name",
                table: "JobMeasurement",
                nullable: true);

            migrationBuilder.AlterColumn<string>(
                name: "Name",
                table: "JobClass",
                nullable: true);

            migrationBuilder.AlterColumn<string>(
                name: "Name",
                table: "JobCategory",
                nullable: true);
        }
    }
}
