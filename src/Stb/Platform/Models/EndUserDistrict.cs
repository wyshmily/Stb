using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Stb.Platform.Models
{
    // 终端用户与工作区域关联表
    public class EndUserDistrict
    {
        public int Id { get; set; }

        [Required]
        public string EndUserId { get; set; }

        public int DistrictId { get; set; }

        public EndUser EndUser { get; set; }

        public District District { get; set; }
    }
}
