using System;
using System.Collections.Generic;
using Microsoft.EntityFrameworkCore.Migrations;

namespace Stb.Data.Migrations
{
    public partial class AlterTableWorkLoad : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_WorkLoad_AspNetUsers_WorkerId",
                table: "WorkLoad");

            migrationBuilder.AlterColumn<string>(
                name: "WorkerId",
                table: "WorkLoad",
                nullable: true,
                oldClrType: typeof(string));

            migrationBuilder.AddColumn<string>(
                name: "OrderId",
                table: "WorkLoad",
                nullable: false,
                defaultValue: "");

            migrationBuilder.CreateIndex(
                name: "IX_WorkLoad_OrderId",
                table: "WorkLoad",
                column: "OrderId");

            migrationBuilder.AddForeignKey(
                name: "FK_WorkLoad_Order_OrderId",
                table: "WorkLoad",
                column: "OrderId",
                principalTable: "Order",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_WorkLoad_AspNetUsers_WorkerId",
                table: "WorkLoad",
                column: "WorkerId",
                principalTable: "AspNetUsers",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_WorkLoad_Order_OrderId",
                table: "WorkLoad");

            migrationBuilder.DropForeignKey(
                name: "FK_WorkLoad_AspNetUsers_WorkerId",
                table: "WorkLoad");

            migrationBuilder.DropIndex(
                name: "IX_WorkLoad_OrderId",
                table: "WorkLoad");

            migrationBuilder.DropColumn(
                name: "OrderId",
                table: "WorkLoad");

            migrationBuilder.AlterColumn<string>(
                name: "WorkerId",
                table: "WorkLoad",
                nullable: false,
                oldClrType: typeof(string),
                oldNullable: true);

            migrationBuilder.AddForeignKey(
                name: "FK_WorkLoad_AspNetUsers_WorkerId",
                table: "WorkLoad",
                column: "WorkerId",
                principalTable: "AspNetUsers",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
