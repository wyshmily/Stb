using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Stb.Platform.Models
{
    public class PaginationViewModel
    {
        public string Action { get; set; }

        public int Page { get; set; }

        public int TotalPage { get; set; }
    }
}
