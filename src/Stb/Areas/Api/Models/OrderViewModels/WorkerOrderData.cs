using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Stb.Data.Models;

namespace Stb.Api.Models.OrderViewModels
{
    public class WorkerOrderData
    {
        public WorkerOrderData(Order order)
        {
            OrderId = order.Id;
            Contactor = order.ContractorStaff?.Name;
            Phone = order.ContractorStaff?.Phone;
            State = order.State;
            Accepted = order.LeadWorkerId == order.AcceptWorkerId;
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

        /// <summary>
        /// 是否已接受
        /// </summary>
        public bool Accepted { get; set; }
    }
}
