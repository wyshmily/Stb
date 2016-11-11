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
    public class WorkerIndexViewModel
    {
        public string Id { get; set; }

        [Display(Name = "手机")]
        public string UserName { get; set; }

        [Display(Name = "姓名")]
        public string Name { get; set; }

        [Display(Name = "性别")]
        public bool Gender { get; set; }  // 性别：true-男；false-女

        [Display(Name = "是否班长")]
        public bool IsHead { get; set; }

        [Display(Name = "班长")]
        public string HeaderName { get; set; }

        public WorkerIndexViewModel()
        {

        }

        public WorkerIndexViewModel(Worker worker)
        {
            Id = worker.Id;
            UserName = worker.UserName;
            Name = worker.Name;
            Gender = worker.Gender;
            IsHead = worker.IsHeader;
            if (worker.Header != null)
                HeaderName = worker.Header.Name;
        }
    }
}
