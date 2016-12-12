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
using Stb.Platform.Models.OrderViewModels;
using Stb.Api.Services.Push;
using Stb.Api.Services;
using Stb.Data.Comparer;

namespace Stb.Platform.Controllers
{
    [Authorize]
    [Area(AreaNames.Platform)]
    public class OrderController : Controller
    {
        private readonly ApplicationDbContext _context;
        private readonly IPushService _pushService;

        public OrderController(ApplicationDbContext context, IPushService pushService)
        {
            _context = context;
            _pushService = pushService;
        }

        // GET: Order
        [Authorize(Roles = Roles.AdminAndCustomerService)]
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

        // GET: Order/Details/5
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

        // GET: Order/Create
        [Authorize(Roles = Roles.AdminAndCustomerService)]
        public async Task<IActionResult> Create()
        {
            ViewBag.Projects = new SelectList(await _context.Project.ToListAsync(), "Id", "Name");
            return View();
        }

        // POST: Order/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        [Authorize(Roles = Roles.AdminAndCustomerService)]
        public async Task<IActionResult> Create(OrderViewModel orderViewModel)
        {
            if (ModelState.IsValid)
            {
                OrderIndexer indexer = new OrderIndexer();
                _context.OrderIndexer.Add(indexer);

                await _context.SaveChangesAsync();

                Order order = orderViewModel.ToOrder();
                order.Id = $"{DateTime.Today:yyMMdd}{(indexer.Id + 10000):D5}";


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

                _context.Add(order);

                if (orderViewModel.WorkLoads != null)
                {
                    foreach (var workLoadViewModel in orderViewModel.WorkLoads)
                    {
                        WorkLoad workLoad = workLoadViewModel.ToWorkLoad();
                        _context.Add(workLoad);
                    }
                }

                // 添加消息
                Message message = new Message
                {
                    EndUserId = order.PlatoonId,
                    IsRead = false,
                    OrderId = order.Id,
                    Title = "新消息",
                    Text = $"工单{order.Id}已由平台下发",
                    Time = DateTime.Now,
                    Type = 1,
                    RootUserName = "平台",
                };
                _context.Message.Add(message);

                // 推送通知
                Platoon platoon = _context.Platoon.Find(order.PlatoonId);
                if (platoon?.PushId != null)
                {
                    await _pushService.PushToSingleAsync(platoon.PushId,
                        new
                        {
                            orderid = message.OrderId,
                            title = message.Title,
                            text = message.Text,
                            msgtype = message.Type,
                        });
                }

                await _context.SaveChangesAsync();

                return RedirectToAction("Index");
            }

            ViewBag.Projects = new SelectList(await _context.Project.ToListAsync(), "Id", "Name");
            return View(orderViewModel);
        }

        // GET: Order/Edit/5
        [Authorize(Roles = Roles.AdminAndCustomerService)]
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
        [Authorize(Roles = Roles.AdminAndCustomerService)]
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

        // GET: Order/Delete/5
        [Authorize(Roles = Roles.AdminAndCustomerService)]
        public async Task<IActionResult> Delete(string id)
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

            ViewBag.Projects = new SelectList(await _context.Project.ToListAsync(), "Id", "Name");
            return View(new OrderViewModel(order));
        }

        // POST: Order/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(string id)
        {
            var order = await _context.Order.SingleOrDefaultAsync(m => m.Id == id);
            _context.Order.Remove(order);
            await _context.SaveChangesAsync();
            return RedirectToAction("Index");
        }


        public async Task<IActionResult> Evaluate(string id)
        {
            OrderEvaluate_Customer evaluate = await _context.OrderEvaluate_Customer.Include(e => e.EvaluateUser).SingleOrDefaultAsync(e => e.OrderId == id);

            ViewBag.OrderId = id;
            return View(evaluate);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> SaveEvaluate(OrderEvaluate_Customer evaluate)
        {
            if (ModelState.IsValid)
            {
                evaluate.Type = 4;
                evaluate.EvaluateUserId = this.UserId();
                evaluate.Time = DateTime.Now;

                _context.OrderEvaluate_Customer.Add(evaluate);
                await _context.SaveChangesAsync();
            }
            return RedirectToAction("Evaluate", new { id = evaluate.OrderId });
        }


        private bool OrderExists(string id)
        {
            return _context.Order.Any(e => e.Id == id);
        }
    }
}
