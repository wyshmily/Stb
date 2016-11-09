using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using System.ComponentModel.DataAnnotations;

namespace Stb.Data.Models
{
    // Add profile data for application users by adding properties to the ApplicationUser class
    public class ApplicationUser : IdentityUser
    {
        [Display(Name="姓名")]
        [Required(ErrorMessage ="{0}不能为空")]
        [StringLength(16, MinimumLength = 1, ErrorMessage = "{0}长度为{2}到{1}个字符")]
        public string Name { get; set; }     // 姓名
    }
}