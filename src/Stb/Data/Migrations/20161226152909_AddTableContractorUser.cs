using System;
using System.Collections.Generic;
using Microsoft.EntityFrameworkCore.Migrations;

namespace Stb.Data.Migrations
{
    public partial class AddTableContractorUser : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "ContractorUserId",
                table: "Order",
                nullable: true);

            migrationBuilder.AddColumn<int>(
                name: "ContractorId",
                table: "AspNetUsers",
                nullable: true);

            migrationBuilder.CreateIndex(
                name: "IX_Order_ContractorUserId",
                table: "Order",
                column: "ContractorUserId");

            migrationBuilder.CreateIndex(
                name: "IX_AspNetUsers_ContractorId",
                table: "AspNetUsers",
                column: "ContractorId");

            migrationBuilder.AddForeignKey(
                name: "FK_AspNetUsers_Contractor_ContractorId",
                table: "AspNetUsers",
                column: "ContractorId",
                principalTable: "Contractor",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_Order_AspNetUsers_ContractorUserId",
                table: "Order",
                column: "ContractorUserId",
                principalTable: "AspNetUsers",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_AspNetUsers_Contractor_ContractorId",
                table: "AspNetUsers");

            migrationBuilder.DropForeignKey(
                name: "FK_Order_AspNetUsers_ContractorUserId",
                table: "Order");

            migrationBuilder.DropIndex(
                name: "IX_Order_ContractorUserId",
                table: "Order");

            migrationBuilder.DropIndex(
                name: "IX_AspNetUsers_ContractorId",
                table: "AspNetUsers");

            migrationBuilder.DropColumn(
                name: "ContractorUserId",
                table: "Order");

            migrationBuilder.DropColumn(
                name: "ContractorId",
                table: "AspNetUsers");
        }
    }
}
