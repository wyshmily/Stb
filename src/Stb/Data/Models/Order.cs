using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Stb.Data.Models
{
    // 工单
    public class Order
    {
        public string Id { get; set; }  // 工单号

        public int? PlatformUserId { get; set; }    // 下单人Id

        public int? ContractorId { get; set; }  // 承包商ID

        public int? ContractorStaffId { get; set; }   // 承包商联系人ID

        public int? DistrictId { get; set; } // 施工地点所在区县

        public string PlatoonId { get; set; } // 排长ID

        public DateTime? ContactDeadline { get; set; }  // 最晚联系客户时间

        public Byte OrderType { get; set; }   // 项目类型：1-包清工;2-项目型

        public int? WorkerNeeded { get; set; }   // 需要人数

        public int? ExpectedDays { get; set; }   // 预计天数

        public DateTime? ExpectedStartTime { get; set; } // 预计开始时间

        public string WorkLocation { get; set; }    // 施工地点坐标

        public string WorkAddress { get; set; } // 施工地址

        public string Description { get; set; } // 施工描述

        public string LeadWorkerId { get; set; }  // 选定的班长ID

        public string AcceptWorkerId { get; set; }  // 接受工单的班长ID

        public byte State { get; set; } // 施工状态

        public int? ProjectId { get; set; } // 项目Id

        public Contractor Contractor { get; set; }

        public ContractorStaff ContractorStaff { get; set; }

        public District District { get; set; }

        public Platoon Platoon { get; set; }

        public Worker LeadWorker { get; set; }

        public List<OrderJobClass> OrderJobClasses { get; set; }

        public List<OrderWorker> OrderWorkers { get; set; }

        public List<Signment> Signments { get; set; }

        public Project Project { get; set; }
    }
}
