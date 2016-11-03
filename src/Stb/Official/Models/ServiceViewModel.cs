using Microsoft.AspNetCore.Mvc.Rendering;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Stb.Official.Models
{
    public class ServiceViewModel
    {
        public SelectList standardList { get; set; }

        public Job job { get; set; }
    }
}
