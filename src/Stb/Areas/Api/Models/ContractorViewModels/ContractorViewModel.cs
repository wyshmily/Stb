using Stb.Data.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Stb.Api.Models.ContractorViewModels
{
    public class ContractorViewModel
    {
        public int Id { get; set; }

        public string Name { get; set; }

        public string HeadStaffId { get; set; }

        public string HeadStaffName { get; set; }

        public string HeadStaffPhone { get; set; }

        public ContractorViewModel(Contractor contractor, ContractorUser header)
        {
            Id = contractor.Id;
            Name = contractor.Name;
            HeadStaffId = header?.Id;
            HeadStaffName = header?.Name;
            HeadStaffPhone = header?.UserName;
        }
    }
}
