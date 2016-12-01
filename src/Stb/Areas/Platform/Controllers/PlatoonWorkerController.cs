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
using Stb.Platform.Models.PlatoonWorkerViewModels;

namespace Stb.Platform.Controllers
{
    [Authorize]
    [Area(AreaNames.Platform)]
    public class PlatoonWorkerController : Controller
    {
        private readonly UserManager<Worker> _userManager;
        private readonly RoleManager<IdentityRole> _roleManager;
        private readonly ApplicationDbContext _context;

        public PlatoonWorkerController(UserManager<Worker> userManager, RoleManager<IdentityRole> roleManager, ApplicationDbContext context)
        {
            _userManager = userManager;
            _roleManager = roleManager;
            _context = context;
        }

        // GET: Worker
        public async Task<IActionResult> Index(int page = 1)
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

            var Worker = await _userManager.Users.Include(u => u.Header).Include(u => u.EndUserDistricts).Include(u => u.EndUserJobClasses).Include(u => u.Header).Include(u => u.Workers).Include(u => u.BestJobClass).SingleOrDefaultAsync(m => m.Id == id);
            if (Worker == null)
            {
                return NotFound();
            }

            await _context.EndUserDistrict.Include(e => e.District).Where(e => e.EndUserId == id).LoadAsync();

            await _context.EndUserJobClass.Include(e => e.JobClass).ThenInclude(c => c.JobCategory).LoadAsync();

            return View(new WorkerViewModel(Worker));
        }

        public async Task<IActionResult> Interview(string id)
        {
            if (id == null)
            {
                return NotFound();
            }

            ViewBag.WorkerId = id;
            return View(await _context.InterView.Include(i=>i.Platoon).SingleOrDefaultAsync(i => i.WorkerId == id));
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> SaveRemoteInterview(RemoteInterviewViewModel remoteInterview)
        {
            if (ModelState.IsValid)
            {
                Interview interview = remoteInterview.ToInterview();
                _context.InterView.Add(interview);
                await _context.SaveChangesAsync();
            }
            return RedirectToAction("Interview", new { id = remoteInterview.WorkerId });
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> SaveFaceInterview(FaceInterviewViewModel faceInterview)
        {
            if (ModelState.IsValid)
            {
                Interview interview = await _context.InterView.FindAsync(faceInterview.Id);
                faceInterview.UpdateInterview(ref interview);
                await _context.SaveChangesAsync();
            }
            return RedirectToAction("Interview", new { id = faceInterview.WorkerId });
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> SaveTotalInterview(TotalInterviewViewModel totalInterview)
        {
            if (ModelState.IsValid)
            {
                Interview interview = await _context.InterView.FindAsync(totalInterview.Id);
                totalInterview.UpdateInterview(ref interview);
                if(totalInterview.TotalInterviewResult == 0)
                {
                    Worker worker = await _context.Worker.FindAsync(totalInterview.WorkerId);
                    worker.IsCandidate = true;
                }
                await _context.SaveChangesAsync();
            }
            return RedirectToAction("Interview", new { id = totalInterview.WorkerId });
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
