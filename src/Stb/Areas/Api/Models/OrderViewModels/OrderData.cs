using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Stb.Data.Models;

namespace Stb.Api.Models.OrderViewModels
{
    public class OrderData
    {
        public OrderData(Order order)
        {
            orderId = order.Id;
            contactor = order.ContractorStaff?.Name;
            phone = order.ContractorStaff?.Phone;
            state = order.State;
        }

        public string orderId { get; set; } // 工单编号

        public string contactor { get; set; } // 联系人

        public string phone { get; set; }   // 联系电话

        public int state { get; set; } // 工单状态 0-准备状态；1-施工状态；2-完成
    }
}
