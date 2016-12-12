using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using Stb.Data;
using Stb.Data.Models;
using Stb.Platform.Models.ContractorViewModels;
using Microsoft.AspNetCore.Authorization;

namespace Stb.Platform.Controllers
{
    [Authorize(Roles = Roles.AdminAndCustomerService)]
    [Area(AreaNames.Platform)]
    public class ContractorController : Controller
    {
        private readonly ApplicationDbContext _context;

        public ContractorController(ApplicationDbContext context)
        {
            _context = context;
        }

        // GET: Contractors
        public async Task<IActionResult> Index(int page = 1)
        {
            int total = _context.Contractor.Count();
            ViewBag.TotalPage = (int)Math.Ceiling((double)total / (double)Constants.PageSize);
            ViewBag.Page = page;

            return View(
                await (from c in _context.Contractor
                       join s in _context.ContractorStaff on c.HeadStaffId equals s.Id into temp
                       from tt in temp.DefaultIfEmpty()
                       select new ContractorViewModel
                       {
                           Contractor = c,
                           HeadStaff = tt
                       })
                       .Skip((page - 1) * Constants.PageSize)
                       .Take(Constants.PageSize)
                       .ToListAsync());
        }

        // GET: Contractors/Details/5
        [Authorize]

        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            ContractorViewModel viewModel =
                await (from c in _context.Contractor
                       join s in _context.ContractorStaff on c.HeadStaffId equals s.Id into temp
                       from tt in temp.DefaultIfEmpty()
                       where c.Id == id
                       select new ContractorViewModel
                       {
                           Contractor = c,
                           HeadStaff = tt
                       }).FirstOrDefaultAsync();

            if (viewModel == null)
            {
                return NotFound();
            }

            return View(viewModel);
        }

        // GET: Contractors/Create
        public IActionResult Create()
        {
            return View();
        }

        // POST: Contractors/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create(Contractor contractor, ContractorStaff headStaff)
        {
            if (ModelState.IsValid)
            {
                headStaff.Contractor = contractor;
                _context.Add(headStaff);
                await _context.SaveChangesAsync();

                contractor.HeadStaffId = headStaff.Id;
                await _context.SaveChangesAsync();

                return RedirectToAction("Index");
            }
            return View(new ContractorViewModel { Contractor = contractor, HeadStaff = headStaff });
        }

        // GET: Contractors/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var contractor = await _context.Contractor.Include(c => c.Staffs).SingleOrDefaultAsync(m => m.Id == id);
            if (contractor == null)
            {
                return NotFound();
            }

            return View(contractor);
        }

        // POST: Contractors/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, Contractor contractor)
        {
            if (id != contractor.Id)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(contractor);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!ContractorExists(contractor.Id))
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
            contractor.Staffs = await _context.ContractorStaff.Where(s => s.ContractorId == id).ToListAsync();
            return View(contractor);
        }

        // GET: Contractors/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            ContractorViewModel viewModel =
                await (from c in _context.Contractor
                       join s in _context.ContractorStaff on c.HeadStaffId equals s.Id into temp
                       from tt in temp.DefaultIfEmpty()
                       where c.Id == id
                       select new ContractorViewModel
                       {
                           Contractor = c,
                           HeadStaff = tt
                       }).FirstOrDefaultAsync();

            if (viewModel == null)
            {
                return NotFound();
            }

            return View(viewModel);
        }

        // POST: Contractors/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var contractor = await _context.Contractor.Include(c => c.Staffs).SingleOrDefaultAsync(m => m.Id == id);
            _context.Contractor.Remove(contractor);
            await _context.SaveChangesAsync();
            return RedirectToAction("Index");
        }

        private bool ContractorExists(int id)
        {
            return _context.Contractor.Any(e => e.Id == id);
        }
    }
}
