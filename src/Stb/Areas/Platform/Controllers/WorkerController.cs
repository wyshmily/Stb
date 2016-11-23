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
using Stb.Platform.Models.WorkerViewModels;
using Stb.Data.Comparer;

namespace Stb.Platform.Controllers
{
    [Authorize]
    [Area(AreaNames.Platform)]
    public class WorkerController : Controller
    {
        private readonly UserManager<Worker> _userManager;
        private readonly RoleManager<IdentityRole> _roleManager;
        private readonly ApplicationDbContext _context;

        public WorkerController(UserManager<Worker> userManager, RoleManager<IdentityRole> roleManager, ApplicationDbContext context)
        {
            _userManager = userManager;
            _roleManager = roleManager;
            _context = context;
        }

        // GET: Worker
        public async Task<IActionResult> Index(int page=1)
        {
            int total = _userManager.Users.Count();
            ViewBag.TotalPage = (int)Math.Ceiling((double)total / (double)Constants.PageSize);
            ViewBag.Page = page;
            return View((await _userManager.Users
                 .Skip((page - 1) * Constants.PageSize)
                 .Take(Constants.PageSize)
                 .ToListAsync())
                 .Select(u => new WorkerIndexViewModel(u)).ToList());
        }

        // GET: Worker/Details/5
        public async Task<IActionResult> Details(string id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var Worker = await _userManager.Users.Include(u => u.Header).Include(u => u.EndUserDistricts).Include(u => u.EndUserJobClasses).Include(u => u.Header).Include(u=>u.Workers).Include(u => u.BestJobClass).SingleOrDefaultAsync(m => m.Id == id);
            if (Worker == null)
            {
                return NotFound();
            }

            await _context.EndUserDistrict.Include(e => e.District).Where(e => e.EndUserId == id).LoadAsync();

            await _context.EndUserJobClass.Include(e => e.JobClass).ThenInclude(c => c.JobCategory).LoadAsync();

            return View(new WorkerViewModel(Worker));
        }

        // GET: Worker/Create
        public IActionResult Create()
        {
            return View(new WorkerViewModel(new Worker()));
        }

        // POST: Worker/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create(WorkerViewModel WorkerViewModel)
        {
            if (ModelState.IsValid)
            {
                Worker Worker = WorkerViewModel.ToWorker();
                var result = await _userManager.CreateAsync(Worker, WorkerViewModel.Password);
                if (result.Succeeded)
                {
                    var addedUser = await _userManager.FindByNameAsync(Worker.UserName);
                    result = await _userManager.AddToRoleAsync(addedUser, Roles.Worker);
                    if (!result.Succeeded)
                    {
                        AddErrors(result);
                        return View(WorkerViewModel);
                    }

                    if (WorkerViewModel.Districts != null)
                    {
                        var provinces = WorkerViewModel.Districts.Select(d => d.ToProvince()).Distinct(new ProvinceComparer());

                        var cities = WorkerViewModel.Districts.Select(d => d.ToCity()).Distinct(new CityComparer());

                        var districts = WorkerViewModel.Districts.Select(d => d.ToDistrict()).Distinct(new DistrictComparer());

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

                    if (WorkerViewModel.JobClasses != null)
                    {
                        var endUserJobClasses = WorkerViewModel.JobClasses.Select(c => new EndUserJobClass
                        {
                            EndUserId = addedUser.Id,
                            JobClassId = c.JobClassId,
                            Grade = c.Grade,
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
            return View(WorkerViewModel);
        }

        // GET: Worker/Edit/5
        public async Task<IActionResult> Edit(string id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var Worker = await _userManager.Users.Include(u => u.EndUserDistricts).Include(u => u.EndUserJobClasses).Include(u => u.Header).SingleOrDefaultAsync(m => m.Id == id);
            if (Worker == null)
            {
                return NotFound();
            }

            await _context.EndUserDistrict.Include(e => e.District).ThenInclude(d => d.City).ThenInclude(c => c.Province).Where(e => e.EndUserId == id).LoadAsync();

            await _context.EndUserJobClass.Include(e => e.JobClass).ThenInclude(c => c.JobCategory).LoadAsync();

            return View(new WorkerViewModel(Worker));
        }

        // POST: Worker/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(string id, WorkerViewModel WorkerViewModel)
        {
            if (id != WorkerViewModel.Id)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    var Worker = await _userManager.FindByIdAsync(id);
                    if(Worker.IsHeader && !WorkerViewModel.IsHead)
                    {
                        List<Worker> workers = _context.Worker.Where(w => w.HeaderId == id).ToList();
                        workers.ForEach(w => w.HeaderId = null);
                    }
                    WorkerViewModel.Update(ref Worker);
                    var result = await _userManager.UpdateAsync(Worker);


                    if (!result.Succeeded)
                    {
                        AddErrors(result);
                        return View(WorkerViewModel);
                    }

                    List<EndUserDistrict> curEndUserDistricts = await _context.EndUserDistrict.Where(e => e.EndUserId == id).ToListAsync();
                    if (WorkerViewModel.Districts == null)
                    {
                        _context.EndUserDistrict.RemoveRange(curEndUserDistricts);
                    }
                    else
                    {
                        var provinces = WorkerViewModel.Districts.Select(d => d.ToProvince()).Distinct(new ProvinceComparer());

                        var cities = WorkerViewModel.Districts.Select(d => d.ToCity()).Distinct(new CityComparer());

                        var districts = WorkerViewModel.Districts.Select(d => d.ToDistrict()).Distinct(new DistrictComparer());

                        var endUserDistricts = districts.Select(d => new EndUserDistrict
                        {
                            DistrictId = d.Id,
                            EndUserId = Worker.Id,
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
                    if (WorkerViewModel.JobClasses == null)
                    {
                        _context.EndUserJobClass.RemoveRange(curEndUserJobClasses);
                    }
                    else
                    {
                        var endUserJobClasses = WorkerViewModel.JobClasses.Select(d => new EndUserJobClass
                        {
                            JobClassId = d.JobClassId,
                            EndUserId = Worker.Id,
                            Grade = d.Grade
                        });

                        _context.EndUserJobClass.RemoveRange(curEndUserJobClasses.Except(endUserJobClasses, new EndUserJobClassComparer()));
                        _context.EndUserJobClass.AddRange(endUserJobClasses.Except(curEndUserJobClasses, new EndUserJobClassComparer()));
                    }

                    await _context.SaveChangesAsync();
                    return RedirectToAction("Index");
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!WorkerExists(WorkerViewModel.Id))
                    {
                        return NotFound();
                    }
                    else
                    {
                        throw;
                    }
                }
            }

            return View(WorkerViewModel);
        }

        // GET: Worker/Delete/5
        public async Task<IActionResult> Delete(string id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var Worker = await _userManager.Users.Include(u => u.EndUserDistricts).Include(u => u.EndUserJobClasses).Include(u => u.Header).Include(u => u.Workers).Include(u=>u.BestJobClass).SingleOrDefaultAsync(m => m.Id == id);
            if (Worker == null)
            {
                return NotFound();
            }

            await _context.EndUserDistrict.Include(e => e.District).Where(e => e.EndUserId == id).LoadAsync();

            await _context.EndUserJobClass.Include(e => e.JobClass).ThenInclude(c => c.JobCategory).LoadAsync();


            return View(new WorkerViewModel(Worker));
        }

        // POST: Worker/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(string id)
        {
            Worker worker = await _userManager.Users.Include(u=>u.Workers).Include(u=>u.LeadOrders).Include(u=>u.LeadOrders).SingleOrDefaultAsync(m => m.Id == id);
            foreach(var subWorker in worker.Workers)
            {
                subWorker.HeaderId = null;
            }
            foreach(var order in worker.LeadOrders)
            {
                order.LeadWorkerId = null;
            }
            await _userManager.DeleteAsync(worker);
            return RedirectToAction("Index");
        }


        #region Helpers
        private bool WorkerExists(string id)
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
