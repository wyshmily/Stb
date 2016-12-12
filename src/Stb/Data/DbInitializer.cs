using System.Linq;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.AspNetCore.Identity;

namespace Stb.Data.Models
{
    public class DbInitializer
    {
        public async static void Initialize(ApplicationDbContext context, RoleManager<IdentityRole> roleManager, UserManager<PlatformUser> userManager)
        {
            context.Database.EnsureCreated();

            if (context.Roles.Any())
                return;

            var roles = new IdentityRole[]
            {
                new IdentityRole { Name="系统管理员" },
                new IdentityRole { Name = "运营客服" },
                new IdentityRole { Name = "质控员" },
                new IdentityRole { Name = "排长" },
                new IdentityRole { Name = "工人" },
            };

            foreach (var role in roles)
            {
                await roleManager.CreateAsync(role);
            }

            PlatformUser user = new PlatformUser
            {
                UserName = "18513110716",
                Name = "房鹤",
            };
            await userManager.CreateAsync(user, "123456");
            await userManager.AddToRoleAsync(user, "系统管理员");
        }
    }
}
