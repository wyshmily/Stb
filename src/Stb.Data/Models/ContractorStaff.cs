using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace Stb.Data.Models
{
    // 承包商员工
    public class ContractorStaff
    {
        public int Id { get; set; }

        [Required(ErrorMessage = "{0}不能为空")]
        [StringLength(64, MinimumLength = 1, ErrorMessage = "{0}长度为{2}到{1}个字符")]
        [Display(Name = "姓名")]
        public string Name { get; set; }

        [Required(ErrorMessage = "{0}不能为空")]
        [Display(Name = "手机")]
        [RegularExpression(@"^1[3|4|5|7|8]\d{9}$", ErrorMessage = "请输入正确的手机号码")]
        [DataType(DataType.PhoneNumber)]
        public string Phone { get; set; }

        public int ContractorId { get; set; }

        public Contractor Contractor { get; set; }

        public List<Project> Projects { get; set; }
    }
}
