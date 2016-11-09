using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace Stb.Official.Models
{
    public class Job
    {
        public int id { get; set; }

        [Range(1, 100000, ErrorMessage = "请选择施工项目类别")]
        public int standardid { get; set; }

        [RegularExpression(@"^1[3|4|5|7|8]\d{9}$", ErrorMessage = "请输入正确的手机号码")]
        [Display(Name = "手机号码")]
        [Required(ErrorMessage = "手机号码不能为空")]
        public string phone { get; set; }

        [Required(ErrorMessage = "任务描述不能为空")]
        [StringLength(60, MinimumLength = 3, ErrorMessage = "任务描述限制3-60个字符")]
        public string content { get; set; }

        public Standard standard { get; set; }
    }
}
