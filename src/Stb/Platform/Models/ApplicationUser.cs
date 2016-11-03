using Microsoft.AspNetCore.Identity.EntityFrameworkCore;

namespace Stb.Platform.Models
{
    // Add profile data for application users by adding properties to the ApplicationUser class
    public class ApplicationUser : IdentityUser
    {
        public string Name { get; set; }     // 姓名
    }
}