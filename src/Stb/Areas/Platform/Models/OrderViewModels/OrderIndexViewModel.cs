using Stb.Data.Models;
using Stb.Platform.Models.DistrictViewModels;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace Stb.Platform.Models.OrderViewModels
{
    public class OrderIndexViewModel
    {
        [Display(Name = "工单号")]
        public string Id { get; set; }

        public int? ContractorId { get; set; }  // 承包商Id

        [Display(Name = "承包商")]
        public string ContractorName { get; set; }  // 承包商名称

        public int? ContractorStaffId { get; set; }

        [Display(Name = "联系人")]
        public string ContractorStaffName { get; set; } // 联系人姓名

        [Display(Name = "联系电话")]
        public string ContractorStaffPhone { get; set; }    // 联系电话

        public string PlatoonId { get; set; }   // 排长ID

        [Display(Name = "排长")]
        public string PlatoonName { get; set; } // 排长姓名

        [Display(Name = "施工状态")]
        public byte State { get; set; } // 工单状态

        //[Display(Name = "工程描述")]
        //public string Description { get; set; }  // 工程描述

        //[Display(Name = "施工地址")]
        //public string WorkAddress { get; set; } // 施工地址

        //[DataType(DataType.Date)]
        //[DisplayFormat(DataFormatString = "{0:yyyy-M-d}")]
        //[Display(Name = "最晚联系时间")]
        //public DateTime? ContactDeadline { get; set; }

        //public DistrictViewModel District { get; set; } // 施工所在区域

        public OrderIndexViewModel()
        {
        }

        public OrderIndexViewModel(Order order)
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
            //Description = order.Description;
            //WorkAddress = order.WorkAddress;
            //ContactDeadline = order.ContactDeadline;
            //if (order.District != null)
            //    District = new DistrictViewModel(order.District);
        }
    }


}
