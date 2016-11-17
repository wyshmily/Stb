using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using System.ComponentModel.DataAnnotations;

namespace Stb.Data.Models
{
    // 终端用户与工作类别关联表
    public class EndUserJobClass
    {
        public int Id { get; set; }

        [Required]
        public string EndUserId { get; set; }

        public int JobClassId { get; set; }

        public byte Grade { get; set; }   // 技能等级：1-大工；2-小工；0-标准工

        public EndUser EndUser { get; set; }

        public JobClass JobClass { get; set; }
    }
}
