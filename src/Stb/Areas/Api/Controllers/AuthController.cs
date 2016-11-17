using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Identity;
using Stb.Data.Models;
using Stb.Data;
using System.Security.Claims;
using System.IdentityModel.Tokens.Jwt;
using Microsoft.Extensions.Options;
using Stb.Api.JwtToken;
using System.Security.Principal;
using Stb.Api.Models.AuthViewModels;
using Microsoft.AspNetCore.Authorization;

namespace Stb.Areas.Api.Controllers
{
    [Produces("application/json")]
    [Route("api/Auth")]
    [Authorize(ActiveAuthenticationSchemes = "Bearer")]
    public class AuthController : Controller
    {
        private readonly UserManager<EndUser> _userManager;
        private readonly SignInManager<EndUser> _signInManager;
        private readonly ApplicationDbContext _context;
        private readonly TokenProviderOptions _options;

        public AuthController(UserManager<EndUser> userManager, SignInManager<EndUser> signInManager, ApplicationDbContext context, IOptions<TokenProviderOptions> options)
        {
            _userManager = userManager;
            _signInManager = signInManager;
            _context = context;
            _options = options.Value;
        }

        [Route("Login")]
        [AllowAnonymous]
        public async Task<IActionResult> Login([FromBody]LoginViewModel loginViewModel)
        {
            string username = loginViewModel.Username;
            string password = loginViewModel.Password;
            var identity = await GetIdentity(username, password);
            if (identity == null)
            {
                return BadRequest("用户名或密码错误。");
            }

            DateTime now = DateTime.UtcNow;

            var claims = new Claim[]
            {
                new Claim(JwtRegisteredClaimNames.Sub, username),
                new Claim(JwtRegisteredClaimNames.Jti, Guid.NewGuid().ToString()),
                new Claim(JwtRegisteredClaimNames.Iat, ToUnixEpochDate(now).ToString(), ClaimValueTypes.Integer64)
            };

            var jwt = new JwtSecurityToken(
               issuer: _options.Issuer,
               audience: _options.Audience,
               claims: claims,
               notBefore: now,
               expires: now.Add(_options.Expiration),
               signingCredentials: _options.SigningCredentials);

            var encodedJwt = new JwtSecurityTokenHandler().WriteToken(jwt);

            return Ok(new
            {
                token = encodedJwt,
                now = new DateTimeOffset(now).ToUniversalTime().ToUnixTimeSeconds().ToString()
            });
        }


        private async Task<ClaimsIdentity> GetIdentity(string username, string password)
        {
            if (username == null)
                return null;
            var result = await _signInManager.PasswordSignInAsync(username, password, false, lockoutOnFailure: false);
            if (result.Succeeded)
            {
                var user = await _userManager.FindByNameAsync(username);
                var claims = await _userManager.GetClaimsAsync(user);

                return new ClaimsIdentity(new GenericIdentity(username, "Token"), claims);
            }

            // Credentials are invalid, or account doesn't exist
            return null;
        }

        public static long ToUnixEpochDate(DateTime date) => new DateTimeOffset(date).ToUniversalTime().ToUnixTimeSeconds();

        [Route("TestToken")]
        public IActionResult TestToken()
        {
            return Ok("Token Is Valid");
        }
    }
}