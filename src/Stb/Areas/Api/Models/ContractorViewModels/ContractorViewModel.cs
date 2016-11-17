using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Stb.Areas.Api.Models.ContractorViewModels
{
    public class ContractorViewModel
    {
        public int Id { get; set; }

        public string Name { get; set; }

        public int? HeadStaffId { get; set; }

        public string HeadStaffName { get; set; }

        public string HeadStaffPhone { get; set; }
    }
}
