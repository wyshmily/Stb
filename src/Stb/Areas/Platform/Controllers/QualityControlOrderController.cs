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
    [Authorize(Roles = Roles.QualityControl)]
    [Area("Platform")]
    public class QualityControlOrderController : Controller
    {

        private ApplicationDbContext _context;

        public QualityControlOrderController(ApplicationDbContext context)
        {
            _context = context;
        }

        public async Task<IActionResult> Index(int page = 1)
        {
            int total = _context.Order.Count();
            ViewBag.TotalPage = (int)Math.Ceiling((double)total / (double)Constants.PageSize);
            ViewBag.Page = page;
            var orders = await _context.Order.Include(p => p.Contractor).Include(p => p.ContractorStaff)
                .Include(p => p.Platoon).Include(p => p.District)
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

        public async Task<IActionResult> Evaluate(string id)
        {
            OrderEvaluate_QualityControl evaluate = await _context.OrderEvaluate_QualityControl.Include(e=>e.EvaluateUser).SingleOrDefaultAsync(e => e.OrderId == id);

            ViewBag.OrderId = id;
            return View(evaluate);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> SaveEvaluate(OrderEvaluate_QualityControl evaluate)
        {
            if (ModelState.IsValid)
            {
                evaluate.Type = 3;
                evaluate.EvaluateUserId = this.UserId();
                evaluate.Time = DateTime.Now;

                _context.OrderEvaluate_QualityControl.Add(evaluate);
                await _context.SaveChangesAsync();
            }
            return RedirectToAction("Evaluate", new { id = evaluate.OrderId });
        }

    }
}