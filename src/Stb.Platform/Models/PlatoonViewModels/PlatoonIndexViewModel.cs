using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace Stb.Data.Models.PlatoonViewModels
{
    public class PlatoonIndexViewModel
    {
        public string Id { get; set; }

        [Display(Name = "账号（手机号）")]
        public string UserName { get; set; }


        [Display(Name = "是否启用")]
        public bool Enabled { get; set; }

        [Display(Name = "姓名")]
        public string Name { get; set; }

        [Display(Name = "性别")]
        public bool Gender { get; set; }   // 性别：true-男；false-女

        public PlatoonIndexViewModel()
        {

        }

        public PlatoonIndexViewModel(Platoon platoon)
        {
            Id = platoon.Id;
            UserName = platoon.UserName;
            Name = platoon.Name;
            Gender = platoon.Gender;
            Enabled = platoon.Enabled;
        }
    }
}
