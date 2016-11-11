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

namespace Stb.Platform.Views.Account
{
    [Authorize]
    [Area(AreaNames.Platform)]
    public class JobCategoryController : Controller
    {
        private readonly ApplicationDbContext _context;

        public JobCategoryController(ApplicationDbContext context)
        {
            _context = context;
        }

        // GET: JobCategorie
        public async Task<IActionResult> Index()
        {
            return View(await _context.JobCategory.ToListAsync());
        }

        // GET: JobCategorie/Create
        public IActionResult Create()
        {
            return View();
        }

        // POST: JobCategorie/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("Id,Name")] JobCategory jobCategory)
        {
            if (ModelState.IsValid)
            {
                _context.Add(jobCategory);
                await _context.SaveChangesAsync();
                return RedirectToAction("Index");
            }
            return View(jobCategory);
        }

        // GET: JobCategorie/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var jobCategory = await _context.JobCategory.SingleOrDefaultAsync(m => m.Id == id);
            if (jobCategory == null)
            {
                return NotFound();
            }
            return View(jobCategory);
        }

        // POST: JobCategorie/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("Id,Name")] JobCategory jobCategory)
        {
            if (id != jobCategory.Id)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(jobCategory);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!JobCategoryExists(jobCategory.Id))
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
            return View(jobCategory);
        }

        // GET: JobCategorie/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var jobCategory = await _context.JobCategory.SingleOrDefaultAsync(m => m.Id == id);
            if (jobCategory == null)
            {
                return NotFound();
            }

            return View(jobCategory);
        }

        // POST: JobCategorie/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var jobCategory = await _context.JobCategory.SingleOrDefaultAsync(m => m.Id == id);
            _context.JobCategory.Remove(jobCategory);
            await _context.SaveChangesAsync();
            return RedirectToAction("Index");
        }

        [HttpGet]
        public async Task<IActionResult> Preview()
        {
            return View(await _context.JobCategory.Include(c => c.JobClasses).ThenInclude(c => c.JobMeasurements).ToListAsync());
        }

        private bool JobCategoryExists(int id)
        {
            return _context.JobCategory.Any(e => e.Id == id);
        }
    }
}
