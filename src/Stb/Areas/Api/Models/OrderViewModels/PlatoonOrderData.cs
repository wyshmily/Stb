using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Stb.Data.Models;

namespace Stb.Api.Models.OrderViewModels
{
    public class PlatoonOrderData
    {
        public PlatoonOrderData(Order order)
        {
            OrderId = order.Id;
            Contactor = order.ContractorStaff?.Name;
            Phone = order.ContractorStaff?.Phone;
            State = order.State;
        }

        /// <summary>
        /// 工单编号
        /// </summary>
        public string OrderId { get; set; }

        /// <summary>
        /// 联系人
        /// </summary>
        public string Contactor { get; set; }

        /// <summary>
        /// 联系电话
        /// </summary>
        public string Phone { get; set; }

        /// <summary>
        /// 工单状态 0-准备状态；1-施工状态；2-完成
        /// </summary>
        public int State { get; set; } 
    }
}
