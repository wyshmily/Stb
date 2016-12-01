using Stb.Data.Models;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace Stb.Platform.Models.PlatoonWorkerViewModels
{
    public class RemoteInterviewViewModel
    {
        public int Id { get; set; }

        public string WorkerId { get; set; }

        [Display(Name = "沟通时间")]
        [DataType(DataType.Date)]
        [Required(ErrorMessage = "请填写{0}")]
        public DateTime? RemoteInterviewDate { get; set; }

        [Display(Name = "沟通人")]
        [Required(ErrorMessage = "请填写{0}")]
        public string RemoteInterviewPlatoonName { get; set; }

        public bool NeedRemoteInsight { get; set; } = true;
        public bool NeedRemoteCompanyIntroduce { get; set; } = true;
        public bool NeedRemoteRequirmentIntroduce { get; set; } = true;
        public bool NeedRemoteCooperationIntroduce { get; set; } = true;

        [Display(Name = "了解、核实人员基本信息")]
        [Compare("NeedRemoteInsight", ErrorMessage = "是否{0}？")]
        public bool RemoteInsight { get; set; }

        [Display(Name = "介绍公司情况")]
        [Compare("NeedRemoteCompanyIntroduce", ErrorMessage = "是否{0}？")]
        public bool RemoteCompanyIntroduce { get; set; }

        [Display(Name = "介绍工作要求、待遇")]
        [Compare("NeedRemoteRequirmentIntroduce", ErrorMessage = "是否{0}？")]
        public bool RemoteRequirmentIntroduce { get; set; }

        [Display(Name = "介绍基本合作形式")]
        [Compare("NeedRemoteCooperationIntroduce", ErrorMessage = "是否{0}？")]
        public bool RemoteCooperationIntroduce { get; set; }

        [Display(Name = "其它洽谈内容")]
        public string RemoteInterviewRecord { get; set; }

        [Display(Name = "意见记录")]
        public string RemoteOpinion { get; set; }

        [Display(Name = "沟通结论")]
        [Required(ErrorMessage = "请填写{0}")]
        public bool? RemoteInterviewResult { get; set; }

        public RemoteInterviewViewModel() { }

        public RemoteInterviewViewModel(Interview interview)
        {
            if(interview != null)
            {
                Id = interview.Id;
                WorkerId = interview.WorkerId;
                RemoteInterviewDate = interview.RemoteInterviewDate;
                RemoteInterviewPlatoonName = interview.RemoteInterviewPlatoonName;
                RemoteInsight = interview.RemoteInsight;
                RemoteCompanyIntroduce = interview.RemoteCompanyIntroduce;
                RemoteRequirmentIntroduce = interview.RemoteRequirmentIntroduce;
                RemoteCooperationIntroduce = interview.RemoteCooperationIntroduce;
                RemoteInterviewRecord = interview.RemoteInterviewRecord;
                RemoteOpinion = interview.RemoteOpinion;
                RemoteInterviewResult = interview.RemoteInterviewResult;
            }
        }

        public Interview ToInterview()
        {
            return new Interview
            {
                WorkerId = WorkerId,
                RemoteInterviewDate = RemoteInterviewDate,
                RemoteInterviewPlatoonName = RemoteInterviewPlatoonName,
                RemoteInsight = RemoteInsight,
                RemoteCompanyIntroduce = RemoteCompanyIntroduce,
                RemoteRequirmentIntroduce = RemoteRequirmentIntroduce,
                RemoteCooperationIntroduce = RemoteCooperationIntroduce,
                RemoteInterviewRecord = RemoteInterviewRecord,
                RemoteOpinion = RemoteOpinion,
                RemoteInterviewResult = RemoteInterviewResult,
            };
        }
    }
}
