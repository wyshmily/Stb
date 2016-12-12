using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace Stb.Data.Models
{
    public class OrderEvaluate_QualityControl : OrderEvaluate
    {
        [Display(Name = "对班长技术的评价")]
        public byte LeaderAbility { get; set; }  // 0-优秀;1-合格;2-不合格

        [Display(Name = "对投诉的处理")]
        public string ComplaintSettlement { get; set; }
    }
}
