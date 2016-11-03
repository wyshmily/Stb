using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Stb.Platform.Models
{
    // 工单
    public class Project
    {
        public string Id { get; set; }  // 工单号

        public int? ContractorId { get; set; }  // 承包商ID

        public int? ContactorStaffId { get; set; }   // 承包商联系人ID

        public int? DistrictId { get; set; } // 施工地点所在区县

        public int? PlatoonId { get; set; } // 排长ID

        public DateTime? ContactDeadline { get; set; }  // 最晚联系客户时间

        public Byte ProjectType { get; set; }   // 项目类型：1-包清工;2-项目型

        public int WorkerNeeded { get; set; }   // 需要人数

        public int ExpectedDays { get; set; }   // 预计天数

        public DateTime ExpectedStartTime { get; set; } // 预计开始时间

        public string WorkLocation { get; set; }    // 施工地点坐标

        public string WorkAddress { get; set; } // 施工地址

        public string Description { get; set; } // 施工描述

        public int? LeadWorkerId { get; set; }  // 选定的班长ID

        public Contractor Contractor { get; set; }

        public ContractorStaff ContractorStaff { get; set; }

        public District District { get; set; }

        public Platoon Platoon { get; set; }

        public Worker LeadWorker { get; set; }

        public List<ProjectJobClass> ProjectJobClasses { get; set; }

        public List<ProjectWorker> ProjectWorkers { get; set; }

        public List<Signment> Signments { get; set; }
    }
}
