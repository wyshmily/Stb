using System;
using System.Collections.Generic;
using Microsoft.EntityFrameworkCore.Migrations;
using Microsoft.EntityFrameworkCore.Metadata;

namespace Stb.Data.Migrations
{
    public partial class Unknown : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AlterColumn<int>(
                name: "Id",
                table: "Province",
                nullable: false);

            migrationBuilder.AlterColumn<int>(
                name: "Id",
                table: "District",
                nullable: false);

            migrationBuilder.AlterColumn<int>(
                name: "Id",
                table: "City",
                nullable: false);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AlterColumn<int>(
                name: "Id",
                table: "Province",
                nullable: false)
                /*.Annotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn)*/;

            migrationBuilder.AlterColumn<int>(
                name: "Id",
                table: "District",
                nullable: false)
                /*.Annotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn)*/;

            migrationBuilder.AlterColumn<int>(
                name: "Id",
                table: "City",
                nullable: false)
                /*.Annotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn)*/;
        }
    }
}
