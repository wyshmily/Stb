using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Stb.Platform.Models;

namespace Stb.Platform.Controllers
{
    [Area("platform")]
    [Authorize]
    public class HomeController : Controller
    {

        UserManager<PlatformUser> _userManager;

        public HomeController(UserManager<PlatformUser> userManager)
        {
            _userManager = userManager;
        }

        public IActionResult Index()
        {
            return View();
        }

        //[Authorize(Policy = "Authenticated")]
        public async Task<IActionResult> About()
        {
            ViewData["Message"] = "Your application description page.";

            PlatformUser appUser = await _userManager.GetUserAsync(User);

            return View();
        }

        //[Authorize(Policy = "Authenticated")]
        public IActionResult Contact()
        {
            ViewData["Message"] = "Your contact page.";
            return View();
        }

        [AllowAnonymous]
        public IActionResult Error()
        {
            return View();
        }
    }
}
