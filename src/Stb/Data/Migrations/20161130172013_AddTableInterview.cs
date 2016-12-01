using System;
using System.Collections.Generic;
using Microsoft.EntityFrameworkCore.Migrations;
using Microsoft.EntityFrameworkCore.Metadata;

namespace Stb.Data.Migrations
{
    public partial class AddTableInterview : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "InterView",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn),
                    FaceInterviewDate = table.Column<DateTime>(nullable: true),
                    FaceInterviewPlatoonName = table.Column<string>(nullable: true),
                    FaceInterviewRecord = table.Column<string>(nullable: true),
                    FaceInterviewResult = table.Column<bool>(nullable: true),
                    OtherResult = table.Column<string>(nullable: true),
                    PlatoonId = table.Column<string>(nullable: true),
                    RemoteCompanyIntroduce = table.Column<bool>(nullable: false),
                    RemoteCooperationIntroduce = table.Column<bool>(nullable: false),
                    RemoteInsight = table.Column<bool>(nullable: false),
                    RemoteInterviewDate = table.Column<DateTime>(nullable: true),
                    RemoteInterviewPlatoonName = table.Column<string>(nullable: true),
                    RemoteInterviewRecord = table.Column<string>(nullable: true),
                    RemoteInterviewResult = table.Column<bool>(nullable: true),
                    RemoteOpinion = table.Column<string>(nullable: true),
                    RemoteRequirmentIntroduce = table.Column<bool>(nullable: false),
                    TotalInterviewResult = table.Column<byte>(nullable: true),
                    WorkerId = table.Column<string>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_InterView", x => x.Id);
                    table.ForeignKey(
                        name: "FK_InterView_AspNetUsers_PlatoonId",
                        column: x => x.PlatoonId,
                        principalTable: "AspNetUsers",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_InterView_AspNetUsers_WorkerId",
                        column: x => x.WorkerId,
                        principalTable: "AspNetUsers",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateIndex(
                name: "IX_InterView_PlatoonId",
                table: "InterView",
                column: "PlatoonId");

            migrationBuilder.CreateIndex(
                name: "IX_InterView_WorkerId",
                table: "InterView",
                column: "WorkerId");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "InterView");
        }
    }
}
