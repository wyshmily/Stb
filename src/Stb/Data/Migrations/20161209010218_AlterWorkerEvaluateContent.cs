using System;
using System.Collections.Generic;
using Microsoft.EntityFrameworkCore.Migrations;

namespace Stb.Data.Migrations
{
    public partial class AlterWorkerEvaluateContent : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<byte>(
                name: "WorkScore",
                table: "Evaluate",
                nullable: true);

            migrationBuilder.AddColumn<bool>(
                name: "WorkerCanDesign",
                table: "Evaluate",
                nullable: true);

            migrationBuilder.AddColumn<bool>(
                name: "WorkerCanReadDrawings",
                table: "Evaluate",
                nullable: true);

            migrationBuilder.AddColumn<bool>(
                name: "WorkerCooperates",
                table: "Evaluate",
                nullable: true);

            migrationBuilder.AddColumn<bool>(
                name: "WorkerGiveAdvices",
                table: "Evaluate",
                nullable: true);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "WorkScore",
                table: "Evaluate");

            migrationBuilder.DropColumn(
                name: "WorkerCanDesign",
                table: "Evaluate");

            migrationBuilder.DropColumn(
                name: "WorkerCanReadDrawings",
                table: "Evaluate");

            migrationBuilder.DropColumn(
                name: "WorkerCooperates",
                table: "Evaluate");

            migrationBuilder.DropColumn(
                name: "WorkerGiveAdvices",
                table: "Evaluate");
        }
    }
}
