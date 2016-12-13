using Stb.Data.Models;
using Stb.Util;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Stb.Api.Models.OrderViewModels
{
    public class IssueData
    {
        /// <summary>
        /// 记录问题Id
        /// </summary>
        public int Id { get; set; }

        /// <summary>
        /// 记录工人Id
        /// </summary>
        public string WorkerId { get; set; }

        /// <summary>
        /// 记录工人姓名
        /// </summary>
        public string WorkerName { get; set; }

        /// <summary>
        /// 工单Id
        /// </summary>
        public string OrderId { get; set; }

        /// <summary>
        /// 记录时间
        /// </summary>
        public long Time { get; set; }

        /// <summary>
        /// 记录时间，web用
        /// </summary>
        public DateTime DateTime { get; set; }

        /// <summary>
        /// 问题类型：1-设计问题；2-业主要求；3-现场环境不具备施工条件；4-不可抗力
        /// </summary>
        public int IssueType { get; set; }

        /// <summary>
        /// 解决方法类型：1-重新施工；2-推迟施工；3-修改设计；4-更换设备型号
        /// </summary>
        public int SolutionType { get; set; }

        /// <summary>
        /// 照片列表，逗号分隔
        /// </summary>
        public string Pics { get; set; }

        /// <summary>
        /// 音频列表，逗号分隔
        /// </summary>
        public string Audios { get; set; }

        public IssueData(Issue issue)
        {
            Id = issue.Id;
            WorkerId = issue.EndUserId;
            WorkerName = issue.EndUser?.Name;
            OrderId = issue.OrderId;
            Time = issue.Time.ToUnixSeconds();
            DateTime = issue.Time;
            IssueType = issue.IssueType;
            SolutionType = issue.SolutionType;
            Pics = issue.Pics;
            Audios = issue.Audios;
        }
    }
}
