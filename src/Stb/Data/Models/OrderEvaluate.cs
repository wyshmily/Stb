using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace Stb.Data.Models
{
    public class OrderEvaluate : Evaluate
    {
        // 对工单服务质量的评价

        [Display(Name = "班组施工质量")]
        public byte WorkQuality { get; set; }  // 0-优秀;1-合格;2-不合格

        [Display(Name = "班组施工效率")]
        public byte WorkEfficiency { get; set; }  // 0-优秀;1-合格;2-不合格

        [Display(Name = "班组施工态度")]
        public byte WorkAttitude { get; set; }  // 0-优秀;1-合格;2-不合格
    }
}
