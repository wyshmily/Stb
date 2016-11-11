using System;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Migrations;
using Stb.Data;

namespace Stb.Data.Migrations
{
    [DbContext(typeof(ApplicationDbContext))]
    [Migration("20161111130441_AddPlatoonTitle")]
    partial class AddPlatoonTitle
    {
        protected override void BuildTargetModel(ModelBuilder modelBuilder)
        {
            modelBuilder
                .HasAnnotation("ProductVersion", "1.0.0-rtm-21431")
                .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

            modelBuilder.Entity("Microsoft.AspNetCore.Identity.EntityFrameworkCore.IdentityRole", b =>
                {
                    b.Property<string>("Id");

                    b.Property<string>("ConcurrencyStamp")
                        .IsConcurrencyToken();

                    b.Property<string>("Name")
                        .HasAnnotation("MaxLength", 256);

                    b.Property<string>("NormalizedName")
                        .HasAnnotation("MaxLength", 256);

                    b.HasKey("Id");

                    b.HasIndex("NormalizedName")
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

                    b.HasIndex("UserId");

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
                    b.Property<string>("Id");

                    b.Property<int>("AccessFailedCount");

                    b.Property<string>("ConcurrencyStamp")
                        .IsConcurrencyToken();

                    b.Property<string>("Discriminator")
                        .IsRequired();

                    b.Property<string>("Email")
                        .HasAnnotation("MaxLength", 256);

                    b.Property<bool>("EmailConfirmed");

                    b.Property<bool>("LockoutEnabled");

                    b.Property<DateTimeOffset?>("LockoutEnd");

                    b.Property<string>("Name")
                        .IsRequired()
                        .HasAnnotation("MaxLength", 16);

                    b.Property<string>("NormalizedEmail")
                        .HasAnnotation("MaxLength", 256);

                    b.Property<string>("NormalizedUserName")
                        .HasAnnotation("MaxLength", 256);

                    b.Property<string>("PasswordHash");

                    b.Property<string>("PhoneNumber");

                    b.Property<bool>("PhoneNumberConfirmed");

                    b.Property<string>("SecurityStamp");

                    b.Property<bool>("TwoFactorEnabled");

                    b.Property<string>("UserName")
                        .HasAnnotation("MaxLength", 256);

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

                    b.HasKey("Id");

                    b.HasIndex("ProvinceId");

                    b.ToTable("City");
                });

            modelBuilder.Entity("Stb.Data.Models.Contractor", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd();

                    b.Property<string>("Address")
                        .HasAnnotation("MaxLength", 256);

                    b.Property<bool>("Enabled");

                    b.Property<int?>("HeadStaffId");

                    b.Property<string>("Name")
                        .IsRequired()
                        .HasAnnotation("MaxLength", 64);

                    b.Property<string>("Phone")
                        .IsRequired()
                        .HasAnnotation("MaxLength", 32);

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
                        .HasAnnotation("MaxLength", 64);

                    b.Property<string>("Phone")
                        .IsRequired();

                    b.HasKey("Id");

                    b.HasIndex("ContractorId");

                    b.ToTable("ContractorStaff");
                });

            modelBuilder.Entity("Stb.Data.Models.District", b =>
                {
                    b.Property<int>("Id");

                    b.Property<int>("AdCode");

                    b.Property<int>("CityId");

                    b.Property<string>("Name");

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

                    b.Property<string>("Grade");

                    b.Property<int>("JobClassId");

                    b.HasKey("Id");

                    b.HasIndex("EndUserId");

                    b.HasIndex("JobClassId");

                    b.ToTable("EndUserJobClass");
                });

            modelBuilder.Entity("Stb.Data.Models.JobCategory", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd();

                    b.Property<string>("Name")
                        .IsRequired()
                        .HasAnnotation("MaxLength", 16);

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
                        .HasAnnotation("MaxLength", 16);

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
                        .HasAnnotation("MaxLength", 16);

                    b.Property<string>("Unit")
                        .IsRequired()
                        .HasAnnotation("MaxLength", 16);

                    b.HasKey("Id");

                    b.HasIndex("JobClassId");

                    b.ToTable("JobMeasurement");
                });

            modelBuilder.Entity("Stb.Data.Models.Project", b =>
                {
                    b.Property<string>("Id");

                    b.Property<DateTime?>("ContactDeadline");

                    b.Property<int?>("ContactorStaffId");

                    b.Property<int?>("ContractorId");

                    b.Property<int?>("ContractorStaffId");

                    b.Property<string>("Description");

                    b.Property<int?>("DistrictId");

                    b.Property<int>("ExpectedDays");

                    b.Property<DateTime>("ExpectedStartTime");

                    b.Property<int?>("LeadWorkerId");

                    b.Property<string>("LeadWorkerId1");

                    b.Property<int?>("PlatoonId");

                    b.Property<string>("PlatoonId1");

                    b.Property<byte>("ProjectType");

                    b.Property<string>("WorkAddress");

                    b.Property<string>("WorkLocation");

                    b.Property<int>("WorkerNeeded");

                    b.HasKey("Id");

                    b.HasIndex("ContractorId");

                    b.HasIndex("ContractorStaffId");

                    b.HasIndex("DistrictId");

                    b.HasIndex("LeadWorkerId1");

                    b.HasIndex("PlatoonId1");

                    b.ToTable("Project");
                });

            modelBuilder.Entity("Stb.Data.Models.ProjectJobClass", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd();

                    b.Property<int>("JobClassId");

                    b.Property<string>("ProjectId")
                        .IsRequired();

                    b.HasKey("Id");

                    b.HasIndex("JobClassId");

                    b.HasIndex("ProjectId");

                    b.ToTable("ProjectJobClass");
                });

            modelBuilder.Entity("Stb.Data.Models.ProjectWorker", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd();

                    b.Property<string>("ProjectId")
                        .IsRequired();

                    b.Property<bool>("Removed");

                    b.Property<string>("WorkerId")
                        .IsRequired();

                    b.HasKey("Id");

                    b.HasIndex("ProjectId");

                    b.HasIndex("WorkerId");

                    b.ToTable("ProjectWorker");
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

                    b.Property<DateTime>("Date");

                    b.Property<string>("ProjectId")
                        .IsRequired();

                    b.Property<string>("WorkerId")
                        .IsRequired();

                    b.HasKey("Id");

                    b.HasIndex("ProjectId");

                    b.HasIndex("WorkerId");

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

                    b.Property<bool>("Enabled");

                    b.Property<bool>("Gender");

                    b.Property<string>("HealthStatus");

                    b.Property<string>("IdCardNumber");

                    b.Property<string>("NativePlace");

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

                    b.Property<string>("HeaderId");

                    b.Property<bool>("IsHeader");

                    b.HasIndex("HeaderId");

                    b.ToTable("Worker");

                    b.HasDiscriminator().HasValue("Worker");
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

            modelBuilder.Entity("Stb.Data.Models.Project", b =>
                {
                    b.HasOne("Stb.Data.Models.Contractor", "Contractor")
                        .WithMany("Projects")
                        .HasForeignKey("ContractorId");

                    b.HasOne("Stb.Data.Models.ContractorStaff", "ContractorStaff")
                        .WithMany("Projects")
                        .HasForeignKey("ContractorStaffId");

                    b.HasOne("Stb.Data.Models.District", "District")
                        .WithMany("Projects")
                        .HasForeignKey("DistrictId");

                    b.HasOne("Stb.Data.Models.Worker", "LeadWorker")
                        .WithMany("LeadProjects")
                        .HasForeignKey("LeadWorkerId1");

                    b.HasOne("Stb.Data.Models.Platoon", "Platoon")
                        .WithMany("Projects")
                        .HasForeignKey("PlatoonId1");
                });

            modelBuilder.Entity("Stb.Data.Models.ProjectJobClass", b =>
                {
                    b.HasOne("Stb.Data.Models.JobClass", "JobClass")
                        .WithMany("ProjectJobClasses")
                        .HasForeignKey("JobClassId")
                        .OnDelete(DeleteBehavior.Cascade);

                    b.HasOne("Stb.Data.Models.Project", "Project")
                        .WithMany("ProjectJobClasses")
                        .HasForeignKey("ProjectId")
                        .OnDelete(DeleteBehavior.Cascade);
                });

            modelBuilder.Entity("Stb.Data.Models.ProjectWorker", b =>
                {
                    b.HasOne("Stb.Data.Models.Project", "Project")
                        .WithMany("ProjectWorkers")
                        .HasForeignKey("ProjectId")
                        .OnDelete(DeleteBehavior.Cascade);

                    b.HasOne("Stb.Data.Models.Worker", "Worker")
                        .WithMany("ProjectWorkers")
                        .HasForeignKey("WorkerId")
                        .OnDelete(DeleteBehavior.Cascade);
                });

            modelBuilder.Entity("Stb.Data.Models.Signment", b =>
                {
                    b.HasOne("Stb.Data.Models.Project", "Project")
                        .WithMany("Signments")
                        .HasForeignKey("ProjectId")
                        .OnDelete(DeleteBehavior.Cascade);

                    b.HasOne("Stb.Data.Models.Worker", "Worker")
                        .WithMany("Signments")
                        .HasForeignKey("WorkerId")
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

            modelBuilder.Entity("Stb.Data.Models.Worker", b =>
                {
                    b.HasOne("Stb.Data.Models.Worker", "Header")
                        .WithMany("Workers")
                        .HasForeignKey("HeaderId");
                });
        }
    }
}
