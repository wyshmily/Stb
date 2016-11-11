using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Stb.Data.Models;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace Stb.Platform.Models.PlatoonViewModels
{
    public class PlatoonIndexViewModel
    {
        public string Id { get; set; }

        [Display(Name = "帮内职务")]
        public string Title { get; set; }


        [Display(Name = "是否启用")]
        public bool Enabled { get; set; }

        [Display(Name = "姓名")]
        public string Name { get; set; }

        [Display(Name = "性别")]
        public bool Gender { get; set; }  // 性别：true-男；false-女

        public PlatoonIndexViewModel()
        {

        }

        public PlatoonIndexViewModel(Platoon platoon)
        {
            Id = platoon.Id;
            Title = platoon.Title;
            Name = platoon.Name;
            Gender = platoon.Gender;
            Enabled = platoon.Enabled;
        }
    }
}
