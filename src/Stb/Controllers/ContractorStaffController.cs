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

namespace Stb.Platform.Controllers
{
    [Authorize/*(Policy = "AdministratorOnly")*/]
    public class ContractorStaffController : Controller
    {
        private readonly ApplicationDbContext _context;

        public ContractorStaffController(ApplicationDbContext context)
        {
            _context = context;
        }

        // GET: ContractorStaff
        public async Task<IActionResult> Index(int id)
        {
            ViewData["Contractor"] = _context.Contractor.Single(c => c.Id == id);
            return View(await _context.ContractorStaff.Include(s => s.Contractor).Where(s => s.ContractorId == id).ToListAsync());
        }

        // GET: ContractorStaff/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var contractorStaff = await _context.ContractorStaff.Include(s => s.Contractor).SingleOrDefaultAsync(m => m.Id == id);
            if (contractorStaff == null)
            {
                return NotFound();
            }

            return View(contractorStaff);
        }

        // GET: ContractorStaff/Create
        public async Task<IActionResult> Create(int id)
        {
            Contractor contractor = await _context.Contractor.SingleOrDefaultAsync(c => c.Id == id);
            ContractorStaff staff = new ContractorStaff
            {
                Contractor = contractor,
                ContractorId = contractor.Id
            };
            return View(staff);
        }

        // POST: ContractorStaff/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("ContractorId,Name,Phone")] ContractorStaff contractorStaff)
        {
            if (ModelState.IsValid)
            {
                _context.Add(contractorStaff);
                await _context.SaveChangesAsync();
                return RedirectToAction("Index", new { id = contractorStaff.ContractorId });
            }
            return View(contractorStaff);
        }

        // GET: ContractorStaff/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var contractorStaff = await _context.ContractorStaff.Include(s => s.Contractor).SingleOrDefaultAsync(m => m.Id == id);
            if (contractorStaff == null)
            {
                return NotFound();
            }
            return View(contractorStaff);
        }

        // POST: ContractorStaff/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("Id,ContractorId,Name,Phone")] ContractorStaff contractorStaff)
        {
            if (id != contractorStaff.Id)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(contractorStaff);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!ContractorStaffExists(contractorStaff.Id))
                    {
                        return NotFound();
                    }
                    else
                    {
                        throw;
                    }
                }
                return RedirectToAction("Index", new { id = contractorStaff.ContractorId });
            }
            return View(contractorStaff);
        }

        // GET: ContractorStaff/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var contractorStaff = await _context.ContractorStaff.Include(s=>s.Contractor).SingleOrDefaultAsync(m => m.Id == id);
            if (contractorStaff == null)
            {
                return NotFound();
            }

            return View(contractorStaff);
        }

        // POST: ContractorStaff/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var contractorStaff = await _context.ContractorStaff.SingleOrDefaultAsync(m => m.Id == id);
            _context.ContractorStaff.Remove(contractorStaff);
            await _context.SaveChangesAsync();
            return RedirectToAction("Index", new { id = contractorStaff.ContractorId });
        }

        private bool ContractorStaffExists(int id)
        {
            return _context.ContractorStaff.Any(e => e.Id == id);
        }
    }
}
