using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using System.ComponentModel.DataAnnotations;

namespace Stb.Data.Models
{
    // 终端用户：包括排长、班长和工人
    public class EndUser : ApplicationUser
    {
        public bool Gender { get; set; } = true;  // 性别：true-男；false-女

        public string IdCardNumber { get; set; }    // 身份证号

        public DateTime? Birthday { get; set; }  // 生日

        public string NativePlace { get; set; } // 籍贯

        public string QQ { get; set; }    // QQ号

        public string Wechat { get; set; }  // 微信号

        public string Alipay { get; set; }  // 支付宝

        public string HealthStatus { get; set; }    // 健康状况

        public bool Enabled { get; set; } = true;// 是否启用

        public List<EndUserDistrict> EndUserDistricts { get; set; }

        public List<EndUserJobClass> EndUserJobClasses { get; set; }
    }
}
