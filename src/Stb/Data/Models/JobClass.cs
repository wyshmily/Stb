using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;

namespace Stb.Data.Models
{
    // 工作种类
    public class JobClass
    {
        public int Id { get; set; }

        [Required(ErrorMessage = "{0}不能为空")]
        [StringLength(16, MinimumLength = 1, ErrorMessage = "{0}长度为{2}到{1}个字符")]
        [Display(Name = "种类名称")]
        public string Name { get; set; }

        public int JobCategoryId { get; set; }

        public JobCategory JobCategory { get; set; }

        public List<JobMeasurement> JobMeasurements { get; set; }

        public List<EndUserJobClass> EndUserJobClasses { get; set; }

        public List<OrderJobClass> OrderJobClasses { get; set; }
    }
}
