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
    }
}
