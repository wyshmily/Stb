using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;

namespace Stb.Platform.Models
{
    // 市
    public class City
    {
        public int Id { get; set; }

        public string Name { get; set; }

        public int AdCode { get; set; }

        public int CityCode { get; set; }

        public int ProvinceId { get; set; }

        public Province Province { get; set; }

        public List<District> Districts { get; set; }

    }
}
