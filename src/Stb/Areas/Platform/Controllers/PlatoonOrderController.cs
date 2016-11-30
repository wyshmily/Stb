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
            var orders = await _context.Order.Where(o => o.PlatoonId == user.Id).Include(p => p.Contractor).Include(p => p.ContractorStaff)
                .Include(p => p.Platoon).Include(p => p.District)
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

            await _context.OrderWorker.Include(ow=>ow.Worker).Where(ow => ow.OrderId == id && !ow.Removed).LoadAsync();

            return View(new PlatoonOrderViewModel(order));
        }

        private Task<ApplicationUser> GetCurrentUserAsync()
        {
            return _userManager.GetUserAsync(HttpContext.User);
        }
    }
}