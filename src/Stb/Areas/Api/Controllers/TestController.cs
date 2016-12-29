using Microsoft.AspNetCore.Mvc;
using Stb.Api.Models;
using Stb.Areas.Platform.Models.TestViewModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Stb.Api.Controllers
{
    [Produces("application/json")]
    [Route("api/test")]
    [ApiExceptionFilter]
    public class TestController : Controller
    {
        [HttpPost("BindingTest")]
        public ApiOutput<ModelBinder> TestBinding([FromBody]ModelBinder data)
        {
            if (ModelState.IsValid)
            {
                return new ApiOutput<ModelBinder>(data);
            }
            else
            {
                string msg = "";
                foreach (var modelState in ModelState.Values)
                {
                    foreach (var error in modelState.Errors)
                    {
                        msg += error.ErrorMessage + "\n";
                    }
                }
                throw new ApiException(msg);
            }
        }
    }
}
