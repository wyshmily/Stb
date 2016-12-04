using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace Stb.Data.Models
{
    // 推送消息
    public class Message
    {
        public int Id { get; set; }

        [Required]
        public string EndUserId { get; set; }   // 用户Id

        public string RootUserName { get; set; } // 产生消息的用户姓名

        [Required]
        public string OrderId { get; set; } // 工单Id

        public int Type { get; set; }   // 消息类型：1-平台下单（排长端）；2-排长下单（班长端）；3-班长签到（排长端）；4-施工问题（排长端）

        public DateTime Time { get; set; } // 消息时间

        public string Title { get; set; }   // 消息标题

        public string Text { get; set; }    // 消息内容

        public bool IsRead { get; set; } // 是否已读

        public bool InOut { get; set; } // 签到还是签退

        public EndUser EndUser { get; set; }   

        public Order Order { get; set; }
    }
}
