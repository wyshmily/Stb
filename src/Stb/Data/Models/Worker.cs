using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using System.ComponentModel.DataAnnotations.Schema;

namespace Stb.Data.Models
{
    // 工人：包括班长和工人
    public class Worker : EndUser
    {
        public bool IsHeader { get; set; }  // 是否班长

        public bool IsCandidate { get; set; }   // 是否候选班长（IsHeader为false时有意义）

        public string HeaderId { get; set; } // 班长Id

        public int? BestJobClassId { get; set; } // 擅长技能Id

        public Worker Header { get; set; } 

        public List<Worker> Workers { get; set; }

        public List<Order> LeadOrders { get; set; } // 作为班长参与的项目

        public List<OrderWorker> OrderWorkers { get; set; } // 作为工人参与的项目

        public List<Signment> Signments { get; set; }

        public List<WorkLoad> WorkLoads { get; set; }

        public JobClass BestJobClass { get; set; }

        public List<Interview> Interviews { get; set; }
    }
}
