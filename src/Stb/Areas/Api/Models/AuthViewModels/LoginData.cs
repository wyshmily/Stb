using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Stb.Api.Models.AuthViewModels
{
    public class LoginData
    {
        public string atoken { get; set; }
        public UserInfo userInfo { get; set; }
    }
}
