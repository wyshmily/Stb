using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Stb.Data;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.AspNetCore.Identity;

namespace Stb.Platform.Models
{
    public class DbInitializer
    {
        public async static void Initialize(ApplicationDbContext context, RoleManager<IdentityRole> roleManager)
        {
            context.Database.EnsureCreated();

            if (context.Roles.Any())
                return;

            var roles = new IdentityRole[]
            {
                new IdentityRole { Name="系统管理员" },
                new IdentityRole {Name = "运营客服" }
            };

            foreach (var role in roles)
            {
                await roleManager.CreateAsync(role);
            }
        }
    }
}
