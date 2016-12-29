using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace Stb.Data.Models
{
    public class WorkerEvaluate : Evaluate
    {
        public string WorkerId { get; set; }    // 工人Id

        public Worker Worker { get; set; }

        // 施工质量评价

        [Display(Name = "施工成绩")]
        public byte WorkScore { get; set; }  // 0-优秀;1-合格;2-不合格

        // 综合能力评价
        [Display(Name = "能否看懂图纸")]
        public bool WorkerCanReadDrawings { get; set; }  // true-能;false-否

        [Display(Name = "有无建设性的施工意见")]
        public bool WorkerGiveAdvices { get; set; }  // true-有;false-无

        [Display(Name = "是否参与协调")]
        public bool WorkerCooperates { get; set; }  // true-是;false-否

        [Display(Name = "有无设计能力")]
        public bool WorkerCanDesign { get; set; }  // true-有;false-无
    }
}
