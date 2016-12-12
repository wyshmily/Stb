using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace Stb.Data.Models
{
    public class OrderEvaluate_Platoon : OrderEvaluate
    {
        // 排长对班长技能水平评价
        [Display(Name = "班长能够看懂图纸")]
        public byte LeaderCanReadDrawings { get; set; }  // 0-能;1-不能

        [Display(Name = "同类工程班长有无设计能力")]
        public byte LeaderCanDoDesign { get; set; }  // 0-能;1-不能

        [Display(Name = "班长的技能水平")]
        public byte LeaderAbilityLevel { get; set; }  // 0-优秀;1-合格;2-不合格

        // 排长对班长沟通水平的评价
        [Display(Name = "班长在施工中能否随时发现问题随时协调")]
        public byte LeaderCanCooperate { get; set; }  // 0-能;1-不能

        [Display(Name = "班长是否及时向排长汇报施工情况")]
        public byte LeaderDoReport { get; set; }  // 0-汇报;1-不汇报

        // 排长对班长团队建设的评价
        [Display(Name = "班组成员对班长在该工单的评价")]
        public byte WorkerJudgement { get; set; }  // 0-优秀;1-合格;2-不合格

        //[Display(Name = "该工单曾经派单给其他班长，是否及时填写")]
        //public byte OtherLeader { get; set; }  //  0-是;1-否
    }
}
