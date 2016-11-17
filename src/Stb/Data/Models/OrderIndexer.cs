using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using System.ComponentModel.DataAnnotations.Schema;

namespace Stb.Data.Models
{
    // 省
    public class Province
    {
        [DatabaseGenerated(DatabaseGeneratedOption.None)]
        public int Id { get; set; } // adcode

        public string Name { get; set; }

        //public int AdCode { get; set; }

        public List<City> Cities { get; set; }
    }
}
