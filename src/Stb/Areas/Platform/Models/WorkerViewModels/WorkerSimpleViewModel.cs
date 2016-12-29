using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Stb.Data.Models;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace Stb.Platform.Models.WorkerViewModels
{
    public class WorkerSimpleViewModel
    {
        public string Id { get; set; }

        [Display(Name = "手机")]
        public string UserName { get; set; }

        [Display(Name = "姓名")]
        public string Name { get; set; }

        public bool IsHeader { get; set; }

        public bool IsCandidate { get; set; }

        public string Portrait { get; set; }

        public WorkerSimpleViewModel()
        {

        }

        public WorkerSimpleViewModel(Worker worker)
        {
            if (worker == null)
                return;
            Id = worker.Id;
            UserName = worker.UserName;
            Name = worker.Name;
            IsHeader = worker.IsHeader;
            IsCandidate = worker.IsCandidate;
            Portrait = worker.Portrait;
        }
    }
}
