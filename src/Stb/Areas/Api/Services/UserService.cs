﻿using Microsoft.AspNetCore.Identity;
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
    public class UserService
    {
        private readonly UserManager<EndUser> _userManager;

        public UserService(UserManager<EndUser> userManager)
        {
            _userManager = userManager;
        }

        public async Task UpdatePushId(ClaimsPrincipal user, string pushId)
        {
            var endUser = await _userManager.GetUserAsync(user);
            if (endUser == null)
                throw new ApiException("用户不存在");

            endUser.PushId = pushId;
            await _userManager.UpdateAsync(endUser);
        }


    }
}