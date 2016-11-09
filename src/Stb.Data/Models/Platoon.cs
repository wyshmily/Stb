using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;

namespace Stb.Data.Models
{
    // 排长
    public class Platoon : EndUser
    {
        public string ArmyPost { get; set; } // 退伍前职务

        public string ArmyRank { get; set; }    // 退伍前军衔

        public DateTime? MilitaryTime { get; set; }  // 入伍时间

        public DateTime? DischargeTime { get; set; } // 退伍时间

        public List<Project> Projects { get; set; }

    }
}
