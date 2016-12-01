using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Stb.Api
{
    public class ApiException : Exception
    {
        public ApiException(string msg) : base(msg) { }
    }
}
