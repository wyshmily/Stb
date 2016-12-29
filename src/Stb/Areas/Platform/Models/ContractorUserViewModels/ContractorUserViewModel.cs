using Stb.Data.Models;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace Stb.Platform.Models.ContractorUserViewModels
{
    public class ContractorUserViewModel
    {
        public string Id { get; set; }

        [Display(Name ="姓名")]
        public string Name { get; set; }

        [Display(Name ="手机")]
        public string UserName { get; set; }

        [Display(Name ="登录密码")]
        public string Password { get; set; }

        public int ContractorId { get; set; }

        public ContractorUserViewModel() { }

        public ContractorUserViewModel(ContractorUser user)
        {
            Id = user.Id;
            Name = user.Name;
            UserName = user.UserName;
            ContractorId = user.ContractorId;
            Password = "Hidden";
        }

        public ContractorUser ToContractorUser()
        {
            ContractorUser appUser = new ContractorUser
            {
                UserName = UserName,
                Name = Name,
                ContractorId = ContractorId,

                //Email = UserName
            };

            if (Id != null)
                appUser.Id = Id;
            

            return appUser;
        }

        public void Update(ref ContractorUser appUser)
        {
            appUser.UserName = UserName;
            appUser.Name = Name;
        }
    }
}
