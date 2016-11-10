using Microsoft.AspNetCore.Mvc.Rendering;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Stb.Data.Models.AccountViewModels
{
    public class AccountEditViewModel
    {
        public AccountViewModel User { get; set; }
        public SelectList Roles { get; set; }
    }
}
