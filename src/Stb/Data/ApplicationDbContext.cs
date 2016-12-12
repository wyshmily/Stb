﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using Stb.Data.Models;

namespace Stb.Data
{
    public class ApplicationDbContext : IdentityDbContext<ApplicationUser>
    {
        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options)
            : base(options)
        {
        }

        protected override void OnModelCreating(ModelBuilder builder)
        {
            base.OnModelCreating(builder);
            // Customize the ASP.NET Identity model and override the defaults if needed.
            // For example, you can rename the ASP.NET Identity table names and more.
            // Add your customizations after calling base.OnModelCreating(builder);
        }

        // 系统用户
        public DbSet<ApplicationUser> ApplicationUser { get; set; }

        // 终端用户
        public DbSet<EndUser> EndUser { get; set; }

        // 后台用户
        public DbSet<PlatformUser> PlatformUser { get; set; }

        // 排长
        public DbSet<Platoon> Platoon { get; set; }

        // 工人
        public DbSet<Worker> Worker { get; set; }

        // 承包商
        public DbSet<Contractor> Contractor { get; set; }

        // 承包商员工
        public DbSet<ContractorStaff> ContractorStaff { get; set; }

        public DbSet<Province> Province { get; set; }

        public DbSet<City> City { get; set; }

        public DbSet<District> District { get; set; }

        public DbSet<EndUserDistrict> EndUserDistrict { get; set; }

        public DbSet<Order> Order { get; set; }

        public DbSet<OrderJobClass> OrderJobClass { get; set; }

        public DbSet<Signment> Signment { get; set; }

        public DbSet<JobCategory> JobCategory { get; set; }

        public DbSet<JobClass> JobClass { get; set; }

        public DbSet<JobMeasurement> JobMeasurement { get; set; }

        public DbSet<EndUserJobClass> EndUserJobClass { get; set; }

        public DbSet<WorkLoad> WorkLoad { get; set; }

        public DbSet<OrderIndexer> OrderIndexer { get; set; }

        public DbSet<Project> Project { get; set; }

        public DbSet<OrderWorker> OrderWorker { get; set; }

        //public DbSet<Stb.Data.Models.Program> Program { get; set; }
        public DbSet<Interview> InterView { get; set; }

        public DbSet<Message> Message { get; set; }

        public DbSet<Issue> Issue { get; set; }

        public DbSet<Evaluate> Evaluate { get; set; }

        public DbSet<OrderEvaluate> OrderEvaluate { get; set; }

        public DbSet<OrderEvaluate_QualityControl> OrderEvaluate_QualityControl { get; set; }

        public DbSet<OrderEvaluate_Customer> OrderEvaluate_Customer { get; set; }

        public DbSet<OrderEvaluate_Platoon> OrderEvaluate_Platoon { get; set; }

        public DbSet<WorkerEvaluate> WorkerEvaluate { get; set; }

        public DbSet<TrailEvaluate> TrailEvaluate { get; set; }
    }
}
