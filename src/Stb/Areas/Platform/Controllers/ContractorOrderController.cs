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
using Stb.Api.Models.OrderViewModels;

namespace Stb.Platform.Controllers
{
    [Authorize]
    [Area("Platform")]
    public class ContractorOrderController : Controller
    {

        private ApplicationDbContext _context;
        private OrderService _orderService;

        public ContractorOrderController(ApplicationDbContext context, OrderService orderService)
        {
            _context = context;
            _orderService = orderService;
        }

        [Authorize(Roles = Roles.Contractor)]
        public async Task<IActionResult> Index(int page = 1)
        {
            var user = await _context.ContractorUser.FindAsync(this.UserId());
            int contractorId = user.ContractorId;
            int total = _context.Order.Count();
            ViewBag.TotalPage = (int)Math.Ceiling((double)total / (double)Constants.PageSize);
            ViewBag.Page = page;
            var orders = await _context.Order.Include(p => p.Contractor).Include(p => p.ContractorUser)
                .Include(p => p.Platoon).Include(p => p.District)
                .Where(p=>p.ContractorId == contractorId)
                .OrderByDescending(p => p.CreateTime)
                .Skip((page - 1) * Constants.PageSize)
                .Take(Constants.PageSize)
                .ToListAsync();
            return View(orders.Select(p => new OrderIndexViewModel(p)).ToList());
        }

        [Authorize(Roles = Roles.Contractor)]
        public async Task<IActionResult> Details(string id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var order = await _context.Order.Include(p => p.Contractor).Include(p => p.ContractorUser).Include(p => p.Platoon).Include(p => p.District).Include(p => p.Project).Include(p => p.OrderWorkers).ThenInclude(ow => ow.Worker).Include(p => p.Evaluates).Include(p => p.WorkLoads).ThenInclude(w => w.JobMeasurement).SingleOrDefaultAsync(m => m.Id == id);
            if (order == null)
            {
                return NotFound();
            }

            ProgressData progressData = null;
            if (order.State != 0)
            {
                progressData = await _orderService.GetOrderProgressAsync(id);
            }

            return View(new OrderViewModel(order, progressData));
        }

        [Authorize(Roles = Roles.Contractor)]
        public async Task<IActionResult> Signments(string id)
        {
            return View(await _orderService.GetWorkerSignmentsAsync(id));
        }

        [Authorize(Roles = Roles.Contractor)]
        public async Task<IActionResult> Issues(string id)
        {
            return View(await _orderService.GetIssueAsync(id));
        }

    }
}