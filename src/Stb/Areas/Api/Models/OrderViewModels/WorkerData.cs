using Stb.Data.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Stb.Api.Models.OrderViewModels
{
    public class WorkerData
    {
        public string Id { get; set; }

        public string Name { get; set; }

        public string Phone { get; set; }

        public WorkerData(Worker worker)
        {
            if (worker == null)
                return;
            Id = worker.Id;
            Name = worker.Name;
            Phone = worker.UserName;
        }
    }
}
