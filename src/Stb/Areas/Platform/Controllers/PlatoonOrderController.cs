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
using Microsoft.AspNetCore.Mvc.Rendering;
using Stb.Data.Comparer;
using Stb.Api.Models.OrderViewModels;

namespace Stb.Platform.Controllers
{
    [Authorize]
    [Area("Platform")]
    public class PlatoonOrderController : Controller
    {

        private ApplicationDbContext _context;
        private UserManager<ApplicationUser> _userManager;
        private OrderService _orderService;

        public PlatoonOrderController(ApplicationDbContext context, UserManager<ApplicationUser> userManager, OrderService orderService)
        {
            _context = context;
            _userManager = userManager;
            _orderService = orderService;
        }

        [Authorize(Roles = Roles.Platoon)]
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
                .OrderByDescending(p => p.CreateTime)
                .Skip((page - 1) * Constants.PageSize)
                .Take(Constants.PageSize)
                .ToListAsync();
            return View(orders.Select(p => new OrderIndexViewModel(p)).ToList());
        }

        [Authorize(Roles = Roles.Platoon)]
        public async Task<IActionResult> Details(string id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var order = await _context.Order.Include(p => p.Contractor).Include(p => p.ContractorStaff).Include(p => p.Platoon).Include(p => p.District).Include(p => p.Project).Include(p => p.OrderWorkers).ThenInclude(ow => ow.Worker).Include(p => p.Evaluates).Include(p => p.WorkLoads).ThenInclude(w => w.JobMeasurement).SingleOrDefaultAsync(m => m.Id == id);
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

        // GET: Order/Edit/5
        [Authorize(Roles = Roles.Platoon)]
        public async Task<IActionResult> Edit(string id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var order = await _context.Order.Include(p => p.Contractor).Include(p => p.ContractorStaff).Include(p => p.Platoon).Include(p => p.District).Include(p => p.WorkLoads).ThenInclude(w => w.JobMeasurement).SingleOrDefaultAsync(m => m.Id == id);
            if (order == null)
            {
                return NotFound();
            }
            ViewBag.Projects = new SelectList(await _context.Project.ToListAsync(), "Id", "Name");
            return View(new OrderViewModel(order));
        }

        // POST: Order/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        [Authorize(Roles = Roles.Platoon)]
        public async Task<IActionResult> Edit(string id, OrderViewModel orderViewModel)
        {
            if (id != orderViewModel.Id)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    Order order = orderViewModel.ToOrder();
                    if (order.ContractorId == null)
                    {
                        Contractor contractor = new Contractor
                        {
                            Enabled = true,
                            Name = orderViewModel.ContractorName,
                        };
                        _context.Add(contractor);

                        ContractorStaff contractorStaff = new ContractorStaff
                        {
                            Contractor = contractor,
                            Name = orderViewModel.ContractorStaffName,
                            Phone = orderViewModel.ContractorStaffPhone,
                        };
                        _context.Add(contractorStaff);

                        await _context.SaveChangesAsync();

                        contractor.HeadStaffId = contractorStaff.Id;

                        order.ContractorId = contractor.Id;
                        order.ContractorStaffId = contractorStaff.Id;
                    }
                    else
                    {
                        Contractor contractor = await _context.Contractor.SingleAsync(c => c.Id == order.ContractorId);
                        if (contractor.Name != orderViewModel.ContractorName)
                            contractor.Name = orderViewModel.ContractorName;
                        if (order.ContractorStaffId == null)
                        {
                            ContractorStaff contractorStaff = new ContractorStaff
                            {
                                Contractor = contractor,
                                Name = orderViewModel.ContractorStaffName,
                                Phone = orderViewModel.ContractorStaffPhone,
                            };
                            _context.Add(contractorStaff);

                            order.ContractorStaff = contractorStaff;
                        }
                        else
                        {
                            ContractorStaff contractorStaff = await _context.ContractorStaff.SingleAsync(c => c.Id == order.ContractorStaffId);

                            if (contractorStaff.Name != orderViewModel.ContractorStaffName)
                                contractorStaff.Name = orderViewModel.ContractorStaffName;
                            if (contractorStaff.Phone != orderViewModel.ContractorStaffPhone)
                                contractorStaff.Phone = orderViewModel.ContractorStaffPhone;
                        }
                    }
                    _context.Update(order);

                    List<WorkLoad> curWorkLoads = await _context.WorkLoad.Where(w => w.OrderId == id && w.WorkerId == null).ToListAsync();
                    List<WorkLoad> workLoads = orderViewModel.WorkLoads.Select(w => w.ToWorkLoad()).ToList();

                    _context.WorkLoad.RemoveRange(curWorkLoads.Except(workLoads, new WorkLoadComparer()));
                    _context.WorkLoad.AddRange(workLoads.Except(curWorkLoads, new WorkLoadComparer()));

                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!OrderExists(orderViewModel.Id))
                    {
                        return NotFound();
                    }
                    else
                    {
                        throw;
                    }
                }
                return RedirectToAction("Index");
            }
            ViewBag.Projects = new SelectList(await _context.Project.ToListAsync(), "Id", "Name");
            return View(orderViewModel);
        }

        [Authorize(Roles = Roles.Platoon)]
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
        [Authorize(Roles = Roles.Platoon)]
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
        [Authorize(Roles = Roles.Platoon)]
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

        [Authorize(Roles = Roles.Platoon)]
        public async Task<IActionResult> Signments(string id)
        {
            return View(await _orderService.GetWorkerSignmentsAsync(id));
        }

        [Authorize(Roles = Roles.Platoon)]
        public async Task<IActionResult> Issues(string id)
        {
            return View(await _orderService.GetIssueAsync(id));
        }

        private Task<ApplicationUser> GetCurrentUserAsync()
        {
            return _userManager.GetUserAsync(HttpContext.User);
        }

        private bool OrderExists(string id)
        {
            return _context.Order.Any(e => e.Id == id);
        }
    }
}