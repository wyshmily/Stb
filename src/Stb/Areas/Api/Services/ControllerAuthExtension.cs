using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Threading.Tasks;

namespace Stb.Api.Services
{
    public static class ControllerAuthExtension
    {
        public static string UserId(this Controller controller)
        {
            return controller.User.Claims.FirstOrDefault(c => c.Type == ClaimTypes.NameIdentifier)?.Value;
        }

        public static int AppType(this Controller controller)
        {
            var type = controller.User.Claims.FirstOrDefault(c => c.Type == "AppType")?.Value;
            if (type == null)
                return 0;
            int.TryParse(type, out int appType);
            return appType;
        }
    }
}
