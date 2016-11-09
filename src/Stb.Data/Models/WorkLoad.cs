using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using System.ComponentModel.DataAnnotations;

namespace Stb.Data.Models
{
    // 终端用户与工作类别关联表
    public class WorkLoad
    {
        public int Id { get; set; }

        [Required]
        public string WorkerId { get; set; }

        public int JobMeasurementId { get; set; }

        public int Amount { get; set; } // 工作量

        public Worker Worker { get; set; }

        public JobMeasurement JobMeasurement { get; set; }
    }
}
