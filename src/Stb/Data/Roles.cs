using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Stb.Data
{
    public class Roles
    {
        public const string Administrator = "系统管理员";
        public const string CustomerService = "运营客服";
        public const string QualityControl = "质控员";
        public const string Platoon = "排长";
        public const string Worker = "工人";
        public const string Contractor = "承包商员工";

        public const string PlatformUser = "系统管理员,运营客服,质控员";
        public const string AdminAndCustomerService = "系统管理员,运营客服";
    }
}
