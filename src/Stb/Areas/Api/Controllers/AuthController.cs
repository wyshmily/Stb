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
using Stb.Api.Services;
using Stb.Api.Models;
using System.ComponentModel.DataAnnotations;

namespace Stb.Api.Controllers
{
    [Produces("application/json")]
    [Route("api/Auth")]
    [Authorize(ActiveAuthenticationSchemes = "Bearer")]
    [ApiExceptionFilter]
    public class AuthController : Controller
    {
        private readonly PlatoonAuthService _platoonAuthService;
        private readonly WorkerAuthService _workerAuthService;

        public AuthController(PlatoonAuthService platoonAuthService, WorkerAuthService workerAuthService)
        {
            _platoonAuthService = platoonAuthService;
            _workerAuthService = workerAuthService;
        }

        /// <summary>
        /// App通用：用户登录
        /// </summary>
        /// <param name="account"></param>
        /// <param name="password"></param>
        /// <param name="deviceid"></param>
        /// <param name="apptype"></param>
        /// <returns>token和UserInfo</returns>
        [HttpGet("Login")]
        [AllowAnonymous]
        public async Task<ApiOutput<LoginData>> LoginAsync([FromQuery]string account, [FromQuery]string password, [FromQuery]string deviceid, [FromQuery]int apptype)
        {
            if (account == null)
                throw new ApiException("用户名不能为空");
            if (deviceid == null)
                throw new ApiException("设备ID不能为空");

            if (apptype == 2) // 排长端登录
            {
                LoginData loginData = await _platoonAuthService.LoginAsync(account, password, deviceid);
                return new ApiOutput<LoginData>(loginData);
            }
            else if(apptype == 3) // 班长端登录
            {
                LoginData loginData = await _workerAuthService.LoginAsync(account, password, deviceid);
                return new ApiOutput<LoginData>(loginData);
            }

            throw new ApiException("登录设备类型错误。");
        }


    }
}