using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Stb.Api.Models.AuthViewModels
{
    public class LoginData
    {
        /// <summary>
        /// Jwt Token
        /// </summary>
        public string Token { get; set; }
        public UserInfo UserInfo { get; set; }
    }
}
