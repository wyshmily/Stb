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
using Stb.Platform.Models.EvaluateViewModels;
using Stb.Platform.Models.WorkLoadViewModels;
using Stb.Api.Models.OrderViewModels;

namespace Stb.Platform.Controllers
{
    [Authorize]
    [Area("Platform")]
    public class WorkerOrderController : Controller
    {

        private ApplicationDbContext _context;
        private OrderService _orderService;

        public WorkerOrderController(ApplicationDbContext context, OrderService orderService)
        {
            _context = context;
            _orderService = orderService;
        }

        [Authorize(Roles = Roles.Worker)]
        public async Task<IActionResult> Index(int page = 1)
        {
            string userId = this.UserId();
            int total = _context.Order.Count(o => o.PlatoonId == userId);
            ViewBag.TotalPage = (int)Math.Ceiling((double)total / (double)Constants.PageSize);
            ViewBag.Page = page;
            var orders = await _context.Order.Where(o => o.LeadWorkerId == userId)
                .Include(p => p.Contractor).Include(p => p.ContractorStaff)
                .Include(p => p.Platoon).Include(p => p.District)
                .Include(p => p.LeadWorker)
                .OrderByDescending(p => p.CreateTime)
                .Skip((page - 1) * Constants.PageSize)
                .Take(Constants.PageSize)
                .ToListAsync();
            return View(orders.Select(p => new OrderIndexViewModel(p)).ToList());
        }

        [Authorize(Roles = Roles.Worker)]
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

        public async Task<IActionResult> Evaluate(string id)
        {
            Order order = await _context.Order.Include(o => o.OrderWorkers).ThenInclude(ow => ow.Worker).SingleOrDefaultAsync(o => o.Id == id);
            order.OrderWorkers.RemoveAll(o => o.WorkerId == order.LeadWorkerId);

            List<WorkerEvaluate> evaluates = await _context.WorkerEvaluate.Include(e => e.EvaluateUser).Include(e => e.Worker).Where(e => e.OrderId == id).ToListAsync();

            List<WorkerEvaluateViewModel> viewModelList = order.OrderWorkers.Select(w => new WorkerEvaluateViewModel(w, evaluates)).ToList();

            OrderWorkerEvaluatesViewModel viewModel = new OrderWorkerEvaluatesViewModel
            {
                WorkerEvaluates = viewModelList
            };

            ViewBag.OrderId = id;
            return View(viewModel);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        [Authorize(Roles = Roles.Worker)]
        public async Task<IActionResult> SaveEvaluate(OrderWorkerEvaluatesViewModel OrderWorkerEvaluatesViewModel)
        {
            if (ModelState.IsValid)
            {
                DateTime now = DateTime.Now;
                foreach (var evaluate in OrderWorkerEvaluatesViewModel.WorkerEvaluates)
                {
                    evaluate.Evaluate.Type = 5;
                    evaluate.Evaluate.EvaluateUserId = this.UserId();
                    evaluate.Evaluate.Time = now;
                    _context.Evaluate.Add(evaluate.Evaluate);
                }
                await _context.SaveChangesAsync();
            }
            return RedirectToAction("Evaluate", new { id = OrderWorkerEvaluatesViewModel.WorkerEvaluates[0].Evaluate.OrderId });
        }

        public async Task<IActionResult> WorkLoad(string id)
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
                        if (worker.WorkerId != this.UserId())
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

            ViewBag.UserId = this.UserId();

            return View(viewModel);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        [Authorize(Roles = Roles.Worker)]
        public async Task<IActionResult> SaveWorkLoad(OrderWorkLoadsViewModel viewModel)
        {
            if (ModelState.IsValid)
            {
                Order order = await _context.Order.FindAsync(viewModel.OrderId);
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
                var me = await _context.Worker.FindAsync(this.UserId());
                viewModel.LeadWorkerName = me.Name;
            }

            ViewBag.UserId = this.UserId();
            return View("WorkLoad", viewModel);
        }

        [Authorize(Roles = Roles.Worker)]
        public async Task<IActionResult> Signments(string id)
        {
            return View(await _orderService.GetWorkerSignmentsAsync(id));
        }

        [Authorize(Roles = Roles.Worker)]
        public async Task<IActionResult> Issues(string id)
        {
            return View(await _orderService.GetIssueAsync(id));
        }
    }
}