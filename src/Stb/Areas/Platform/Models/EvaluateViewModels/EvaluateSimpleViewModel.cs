using Stb.Data.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Stb.Platform.Models.EvaluateViewModels
{
    public class EvaluateSimpleViewModel
    {
        public int Id { get; set; }

        public byte Type { get; set; }

        public EvaluateSimpleViewModel() { }

        public EvaluateSimpleViewModel(Evaluate evaluate)
        {
            Id = evaluate.Id;
            Type = evaluate.Type;
        }
    }
}
