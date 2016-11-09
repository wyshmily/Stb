using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using Stb.Data;
using Stb.Data.Models;
using Microsoft.AspNetCore.Authorization;
using Stb.Data.Models.AccountViewModels;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Stb.Data.Models.PlatoonViewModels;

namespace Stb.Platform.Controllers
{
    [Authorize]
    public class PlatoonController : Controller
    {
        private readonly UserManager<Platoon> _userManager;
        private readonly RoleManager<IdentityRole> _roleManager;

        public PlatoonController(UserManager<Platoon> userManager, RoleManager<IdentityRole> roleManager)
        {
            _userManager = userManager;
            _roleManager = roleManager;
        }

        // GET: Platoon
        public async Task<IActionResult> Index()
        {
            return View((await _userManager.Users.ToListAsync()).Select(u=>new PlatoonIndexViewModel(u)).ToList());
        }

        // GET: Platoon/Details/5
        public async Task<IActionResult> Details(string id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var platoon = await _userManager.Users.SingleOrDefaultAsync(m => m.Id == id);
            if (platoon == null)
            {
                return NotFound();
            }

            return View(new PlatoonViewModel(platoon));
        }

        // GET: Platoon/Create
        public IActionResult Create()
        {
            return View();
        }

        // POST: Platoon/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create(PlatoonViewModel platoonViewModel)
        {
            if (ModelState.IsValid)
            {
                Platoon platoon = platoonViewModel.ToPlatoon();
                var result = await _userManager.CreateAsync(platoon, platoonViewModel.Password);
                if (result.Succeeded)
                {
                    var addedUser = await _userManager.FindByNameAsync(platoon.UserName);
                    result = await _userManager.AddToRoleAsync(addedUser, Roles.Platoon);
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
            return View(platoonViewModel);
        }

        // GET: Platoon/Edit/5
        public async Task<IActionResult> Edit(string id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var platoon = await _userManager.Users.SingleOrDefaultAsync(m => m.Id == id);
            if (platoon == null)
            {
                return NotFound();
            }
            return View(new PlatoonViewModel(platoon));
        }

        // POST: Platoon/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(string id, PlatoonViewModel platoonViewModel)
        {
            if (id != platoonViewModel.Id)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    var platoon = await _userManager.FindByIdAsync(id);
                    platoonViewModel.Update(ref platoon);
                    var result = await _userManager.UpdateAsync(platoon);

                    return RedirectToAction("Index");
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!PlatoonExists(platoonViewModel.Id))
                    {
                        return NotFound();
                    }
                    else
                    {
                        throw;
                    }
                }
            }

            return View(platoonViewModel);
        }

        // GET: Platoon/Delete/5
        public async Task<IActionResult> Delete(string id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var platoon = await _userManager.Users.SingleOrDefaultAsync(m => m.Id == id);
            if (platoon == null)
            {
                return NotFound();
            }

            return View(new PlatoonViewModel(platoon));
        }

        // POST: Platoon/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(string id)
        {
            var platoon = await _userManager.Users.SingleOrDefaultAsync(m => m.Id == id);
            await _userManager.DeleteAsync(platoon);
            return RedirectToAction("Index");
        }


        #region Helpers
        private bool PlatoonExists(string id)
        {
            return _userManager.Users.Any(e => e.Id == id);
        }

        private void AddErrors(IdentityResult result)
        {
            foreach (var error in result.Errors)
            {
                if (error.Code == "DuplicateUserName")
                    ModelState.AddModelError(string.Empty, "’À∫≈“—æ≠¥Ê‘⁄°£");
                else
                    ModelState.AddModelError(string.Empty, error.Description);
            }
        }

        #endregion Helpers
    }
}
