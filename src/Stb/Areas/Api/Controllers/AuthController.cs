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
        /// <param name="account">Required - 账号（手机号）</param>
        /// <param name="password">Required - 密码（明文）</param>
        /// <param name="deviceId">Required - 设备唯一Id</param>
        /// <param name="appType">Required - App类型：2-排长端；3-班长端</param>
        /// <returns>返回JWT Token和用户信息</returns>
        [HttpGet("Login")]
        [AllowAnonymous]
        public async Task<ApiOutput<LoginData>> LoginAsync([RequiredFromQuery]string account, [RequiredFromQuery]string password, [RequiredFromQuery]string deviceId, [RequiredFromQuery]int appType)
        {
            if (appType == 2) // 排长端登录
            {
                LoginData loginData = await _platoonAuthService.LoginAsync(account, password, deviceId, appType);
                return new ApiOutput<LoginData>(loginData);
            }
            else if (appType == 3) // 班长端登录
            {
                LoginData loginData = await _workerAuthService.LoginAsync(account, password, deviceId, appType);
                return new ApiOutput<LoginData>(loginData);
            }

            throw new ApiException("登录设备类型错误。");
        }

        /// <summary>
        /// App通用：修改密码
        /// </summary>
        /// <param name="oldPwd">Required - 当前密码</param>
        /// <param name="newPwd">Required - 新密码</param>
        /// <returns></returns>
        [HttpGet("ChangePwd")]
        public async Task<ApiOutput<bool>> ChangedPwdAsync([RequiredFromQuery]string oldPwd, [RequiredFromQuery]string newPwd)
        {
            int appType = this.AppType();
            if (appType == 2)
                return new ApiOutput<bool>(await _platoonAuthService.ChangePwdAsync(this.UserId(), oldPwd, newPwd));
            else if (appType == 3)
                return new ApiOutput<bool>(await _workerAuthService.ChangePwdAsync(this.UserId(), oldPwd, newPwd));

            throw new ApiException("登录设备类型错误。");
        }
    }
}