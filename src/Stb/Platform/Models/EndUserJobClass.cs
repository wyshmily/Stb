using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using System.ComponentModel.DataAnnotations;

namespace Stb.Platform.Models
{
    // 终端用户与工作类别关联表
    public class EndUserJobClass
    {
        public int Id { get; set; }

        [Required]
        public string EndUserId { get; set; }

        public int JobClassId { get; set; }

        public string Grade { get; set; }   // 技能等级：大工；小工；标准工

        public EndUser EndUser { get; set; }

        public JobClass JobClass { get; set; }
    }
}
