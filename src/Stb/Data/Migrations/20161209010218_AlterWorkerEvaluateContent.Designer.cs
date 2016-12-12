using System;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Migrations;
using Stb.Data;

namespace Stb.Data.Migrations
{
    [DbContext(typeof(ApplicationDbContext))]
    [Migration("20161209010218_AlterWorkerEvaluateContent")]
    partial class AlterWorkerEvaluateContent
    {
        protected override void BuildTargetModel(ModelBuilder modelBuilder)
        {
            modelBuilder
                .HasAnnotation("ProductVersion", "1.1.0-rtm-22752")
                .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

            modelBuilder.Entity("Microsoft.AspNetCore.Identity.EntityFrameworkCore.IdentityRole", b =>
                {
                    b.Property<string>("Id")
                        .ValueGeneratedOnAdd();

                    b.Property<string>("ConcurrencyStamp")
                        .IsConcurrencyToken();

                    b.Property<string>("Name")
                        .HasMaxLength(256);

                    b.Property<string>("NormalizedName")
                        .HasMaxLength(256);

                    b.HasKey("Id");

                    b.HasIndex("NormalizedName")
                        .IsUnique()
                        .HasName("RoleNameIndex");

                    b.ToTable("AspNetRoles");
                });

            modelBuilder.Entity("Microsoft.AspNetCore.Identity.EntityFrameworkCore.IdentityRoleClaim<string>", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd();

                    b.Property<string>("ClaimType");

                    b.Property<string>("ClaimValue");

                    b.Property<string>("RoleId")
                        .IsRequired();

                    b.HasKey("Id");

                    b.HasIndex("RoleId");

                    b.ToTable("AspNetRoleClaims");
                });

            modelBuilder.Entity("Microsoft.AspNetCore.Identity.EntityFrameworkCore.IdentityUserClaim<string>", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd();

                    b.Property<string>("ClaimType");

                    b.Property<string>("ClaimValue");

                    b.Property<string>("UserId")
                        .IsRequired();

                    b.HasKey("Id");

                    b.HasIndex("UserId");

                    b.ToTable("AspNetUserClaims");
                });

            modelBuilder.Entity("Microsoft.AspNetCore.Identity.EntityFrameworkCore.IdentityUserLogin<string>", b =>
                {
                    b.Property<string>("LoginProvider");

                    b.Property<string>("ProviderKey");

                    b.Property<string>("ProviderDisplayName");

                    b.Property<string>("UserId")
                        .IsRequired();

                    b.HasKey("LoginProvider", "ProviderKey");

                    b.HasIndex("UserId");

                    b.ToTable("AspNetUserLogins");
                });

            modelBuilder.Entity("Microsoft.AspNetCore.Identity.EntityFrameworkCore.IdentityUserRole<string>", b =>
                {
                    b.Property<string>("UserId");

                    b.Property<string>("RoleId");

                    b.HasKey("UserId", "RoleId");

                    b.HasIndex("RoleId");

                    b.ToTable("AspNetUserRoles");
                });

            modelBuilder.Entity("Microsoft.AspNetCore.Identity.EntityFrameworkCore.IdentityUserToken<string>", b =>
                {
                    b.Property<string>("UserId");

                    b.Property<string>("LoginProvider");

                    b.Property<string>("Name");

                    b.Property<string>("Value");

                    b.HasKey("UserId", "LoginProvider", "Name");

                    b.ToTable("AspNetUserTokens");
                });

            modelBuilder.Entity("Stb.Data.Models.ApplicationUser", b =>
                {
                    b.Property<string>("Id")
                        .ValueGeneratedOnAdd();

                    b.Property<int>("AccessFailedCount");

                    b.Property<string>("ConcurrencyStamp")
                        .IsConcurrencyToken();

                    b.Property<string>("Discriminator")
                        .IsRequired();

                    b.Property<string>("Email")
                        .HasMaxLength(256);

                    b.Property<bool>("EmailConfirmed");

                    b.Property<bool>("LockoutEnabled");

                    b.Property<DateTimeOffset?>("LockoutEnd");

                    b.Property<string>("Name")
                        .IsRequired()
                        .HasMaxLength(16);

                    b.Property<string>("NormalizedEmail")
                        .HasMaxLength(256);

                    b.Property<string>("NormalizedUserName")
                        .HasMaxLength(256);

                    b.Property<string>("PasswordHash");

                    b.Property<string>("PhoneNumber");

                    b.Property<bool>("PhoneNumberConfirmed");

                    b.Property<string>("SecurityStamp");

                    b.Property<bool>("TwoFactorEnabled");

                    b.Property<string>("UserName")
                        .HasMaxLength(256);

                    b.HasKey("Id");

                    b.HasIndex("NormalizedEmail")
                        .HasName("EmailIndex");

                    b.HasIndex("NormalizedUserName")
                        .IsUnique()
                        .HasName("UserNameIndex");

                    b.ToTable("AspNetUsers");

                    b.HasDiscriminator<string>("Discriminator").HasValue("ApplicationUser");
                });

            modelBuilder.Entity("Stb.Data.Models.City", b =>
                {
                    b.Property<int>("Id");

                    b.Property<string>("Name");

                    b.Property<int>("ProvinceId");

                    b.Property<string>("ProvinceName");

                    b.HasKey("Id");

                    b.HasIndex("ProvinceId");

                    b.ToTable("City");
                });

            modelBuilder.Entity("Stb.Data.Models.Contractor", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd();

                    b.Property<string>("Address")
                        .HasMaxLength(256);

                    b.Property<bool>("Enabled");

                    b.Property<int?>("HeadStaffId");

                    b.Property<string>("Name")
                        .IsRequired()
                        .HasMaxLength(64);

                    b.HasKey("Id");

                    b.ToTable("Contractor");
                });

            modelBuilder.Entity("Stb.Data.Models.ContractorStaff", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd();

                    b.Property<int>("ContractorId");

                    b.Property<string>("Name")
                        .IsRequired()
                        .HasMaxLength(64);

                    b.Property<string>("Phone")
                        .IsRequired();

                    b.HasKey("Id");

                    b.HasIndex("ContractorId");

                    b.ToTable("ContractorStaff");
                });

            modelBuilder.Entity("Stb.Data.Models.District", b =>
                {
                    b.Property<int>("Id");

                    b.Property<int>("CityId");

                    b.Property<string>("CityName");

                    b.Property<string>("Name");

                    b.Property<int>("ProvinceId");

                    b.Property<string>("ProvinceName");

                    b.HasKey("Id");

                    b.HasIndex("CityId");

                    b.ToTable("District");
                });

            modelBuilder.Entity("Stb.Data.Models.EndUserDistrict", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd();

                    b.Property<int>("DistrictId");

                    b.Property<string>("EndUserId")
                        .IsRequired();

                    b.HasKey("Id");

                    b.HasIndex("DistrictId");

                    b.HasIndex("EndUserId");

                    b.ToTable("EndUserDistrict");
                });

            modelBuilder.Entity("Stb.Data.Models.EndUserJobClass", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd();

                    b.Property<string>("EndUserId")
                        .IsRequired();

                    b.Property<byte>("Grade");

                    b.Property<int>("JobClassId");

                    b.HasKey("Id");

                    b.HasIndex("EndUserId");

                    b.HasIndex("JobClassId");

                    b.ToTable("EndUserJobClass");
                });

            modelBuilder.Entity("Stb.Data.Models.Evaluate", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd();

                    b.Property<string>("Discriminator")
                        .IsRequired();

                    b.Property<string>("EvaluateUserId");

                    b.Property<string>("OrderId");

                    b.Property<DateTime>("Time");

                    b.Property<byte>("Type");

                    b.HasKey("Id");

                    b.HasIndex("EvaluateUserId");

                    b.HasIndex("OrderId");

                    b.ToTable("Evaluate");

                    b.HasDiscriminator<string>("Discriminator").HasValue("Evaluate");
                });

            modelBuilder.Entity("Stb.Data.Models.Interview", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd();

                    b.Property<DateTime?>("FaceInterviewDate");

                    b.Property<string>("FaceInterviewPlatoonName");

                    b.Property<string>("FaceInterviewRecord");

                    b.Property<bool?>("FaceInterviewResult");

                    b.Property<string>("OtherResult");

                    b.Property<string>("PlatoonId");

                    b.Property<bool>("RemoteCompanyIntroduce");

                    b.Property<bool>("RemoteCooperationIntroduce");

                    b.Property<bool>("RemoteInsight");

                    b.Property<DateTime?>("RemoteInterviewDate");

                    b.Property<string>("RemoteInterviewPlatoonName");

                    b.Property<string>("RemoteInterviewRecord");

                    b.Property<bool?>("RemoteInterviewResult");

                    b.Property<string>("RemoteOpinion");

                    b.Property<bool>("RemoteRequirmentIntroduce");

                    b.Property<byte?>("TotalInterviewResult");

                    b.Property<string>("WorkerId");

                    b.HasKey("Id");

                    b.HasIndex("PlatoonId");

                    b.HasIndex("WorkerId");

                    b.ToTable("InterView");
                });

            modelBuilder.Entity("Stb.Data.Models.Issue", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd();

                    b.Property<string>("Audios");

                    b.Property<string>("EndUserId")
                        .IsRequired();

                    b.Property<int>("IssueType");

                    b.Property<string>("OrderId")
                        .IsRequired();

                    b.Property<string>("Pics");

                    b.Property<int>("SolutionType");

                    b.Property<DateTime>("Time");

                    b.HasKey("Id");

                    b.HasIndex("EndUserId");

                    b.HasIndex("OrderId");

                    b.ToTable("Issue");
                });

            modelBuilder.Entity("Stb.Data.Models.JobCategory", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd();

                    b.Property<string>("Name")
                        .IsRequired()
                        .HasMaxLength(16);

                    b.HasKey("Id");

                    b.ToTable("JobCategory");
                });

            modelBuilder.Entity("Stb.Data.Models.JobClass", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd();

                    b.Property<int>("JobCategoryId");

                    b.Property<string>("Name")
                        .IsRequired()
                        .HasMaxLength(16);

                    b.HasKey("Id");

                    b.HasIndex("JobCategoryId");

                    b.ToTable("JobClass");
                });

            modelBuilder.Entity("Stb.Data.Models.JobMeasurement", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd();

                    b.Property<int>("JobClassId");

                    b.Property<string>("Name")
                        .IsRequired()
                        .HasMaxLength(16);

                    b.Property<string>("Unit")
                        .IsRequired()
                        .HasMaxLength(16);

                    b.HasKey("Id");

                    b.HasIndex("JobClassId");

                    b.ToTable("JobMeasurement");
                });

            modelBuilder.Entity("Stb.Data.Models.Message", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd();

                    b.Property<string>("EndUserId")
                        .IsRequired();

                    b.Property<bool>("InOut");

                    b.Property<bool>("IsRead");

                    b.Property<string>("OrderId")
                        .IsRequired();

                    b.Property<string>("RootUserName");

                    b.Property<string>("Text");

                    b.Property<DateTime>("Time");

                    b.Property<string>("Title");

                    b.Property<int>("Type");

                    b.HasKey("Id");

                    b.HasIndex("EndUserId");

                    b.HasIndex("OrderId");

                    b.ToTable("Message");
                });

            modelBuilder.Entity("Stb.Data.Models.Order", b =>
                {
                    b.Property<string>("Id")
                        .ValueGeneratedOnAdd();

                    b.Property<string>("AcceptWorkerId");

                    b.Property<DateTime?>("ContactDeadline");

                    b.Property<int?>("ContractorId");

                    b.Property<int?>("ContractorStaffId");

                    b.Property<string>("Description");

                    b.Property<int?>("DistrictId");

                    b.Property<int?>("ExpectedDays");

                    b.Property<DateTime?>("ExpectedStartTime");

                    b.Property<string>("LeadWorkerId");

                    b.Property<byte>("OrderType");

                    b.Property<int?>("PlatformUserId");

                    b.Property<string>("PlatoonId");

                    b.Property<int?>("ProjectId");

                    b.Property<byte>("State");

                    b.Property<string>("WorkAddress");

                    b.Property<string>("WorkLocation");

                    b.Property<int?>("WorkerNeeded");

                    b.HasKey("Id");

                    b.HasIndex("ContractorId");

                    b.HasIndex("ContractorStaffId");

                    b.HasIndex("DistrictId");

                    b.HasIndex("LeadWorkerId");

                    b.HasIndex("PlatoonId");

                    b.HasIndex("ProjectId");

                    b.ToTable("Order");
                });

            modelBuilder.Entity("Stb.Data.Models.OrderIndexer", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd();

                    b.HasKey("Id");

                    b.ToTable("OrderIndexer");
                });

            modelBuilder.Entity("Stb.Data.Models.OrderJobClass", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd();

                    b.Property<int>("JobClassId");

                    b.Property<string>("OrderId")
                        .IsRequired();

                    b.HasKey("Id");

                    b.HasIndex("JobClassId");

                    b.HasIndex("OrderId");

                    b.ToTable("OrderJobClass");
                });

            modelBuilder.Entity("Stb.Data.Models.OrderWorker", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd();

                    b.Property<string>("OrderId")
                        .IsRequired();

                    b.Property<bool>("Removed");

                    b.Property<string>("WorkerId")
                        .IsRequired();

                    b.HasKey("Id");

                    b.HasIndex("OrderId");

                    b.HasIndex("WorkerId");

                    b.ToTable("OrderWorker");
                });

            modelBuilder.Entity("Stb.Data.Models.Project", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd();

                    b.Property<string>("Client")
                        .HasMaxLength(64);

                    b.Property<string>("Contract")
                        .HasMaxLength(64);

                    b.Property<decimal?>("ContractAmount");

                    b.Property<string>("ContractNo")
                        .HasMaxLength(64);

                    b.Property<string>("ContractUrl");

                    b.Property<string>("Description");

                    b.Property<string>("Name")
                        .HasMaxLength(64);

                    b.HasKey("Id");

                    b.ToTable("Project");
                });

            modelBuilder.Entity("Stb.Data.Models.Province", b =>
                {
                    b.Property<int>("Id");

                    b.Property<string>("Name");

                    b.HasKey("Id");

                    b.ToTable("Province");
                });

            modelBuilder.Entity("Stb.Data.Models.Signment", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd();

                    b.Property<string>("Address");

                    b.Property<string>("EndUserId")
                        .IsRequired();

                    b.Property<bool>("InOut");

                    b.Property<string>("Location");

                    b.Property<string>("OrderId")
                        .IsRequired();

                    b.Property<string>("Pics");

                    b.Property<DateTime>("Time");

                    b.Property<int>("Type");

                    b.HasKey("Id");

                    b.HasIndex("EndUserId");

                    b.HasIndex("OrderId");

                    b.ToTable("Signment");
                });

            modelBuilder.Entity("Stb.Data.Models.WorkLoad", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd();

                    b.Property<int>("Amount");

                    b.Property<int>("JobMeasurementId");

                    b.Property<string>("WorkerId")
                        .IsRequired();

                    b.HasKey("Id");

                    b.HasIndex("JobMeasurementId");

                    b.HasIndex("WorkerId");

                    b.ToTable("WorkLoad");
                });

            modelBuilder.Entity("Stb.Data.Models.EndUser", b =>
                {
                    b.HasBaseType("Stb.Data.Models.ApplicationUser");

                    b.Property<string>("Alipay");

                    b.Property<DateTime?>("Birthday");

                    b.Property<string>("DeviceId");

                    b.Property<bool>("Enabled");

                    b.Property<bool>("Gender");

                    b.Property<string>("HealthStatus");

                    b.Property<string>("IdCardNumber");

                    b.Property<string>("NativePlace");

                    b.Property<string>("Portrait");

                    b.Property<string>("PushId");

                    b.Property<string>("QQ");

                    b.Property<string>("Wechat");

                    b.ToTable("EndUser");

                    b.HasDiscriminator().HasValue("EndUser");
                });

            modelBuilder.Entity("Stb.Data.Models.PlatformUser", b =>
                {
                    b.HasBaseType("Stb.Data.Models.ApplicationUser");


                    b.ToTable("PlatformUser");

                    b.HasDiscriminator().HasValue("PlatformUser");
                });

            modelBuilder.Entity("Stb.Data.Models.OrderEvaluate", b =>
                {
                    b.HasBaseType("Stb.Data.Models.Evaluate");

                    b.Property<byte>("WorkAttitude");

                    b.Property<byte>("WorkEfficiency");

                    b.Property<byte>("WorkQuality");

                    b.ToTable("OrderEvaluate");

                    b.HasDiscriminator().HasValue("OrderEvaluate");
                });

            modelBuilder.Entity("Stb.Data.Models.TrailEvaluate", b =>
                {
                    b.HasBaseType("Stb.Data.Models.Evaluate");

                    b.Property<bool>("AllocationProblem");

                    b.Property<bool>("CanExplainShutdown");

                    b.Property<bool>("CanStrain");

                    b.Property<bool>("ClearThinking");

                    b.Property<byte>("Communicate");

                    b.Property<byte>("Customer");

                    b.Property<byte>("CustomerJudgement");

                    b.Property<bool>("GoodAdvice");

                    b.Property<byte>("HandleConflits");

                    b.Property<bool>("HandleContradictions");

                    b.Property<bool>("HandleProblemClear");

                    b.Property<bool>("HandleProblemInTime");

                    b.Property<bool>("HandleProblemResonable");

                    b.Property<byte>("HandleProblems");

                    b.Property<bool>("HandleWorkChange");

                    b.Property<byte>("HandleWorkerChange");

                    b.Property<bool>("HandleWorkerChangeWell");

                    b.Property<bool>("KnowRelation");

                    b.Property<bool>("KnowSafty");

                    b.Property<bool>("KnowTask");

                    b.Property<string>("LeadWorkerId");

                    b.Property<bool>("MasterTrend");

                    b.Property<bool>("NiceAttitude");

                    b.Property<byte>("ObayPlatform");

                    b.Property<string>("ObayPlatformJudgement");

                    b.Property<bool>("ObeyOrder");

                    b.Property<byte>("PersonalAbility");

                    b.Property<string>("PersonalAbilityJudgement");

                    b.Property<bool>("ProblemDueToWorkers");

                    b.Property<bool>("ProblemNegativeEffect");

                    b.Property<bool>("ReportInTime");

                    b.Property<bool>("ReportNoHide");

                    b.Property<bool>("RequireWorkers");

                    b.Property<bool>("RightForAll");

                    b.Property<byte>("Situation");

                    b.Property<bool>("TrailResult");

                    b.Property<byte>("WorkForce");

                    b.Property<byte>("WorkPlaceOrder");

                    b.Property<byte>("WorkQuality");

                    b.Property<bool>("WorkShutdown");

                    b.Property<byte>("WorkTime");

                    b.HasIndex("LeadWorkerId");

                    b.ToTable("TrailEvaluate");

                    b.HasDiscriminator().HasValue("TrailEvaluate");
                });

            modelBuilder.Entity("Stb.Data.Models.WorkerEvaluate", b =>
                {
                    b.HasBaseType("Stb.Data.Models.Evaluate");

                    b.Property<byte>("WorkScore");

                    b.Property<bool>("WorkerCanDesign");

                    b.Property<bool>("WorkerCanReadDrawings");

                    b.Property<bool>("WorkerCooperates");

                    b.Property<bool>("WorkerGiveAdvices");

                    b.Property<string>("WorkerId");

                    b.HasIndex("WorkerId");

                    b.ToTable("WorkerEvaluate");

                    b.HasDiscriminator().HasValue("WorkerEvaluate");
                });

            modelBuilder.Entity("Stb.Data.Models.Platoon", b =>
                {
                    b.HasBaseType("Stb.Data.Models.EndUser");

                    b.Property<string>("ArmyPost");

                    b.Property<string>("ArmyRank");

                    b.Property<DateTime?>("DischargeTime");

                    b.Property<DateTime?>("MilitaryTime");

                    b.Property<string>("Title");

                    b.ToTable("Platoon");

                    b.HasDiscriminator().HasValue("Platoon");
                });

            modelBuilder.Entity("Stb.Data.Models.Worker", b =>
                {
                    b.HasBaseType("Stb.Data.Models.EndUser");

                    b.Property<int?>("BestJobClassId");

                    b.Property<string>("HeaderId");

                    b.Property<bool>("IsCandidate");

                    b.Property<bool>("IsHeader");

                    b.HasIndex("BestJobClassId");

                    b.HasIndex("HeaderId");

                    b.ToTable("Worker");

                    b.HasDiscriminator().HasValue("Worker");
                });

            modelBuilder.Entity("Stb.Data.Models.OrderEvaluate_Customer", b =>
                {
                    b.HasBaseType("Stb.Data.Models.OrderEvaluate");

                    b.Property<byte>("HaveCase");

                    b.Property<byte>("LeaderCanCooperate");

                    b.Property<byte>("LeaderDoReport");

                    b.Property<byte>("LeaderLeaveCard");

                    b.ToTable("OrderEvaluate_Customer");

                    b.HasDiscriminator().HasValue("OrderEvaluate_Customer");
                });

            modelBuilder.Entity("Stb.Data.Models.OrderEvaluate_Platoon", b =>
                {
                    b.HasBaseType("Stb.Data.Models.OrderEvaluate");

                    b.Property<byte>("LeaderAbilityLevel");

                    b.Property<byte>("LeaderCanCooperate");

                    b.Property<byte>("LeaderCanDoDesign");

                    b.Property<byte>("LeaderCanReadDrawings");

                    b.Property<byte>("LeaderDoReport");

                    b.Property<byte>("WorkerJudgement");

                    b.ToTable("OrderEvaluate_Platoon");

                    b.HasDiscriminator().HasValue("OrderEvaluate_Platoon");
                });

            modelBuilder.Entity("Stb.Data.Models.OrderEvaluate_QualityControl", b =>
                {
                    b.HasBaseType("Stb.Data.Models.OrderEvaluate");

                    b.Property<string>("ComplaintSettlement");

                    b.Property<byte>("LeaderAbility");

                    b.ToTable("OrderEvaluate_QualityControl");

                    b.HasDiscriminator().HasValue("OrderEvaluate_QualityControl");
                });

            modelBuilder.Entity("Microsoft.AspNetCore.Identity.EntityFrameworkCore.IdentityRoleClaim<string>", b =>
                {
                    b.HasOne("Microsoft.AspNetCore.Identity.EntityFrameworkCore.IdentityRole")
                        .WithMany("Claims")
                        .HasForeignKey("RoleId")
                        .OnDelete(DeleteBehavior.Cascade);
                });

            modelBuilder.Entity("Microsoft.AspNetCore.Identity.EntityFrameworkCore.IdentityUserClaim<string>", b =>
                {
                    b.HasOne("Stb.Data.Models.ApplicationUser")
                        .WithMany("Claims")
                        .HasForeignKey("UserId")
                        .OnDelete(DeleteBehavior.Cascade);
                });

            modelBuilder.Entity("Microsoft.AspNetCore.Identity.EntityFrameworkCore.IdentityUserLogin<string>", b =>
                {
                    b.HasOne("Stb.Data.Models.ApplicationUser")
                        .WithMany("Logins")
                        .HasForeignKey("UserId")
                        .OnDelete(DeleteBehavior.Cascade);
                });

            modelBuilder.Entity("Microsoft.AspNetCore.Identity.EntityFrameworkCore.IdentityUserRole<string>", b =>
                {
                    b.HasOne("Microsoft.AspNetCore.Identity.EntityFrameworkCore.IdentityRole")
                        .WithMany("Users")
                        .HasForeignKey("RoleId")
                        .OnDelete(DeleteBehavior.Cascade);

                    b.HasOne("Stb.Data.Models.ApplicationUser")
                        .WithMany("Roles")
                        .HasForeignKey("UserId")
                        .OnDelete(DeleteBehavior.Cascade);
                });

            modelBuilder.Entity("Stb.Data.Models.City", b =>
                {
                    b.HasOne("Stb.Data.Models.Province", "Province")
                        .WithMany("Cities")
                        .HasForeignKey("ProvinceId")
                        .OnDelete(DeleteBehavior.Cascade);
                });

            modelBuilder.Entity("Stb.Data.Models.ContractorStaff", b =>
                {
                    b.HasOne("Stb.Data.Models.Contractor", "Contractor")
                        .WithMany("Staffs")
                        .HasForeignKey("ContractorId")
                        .OnDelete(DeleteBehavior.Cascade);
                });

            modelBuilder.Entity("Stb.Data.Models.District", b =>
                {
                    b.HasOne("Stb.Data.Models.City", "City")
                        .WithMany("Districts")
                        .HasForeignKey("CityId")
                        .OnDelete(DeleteBehavior.Cascade);
                });

            modelBuilder.Entity("Stb.Data.Models.EndUserDistrict", b =>
                {
                    b.HasOne("Stb.Data.Models.District", "District")
                        .WithMany("EndUserDistricts")
                        .HasForeignKey("DistrictId")
                        .OnDelete(DeleteBehavior.Cascade);

                    b.HasOne("Stb.Data.Models.EndUser", "EndUser")
                        .WithMany("EndUserDistricts")
                        .HasForeignKey("EndUserId")
                        .OnDelete(DeleteBehavior.Cascade);
                });

            modelBuilder.Entity("Stb.Data.Models.EndUserJobClass", b =>
                {
                    b.HasOne("Stb.Data.Models.EndUser", "EndUser")
                        .WithMany("EndUserJobClasses")
                        .HasForeignKey("EndUserId")
                        .OnDelete(DeleteBehavior.Cascade);

                    b.HasOne("Stb.Data.Models.JobClass", "JobClass")
                        .WithMany("EndUserJobClasses")
                        .HasForeignKey("JobClassId")
                        .OnDelete(DeleteBehavior.Cascade);
                });

            modelBuilder.Entity("Stb.Data.Models.Evaluate", b =>
                {
                    b.HasOne("Stb.Data.Models.ApplicationUser", "EvaluateUser")
                        .WithMany()
                        .HasForeignKey("EvaluateUserId");

                    b.HasOne("Stb.Data.Models.Order", "Order")
                        .WithMany()
                        .HasForeignKey("OrderId");
                });

            modelBuilder.Entity("Stb.Data.Models.Interview", b =>
                {
                    b.HasOne("Stb.Data.Models.Platoon", "Platoon")
                        .WithMany()
                        .HasForeignKey("PlatoonId");

                    b.HasOne("Stb.Data.Models.Worker", "Worker")
                        .WithMany("Interviews")
                        .HasForeignKey("WorkerId");
                });

            modelBuilder.Entity("Stb.Data.Models.Issue", b =>
                {
                    b.HasOne("Stb.Data.Models.EndUser", "EndUser")
                        .WithMany()
                        .HasForeignKey("EndUserId")
                        .OnDelete(DeleteBehavior.Cascade);

                    b.HasOne("Stb.Data.Models.Order", "Order")
                        .WithMany()
                        .HasForeignKey("OrderId")
                        .OnDelete(DeleteBehavior.Cascade);
                });

            modelBuilder.Entity("Stb.Data.Models.JobClass", b =>
                {
                    b.HasOne("Stb.Data.Models.JobCategory", "JobCategory")
                        .WithMany("JobClasses")
                        .HasForeignKey("JobCategoryId")
                        .OnDelete(DeleteBehavior.Cascade);
                });

            modelBuilder.Entity("Stb.Data.Models.JobMeasurement", b =>
                {
                    b.HasOne("Stb.Data.Models.JobClass", "JobClass")
                        .WithMany("JobMeasurements")
                        .HasForeignKey("JobClassId")
                        .OnDelete(DeleteBehavior.Cascade);
                });

            modelBuilder.Entity("Stb.Data.Models.Message", b =>
                {
                    b.HasOne("Stb.Data.Models.EndUser", "EndUser")
                        .WithMany()
                        .HasForeignKey("EndUserId")
                        .OnDelete(DeleteBehavior.Cascade);

                    b.HasOne("Stb.Data.Models.Order", "Order")
                        .WithMany()
                        .HasForeignKey("OrderId")
                        .OnDelete(DeleteBehavior.Cascade);
                });

            modelBuilder.Entity("Stb.Data.Models.Order", b =>
                {
                    b.HasOne("Stb.Data.Models.Contractor", "Contractor")
                        .WithMany("Orders")
                        .HasForeignKey("ContractorId");

                    b.HasOne("Stb.Data.Models.ContractorStaff", "ContractorStaff")
                        .WithMany("Orders")
                        .HasForeignKey("ContractorStaffId");

                    b.HasOne("Stb.Data.Models.District", "District")
                        .WithMany("Orders")
                        .HasForeignKey("DistrictId");

                    b.HasOne("Stb.Data.Models.Worker", "LeadWorker")
                        .WithMany("LeadOrders")
                        .HasForeignKey("LeadWorkerId");

                    b.HasOne("Stb.Data.Models.Platoon", "Platoon")
                        .WithMany("Orders")
                        .HasForeignKey("PlatoonId");

                    b.HasOne("Stb.Data.Models.Project", "Project")
                        .WithMany("Orders")
                        .HasForeignKey("ProjectId");
                });

            modelBuilder.Entity("Stb.Data.Models.OrderJobClass", b =>
                {
                    b.HasOne("Stb.Data.Models.JobClass", "JobClass")
                        .WithMany("OrderJobClasses")
                        .HasForeignKey("JobClassId")
                        .OnDelete(DeleteBehavior.Cascade);

                    b.HasOne("Stb.Data.Models.Order", "Order")
                        .WithMany("OrderJobClasses")
                        .HasForeignKey("OrderId")
                        .OnDelete(DeleteBehavior.Cascade);
                });

            modelBuilder.Entity("Stb.Data.Models.OrderWorker", b =>
                {
                    b.HasOne("Stb.Data.Models.Order", "Order")
                        .WithMany("OrderWorkers")
                        .HasForeignKey("OrderId")
                        .OnDelete(DeleteBehavior.Cascade);

                    b.HasOne("Stb.Data.Models.Worker", "Worker")
                        .WithMany("OrderWorkers")
                        .HasForeignKey("WorkerId")
                        .OnDelete(DeleteBehavior.Cascade);
                });

            modelBuilder.Entity("Stb.Data.Models.Signment", b =>
                {
                    b.HasOne("Stb.Data.Models.EndUser", "EndUser")
                        .WithMany("Signments")
                        .HasForeignKey("EndUserId")
                        .OnDelete(DeleteBehavior.Cascade);

                    b.HasOne("Stb.Data.Models.Order", "Order")
                        .WithMany("Signments")
                        .HasForeignKey("OrderId")
                        .OnDelete(DeleteBehavior.Cascade);
                });

            modelBuilder.Entity("Stb.Data.Models.WorkLoad", b =>
                {
                    b.HasOne("Stb.Data.Models.JobMeasurement", "JobMeasurement")
                        .WithMany("WorkLoads")
                        .HasForeignKey("JobMeasurementId")
                        .OnDelete(DeleteBehavior.Cascade);

                    b.HasOne("Stb.Data.Models.Worker", "Worker")
                        .WithMany("WorkLoads")
                        .HasForeignKey("WorkerId")
                        .OnDelete(DeleteBehavior.Cascade);
                });

            modelBuilder.Entity("Stb.Data.Models.TrailEvaluate", b =>
                {
                    b.HasOne("Stb.Data.Models.Worker", "LeadWorker")
                        .WithMany()
                        .HasForeignKey("LeadWorkerId");
                });

            modelBuilder.Entity("Stb.Data.Models.WorkerEvaluate", b =>
                {
                    b.HasOne("Stb.Data.Models.Worker", "Worker")
                        .WithMany()
                        .HasForeignKey("WorkerId");
                });

            modelBuilder.Entity("Stb.Data.Models.Worker", b =>
                {
                    b.HasOne("Stb.Data.Models.JobClass", "BestJobClass")
                        .WithMany("BestWorkers")
                        .HasForeignKey("BestJobClassId");

                    b.HasOne("Stb.Data.Models.Worker", "Header")
                        .WithMany("Workers")
                        .HasForeignKey("HeaderId");
                });
        }
    }
}
