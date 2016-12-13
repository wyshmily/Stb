using Stb.Api.Models.OrderViewModels;
using Stb.Data.Models;
using Stb.Platform.Models.DistrictViewModels;
using Stb.Platform.Models.EvaluateViewModels;
using Stb.Platform.Models.WorkerViewModels;
using Stb.Platform.Models.WorkLoadViewModels;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace Stb.Platform.Models.OrderViewModels
{
    public class OrderViewModel
    {
        public string Id { get; set; }

        public int? ContractorId { get; set; }  // 承包商Id

        [Display(Name = "承包商")]
        [Required(ErrorMessage = "{0}不能为空")]
        [StringLength(64, MinimumLength = 1, ErrorMessage = "{0}长度为{2}到{1}个字符")]
        public string ContractorName { get; set; }  // 承包商名称

        public int? ContractorStaffId { get; set; }

        [Display(Name = "联系人")]
        [Required(ErrorMessage = "{0}不能为空")]
        [StringLength(64, MinimumLength = 1, ErrorMessage = "{0}长度为{2}到{1}个字符")]
        public string ContractorStaffName { get; set; } // 联系人姓名

        [Display(Name = "联系电话")]
        [RegularExpression(@"^1[3|4|5|7|8]\d{9}$", ErrorMessage = "请输入正确的手机号码")]
        [Required(ErrorMessage = "{0}不能为空")]
        public string ContractorStaffPhone { get; set; }    // 联系电话

        [Display(Name = "排长")]
        [Required(ErrorMessage = "请为项目选择排长")]
        public string PlatoonId { get; set; }   // 排长ID

        [Display(Name = "排长")]
        //[Required(ErrorMessage = "请为项目选择排长")]
        public string PlatoonName { get; set; } // 排长姓名

        [Display(Name = "施工状态")]
        public byte State { get; set; } // 工单状态

        [Display(Name = "工程描述")]
        [DataType(DataType.MultilineText)]
        [StringLength(1000, MinimumLength = 1, ErrorMessage = "{0}长度为{2}到{1}个字符")]
        [Required(ErrorMessage = "{0}不能为空")]
        public string Description { get; set; }  // 工程描述

        [Display(Name = "施工地址")]
        [DataType(DataType.MultilineText)]
        [StringLength(1000, MinimumLength = 1, ErrorMessage = "{0}长度为{2}到{1}个字符")]
        [Required(ErrorMessage = "{0}不能为空")]
        public string WorkAddress { get; set; } // 详细地址

        [Display(Name = "最晚联系时间")]
        [DataType(DataType.Date)]
        [DisplayFormat(DataFormatString = "{0:yyyy-M-d}")]
        public DateTime? ContactDeadline { get; set; }

        [Display(Name = "需要人数")]
        public int? WorkerNeeded { get; set; }   // 需要人数

        [Display(Name = "预计开始时间")]
        [DataType(DataType.Date)]
        [DisplayFormat(DataFormatString = "{0:yyyy-M-d}")]
        public DateTime? ExpectedStartTime { get; set; } // 预计开始时间

        [Display(Name = "预计工期天数")]
        public int? ExpectedDays { get; set; }   // 预计天数

        [Display(Name = "坐标")]
        public string WorkLocation { get; set; }    // 施工地点坐标

        [Display(Name ="项目")]
        public int? ProjectId { get; set; }  // 项目ID

        [Display(Name = "项目")]
        public string ProjectName { get; set; }  // 项目名称

        public DistrictViewModel District { get; set; } // 施工所在地区

        [Display(Name = "工作量")]
        public List<WorkLoadViewModel> WorkLoads { get; set; }

        public ProgressData ProgressData { get; set; }

        [Display(Name = "工人")]
        public List<WorkerSimpleViewModel> Workers { get; set; }

        public List<EvaluateSimpleViewModel> Evaluates { get; set; }

        public bool IsWokerWorkLoadSet { get; set; }

        public string LeadWorkerId { get; set; }


        public OrderViewModel()
        {
        }

        public OrderViewModel(Order order, ProgressData progressData = null)
        {
            Id = order.Id;
            ContractorId = order.ContractorId;
            ContractorName = order.Contractor?.Name;
            ContractorStaffId = order.ContractorStaffId;
            ContractorStaffName = order.ContractorStaff?.Name;
            ContractorStaffPhone = order.ContractorStaff?.Phone;
            PlatoonId = order.PlatoonId;
            PlatoonName = order.Platoon?.Name;
            State = order.State;
            Description = order.Description;
            WorkAddress = order.WorkAddress;
            ContactDeadline = order.ContactDeadline;
            WorkerNeeded = order.WorkerNeeded;
            ExpectedStartTime = order.ExpectedStartTime;
            ExpectedDays = order.ExpectedDays;
            WorkLocation = order.WorkLocation;
            ProjectId = order.ProjectId;
            ProjectName = order.Project?.Name;
            LeadWorkerId = order.LeadWorkerId;
            IsWokerWorkLoadSet = order.IsWorkerWorkLoadSet;
            if (order.District != null)
                District = new DistrictViewModel(order.District);
            if (order.WorkLoads != null)
                WorkLoads = order.WorkLoads.Select(w => new WorkLoadViewModel(w)).ToList();
            if (order.OrderWorkers != null)
                Workers = order.OrderWorkers.Select(ow => new WorkerSimpleViewModel(ow.Worker)).ToList();
            if (order.Evaluates != null)
                Evaluates = order.Evaluates.Select(e => new EvaluateSimpleViewModel(e)).ToList();

            ProgressData = progressData;
        }

        public Order ToOrder()
        {
            return new Order
            {
                Id = Id,
                ContractorId = ContractorId,
                ContractorStaffId = ContractorStaffId,
                PlatoonId = PlatoonId,
                State = State,
                Description = Description,
                WorkAddress = WorkAddress,
                ContactDeadline = ContactDeadline,
                WorkerNeeded = WorkerNeeded,
                ExpectedStartTime = ExpectedStartTime,
                ExpectedDays = ExpectedDays,
                WorkLocation = WorkLocation,
                ProjectId = ProjectId,
                DistrictId = District?.DistrictAdcode,
            };
        }
    }


}
