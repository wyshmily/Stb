using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Stb.Api.Models.OrderViewModels
{
    public class ProgressData
    {
        /// <summary>
        /// 工单工人签到和签退记录总数
        /// </summary>
        public int SignmentCount { get; set; }

        /// <summary>
        /// 最新工人签到或签退记录
        /// </summary>
        public SignmentData Signment { get; set; }

        /// <summary>
        /// 工单工人记录问题总数
        /// </summary>
        public int IssueCount { get; set; }

        /// <summary>
        /// 最新工人记录问题
        /// </summary>
        public IssueData Issue { get; set; }
    }
}
