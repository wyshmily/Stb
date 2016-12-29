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
using Stb.Platform.Models.EvaluateViewModels;

namespace Stb.Platform.Controllers
{
    [Area("Platform")]
    public class CustomerController : Controller
    {

        private ApplicationDbContext _context;
        private OrderService _orderService;

        public CustomerController(ApplicationDbContext context, OrderService orderService)
        {
            _context = context;
            _orderService = orderService;
        }

        public IActionResult Index()
        {
            return View();
        }

        public IActionResult OrderNotFound()
        {
            return View();
        }

        public async Task<IActionResult> Details(string id)
        {
            if (id == null)
            {
                ViewBag.Error = "您所查找的工单不存在！";
                return View("Index");
            }

            var order = await _context.Order.Include(p => p.Contractor).Include(p => p.ContractorUser).Include(p => p.Platoon).Include(p => p.District).Include(p => p.Project).Include(p => p.OrderWorkers).ThenInclude(ow => ow.Worker).Include(p => p.Evaluates).Include(p => p.WorkLoads).ThenInclude(w => w.JobMeasurement).SingleOrDefaultAsync(m => m.Id == id);
            if (order == null)
            {
                ViewBag.Error = "您所查找的工单不存在！";
                return View("Index");
            }

            ProgressData progressData = null;
            if (order.State != 0)
            {
                progressData = await _orderService.GetOrderProgressAsync(id);
            }

            return View(new OrderViewModel(order, progressData));
        }

        public async Task<IActionResult> Signments(string id)
        {
            return View(await _orderService.GetWorkerSignmentsAsync(id));
        }

        public async Task<IActionResult> Issues(string id)
        {
            return View(await _orderService.GetIssueAsync(id));
        }


        public async Task<IActionResult> OrderEvaluate(string id)
        {
            OrderEvaluate_Platoon evaluate = await _context.OrderEvaluate_Platoon.Include(e => e.EvaluateUser).SingleOrDefaultAsync(e => e.OrderId == id);

            ViewBag.OrderId = id;
            //ViewBag.Blank = blank;
            return View(evaluate);
        }

        public async Task<IActionResult> TrailEvaluate(string id)
        {
            TrailEvaluate evaluate = await _context.TrailEvaluate.Include(e => e.EvaluateUser).SingleOrDefaultAsync(e => e.OrderId == id);

            ViewBag.OrderId = id;
            //ViewBag.Blank = blank;
            return View(evaluate);
        }

        public async Task<IActionResult> QualityControlEvaluate(string id)
        {
            OrderEvaluate_QualityControl evaluate = await _context.OrderEvaluate_QualityControl.Include(e => e.EvaluateUser).SingleOrDefaultAsync(e => e.OrderId == id);

            ViewBag.OrderId = id;
            //ViewBag.Blank = blank;
            return View(evaluate);
        }

        public async Task<IActionResult> CustomerEvaluate(string id)
        {
            OrderEvaluate_Customer evaluate = await _context.OrderEvaluate_Customer.Include(e => e.EvaluateUser).SingleOrDefaultAsync(e => e.OrderId == id);

            ViewBag.OrderId = id;
            return View(evaluate);
        }

        public async Task<IActionResult> WorkerEvaluate(string id)
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
            //ViewBag.Blank = blank;
            return View(viewModel);
        }
    }
}