using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace Stb.Areas.Platform.Models.TestViewModels
{
    public class ModelBinder
    {
        [Range(1,100, ErrorMessage ="{0}的范围为1到100")]
        public int Count { get; set; }

        [Required(ErrorMessage ="{0}是必须项")]
        public string Name { get; set; }
    }
}
