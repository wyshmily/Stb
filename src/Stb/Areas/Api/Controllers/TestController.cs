using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using Stb.Api.Models;
using Microsoft.Extensions.Options;
using Microsoft.AspNetCore.Authorization;

namespace Stb.Areas.Api.Controllers
{
    [Produces("application/json")]
    [Route("api/test")]
    public class TestController : Controller
    {
        Settings _settings;

        public TestController(IOptions<Settings> settings)
        {
            _settings = settings.Value;
        }

        [HttpGet("time")]
        public string Time()
        {
            DateTime now = DateTime.Now;
            return now.ToString("yyyy-MM-dd HH:mm:ss") + "   "
                + new DateTimeOffset(now).ToUniversalTime().ToUnixTimeSeconds().ToString();


        }

        [HttpGet("echo/{msg?}")]
        public string Echo(string msg)
        {
            return msg;
        }

        [HttpGet("config")]
        public Settings Config()
        {
            return _settings;
        }
    }
}
