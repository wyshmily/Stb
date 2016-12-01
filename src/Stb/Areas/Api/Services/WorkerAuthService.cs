using Microsoft.AspNetCore.Identity;
using Microsoft.Extensions.Options;
using Stb.Api.JwtToken;
using Stb.Api.Models.AuthViewModels;
using Stb.Data;
using Stb.Data.Models;
using System;
using System.Collections.Generic;
using System.IdentityModel.Tokens.Jwt;
using System.Linq;
using System.Security.Claims;
using System.Security.Principal;
using System.Threading.Tasks;

namespace Stb.Api.Services
{
    public class WorkerAuthService
    {
        private readonly UserManager<Worker> _userManager;
        private readonly SignInManager<Worker> _signInManager;
        private readonly ApplicationDbContext _context;
        private readonly TokenProviderOptions _options;

        public WorkerAuthService(UserManager<Worker> userManager, SignInManager<Worker> signInManager, ApplicationDbContext context, IOptions<TokenProviderOptions> options)
        {
            _userManager = userManager;
            _signInManager = signInManager;
            _context = context;
            _options = options.Value;
        }

        public async Task<LoginData> LoginAsync(string username, string password, string deviceId)
        {
           
            var result = await _signInManager.PasswordSignInAsync(username, password, false, lockoutOnFailure: false);
            if (!result.Succeeded)
                throw new ApiException("用户名或密码错误");

            var user = await _userManager.FindByNameAsync(username);
            var claims = await _userManager.GetClaimsAsync(user);
            var roles = await _userManager.GetRolesAsync(user);
            claims.Add(new Claim(ClaimTypes.Role, roles.FirstOrDefault()));
            //claims.Add(new Claim("DeviceId", deviceId));

            if(user.DeviceId != deviceId)
            {
                user.DeviceId = deviceId;
                await _userManager.UpdateAsync(user);
                // todo 。。。更换设备登录
            }

            var identity = new ClaimsIdentity(new GenericIdentity(username, "Token"), claims);

            string token = Jwt.GenerateJwtToken(user.Id, identity, _options);

            UserInfo userInfo = new UserInfo
            {
                account = username,
                name = user.Name,
                portrait = user.Portrait,
                uid = user.Id,
            };

            return new LoginData { atoken = token, userInfo = userInfo };
        }

        
    }
}
