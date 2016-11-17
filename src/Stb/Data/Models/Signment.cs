using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using System.ComponentModel.DataAnnotations;

namespace Stb.Data.Models
{
    // 工单与工作类别关联表
    public class Signment
    {
        public int Id { get; set; }

        [Required]
        public string OrderId { get; set; }

        [Required]
        public string WorkerId { get; set; }

        public DateTime Date { get; set; }

        public Order Order { get; set; }

        public Worker Worker { get; set; }
    }
}
