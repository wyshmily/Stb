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
            District = order.District?.FullName;
            Address = order.WorkAddress;
            Location = order.WorkLocation;
            Description = order.Description;
            ExpectedStartTime = order.ExpectedStartTime?.ToString("yyyy年M月d日");
            ExpectedDays = order.ExpectedDays;
            PlatoonName = order.Platoon?.Name;
            PlatoonPhone = order.Platoon?.UserName;

            Workers = order.OrderWorkers?.Where(ow => ow.WorkerId != order.LeadWorkerId).Select(ow => new WorkerData(ow.Worker)).ToList();
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
        /// 施工区域
        /// </summary>
        public string District { get; set; }
        
        /// <summary>
        /// 施工地址
        /// </summary>
        public string Address { get; set; }
      
        /// <summary>
        /// 施工地点坐标
        /// </summary>
        public string Location { get; set; }

        /// <summary>
        /// 施工描述
        /// </summary>
        public string Description { get; set; }

        /// <summary>
        /// 项目预计开始时间
        /// </summary>
        public string ExpectedStartTime { get; set; }

        /// <summary>
        /// 预计施工天数
        /// </summary>
        public int? ExpectedDays { get; set; }

        /// <summary>
        /// 工单排长姓名
        /// </summary>
        public string PlatoonName { get; set; }

        /// <summary>
        /// 工单班长手机
        /// </summary>
        public string PlatoonPhone { get; set; }

        /// <summary>
        /// 是否已接受
        /// </summary>
        public bool Accepted { get; set; }

        /// <summary>
        /// 工单工人列表
        /// </summary>
        public List<WorkerData> Workers { get; set; }
    }
}
