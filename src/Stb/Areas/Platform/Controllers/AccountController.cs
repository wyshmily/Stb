using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.Extensions.Logging;
using Stb.Data.Models;
using Stb.Platform.Models.AccountViewModels;
using Stb.Services;
using Microsoft.AspNetCore.Http.Authentication;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using Stb.Data;

namespace Stb.Platform.Controllers
{
    [Authorize]
    [Area(AreaNames.Platform)]
    public class AccountController : Controller
    {
        private readonly UserManager<PlatformUser> _userManager;
        private readonly SignInManager<ApplicationUser> _signInManager;
        private readonly RoleManager<IdentityRole> _roleManager;

        private readonly ILogger _logger;

        public AccountController(
            UserManager<PlatformUser> userManager,
            SignInManager<ApplicationUser> signInManager,
            RoleManager<IdentityRole> roleManager,
            ILoggerFactory loggerFactory)
        {
            _userManager = userManager;
            _signInManager = signInManager;
            _roleManager = roleManager;
            _logger = loggerFactory.CreateLogger<AccountController>();
        }

        //
        // GET: /Account/Login
        [HttpGet]
        [AllowAnonymous]
        public IActionResult Login(string returnUrl = null)
        {
            ViewData["ReturnUrl"] = returnUrl;
            return View();
        }

        //
        // POST: /Account/Login
        [HttpPost]
        [AllowAnonymous]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Login(LoginViewModel model, string returnUrl = null)
        {
            ViewData["ReturnUrl"] = returnUrl;
            if (ModelState.IsValid)
            {
                // This doesn't count login failures towards account lockout
                // To enable password failures to trigger account lockout, set lockoutOnFailure: true
                var result = await _signInManager.PasswordSignInAsync(model.UserName, model.Password, model.RememberMe, lockoutOnFailure: false);
                if (result.Succeeded)
                {
                    _logger.LogInformation(1, "User logged in.");

                    //var userIdentity = new ClaimsIdentity("SuperSecureLogin");
                    //var userPrincipal = new ClaimsPrincipal(userIdentity);
                    //await HttpContext.Authentication.SignInAsync("Cookie", userPrincipal,
                    //    new AuthenticationProperties
                    //    {
                    //        ExpiresUtc = DateTime.UtcNow.AddDays(1),
                    //        IsPersistent = model.RememberMe,
                    //        AllowRefresh = false
                    //    });

                    return RedirectToLocal(returnUrl);
                }

                ModelState.AddModelError(string.Empty, "用户名或密码错误！");
                return View(model);
            }

            // If we got this far, something failed, redisplay form
            return View(model);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> LogOff()
        {
            //await HttpContext.Authentication.SignOutAsync("Cookie");
            await _signInManager.SignOutAsync();
            _logger.LogInformation(4, "User logged out.");
            return RedirectToAction(nameof(HomeController.Index), "Home");
        }

        [AllowAnonymous]
        public IActionResult Forbidden()
        {
            return View();
        }

        // GET: ApplicationUsers
        [Authorize(Roles = Roles.Administrator)]
        public async Task<IActionResult> Index()
        {
            List<PlatformUser> appUserList = await _userManager.Users.Include(u => u.Roles).OrderBy(u => u.UserName).ToListAsync();

            return View(appUserList.Select(appUser => GetAppUserViewModel(appUser).Result).ToList());
        }

        // GET: ApplicationUsers/Details/5
        [Authorize(Roles = Roles.Administrator)]
        public async Task<IActionResult> Details(string id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var appUser = await _userManager.FindByIdAsync(id);
            if (appUser == null)
            {
                return NotFound();
            }

            return View(await GetAppUserViewModel(appUser));
        }

        // GET: ApplicationUsers/Create
        [Authorize(Roles = Roles.Administrator)]
        public IActionResult Create()
        {
            AccountEditViewModel viewModel = new AccountEditViewModel
            {
                Roles = new SelectList(new[] { Roles.Administrator, Roles.CustomerService })
            };

            return View(viewModel);
        }

        // POST: ApplicationUsers/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        [Authorize(Roles = Roles.Administrator)]
        public async Task<IActionResult> Create(AccountViewModel user)
        {
            if (ModelState.IsValid)
            {
                PlatformUser appUser = user.ToApplicationUser();
                var result = await _userManager.CreateAsync(user.ToApplicationUser(), user.Password);
                if (result.Succeeded)
                {
                    var addedUser = await _userManager.FindByNameAsync(user.UserName);
                    result = await _userManager.AddToRoleAsync(addedUser, user.Role);
                    if (result.Succeeded)
                        return RedirectToAction("Index");
                    else
                        AddErrors(result);
                }
                else
                {
                    AddErrors(result);
                }
            }
            AccountEditViewModel viewModel = new AccountEditViewModel
            {
                User = user,
                Roles = new SelectList(new[] { Roles.Administrator, Roles.CustomerService })
            };
            return View(viewModel);
        }

        // GET: ApplicationUsers/Edit/5
        [Authorize(Roles = Roles.Administrator)]
        public async Task<IActionResult> Edit(string id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var appUser = await _userManager.FindByIdAsync(id);
            if (appUser == null)
            {
                return NotFound();
            }

            return View(new AccountEditViewModel
            {
                User = await GetAppUserViewModel(appUser),
                Roles = new SelectList(new[] { Roles.Administrator, Roles.CustomerService })
            });
        }

        // POST: ApplicationUsers/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        [Authorize(Roles = Roles.Administrator)]
        public async Task<IActionResult> Edit(string id, AccountViewModel user)
        {
            if (id != user.Id)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    var appUser = await _userManager.FindByIdAsync(id);
                    user.Update(ref appUser);
                    var result = await _userManager.UpdateAsync(appUser);
                    if (result.Succeeded)
                    {
                        var roles = await _userManager.GetRolesAsync(appUser);
                        if (roles.FirstOrDefault() != user.Role)
                        {
                            await _userManager.RemoveFromRolesAsync(appUser, roles);
                            await _userManager.AddToRoleAsync(appUser, user.Role);
                        }

                        return RedirectToAction("Index");
                    }

                    AddErrors(result);
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!ApplicationUserExists(user.Id))
                    {
                        return NotFound();
                    }
                    else
                    {
                        throw;
                    }
                }
            }
            return View(new AccountEditViewModel
            {
                User = user,
                Roles = new SelectList(new[] { Roles.Administrator, Roles.CustomerService })
            });
        }

        // GET: ApplicationUsers/Delete/5
        [Authorize(Roles = Roles.Administrator)]
        public async Task<IActionResult> Delete(string id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var appUser = await _userManager.FindByIdAsync(id);
            if (appUser == null)
            {
                return NotFound();
            }

            return View(await GetAppUserViewModel(appUser));
        }

        // POST: ApplicationUsers/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        [Authorize(Roles = Roles.Administrator)]
        public async Task<IActionResult> DeleteConfirmed(string id)
        {
            var appUser = await _userManager.FindByIdAsync(id);
            await _userManager.DeleteAsync(appUser);
            return RedirectToAction("Index");
        }

        #region Helpers

        private bool ApplicationUserExists(string id)
        {
            return _userManager.Users.Any(u => u.Id == id);
        }

        private async Task<AccountViewModel> GetAppUserViewModel(PlatformUser user)
        {
            AccountViewModel viewModel = new AccountViewModel(user);
            viewModel.Role = (await _userManager.GetRolesAsync(user)).FirstOrDefault();
            return viewModel;
        }

        private void AddErrors(IdentityResult result)
        {
            foreach (var error in result.Errors)
            {
                if (error.Code == "DuplicateUserName")
                    ModelState.AddModelError(string.Empty, "账号已经存在。");
                else
                    ModelState.AddModelError(string.Empty, error.Description);
            }
        }

        private IActionResult RedirectToLocal(string returnUrl)
        {
            if (Url.IsLocalUrl(returnUrl))
            {
                return Redirect(returnUrl);
            }
            else
            {
                return RedirectToAction(nameof(HomeController.Index), "Home");
            }
        }

        #endregion
    }
}
