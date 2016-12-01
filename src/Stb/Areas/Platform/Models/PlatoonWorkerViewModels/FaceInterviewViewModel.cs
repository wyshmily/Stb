using Stb.Data.Models;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace Stb.Platform.Models.PlatoonWorkerViewModels
{
    public class FaceInterviewViewModel
    {
        public int Id { get; set; }

        public string WorkerId { get; set; }

        [Display(Name = "沟通时间")]
        [DataType(DataType.Date)]
        [Required(ErrorMessage = "请填写{0}")]
        public DateTime? FaceInterviewDate { get; set; }

        [Display(Name = "沟通人")]
        [Required(ErrorMessage = "请填写{0}")]
        public string FaceInterviewPlatoonName { get; set; }

        [Display(Name = "面试记录")]
        public string FaceInterviewRecord { get; set; }

        [Display(Name = "面试结论")]
        [Required(ErrorMessage = "请填写{0}")]
        public bool? FaceInterviewResult { get; set; }

        public FaceInterviewViewModel() { }

        public FaceInterviewViewModel(Interview interview)
        {
            if(interview != null)
            {
                Id = interview.Id;
                WorkerId = interview.WorkerId;
                FaceInterviewDate = interview.FaceInterviewDate;
                FaceInterviewPlatoonName = interview.FaceInterviewPlatoonName;
                FaceInterviewRecord = interview.FaceInterviewRecord;
                FaceInterviewResult = interview.FaceInterviewResult;
            }
        }

        public void UpdateInterview(ref Interview interview)
        {
            interview.FaceInterviewDate = FaceInterviewDate;
            interview.FaceInterviewPlatoonName = FaceInterviewPlatoonName;
            interview.FaceInterviewRecord = FaceInterviewRecord;
            interview.FaceInterviewResult = FaceInterviewResult;
        }
    }
}
