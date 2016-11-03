using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;

namespace Stb.Platform.Models
{
    // 省
    public class Province
    {
        public int Id { get; set; }

        public string Name { get; set; }

        public int AdCode { get; set; }

        public List<City> Cities { get; set; }
    }
}
