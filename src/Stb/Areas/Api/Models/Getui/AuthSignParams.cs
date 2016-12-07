using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Stb.Api.Models.Getui
{
    public class AuthSignParams
    {
        public string sign { get; set; }

        public string timestamp { get; set; }

        public string appkey { get; set; }
    }
}
