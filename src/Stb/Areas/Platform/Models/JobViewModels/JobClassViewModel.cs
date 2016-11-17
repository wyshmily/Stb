using Stb.Data.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Stb.Platform.Models.DistrictViewModels
{
    public class JobClassViewModel
    {
        public int JobClassId { get; set; }

        public string JobClassName { get; set; }

        public int JobCategoryId { get; set; }

        public string JobCategoryName { get; set; }

        public byte Grade { get; set; }

        public JobClassViewModel()
        {

        }

        public JobClassViewModel(EndUserJobClass endUserJobClass)
        {
            if(endUserJobClass.JobClass!=null)
            {
                JobClassId = endUserJobClass.JobClassId;
                JobClassName = endUserJobClass.JobClass.Name;
                Grade = endUserJobClass.Grade;

                if(endUserJobClass.JobClass.JobCategory!=null)
                {
                    JobCategoryId = endUserJobClass.JobClass.JobCategoryId;
                    JobCategoryName = endUserJobClass.JobClass.JobCategory.Name;
                }
            }
        }

    }
}
