using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using System.ComponentModel.DataAnnotations.Schema;

namespace Stb.Data.Models
{
    // 区
    public class District
    {
        [DatabaseGenerated(DatabaseGeneratedOption.None)]
        public int Id { get; set; }

        public string Name { get; set; }

        public int CityId { get; set; }

        public string CityName { get; set; }

        public int ProvinceId { get; set; }

        public string ProvinceName { get; set; }

        public City City { get; set; }

        public List<EndUserDistrict> EndUserDistricts { get; set; }

        public List<Order> Orders { get; set; }
        public string FullName
        {
            get
            {
                return $"{ProvinceName} {CityName} {Name}";
            }
        }
    }
}
