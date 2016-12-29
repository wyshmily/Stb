using System;
using System.Collections.Generic;
using Microsoft.EntityFrameworkCore.Migrations;

namespace Stb.Data.Migrations
{
    public partial class AddHeadUserIdToContractor : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Order_ContractorStaff_ContractorStaffId",
                table: "Order");

            migrationBuilder.DropIndex(
                name: "IX_Order_ContractorStaffId",
                table: "Order");

            migrationBuilder.DropColumn(
                name: "ContractorStaffId",
                table: "Order");

            migrationBuilder.AddColumn<string>(
                name: "HeadUserId",
                table: "Contractor",
                nullable: true);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "HeadUserId",
                table: "Contractor");

            migrationBuilder.AddColumn<int>(
                name: "ContractorStaffId",
                table: "Order",
                nullable: true);

            migrationBuilder.CreateIndex(
                name: "IX_Order_ContractorStaffId",
                table: "Order",
                column: "ContractorStaffId");

            migrationBuilder.AddForeignKey(
                name: "FK_Order_ContractorStaff_ContractorStaffId",
                table: "Order",
                column: "ContractorStaffId",
                principalTable: "ContractorStaff",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);
        }
    }
}
