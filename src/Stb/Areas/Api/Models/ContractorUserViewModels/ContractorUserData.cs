using Stb.Data.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Stb.Api.Models.ContractorUserViewModels
{
    public class ContractorUserData
    {
        public string Id { get; set; }

        public string Name { get; set; }

        public string Phone { get; set; }


        public ContractorUserData() { }

        public ContractorUserData(ContractorUser user)
        {
            Id = user.Id;
            Name = user.Name;
            Phone = user.UserName;
        }
    }
}
