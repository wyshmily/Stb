using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Stb.Api.Models.OrderViewModels
{
    public class WorkerSignmentData
    {
        /// <summary>
        /// 签到状态：0-无签到信息；1-已签到；2-已签退
        /// </summary>
        public int State { get; set; }

        /// <summary>
        /// 签到数据
        /// </summary>
        public SignmentData SignIn { get; set; } 

        /// <summary>
        /// 签退数据
        /// </summary>
        public SignmentData SignOut { get; set; }
    }
}
