using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Stb.Api.Models.AuthViewModels;

namespace Stb.Api.Models
{
    public class ApiOutput<T>
    {
        public string code { get; set; }

        public string msg { get; set; }

        public T data { get; set; }

        public ApiOutput(T loginData, string code = "A00000", string msg = null)
        {
            this.data = loginData;
            this.code = code;
            this.msg = msg;
        }
    }
}
