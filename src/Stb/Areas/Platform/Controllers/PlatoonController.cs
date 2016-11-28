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
using Stb.Platform.Models.AccountViewModels;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Stb.Platform.Models.PlatoonViewModels;
using Stb.Data.Comparer;

namespace Stb.Platform.Controllers
{
    [Authorize(Roles = Roles.PlatformUser)]
    [Area(AreaNames.Platform)]
    public class PlatoonController : Controller
    {
        private readonly UserManager<Platoon> _userManager;
        private readonly RoleManager<IdentityRole> _roleManager;
        private readonly ApplicationDbContext _context;

        public PlatoonController(UserManager<Platoon> userManager, RoleManager<IdentityRole> roleManager, ApplicationDbContext context)
        {
            _userManager = userManager;
            _roleManager = roleManager;
            _context = context;
        }

        // GET: Platoon
        public async Task<IActionResult> Index(int page = 1)
        {
            int total = _userManager.Users.Count();
            ViewBag.TotalPage = (int)Math.Ceiling((double)total / (double)Constants.PageSize);
            ViewBag.Page = page;
            return View((await _userManager.Users
                 .Skip((page - 1) * Constants.PageSize)
                 .Take(Constants.PageSize)
                 .ToListAsync())
                 .Select(u => new PlatoonIndexViewModel(u)).ToList());
        }

        // GET: Platoon/Details/5
        public async Task<IActionResult> Details(string id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var platoon = await _userManager.Users.Include(u => u.EndUserDistricts).Include(u => u.EndUserJobClasses).SingleOrDefaultAsync(m => m.Id == id);
            if (platoon == null)
            {
                return NotFound();
            }

            await _context.EndUserDistrict.Include(e => e.District).Where(e => e.EndUserId == id).LoadAsync();

            await _context.EndUserJobClass.Include(e => e.JobClass).ThenInclude(c => c.JobCategory).LoadAsync();

            return View(new PlatoonViewModel(platoon));
        }

        // GET: Platoon/Create
        public IActionResult Create()
        {
            return View(new PlatoonViewModel(new Platoon()));
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
                    if (!result.Succeeded)
                    {
                        AddErrors(result);
                        return View(platoonViewModel);
                    }

                    if (platoonViewModel.Districts != null)
                    {
                        var provinces = platoonViewModel.Districts.Select(d => d.ToProvince()).Distinct(new ProvinceComparer());

                        var cities = platoonViewModel.Districts.Select(d => d.ToCity()).Distinct(new CityComparer());

                        var districts = platoonViewModel.Districts.Select(d => d.ToDistrict()).Distinct(new DistrictComparer());

                        var endUserDistricts = districts.Select(d => new EndUserDistrict
                        {
                            DistrictId = d.Id,
                            EndUserId = addedUser.Id,
                        });

                        foreach (var province in provinces)
                        {
                            if (!await _context.Province.AnyAsync(p => p.Id == province.Id))
                                _context.Add(province);
                        }

                        foreach (var city in cities)
                        {
                            if (!await _context.City.AnyAsync(c => c.Id == city.Id))
                                _context.Add(city);
                        }

                        foreach (var district in districts)
                        {
                            if (!await _context.District.AnyAsync(c => c.Id == district.Id))
                                _context.Add(district);
                        }
                        _context.EndUserDistrict.AddRange(endUserDistricts);
                    }

                    if (platoonViewModel.JobClasses != null)
                    {
                        var endUserJobClasses = platoonViewModel.JobClasses.Select(c => new EndUserJobClass
                        {
                            EndUserId = addedUser.Id,
                            JobClassId = c.JobClassId,
                        }).ToList();

                        _context.EndUserJobClass.AddRange(endUserJobClasses);
                    }

                    await _context.SaveChangesAsync();
                    return RedirectToAction("Index");
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

            var platoon = await _userManager.Users.Include(u => u.EndUserDistricts).Include(u => u.EndUserJobClasses).SingleOrDefaultAsync(m => m.Id == id);
            if (platoon == null)
            {
                return NotFound();
            }

            await _context.EndUserDistrict.Include(e => e.District).Where(e => e.EndUserId == id).LoadAsync();

            await _context.EndUserJobClass.Include(e => e.JobClass).ThenInclude(c => c.JobCategory).LoadAsync();

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
                    if (!result.Succeeded)
                    {
                        AddErrors(result);
                        return View(platoonViewModel);
                    }

                    List<EndUserDistrict> curEndUserDistricts = await _context.EndUserDistrict.Where(e => e.EndUserId == id).ToListAsync();
                    if (platoonViewModel.Districts == null)
                    {
                        _context.EndUserDistrict.RemoveRange(curEndUserDistricts);
                    }
                    else
                    {
                        var provinces = platoonViewModel.Districts.Select(d => d.ToProvince()).Distinct(new ProvinceComparer());

                        var cities = platoonViewModel.Districts.Select(d => d.ToCity()).Distinct(new CityComparer());

                        var districts = platoonViewModel.Districts.Select(d => d.ToDistrict()).Distinct(new DistrictComparer());

                        var endUserDistricts = districts.Select(d => new EndUserDistrict
                        {
                            DistrictId = d.Id,
                            EndUserId = platoon.Id,
                        });

                        foreach (var province in provinces)
                        {
                            if (!await _context.Province.AnyAsync(p => p.Id == province.Id))
                                _context.Add(province);
                        }

                        foreach (var city in cities)
                        {
                            if (!await _context.City.AnyAsync(c => c.Id == city.Id))
                                _context.Add(city);
                        }

                        foreach (var district in districts)
                        {
                            if (!await _context.District.AnyAsync(c => c.Id == district.Id))
                                _context.Add(district);
                        }

                        _context.EndUserDistrict.RemoveRange(curEndUserDistricts.Except(endUserDistricts, new EndUserDistrictComparer()));
                        _context.EndUserDistrict.AddRange(endUserDistricts.Except(curEndUserDistricts, new EndUserDistrictComparer()));
                    }

                    List<EndUserJobClass> curEndUserJobClasses = await _context.EndUserJobClass.Where(e => e.EndUserId == id).ToListAsync();
                    if (platoonViewModel.JobClasses == null)
                    {
                        _context.EndUserJobClass.RemoveRange(curEndUserJobClasses);
                    }
                    else
                    {
                        var endUserJobClasses = platoonViewModel.JobClasses.Select(d => new EndUserJobClass
                        {
                            JobClassId = d.JobClassId,
                            EndUserId = platoon.Id,
                        });

                        _context.EndUserJobClass.RemoveRange(curEndUserJobClasses.Except(endUserJobClasses, new EndUserJobClassComparer()));
                        _context.EndUserJobClass.AddRange(endUserJobClasses.Except(curEndUserJobClasses, new EndUserJobClassComparer()));
                    }

                    await _context.SaveChangesAsync();
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

            var platoon = await _userManager.Users.Include(u => u.EndUserDistricts).Include(u => u.EndUserJobClasses).SingleOrDefaultAsync(m => m.Id == id);
            if (platoon == null)
            {
                return NotFound();
            }

            await _context.EndUserDistrict.Include(e => e.District).ThenInclude(d => d.City).ThenInclude(c => c.Province).Where(e => e.EndUserId == id).LoadAsync();

            await _context.EndUserJobClass.Include(e => e.JobClass).ThenInclude(c => c.JobCategory).LoadAsync();


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
