using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using System.ComponentModel.DataAnnotations;

namespace Stb.Data.Models
{
    // 项目与工人关联表
    public class ProjectWorker
    {
        public int Id { get; set; }

        [Required]
        public string ProjectId { get; set; }

        [Required]
        public string WorkerId { get; set; }

        public bool Removed { get; set; }  // 被替换（工人被替换后以Removed=True的状态保留）

        public Project Project { get; set; }

        public Worker Worker { get; set; }
    }
}
