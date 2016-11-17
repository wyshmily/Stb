using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace Stb.Data.Models
{
    // 承包商
    public class Contractor
    {
        public int Id { get; set; }

        [Required(ErrorMessage = "{0}不能为空")]
        [StringLength(64, MinimumLength = 1, ErrorMessage = "{0}长度为{2}到{1}个字符")]
        [Display(Name = "名称")]
        public string Name { get; set; }

        //[Required(ErrorMessage = "{0}不能为空")]
        //[StringLength(32, MinimumLength = 1, ErrorMessage = "{0}长度为{2}到{1}个字符")]
        //[Display(Name = "联系电话")]
        //[DataType(DataType.PhoneNumber)]
        //public string Phone { get; set; }

        [StringLength(256, MinimumLength = 1, ErrorMessage = "{0}长度为{2}到{1}个字符")]
        [Display(Name = "地址")]
        public string Address { get; set; }

        [Display(Name = "是否启用")]
        public bool Enabled { get; set; }

        [Display(Name = "负责人")]
        public int? HeadStaffId { get; set; }

        public List<ContractorStaff> Staffs { get; set; }

        public List<Order> Orders { get; set; }
    }
}
