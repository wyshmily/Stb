using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace Stb.Data.Models
{
    // 上报问题
    public class Issue
    {
        public int Id { get; set; }

        [Required]
        public string EndUserId { get; set; }   // 用户Id

        [Required]
        public string OrderId { get; set; } // 工单Id

        public int IssueType { get; set; }   // 问题类型：1-设计问题；2-业主要求；3-现场环境不具备施工条件；4-不可抗力

        public int SolutionType { get; set; }   // 解决方法类型：1-重新施工；2-推迟施工；3-修改设计；4-更换设备型号

        public string Pics { get; set; }    // 图片链接，逗号分隔

        public string Audios { get; set; } // 录音链接，逗号分隔

        public EndUser EndUser { get; set; }   

        public Order Order { get; set; }
    }
}
