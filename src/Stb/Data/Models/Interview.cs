using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace Stb.Data.Models
{
    public class Interview
    {
        public int Id { get; set; }

        public string WorkerId { get; set; }

        [Display(Name = "沟通时间")]
        [DataType(DataType.Date)]
        public DateTime? RemoteInterviewDate { get; set; }

        [Display(Name = "沟通人")]
        public string RemoteInterviewPlatoonName { get; set; }

        [Display(Name = "了解、核实人员基本信息")]
        public bool RemoteInsight { get; set; }

        [Display(Name = "介绍公司情况")]
        public bool RemoteCompanyIntroduce { get; set; }

        [Display(Name = "介绍工作要求、待遇")]
        public bool RemoteRequirmentIntroduce { get; set; }

        [Display(Name = "介绍基本合作形式")]
        public bool RemoteCooperationIntroduce { get; set; }

        [Display(Name = "其它洽谈内容")]
        public string RemoteInterviewRecord { get; set; }

        [Display(Name = "意见记录")]
        public string RemoteOpinion { get; set; }

        [Display(Name = "沟通结论")]
        public bool? RemoteInterviewResult { get; set; }    // null-尚无结论；true-合格；false-不合格

        [Display(Name = "面试时间")]
        [DataType(DataType.Date)]
        public DateTime? FaceInterviewDate { get; set; }

        [Display(Name = "面试人")]
        public string FaceInterviewPlatoonName { get; set; }

        [Display(Name = "面试记录")]
        public string FaceInterviewRecord { get; set; }

        [Display(Name = "面试结论")]
        public bool? FaceInterviewResult { get; set; }    // null-尚无结论；true-合格；false-不合格

        [Display(Name = "排长")]
        public string PlatoonId { get; set; }

        [Display(Name = "排长结论")]
        public byte? TotalInterviewResult { get; set; }    // null-尚无结论；0-合格；1-不合格；2-其它

        [Display(Name = "其它结论")]
        public string OtherResult { get; set; }

        public Worker Worker { get; set; }

        public Platoon Platoon { get; set; }
    }
}
