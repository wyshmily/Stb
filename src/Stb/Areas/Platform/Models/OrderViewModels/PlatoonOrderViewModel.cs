using Stb.Data.Models;
using Stb.Platform.Models.DistrictViewModels;
using Stb.Platform.Models.WorkerViewModels;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace Stb.Platform.Models.OrderViewModels
{
    public class PlatoonOrderViewModel
    {
        public string Id { get; set; }

        public int? ContractorId { get; set; }  // 承包商Id

        [Display(Name = "承包商")]
        public string ContractorName { get; set; }  // 承包商名称

        public int? ContractorStaffId { get; set; }

        [Display(Name = "联系人")]
        public string ContractorStaffName { get; set; } // 联系人姓名

        public string ContractorStaffPhone { get; set; }    // 联系电话

        public byte State { get; set; } // 工单状态

        [Display(Name = "施工描述")]
        public string Description { get; set; }  // 工程描述

        public string WorkAddress { get; set; } // 详细地址

        [Display(Name = "需要人数")]
        public int? WorkerNeeded { get; set; }   // 需要人数

        public string WorkLocation { get; set; }    // 施工地点坐标

        public DistrictViewModel District { get; set; } // 施工所在地区

        public string LeaderWorkerId { get; set; } // 班长ID

        public WorkerSimpleViewModel LeaderWorker { get; set; } // 班长

        public List<WorkerSimpleViewModel> Workers { get; set; } = new List<WorkerSimpleViewModel>(); // 工人列表


        public PlatoonOrderViewModel()
        {
        }

        public PlatoonOrderViewModel(Order order)
        {
            Id = order.Id;
            ContractorId = order.ContractorId;
            ContractorName = order.Contractor?.Name;
            ContractorStaffId = order.ContractorStaffId;
            ContractorStaffName = order.ContractorStaff?.Name;
            ContractorStaffPhone = order.ContractorStaff?.Phone;
            State = order.State;
            Description = order.Description;
            WorkAddress = order.WorkAddress;
            WorkerNeeded = order.WorkerNeeded;
            WorkLocation = order.WorkLocation;
            if (order.District != null)
                District = new DistrictViewModel(order.District);

            LeaderWorkerId = order.LeadWorkerId;
            if (order.LeadWorker != null)
                LeaderWorker = new WorkerSimpleViewModel(order.LeadWorker);
            if (order.OrderWorkers != null)
                Workers = order.OrderWorkers.Select(w => new WorkerSimpleViewModel(w.Worker)).ToList();
        }

        
    }


}
