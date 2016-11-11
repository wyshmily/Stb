using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Stb.Api.Models
{
    public class Settings
    {
        public ConnectionString connectionstrings { get; set; }
    }

    public class ConnectionString
    {
        public string stbconnection { get; set; }
    }
}
