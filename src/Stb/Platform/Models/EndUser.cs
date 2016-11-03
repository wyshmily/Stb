using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;

namespace Stb.Platform.Models
{
    // 终端用户：包括排长、班长和工人
    public class EndUser : ApplicationUser
    {
        public string IdCardNumber { get; set; }    // 身份证号

        public string Gender { get; set; }   // 性别

        public DateTime Birthday { get; set; }  // 生日

        public string NativePlace { get; set; } // 籍贯

        public string QQ { get; set; }    // QQ号

        public string Wechat { get; set; }  // 微信号

        public string Alipay { get; set; }  // 支付宝

        public string HealthStatus { get; set; }    // 健康状况

        public bool Enabled { get; set; } // 是否启用

        public List<EndUserDistrict> EndUserDistricts { get; set; }

        public List<EndUserJobClass> EndUserJobClasses { get; set; }
    }
}
