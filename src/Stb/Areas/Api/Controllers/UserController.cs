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

namespace Stb.Api.Controllers
{
    [Produces("application/json")]
    [Route("api/User")]
    [Authorize(ActiveAuthenticationSchemes = "Bearer")]
    [ApiExceptionFilter]
    public class UserController : Controller
    {
        private readonly UserService _userService;

        public UserController(UserService userService)
        {
            _userService = userService;
        }

        /// <summary>
        /// App通用：上报个推ID
        /// </summary>
        /// <param name="pushId"></param>
        /// <returns></returns>
        [HttpGet("updatePushId")]
        public async Task<ApiOutput<bool>> UpdatePushId([FromQuery]string pushId)
        {
            await _userService.UpdatePushId(User, pushId);
            return new ApiOutput<bool>(true);
        }


    }
}