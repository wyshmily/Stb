using System;
using System.Collections.Generic;
using Microsoft.EntityFrameworkCore.Migrations;
using Microsoft.EntityFrameworkCore.Metadata;

namespace Stb.Data.Migrations
{
    public partial class AddEvaluateTables : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "Evaluate",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn),
                    Discriminator = table.Column<string>(nullable: false),
                    EvaluateUserId = table.Column<string>(nullable: true),
                    OrderId = table.Column<string>(nullable: true),
                    Type = table.Column<byte>(nullable: false),
                    WorkAttitude = table.Column<byte>(nullable: true),
                    WorkEfficiency = table.Column<byte>(nullable: true),
                    WorkQuality = table.Column<byte>(nullable: true),
                    HaveCase = table.Column<byte>(nullable: true),
                    LeaderCanCooperate = table.Column<byte>(nullable: true),
                    LeaderDoReport = table.Column<byte>(nullable: true),
                    LeaderLeaveCard = table.Column<byte>(nullable: true),
                    LeaderAbilityLevel = table.Column<byte>(nullable: true),
                    LeaderCanDoDesign = table.Column<byte>(nullable: true),
                    LeaderCanReadDrawings = table.Column<byte>(nullable: true),
                    WorkerJudgement = table.Column<byte>(nullable: true),
                    ComplaintSettlement = table.Column<string>(nullable: true),
                    LeaderAbility = table.Column<byte>(nullable: true),
                    AllocationProblem = table.Column<bool>(nullable: true),
                    CanExplainShutdown = table.Column<bool>(nullable: true),
                    CanStrain = table.Column<bool>(nullable: true),
                    ClearThinking = table.Column<bool>(nullable: true),
                    Communicate = table.Column<byte>(nullable: true),
                    Customer = table.Column<byte>(nullable: true),
                    CustomerJudgement = table.Column<byte>(nullable: true),
                    GoodAdvice = table.Column<bool>(nullable: true),
                    HandleConflits = table.Column<byte>(nullable: true),
                    HandleContradictions = table.Column<bool>(nullable: true),
                    HandleProblemClear = table.Column<bool>(nullable: true),
                    HandleProblemInTime = table.Column<bool>(nullable: true),
                    HandleProblemResonable = table.Column<bool>(nullable: true),
                    HandleProblems = table.Column<byte>(nullable: true),
                    HandleWorkChange = table.Column<bool>(nullable: true),
                    HandleWorkerChange = table.Column<byte>(nullable: true),
                    HandleWorkerChangeWell = table.Column<bool>(nullable: true),
                    KnowRelation = table.Column<bool>(nullable: true),
                    KnowSafty = table.Column<bool>(nullable: true),
                    KnowTask = table.Column<bool>(nullable: true),
                    LeadWorkerId = table.Column<string>(nullable: true),
                    MasterTrend = table.Column<bool>(nullable: true),
                    NiceAttitude = table.Column<bool>(nullable: true),
                    ObayPlatform = table.Column<byte>(nullable: true),
                    ObayPlatformJudgement = table.Column<string>(nullable: true),
                    ObeyOrder = table.Column<bool>(nullable: true),
                    PersonalAbility = table.Column<byte>(nullable: true),
                    PersonalAbilityJudgement = table.Column<string>(nullable: true),
                    ProblemDueToWorkers = table.Column<bool>(nullable: true),
                    ProblemNegativeEffect = table.Column<bool>(nullable: true),
                    ReportInTime = table.Column<bool>(nullable: true),
                    ReportNoHide = table.Column<bool>(nullable: true),
                    RequireWorkers = table.Column<bool>(nullable: true),
                    RightForAll = table.Column<bool>(nullable: true),
                    Situation = table.Column<byte>(nullable: true),
                    TrailResult = table.Column<bool>(nullable: true),
                    WorkForce = table.Column<byte>(nullable: true),
                    WorkPlaceOrder = table.Column<byte>(nullable: true),
                    WorkShutdown = table.Column<bool>(nullable: true),
                    WorkTime = table.Column<byte>(nullable: true),
                    WorkerId = table.Column<string>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Evaluate", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Evaluate_AspNetUsers_EvaluateUserId",
                        column: x => x.EvaluateUserId,
                        principalTable: "AspNetUsers",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_Evaluate_Order_OrderId",
                        column: x => x.OrderId,
                        principalTable: "Order",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_Evaluate_AspNetUsers_LeadWorkerId",
                        column: x => x.LeadWorkerId,
                        principalTable: "AspNetUsers",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_Evaluate_AspNetUsers_WorkerId",
                        column: x => x.WorkerId,
                        principalTable: "AspNetUsers",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateIndex(
                name: "IX_Evaluate_EvaluateUserId",
                table: "Evaluate",
                column: "EvaluateUserId");

            migrationBuilder.CreateIndex(
                name: "IX_Evaluate_OrderId",
                table: "Evaluate",
                column: "OrderId");

            migrationBuilder.CreateIndex(
                name: "IX_Evaluate_LeadWorkerId",
                table: "Evaluate",
                column: "LeadWorkerId");

            migrationBuilder.CreateIndex(
                name: "IX_Evaluate_WorkerId",
                table: "Evaluate",
                column: "WorkerId");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "Evaluate");
        }
    }
}
