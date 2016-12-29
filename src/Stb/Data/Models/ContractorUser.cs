using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace Stb.Data.Models
{
    // 承包商员工
    public class ContractorUser:ApplicationUser
    {
        public int ContractorId { get; set; }

        public Contractor Contractor { get; set; }

        public List<Order> Orders { get; set; }
    }
}
