using System.Linq;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;

namespace Stb.Data.Models
{
    public class DbInitializer
    {
        public async static void Initialize(ApplicationDbContext context, RoleManager<IdentityRole> roleManager, UserManager<PlatformUser> userManager, UserManager<ContractorUser> contractorUserManager)
        {
            context.Database.EnsureCreated();


            //var staffs = await context.ContractorStaff.ToListAsync();

            //foreach (ContractorStaff staff in staffs)
            //{
            //    if (context.ApplicationUser.Any(u => u.UserName == staff.Phone))
            //        continue;

            //    ContractorUser staffUser = new ContractorUser
            //    {
            //        Name = staff.Name,
            //        UserName = staff.Phone,
            //        ContractorId = staff.ContractorId,
            //    };

            //    await contractorUserManager.CreateAsync(staffUser, "123456");
            //}


            //await roleManager.CreateAsync(new IdentityRole("承包商员工"));
            //foreach (var cUser in await contractorUserManager.Users.ToListAsync())
            //{
            //    await contractorUserManager.AddToRoleAsync(cUser, "承包商员工");
            //}




            if (context.Roles.Any())
                return;

            var roles = new IdentityRole[]
            {
                new IdentityRole(Roles.Administrator),
                new IdentityRole(Roles.CustomerService),
                new IdentityRole(Roles.QualityControl),
                new IdentityRole(Roles.Platoon),
                new IdentityRole(Roles.Worker),
                new IdentityRole(Roles.Contractor),
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
