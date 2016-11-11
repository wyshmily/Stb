using System;
using System.Collections.Generic;
using Microsoft.EntityFrameworkCore.Migrations;
using Microsoft.EntityFrameworkCore.Metadata;

namespace Stb.Data.Migrations
{
    public partial class AddBusinessTables : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Something",
                table: "AspNetUsers");

            migrationBuilder.CreateTable(
                name: "JobCategory",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn),
                    Name = table.Column<string>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_JobCategory", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "Province",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false)
                       /* .Annotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn)*/,
                    AdCode = table.Column<int>(nullable: false),
                    Name = table.Column<string>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Province", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "JobClass",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn),
                    JobCategoryId = table.Column<int>(nullable: false),
                    Name = table.Column<string>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_JobClass", x => x.Id);
                    table.ForeignKey(
                        name: "FK_JobClass_JobCategory_JobCategoryId",
                        column: x => x.JobCategoryId,
                        principalTable: "JobCategory",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "City",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false)
                        /*.Annotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn)*/,
                    AdCode = table.Column<int>(nullable: false),
                    CityCode = table.Column<int>(nullable: false),
                    Name = table.Column<string>(nullable: true),
                    ProvinceId = table.Column<int>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_City", x => x.Id);
                    table.ForeignKey(
                        name: "FK_City_Province_ProvinceId",
                        column: x => x.ProvinceId,
                        principalTable: "Province",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "EndUserJobClass",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn),
                    EndUserId = table.Column<string>(nullable: false),
                    Grade = table.Column<string>(nullable: true),
                    JobClassId = table.Column<int>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_EndUserJobClass", x => x.Id);
                    table.ForeignKey(
                        name: "FK_EndUserJobClass_AspNetUsers_EndUserId",
                        column: x => x.EndUserId,
                        principalTable: "AspNetUsers",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_EndUserJobClass_JobClass_JobClassId",
                        column: x => x.JobClassId,
                        principalTable: "JobClass",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "JobMeasurement",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn),
                    JobClassId = table.Column<int>(nullable: false),
                    Name = table.Column<string>(nullable: true),
                    Unit = table.Column<string>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_JobMeasurement", x => x.Id);
                    table.ForeignKey(
                        name: "FK_JobMeasurement_JobClass_JobClassId",
                        column: x => x.JobClassId,
                        principalTable: "JobClass",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "District",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false)
                       /* .Annotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn)*/,
                    AdCode = table.Column<int>(nullable: false),
                    CityId = table.Column<int>(nullable: false),
                    Name = table.Column<string>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_District", x => x.Id);
                    table.ForeignKey(
                        name: "FK_District_City_CityId",
                        column: x => x.CityId,
                        principalTable: "City",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "WorkLoad",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn),
                    Amount = table.Column<int>(nullable: false),
                    JobMeasurementId = table.Column<int>(nullable: false),
                    WorkerId = table.Column<string>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_WorkLoad", x => x.Id);
                    table.ForeignKey(
                        name: "FK_WorkLoad_JobMeasurement_JobMeasurementId",
                        column: x => x.JobMeasurementId,
                        principalTable: "JobMeasurement",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_WorkLoad_AspNetUsers_WorkerId",
                        column: x => x.WorkerId,
                        principalTable: "AspNetUsers",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "EndUserDistrict",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn),
                    DistrictId = table.Column<int>(nullable: false),
                    EndUserId = table.Column<string>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_EndUserDistrict", x => x.Id);
                    table.ForeignKey(
                        name: "FK_EndUserDistrict_District_DistrictId",
                        column: x => x.DistrictId,
                        principalTable: "District",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_EndUserDistrict_AspNetUsers_EndUserId",
                        column: x => x.EndUserId,
                        principalTable: "AspNetUsers",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "Project",
                columns: table => new
                {
                    Id = table.Column<string>(nullable: false),
                    ContactDeadline = table.Column<DateTime>(nullable: true),
                    ContactorStaffId = table.Column<int>(nullable: true),
                    ContractorId = table.Column<int>(nullable: true),
                    ContractorStaffId = table.Column<int>(nullable: true),
                    Description = table.Column<string>(nullable: true),
                    DistrictId = table.Column<int>(nullable: true),
                    ExpectedDays = table.Column<int>(nullable: false),
                    ExpectedStartTime = table.Column<DateTime>(nullable: false),
                    LeadWorkerId = table.Column<int>(nullable: true),
                    LeadWorkerId1 = table.Column<string>(nullable: true),
                    PlatoonId = table.Column<int>(nullable: true),
                    PlatoonId1 = table.Column<string>(nullable: true),
                    ProjectType = table.Column<byte>(nullable: false),
                    WorkAddress = table.Column<string>(nullable: true),
                    WorkLocation = table.Column<string>(nullable: true),
                    WorkerNeeded = table.Column<int>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Project", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Project_Contractor_ContractorId",
                        column: x => x.ContractorId,
                        principalTable: "Contractor",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_Project_ContractorStaff_ContractorStaffId",
                        column: x => x.ContractorStaffId,
                        principalTable: "ContractorStaff",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_Project_District_DistrictId",
                        column: x => x.DistrictId,
                        principalTable: "District",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_Project_AspNetUsers_LeadWorkerId1",
                        column: x => x.LeadWorkerId1,
                        principalTable: "AspNetUsers",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_Project_AspNetUsers_PlatoonId1",
                        column: x => x.PlatoonId1,
                        principalTable: "AspNetUsers",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "ProjectJobClass",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn),
                    JobClassId = table.Column<int>(nullable: false),
                    ProjectId = table.Column<string>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_ProjectJobClass", x => x.Id);
                    table.ForeignKey(
                        name: "FK_ProjectJobClass_JobClass_JobClassId",
                        column: x => x.JobClassId,
                        principalTable: "JobClass",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_ProjectJobClass_Project_ProjectId",
                        column: x => x.ProjectId,
                        principalTable: "Project",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "ProjectWorker",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn),
                    ProjectId = table.Column<string>(nullable: false),
                    Removed = table.Column<bool>(nullable: false),
                    WorkerId = table.Column<string>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_ProjectWorker", x => x.Id);
                    table.ForeignKey(
                        name: "FK_ProjectWorker_Project_ProjectId",
                        column: x => x.ProjectId,
                        principalTable: "Project",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_ProjectWorker_AspNetUsers_WorkerId",
                        column: x => x.WorkerId,
                        principalTable: "AspNetUsers",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "Signment",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn),
                    Date = table.Column<DateTime>(nullable: false),
                    ProjectId = table.Column<string>(nullable: false),
                    WorkerId = table.Column<string>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Signment", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Signment_Project_ProjectId",
                        column: x => x.ProjectId,
                        principalTable: "Project",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_Signment_AspNetUsers_WorkerId",
                        column: x => x.WorkerId,
                        principalTable: "AspNetUsers",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.AddColumn<string>(
                name: "Alipay",
                table: "AspNetUsers",
                nullable: true);

            migrationBuilder.AddColumn<DateTime>(
                name: "Birthday",
                table: "AspNetUsers",
                nullable: true);

            migrationBuilder.AddColumn<bool>(
                name: "Enabled",
                table: "AspNetUsers",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "Gender",
                table: "AspNetUsers",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "HealthStatus",
                table: "AspNetUsers",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "IdCardNumber",
                table: "AspNetUsers",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "NativePlace",
                table: "AspNetUsers",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "QQ",
                table: "AspNetUsers",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "Wechat",
                table: "AspNetUsers",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "ArmyPost",
                table: "AspNetUsers",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "ArmyRank",
                table: "AspNetUsers",
                nullable: true);

            migrationBuilder.AddColumn<DateTime>(
                name: "DischargeTime",
                table: "AspNetUsers",
                nullable: true);

            migrationBuilder.AddColumn<DateTime>(
                name: "MilitaryTime",
                table: "AspNetUsers",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "HeaderId",
                table: "AspNetUsers",
                nullable: true);

            migrationBuilder.AddColumn<bool>(
                name: "IsHeader",
                table: "AspNetUsers",
                nullable: true);

            migrationBuilder.CreateIndex(
                name: "IX_AspNetUsers_HeaderId",
                table: "AspNetUsers",
                column: "HeaderId");

            migrationBuilder.CreateIndex(
                name: "IX_City_ProvinceId",
                table: "City",
                column: "ProvinceId");

            migrationBuilder.CreateIndex(
                name: "IX_District_CityId",
                table: "District",
                column: "CityId");

            migrationBuilder.CreateIndex(
                name: "IX_EndUserDistrict_DistrictId",
                table: "EndUserDistrict",
                column: "DistrictId");

            migrationBuilder.CreateIndex(
                name: "IX_EndUserDistrict_EndUserId",
                table: "EndUserDistrict",
                column: "EndUserId");

            migrationBuilder.CreateIndex(
                name: "IX_EndUserJobClass_EndUserId",
                table: "EndUserJobClass",
                column: "EndUserId");

            migrationBuilder.CreateIndex(
                name: "IX_EndUserJobClass_JobClassId",
                table: "EndUserJobClass",
                column: "JobClassId");

            migrationBuilder.CreateIndex(
                name: "IX_JobClass_JobCategoryId",
                table: "JobClass",
                column: "JobCategoryId");

            migrationBuilder.CreateIndex(
                name: "IX_JobMeasurement_JobClassId",
                table: "JobMeasurement",
                column: "JobClassId");

            migrationBuilder.CreateIndex(
                name: "IX_Project_ContractorId",
                table: "Project",
                column: "ContractorId");

            migrationBuilder.CreateIndex(
                name: "IX_Project_ContractorStaffId",
                table: "Project",
                column: "ContractorStaffId");

            migrationBuilder.CreateIndex(
                name: "IX_Project_DistrictId",
                table: "Project",
                column: "DistrictId");

            migrationBuilder.CreateIndex(
                name: "IX_Project_LeadWorkerId1",
                table: "Project",
                column: "LeadWorkerId1");

            migrationBuilder.CreateIndex(
                name: "IX_Project_PlatoonId1",
                table: "Project",
                column: "PlatoonId1");

            migrationBuilder.CreateIndex(
                name: "IX_ProjectJobClass_JobClassId",
                table: "ProjectJobClass",
                column: "JobClassId");

            migrationBuilder.CreateIndex(
                name: "IX_ProjectJobClass_ProjectId",
                table: "ProjectJobClass",
                column: "ProjectId");

            migrationBuilder.CreateIndex(
                name: "IX_ProjectWorker_ProjectId",
                table: "ProjectWorker",
                column: "ProjectId");

            migrationBuilder.CreateIndex(
                name: "IX_ProjectWorker_WorkerId",
                table: "ProjectWorker",
                column: "WorkerId");

            migrationBuilder.CreateIndex(
                name: "IX_Signment_ProjectId",
                table: "Signment",
                column: "ProjectId");

            migrationBuilder.CreateIndex(
                name: "IX_Signment_WorkerId",
                table: "Signment",
                column: "WorkerId");

            migrationBuilder.CreateIndex(
                name: "IX_WorkLoad_JobMeasurementId",
                table: "WorkLoad",
                column: "JobMeasurementId");

            migrationBuilder.CreateIndex(
                name: "IX_WorkLoad_WorkerId",
                table: "WorkLoad",
                column: "WorkerId");

            migrationBuilder.AddForeignKey(
                name: "FK_AspNetUsers_AspNetUsers_HeaderId",
                table: "AspNetUsers",
                column: "HeaderId",
                principalTable: "AspNetUsers",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_AspNetUsers_AspNetUsers_HeaderId",
                table: "AspNetUsers");

            migrationBuilder.DropIndex(
                name: "IX_AspNetUsers_HeaderId",
                table: "AspNetUsers");

            migrationBuilder.DropColumn(
                name: "Alipay",
                table: "AspNetUsers");

            migrationBuilder.DropColumn(
                name: "Birthday",
                table: "AspNetUsers");

            migrationBuilder.DropColumn(
                name: "Enabled",
                table: "AspNetUsers");

            migrationBuilder.DropColumn(
                name: "Gender",
                table: "AspNetUsers");

            migrationBuilder.DropColumn(
                name: "HealthStatus",
                table: "AspNetUsers");

            migrationBuilder.DropColumn(
                name: "IdCardNumber",
                table: "AspNetUsers");

            migrationBuilder.DropColumn(
                name: "NativePlace",
                table: "AspNetUsers");

            migrationBuilder.DropColumn(
                name: "QQ",
                table: "AspNetUsers");

            migrationBuilder.DropColumn(
                name: "Wechat",
                table: "AspNetUsers");

            migrationBuilder.DropColumn(
                name: "ArmyPost",
                table: "AspNetUsers");

            migrationBuilder.DropColumn(
                name: "ArmyRank",
                table: "AspNetUsers");

            migrationBuilder.DropColumn(
                name: "DischargeTime",
                table: "AspNetUsers");

            migrationBuilder.DropColumn(
                name: "MilitaryTime",
                table: "AspNetUsers");

            migrationBuilder.DropColumn(
                name: "HeaderId",
                table: "AspNetUsers");

            migrationBuilder.DropColumn(
                name: "IsHeader",
                table: "AspNetUsers");

            migrationBuilder.DropTable(
                name: "EndUserDistrict");

            migrationBuilder.DropTable(
                name: "EndUserJobClass");

            migrationBuilder.DropTable(
                name: "ProjectJobClass");

            migrationBuilder.DropTable(
                name: "ProjectWorker");

            migrationBuilder.DropTable(
                name: "Signment");

            migrationBuilder.DropTable(
                name: "WorkLoad");

            migrationBuilder.DropTable(
                name: "Project");

            migrationBuilder.DropTable(
                name: "JobMeasurement");

            migrationBuilder.DropTable(
                name: "District");

            migrationBuilder.DropTable(
                name: "JobClass");

            migrationBuilder.DropTable(
                name: "City");

            migrationBuilder.DropTable(
                name: "JobCategory");

            migrationBuilder.DropTable(
                name: "Province");

            migrationBuilder.AddColumn<string>(
                name: "Something",
                table: "AspNetUsers",
                nullable: true);
        }
    }
}
