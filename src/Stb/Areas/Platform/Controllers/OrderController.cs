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

namespace Stb.Platform.Controllers
{
    [Authorize(Roles = Roles.PlatformUser)]
    [Area(AreaNames.Platform)]
    public class OrderController : Controller
    {
        private readonly ApplicationDbContext _context;

        public OrderController(ApplicationDbContext context)
        {
            _context = context;
        }

        // GET: Order
        public async Task<IActionResult> Index(int page = 1)
        {
            int total = _context.Order.Count();
            ViewBag.TotalPage = (int)Math.Ceiling((double)total / (double)Constants.PageSize);
            ViewBag.Page = page;
            var orders = await _context.Order.Include(p => p.Contractor).Include(p => p.ContractorStaff)
                .Include(p => p.Platoon).Include(p => p.District)
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
        public async Task<IActionResult> Create(OrderViewModel orderViewModel)
        {
            if (ModelState.IsValid)
            {
                OrderIndexer indexer = new OrderIndexer();
                _context.OrderIndexer.Add(indexer);

                await _context.SaveChangesAsync();

                Order order = orderViewModel.ToOrder();
                order.Id = $"pb{DateTime.Today:yyyyMMdd}{(indexer.Id + 10000):D5}";


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
                await _context.SaveChangesAsync();
                return RedirectToAction("Index");
            }

            ViewBag.Projects = new SelectList(await _context.Project.ToListAsync(), "Id", "Name");
            return View(orderViewModel);
        }

        // GET: Order/Edit/5
        public async Task<IActionResult> Edit(string id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var order = await _context.Order.Include(p => p.Contractor).Include(p => p.ContractorStaff).Include(p => p.Platoon).Include(p => p.District).SingleOrDefaultAsync(m => m.Id == id);
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

        private bool OrderExists(string id)
        {
            return _context.Order.Any(e => e.Id == id);
        }
    }
}
