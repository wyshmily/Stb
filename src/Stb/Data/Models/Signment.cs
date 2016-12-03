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
        public string EndUserId { get; set; }

        public DateTime Time { get; set; } // 签到时间

        public Order Order { get; set; }

        public EndUser EndUser { get; set; }

        public bool InOut { get; set; } // 签到还是签退：true-签到，false-签退

        public string Pics { get; set; } // 签到图片，逗号分隔

        public string Location { get; set; } // 签到地点坐标

        public string Address { get; set; } // 签到地点地址

        public int Type { get; set; }   // 签到类型：1-排长签到；2-工人签到
    }
}
