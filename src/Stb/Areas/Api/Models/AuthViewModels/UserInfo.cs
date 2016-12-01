using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Stb.Api.Models.AuthViewModels
{
    public class UserInfo
    {
        public string uid { get; set; } // 用户id

        public string account { get; set; } // 用户账号

        public string name { get; set; }    // 用户姓名

        public string portrait { get; set; }    // 用户头像
    }
}
