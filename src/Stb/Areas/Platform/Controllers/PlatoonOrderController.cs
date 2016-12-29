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
using Stb.Platform.Models.WorkLoadViewModels;

namespace Stb.Platform.Controllers
{
    [Authorize]
    [Area("Platform")]
    public class PlatoonOrderController : Controller
    {

        private ApplicationDbContext _context;
        private OrderService _orderService;
        private readonly UserManager<ContractorUser> _userManager;

        public PlatoonOrderController(ApplicationDbContext context, UserManager<ContractorUser> userManager, OrderService orderService)
        {
            _context = context;
            _userManager = userManager;
            _orderService = orderService;
        }

        [Authorize(Roles = Roles.Platoon)]
        public async Task<IActionResult> Index(int page = 1)
        {
            string userId = this.UserId();
            int total = _context.Order.Count(o => o.PlatoonId == userId);
            ViewBag.TotalPage = (int)Math.Ceiling((double)total / (double)Constants.PageSize);
            ViewBag.Page = page;
            var orders = await _context.Order.Where(o => o.PlatoonId == userId)
                .Include(p => p.Contractor).Include(p => p.ContractorUser)
                .Include(p => p.Platoon).Include(p => p.District)
                .Include(p => p.LeadWorker)
                .Include(p => p.Evaluates)
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

        // GET: Order/Edit/5
        [Authorize(Roles = Roles.Platoon)]
        public async Task<IActionResult> Edit(string id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var order = await _context.Order.Include(p => p.Contractor).Include(p => p.ContractorUser).Include(p => p.Platoon).Include(p => p.District).Include(p => p.WorkLoads).ThenInclude(w => w.JobMeasurement).SingleOrDefaultAsync(m => m.Id == id);
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

                        ContractorUser contractorUser = new ContractorUser
                        {
                            Contractor = contractor,
                            Name = orderViewModel.ContractorUserName,
                            UserName = orderViewModel.ContractorUserPhone,
                        };
                        await _userManager.CreateAsync(contractorUser, "123456");
                        await _userManager.AddToRoleAsync(contractorUser, Roles.Contractor);

                        contractor.HeadUserId = contractorUser.Id;

                        order.ContractorId = contractor.Id;
                        order.ContractorUserId = contractorUser.Id;
                    }
                    else
                    {
                        Contractor contractor = await _context.Contractor.SingleAsync(c => c.Id == order.ContractorId);
                        if (contractor.Name != orderViewModel.ContractorName)
                            contractor.Name = orderViewModel.ContractorName;
                        if (order.ContractorUserId == null)
                        {
                            ContractorUser contractorUser = new ContractorUser
                            {
                                Contractor = contractor,
                                Name = orderViewModel.ContractorUserName,
                                UserName = orderViewModel.ContractorUserPhone,
                            };
                            await _userManager.CreateAsync(contractorUser, "123456");
                            await _userManager.AddToRoleAsync(contractorUser, Roles.Contractor);

                            order.ContractorUser = contractorUser;
                        }
                        else
                        {
                            ContractorUser contractorUser = await _context.ContractorUser.SingleAsync(c => c.Id == order.ContractorUserId);

                            if (contractorUser.Name != orderViewModel.ContractorUserName)
                                contractorUser.Name = orderViewModel.ContractorUserName;
                            //if (contractorStaff.Phone != orderViewModel.ContractorStaffPhone)
                            //    contractorStaff.Phone = orderViewModel.ContractorStaffPhone;
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

            var order = await _context.Order.Include(p => p.Contractor).Include(p => p.ContractorUser).Include(p => p.District).Include(p => p.LeadWorker).SingleOrDefaultAsync(m => m.Id == id);
            if (order == null)
            {
                return NotFound();
            }

            await _context.OrderWorker.Include(ow => ow.Worker).Where(ow => ow.OrderId == id && !ow.Removed).LoadAsync();

            return View(new PlatoonOrderViewModel(order));
        }

        public async Task<IActionResult> OrderEvaluate(string id, bool blank=false)
        {
            OrderEvaluate_Platoon evaluate = await _context.OrderEvaluate_Platoon.Include(e => e.EvaluateUser).SingleOrDefaultAsync(e => e.OrderId == id);

            ViewBag.OrderId = id;
            ViewBag.Blank = blank;
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

        public async Task<IActionResult> TrailEvaluate(string id, bool blank)
        {
            TrailEvaluate evaluate = await _context.TrailEvaluate.Include(e => e.EvaluateUser).SingleOrDefaultAsync(e => e.OrderId == id);

            ViewBag.OrderId = id;
            ViewBag.Blank = blank;
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

        public async Task<IActionResult> WorkLoad(string id, bool blank = false)
        {
            Order order = await _context.Order.Include(o => o.OrderWorkers).ThenInclude(ow => ow.Worker).Include(o => o.WorkLoads).ThenInclude(wl => wl.JobMeasurement).SingleOrDefaultAsync(o => o.Id == id);

            List<WorkLoad> orderWorkLoads = order.WorkLoads.Where(w => w.WorkerId == null).ToList();
            List<WorkLoadViewModel> workLoadViewModels = new List<WorkLoadViewModel>();
            foreach (var worker in order.OrderWorkers)
            {
                foreach (var orderWorkLoad in orderWorkLoads)
                {
                    var workerWorkLoad = order.WorkLoads.FirstOrDefault(w => w.WorkerId == worker.WorkerId && w.JobMeasurementId == orderWorkLoad.JobMeasurementId);
                    WorkLoadViewModel workerWorkLoadViewModel = null;
                    if (workerWorkLoad == null)
                    {
                        workerWorkLoad = orderWorkLoad;
                        workerWorkLoad.WorkerId = worker.WorkerId;
                        workerWorkLoad.Worker = worker.Worker;
                        workerWorkLoadViewModel = new WorkLoadViewModel(workerWorkLoad);
                        if (worker.WorkerId != order.LeadWorkerId)
                            workerWorkLoadViewModel.Amount = null;
                    }
                    else
                    {
                        workerWorkLoadViewModel = new WorkLoadViewModel(workerWorkLoad);
                    }
                    workLoadViewModels.Add(workerWorkLoadViewModel);
                }
            }

            OrderWorkLoadsViewModel viewModel = new OrderWorkLoadsViewModel
            {
                WorkLoads = workLoadViewModels,
                IsWorkerWorkLoadSet = order.IsWorkerWorkLoadSet,
                LeadWorkerName = order.LeadWorker?.Name,
                WorkerWorkLoadSetTime = order.WorkerWorkLoadSetTime,
                OrderId = order.Id
            };

            ViewBag.LeadWorkerId = order.LeadWorkerId;
            ViewBag.Blank = blank;

            return View(viewModel);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        [Authorize(Roles = Roles.Platoon)]
        public async Task<IActionResult> SaveWorkLoad(OrderWorkLoadsViewModel viewModel)
        {
            Order order = await _context.Order.FindAsync(viewModel.OrderId);
            if (ModelState.IsValid)
            {
                order.IsWorkerWorkLoadSet = true;
                order.WorkerWorkLoadSetTime = DateTime.Now;
                foreach (var workLoadViewModel in viewModel.WorkLoads)
                {
                    if (workLoadViewModel.Amount != null && workLoadViewModel.Amount.Value > 0)
                    {
                        var workLoad = workLoadViewModel.ToWorkLoad();
                        _context.WorkLoad.Add(workLoad);
                    }
                }
                await _context.SaveChangesAsync();
                viewModel.IsWorkerWorkLoadSet = true;
                viewModel.WorkerWorkLoadSetTime = order.WorkerWorkLoadSetTime;
                var me = await _context.Worker.FindAsync(order.LeadWorkerId);
                viewModel.LeadWorkerName = me.Name;
                ViewBag.LeadWorkerId = order.LeadWorkerId;
            }

            ViewBag.Blank = false;
            return View("WorkLoad", viewModel);
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


        private bool OrderExists(string id)
        {
            return _context.Order.Any(e => e.Id == id);
        }
    }
}