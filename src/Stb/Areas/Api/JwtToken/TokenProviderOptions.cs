using Microsoft.IdentityModel.Tokens;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Stb.Api.JwtToken
{
    public class TokenProviderOptions
    {
        public string Path { get; set; } = "/api/token";

        public string Issuer { get; set; }

        public string Audience { get; set; }

        public TimeSpan Expiration { get; set; } = TimeSpan.FromHours(1);

        public SigningCredentials SigningCredentials { get; set; }
    }
}
