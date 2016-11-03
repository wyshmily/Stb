using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using System.ComponentModel.DataAnnotations;

namespace Stb.Platform.Models
{
    // 工单与工作类别关联表
    public class ProjectJobClass
    {
        public int Id { get; set; }

        [Required]
        public string ProjectId { get; set; }

        public int JobClassId { get; set; }

        public Project Project { get; set; }

        public JobClass JobClass { get; set; }
    }
}
