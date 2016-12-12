using Stb.Data.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Stb.Platform.Models.EvaluateViewModels
{
    public class WorkerEvaluateViewModel
    {
        public string WorkerId { get; set; }

        public string WorkerName { get; set; }

        public WorkerEvaluate Evaluate { get; set; }

        public WorkerEvaluateViewModel() { }

        public WorkerEvaluateViewModel(OrderWorker worker, List<WorkerEvaluate> evaluates)
        {
            WorkerId = worker.WorkerId;
            WorkerName = worker.Worker?.Name;
            Evaluate = evaluates.FirstOrDefault(e => e.WorkerId == WorkerId);
        }

    }
}
