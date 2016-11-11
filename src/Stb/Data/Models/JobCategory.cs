using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;

namespace Stb.Data.Models
{
    // 工作门类
    public class JobCategory
    {
        public int Id { get; set; }

        [Required(ErrorMessage = "{0}不能为空")]
        [StringLength(16, MinimumLength = 1, ErrorMessage = "{0}长度为{2}到{1}个字符")]
        [Display(Name = "门类名称")]
        public string Name { get; set; }

        public List<JobClass> JobClasses { get; set; }
    }
}
