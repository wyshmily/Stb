using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Stb.Data;
using Microsoft.AspNetCore.Authorization;
using Stb.Data.Models;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Stb.Platform.Models.OrderViewModels;
using Stb.Api.Services;

namespace Stb.Platform.Controllers
{
    [Authorize(Roles = Roles.Platoon)]
    [Area("Platform")]
    public class PlatoonOrderController : Controller
    {

        private ApplicationDbContext _context;
        private UserManager<ApplicationUser> _userManager;

        public PlatoonOrderController(ApplicationDbContext context, UserManager<ApplicationUser> userManager)
        {
            _context = context;
            _userManager = userManager;
        }

        public async Task<IActionResult> Index(int page = 1)
        {
            ApplicationUser user = await GetCurrentUserAsync();
            int total = _context.Order.Count(o => o.PlatoonId == user.Id);
            ViewBag.TotalPage = (int)Math.Ceiling((double)total / (double)Constants.PageSize);
            ViewBag.Page = page;
            var orders = await _context.Order.Where(o => o.PlatoonId == user.Id)
                .Include(p => p.Contractor).Include(p => p.ContractorStaff)
                .Include(p => p.Platoon).Include(p => p.District)
                .Include(p => p.LeadWorker)
                .OrderByDescending(p => p.Id)
                .Skip((page - 1) * Constants.PageSize)
                .Take(Constants.PageSize)
                .ToListAsync();
            return View(orders.Select(p => new OrderIndexViewModel(p)).ToList());
        }

        public async Task<IActionResult> Details(string id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var order = await _context.Order.Include(p => p.Contractor).Include(p => p.ContractorStaff).Include(p => p.Platoon).Include(p => p.District).Include(p => p.Project).SingleOrDefaultAsync(m => m.Id == id);
            if (order == null)
            {
                return NotFound();
            }

            return View(new OrderViewModel(order));
        }

        public async Task<IActionResult> Organize(string id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var order = await _context.Order.Include(p => p.Contractor).Include(p => p.ContractorStaff).Include(p => p.District).Include(p => p.LeadWorker).SingleOrDefaultAsync(m => m.Id == id);
            if (order == null)
            {
                return NotFound();
            }

            await _context.OrderWorker.Include(ow => ow.Worker).Where(ow => ow.OrderId == id && !ow.Removed).LoadAsync();

            return View(new PlatoonOrderViewModel(order));
        }

        public async Task<IActionResult> OrderEvaluate(string id)
        {
            OrderEvaluate_Platoon evaluate = await _context.OrderEvaluate_Platoon.Include(e => e.EvaluateUser).SingleOrDefaultAsync(e => e.OrderId == id);

            ViewBag.OrderId = id;
            return View(evaluate);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> SaveOrderEvaluate(OrderEvaluate_Platoon evaluate)
        {
            if (ModelState.IsValid)
            {
                evaluate.Type = 1;
                evaluate.EvaluateUserId = this.UserId();
                evaluate.Time = DateTime.Now;

                _context.OrderEvaluate_Platoon.Add(evaluate);
                await _context.SaveChangesAsync();
            }
            return RedirectToAction("OrderEvaluate", new { id = evaluate.OrderId });
        }

        public async Task<IActionResult> TrailEvaluate(string id)
        {
            TrailEvaluate evaluate = await _context.TrailEvaluate.Include(e => e.EvaluateUser).SingleOrDefaultAsync(e => e.OrderId == id);

            ViewBag.OrderId = id;
            return View(evaluate);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> SaveTrailEvaluate(TrailEvaluate evaluate)
        {
            if (ModelState.IsValid)
            {
                Order order = await _context.Order.FindAsync(evaluate.OrderId);
                evaluate.Type = 2;
                evaluate.EvaluateUserId = this.UserId();
                evaluate.Time = DateTime.Now;
                evaluate.LeadWorkerId = order?.LeadWorkerId;

                _context.TrailEvaluate.Add(evaluate);
                await _context.SaveChangesAsync();
            }
            return RedirectToAction("TrailEvaluate", new { id = evaluate.OrderId });
        }

        private Task<ApplicationUser> GetCurrentUserAsync()
        {
            return _userManager.GetUserAsync(HttpContext.User);
        }
    }
}