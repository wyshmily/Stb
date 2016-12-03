using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Stb.Data.Models;

namespace Stb.Api.Models.MessageViewModels
{
    public class MessageData
    {
        /// <summary>
        /// 消息Id
        /// </summary>
        public int Id { get; set; }

        /// <summary>
        /// 工单Id
        /// </summary>
        public string OrderId { get; set; }

        /// <summary>
        /// 消息类型：1-平台下单（排长端）；2-排长下单（班长端）；3-班长签到（排长端）；4-施工问题（排长端）
        /// </summary>
        public int Type { get; set; }

        /// <summary>
        /// 消息标题
        /// </summary>
        public string Title { get; set; }

        /// <summary>
        /// 消息内容
        /// </summary>
        public string Text { get; set; }

        /// <summary>
        /// 是否已读：true-已读；false-未读
        /// </summary>
        public bool IsRead { get; set; }

        public MessageData(Message message)
        {
            Id = message.Id;
            OrderId = message.OrderId;
            Type = message.Type;
            Title = message.Title;
            Text = message.Text;
            IsRead = message.IsRead;
        }
    }
}
