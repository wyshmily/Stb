using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Stb.Data.Models.ContractorViewModels
{
    public class ContractorViewModel
    {
        public Contractor Contractor {get;set;}
        public ContractorStaff HeadStaff { get; set;}
    }
}
