using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace Stb.Platform.Models.AccountViewModels
{
    public class AccountViewModel
    {
        public string Id { get; set; }

        [Required(ErrorMessage = "{0}不能为空")]
        [Display(Name = "账号（手机号）")]
        [RegularExpression(@"^1[3|4|5|7|8]\d{9}$", ErrorMessage = "请输入正确的手机号码")]
        public string UserName { get; set; }


        [Display(Name = "姓名")]
        [Required(ErrorMessage = "{0}不能为空")]
        [StringLength(32, MinimumLength = 1, ErrorMessage = "{0}长度为{2}到{1}个字符")]
        public string Name { get; set; }


        [Display(Name = "角色")]
        [Required(ErrorMessage = "{0}不能为空")]
        public string Role { get; set; }

        [Required(ErrorMessage = "{0}不能为空")]
        [StringLength(32, MinimumLength = 4, ErrorMessage = "{0}长度为{2}到{1}个字符")]
        [DataType(DataType.Password)]
        [Display(Name = "密码")]
        public string Password { get; set; }


        public AccountViewModel()
        {

        }

        public AccountViewModel(PlatformUser user)
        {
            Id = user.Id;
            UserName = user.UserName;
            Name = user.Name;
            Password = "Hidden";
        }

        public PlatformUser ToApplicationUser()
        {
            PlatformUser appUser = new PlatformUser
            {
                UserName = UserName,
                Name = Name,
                //Email = UserName
            };

            if (Id != null)
                appUser.Id = Id;

            return appUser;
        }
    }
}
