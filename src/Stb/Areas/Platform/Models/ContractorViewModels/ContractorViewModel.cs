using Stb.Data.Models;
using Stb.Platform.Models.ContractorUserViewModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Stb.Platform.Models.ContractorViewModels
{
    public class ContractorViewModel
    {
        public Contractor Contractor {get;set;}
        public ContractorUserViewModel HeadUser { get; set;}
    }
}
