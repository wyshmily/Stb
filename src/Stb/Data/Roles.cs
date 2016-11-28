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
        public const string Platoon = "排长";
        public const string Worker = "工人";

        public const string PlatformUser = "系统管理员,运营客服";
        public const string PlatoonAndPlatformUser = "排长,系统管理员,运营客服";
    }
}
