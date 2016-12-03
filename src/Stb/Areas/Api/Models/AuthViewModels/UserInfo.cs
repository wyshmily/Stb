using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Stb.Api.Models.AuthViewModels
{
    public class UserInfo
    {
        /// <summary>
        /// 用户Id string类型（uuid）
        /// </summary>
        public string UserId { get; set; }

        /// <summary>
        /// 用户账号（手机号）
        /// </summary>
        public string Account { get; set; }

        /// <summary>
        /// 用户姓名
        /// </summary>
        public string Name { get; set; }

        /// <summary>
        /// 用户头像url
        /// </summary>
        public string Portrait { get; set; }
    }
}
