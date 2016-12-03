using Microsoft.AspNetCore.Mvc;
using Stb.Util;
using System;
using System.Collections.Generic;
using System.IdentityModel.Tokens.Jwt;
using System.Linq;
using System.Security.Claims;
using System.Threading.Tasks;

namespace Stb.Api.JwtToken
{
    public static class Jwt
    {
        public static string GenerateJwtToken(string username, ClaimsIdentity identity, TokenProviderOptions options)
        {
            DateTime now = DateTime.UtcNow;

            List<Claim> claims = new List<Claim>
            {
                new Claim(JwtRegisteredClaimNames.Sub, username),
                new Claim(JwtRegisteredClaimNames.Jti, Guid.NewGuid().ToString()),
                new Claim(JwtRegisteredClaimNames.Iat, now.ToUnixEpochDate().ToString(), ClaimValueTypes.Integer64)
            };

            foreach (var claim in identity.Claims)
            {
                claims.Add(claim);
            }

            var jwt = new JwtSecurityToken(
               issuer: options.Issuer,
               audience: options.Audience,
               claims: claims,
               notBefore: now,
               expires: now.Add(options.Expiration),
               signingCredentials: options.SigningCredentials);

            var encodedJwt = new JwtSecurityTokenHandler().WriteToken(jwt);
            return encodedJwt;
        }

       

    }
}
