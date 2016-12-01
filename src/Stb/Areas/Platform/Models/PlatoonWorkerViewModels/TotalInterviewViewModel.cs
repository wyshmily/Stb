using Stb.Data.Models;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace Stb.Platform.Models.PlatoonWorkerViewModels
{
    public class TotalInterviewViewModel
    {
        public int Id { get; set; }

        public string WorkerId { get; set; }

        public string PlatoonId { get; set; }

        public string PlatoonName { get; set; }

        [Display(Name = "排长结论")]
        [Required(ErrorMessage = "请填写{0}")]
        public byte? TotalInterviewResult { get; set; }

        [Display(Name = "其它结论")]
        public string OtherResult { get; set; }

        public TotalInterviewViewModel() { }

        public TotalInterviewViewModel(Interview interview)
        {
            if(interview != null)
            {
                Id = interview.Id;
                WorkerId = interview.WorkerId;
                TotalInterviewResult = interview.TotalInterviewResult;
                OtherResult = interview.OtherResult;
                PlatoonId = interview.Platoon?.Id;
                PlatoonName = interview.Platoon?.Name;
            }
        }

        public void UpdateInterview(ref Interview interview)
        {
            interview.TotalInterviewResult = TotalInterviewResult;
            interview.PlatoonId = PlatoonId;
            interview.OtherResult = OtherResult;
        }
    }
}
