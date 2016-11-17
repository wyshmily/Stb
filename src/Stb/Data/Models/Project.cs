using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace Stb.Data.Models
{
    public class Project
    {
        public int Id { get; set; }

        [Display(Name="项目名称")]
        [StringLength(64, ErrorMessage ="{0}的长度为不超过{1}个字符")]
        public string Name { get; set; }    // 项目名称

        [Display(Name="客户名称")]
        [StringLength(64, ErrorMessage ="{0}的长度为不超过{1}个字符")]
        public string Client { get; set; }  // 客户名称


        [Display(Name="合同编号")]
        [StringLength(64, ErrorMessage ="{0}的长度为不超过{1}个字符")]
        public string ContractNo { get; set; }  // 合同编号

        [Display(Name="合同名称")]
        [StringLength(64, ErrorMessage ="{0}的长度为不超过{1}个字符")]
        public string Contract { get; set; }    // 合同名称

        public string ContractUrl { get; set; } // 合同url

        [Display(Name="合同金额")]
        [Range(0, 1000000000)]
        public decimal? ContractAmount { get; set; } // 合同金额

        [Display(Name="项目描述")]
        public string Description { get; set; } // 项目描述

        [Display(Name="项目工单")]
        public List<Order> Orders { get; set; }
    }
}
