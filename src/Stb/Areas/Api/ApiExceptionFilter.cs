using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Filters;
using Stb.Api.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Threading.Tasks;

namespace Stb.Api
{
    public class ApiExceptionFilterAttribute : ExceptionFilterAttribute
    {
        public override void OnException(ExceptionContext context)
        {
            string code = context.Exception is ApiException ? "B00000" : "E00000";
            context.Result = new JsonResult(new ApiOutput<object>(null, code, context.Exception.Message));
            base.OnException(context);
        }
    }
}
