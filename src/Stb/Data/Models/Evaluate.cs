using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace Stb.Data.Models
{
    public class Evaluate
    {
        public int Id { get; set; }

        [DataType(DataType.Date)]
        [DisplayFormat(DataFormatString ="{0:yyyy-MM-dd}")]
        public DateTime Time { get; set; }  // 评价时间

        public string OrderId { get; set; } // 工单Id

        public byte Type { get; set; }  // 1- 排长评价；2-排长试单评价；3-品控评价；4-客服评价；5-班长评价

        public string EvaluateUserId { get; set; } // 评价人Id

        public Order Order { get; set; }

        public ApplicationUser EvaluateUser { get; set; }
    }
}
