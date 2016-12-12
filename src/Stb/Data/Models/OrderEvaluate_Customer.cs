using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace Stb.Data.Models
{
    public class OrderEvaluate_Customer : OrderEvaluate
    {
        [Display(Name = "班长是否留下平台保修卡")]
        public byte LeaderLeaveCard { get; set; }  // 0-已留下;1-没留下

        // 客户对班长协调能力的评价
        [Display(Name = "班长在施工前主动和客户沟通情况")]
        public byte LeaderCanCooperate { get; set; }  // 0-满意;1-不满意

        [Display(Name = "班长在施工中主动和客户沟通情况")]
        public byte LeaderDoReport { get; set; }  // 0-满意;1-不满意

        // 客户对排长协调能力的评价
        [Display(Name = "施工中是否存在缺工、少料、与第三方冲突等协调失当的情况")]
        public byte HaveCase{ get; set; }  // 0-存在;1-不存在
    }
}
