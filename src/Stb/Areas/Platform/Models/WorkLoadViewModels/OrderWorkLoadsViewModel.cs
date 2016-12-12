using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace Stb.Platform.Models.WorkLoadViewModels
{
    public class OrderWorkLoadsViewModel
    {
        public string OrderId { get; set; }

        public bool IsWorkerWorkLoadSet { get; set; }

        [DisplayFormat(DataFormatString = "{0:yyyy-MM-dd}")]
        public DateTime? WorkerWorkLoadSetTime { get; set; }

        public string LeadWorkerName { get; set; }

        public List<WorkLoadViewModel> WorkLoads { get; set; }
    }
}
