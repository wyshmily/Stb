using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace Stb.Data.Models
{
    public class TrailEvaluate : Evaluate
    {
        public string LeadWorkerId { get; set; }    // 试单班长Id

        public Worker LeadWorker { get; set; }

        [Display(Name = "客户方评价")]
        public byte CustomerJudgement { get; set; } // 0-优秀;1-合格;2-不合格

        // 现场工作秩序和效率
        [Display(Name = "表现等级")]
        public byte WorkPlaceOrder { get; set; }    // 0-优秀;1-合格;2-不合格

        [Display(Name = "掌握并且遵守了现场甲方（业主）的管理要求")]
        public bool ObeyOrder { get; set; }  // true-是;false-否

        [Display(Name = "明确我方在场的工作内容，责任范围")]
        public bool KnowTask { get; set; }  // true-是;false-否

        [Display(Name = "了解现场的安全管理要求")]
        public bool KnowSafty { get; set; }  // true-是;false-否

        [Display(Name = "以上要求传达到了工人个体")]
        public bool RequireWorkers { get; set; }  // true-是;false-否

        [Display(Name = "及时调整了工人班组、分工")]
        public bool CanStrain { get; set; }  // true-是;false-否

        [Display(Name = "现场存在工人劳动强度不一，有闲散，聚集等情况")]
        public bool AllocationProblem { get; set; }  // true-是;false-否

        // 处理各方矛盾
        [Display(Name = "表现等级")]
        public byte HandleConflits { get; set; }    // 0-优秀;1-合格;2-不合格

        [Display(Name = "出现因协调各方关系失当导致我方停工或影响工作效率的情况")]
        public bool WorkShutdown { get; set; }  // true-是;false-否

        [Display(Name = "对于以上情况班长能够给出合理的解释")]
        public bool CanExplainShutdown { get; set; }  // true-是;false-否

        [Display(Name = "掌握现场甲方联系人的情况和动向")]
        public bool MasterTrend { get; set; }  // true-是;false-否

        [Display(Name = "掌握第三方施工队信息，及其工作内容与我方施工内容之关系")]
        public bool KnowRelation { get; set; }  // true-是;false-否

        [Display(Name = "了解团队内部人员矛盾，并且做出了应有的调整工作")]
        public bool HandleContradictions { get; set; }  // true-是;false-否

        [Display(Name = "根据现场工作内容的变化，及时进行工人分工、编组的调整")]
        public bool HandleWorkChange { get; set; }  // true-是;false-否

        // 解决现场问题
        [Display(Name = "表现等级")]
        public byte HandleProblems { get; set; }    // 0-优秀;1-合格;2-不合格

        [Display(Name = "问题处理及时")]
        public bool HandleProblemInTime { get; set; }  // true-是;false-否

        [Display(Name = "问题处理方法得当")]
        public bool HandleProblemResonable { get; set; }  // true-是;false-否

        [Display(Name = "该问题的发生原因与我方班组有关")]
        public bool ProblemDueToWorkers { get; set; }  // true-是;false-否

        [Display(Name = "问题定位清晰准确，影响小，评价公正")]
        public bool HandleProblemClear { get; set; }  // true-是;false-否

        [Display(Name = "该问题的处理方案对我方与客户关系造成了负面影响")]
        public bool ProblemNegativeEffect { get; set; }  // true-是;false-否

        // 与各方沟通的表现
        [Display(Name = "表现等级")]
        public byte Communicate { get; set; }    // 0-优秀;1-合格;2-不合格

        [Display(Name = "有理有节，态度良好")]
        public bool NiceAttitude { get; set; }  // true-是;false-否

        [Display(Name = "思路清晰，沟通及时、到位")]
        public bool ClearThinking { get; set; }  // true-是;false-否

        [Display(Name = "兼顾各方情况取得最好的方案和结果")]
        public bool RightForAll { get; set; }  // true-是;false-否

        // 处理人员变动的能力
        [Display(Name = "表现等级")]
        public byte HandleWorkerChange { get; set; }    // 0-优秀;1-合格;2-不合格

        [Display(Name = "上报人员更换信息准确、及时")]
        public bool ReportInTime { get; set; }  // true-是;false-否

        [Display(Name = "说明原因呢不隐瞒、不偏颇")]
        public bool ReportNoHide { get; set; }  // true-是;false-否

        [Display(Name = "能良好处理工人名额变动")]
        public bool HandleWorkerChangeWell { get; set; }  // true-是;false-否

        [Display(Name = "针对变动范围或候选人员提供的建议有很好的效果")]
        public bool GoodAdvice { get; set; }  // true-是;false-否

        // 对平台规定遵守情况
        [Display(Name = "表现等级")]
        public byte ObayPlatform { get; set; }    // 0-优秀;1-合格;2-不合格

        [Display(Name = "评价")]
        public string ObayPlatformJudgement { get; set; }

        // 个人技术能力评价
        [Display(Name = "表现等级")]
        public byte PersonalAbility { get; set; }    // 0-优秀;1-合格;2-不合格

        [Display(Name = "评价")]
        public string PersonalAbilityJudgement { get; set; }

        // 工作效果比较
        [Display(Name = "工作时间")]
        public byte WorkTime { get; set; }  // 0-优秀;1-高于一般水平;2-符合预期;3-不合格

        [Display(Name = "工作人力")]
        public byte WorkForce { get; set; }  // 0-优秀;1-高于一般水平;2-符合预期;3-不合格

        [Display(Name = "意外情况处理")]
        public byte Situation { get; set; }  // 0-优秀;1-高于一般水平;2-符合预期;3-不合格

        [Display(Name = "工作质量")]
        public byte WorkQuality { get; set; }  // 0-优秀;1-高于一般水平;2-符合预期;3-不合格

        [Display(Name = "客户评价")]
        public byte Customer { get; set; }  // 0-优秀;1-高于一般水平;2-符合预期;3-不合格


        [Display(Name = "试单结论")]
        public bool TrailResult { get; set; }  // true-合格;false-不合格
    }
}
