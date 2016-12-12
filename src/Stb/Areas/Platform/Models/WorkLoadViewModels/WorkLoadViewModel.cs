using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Stb.Data.Models;
using System.ComponentModel.DataAnnotations;

namespace Stb.Platform.Models.WorkLoadViewModels
{
    public class WorkLoadViewModel
    {
        public int JobMeasurementId { get; set; }

        public string JobMeasurementName { get; set; }

        public string Unit { get; set; }

        [NoBiggerThanOrderWorkLoad(ErrorMessage = "工人工作量不能超过工单的对应工作量")]
        public int? Amount { get; set; }

        public string WorkerId { get; set; }

        public string WorkerName { get; set; }

        public string OrderId { get; set; }

        public int OrderAmount { get; set; }

        public WorkLoadViewModel() { }

        public WorkLoadViewModel(WorkLoad workLoad)
        {
            JobMeasurementId = workLoad.JobMeasurementId;
            JobMeasurementName = workLoad.JobMeasurement?.Name;
            Unit = workLoad.JobMeasurement?.Unit;
            OrderAmount = workLoad.Amount;
            Amount = workLoad.Amount;
            WorkerId = workLoad.WorkerId;
            WorkerName = workLoad.Worker?.Name;
            OrderId = workLoad.OrderId;
        }

        public WorkLoad ToWorkLoad()
        {
            return new WorkLoad
            {
                JobMeasurementId = JobMeasurementId,
                Amount = Amount.GetValueOrDefault(),
                OrderId = OrderId,
                WorkerId = WorkerId,
            };
        }
    }
}
