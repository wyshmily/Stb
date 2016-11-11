using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using System.ComponentModel.DataAnnotations.Schema;

namespace Stb.Data.Models
{
    // 市
    public class City
    {
        [DatabaseGenerated(DatabaseGeneratedOption.None)]
        public int Id { get; set; } // adcode

        public string Name { get; set; }

        //public int AdCode { get; set; }

        //public int CityCode { get; set; }

        public int ProvinceId { get; set; }

        public Province Province { get; set; }

        public List<District> Districts { get; set; }

    }
}
