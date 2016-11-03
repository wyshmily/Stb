using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;

namespace Stb.Platform.Models
{
    // 工作种类
    public class JobMeasurement
    {
        public int Id { get; set; }

        [Required(ErrorMessage = "{0}不能为空")]
        [StringLength(16, MinimumLength = 1, ErrorMessage = "{0}长度为{2}到{1}个字符")]
        [Display(Name = "计量项目")]
        public string Name { get; set; }

        [Required(ErrorMessage = "{0}不能为空")]
        [StringLength(16, MinimumLength = 1, ErrorMessage = "{0}长度为{2}到{1}个字符")]
        [Display(Name = "计量单位")]
        public string Unit { get; set; }

        public int JobClassId { get; set; }

        public JobClass JobClass { get; set; }

        public List<WorkLoad> WorkLoads { get; set; }
    }
}
