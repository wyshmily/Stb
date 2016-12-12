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
    [Authorize(Roles = Roles.AdminAndCustomerService)]
    [Area(AreaNames.Platform)]
    public class JobClassController : Controller
    {
        private readonly ApplicationDbContext _context;

        public JobClassController(ApplicationDbContext context)
        {
            _context = context;
        }

        // GET: JobClass
        public async Task<IActionResult> Index(int id)
        {
            JobCategory category = _context.JobCategory.Single(c => c.Id == id);
            ViewData["Category"] = category;
            return View(await _context.JobClass.Where(c => c.JobCategoryId == id).ToListAsync());
        }

        // GET: JobClass/Create
        public IActionResult Create(int id)
        {
            JobCategory category = _context.JobCategory.Single(c => c.Id == id);
            ViewData["Category"] = category;
            JobClass jobClass = new JobClass { JobCategoryId = category.Id };
            return View(jobClass);
        }

        // POST: JobClass/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("JobCategoryId,Name")] JobClass jobClass)
        {
            if (ModelState.IsValid)
            {
                _context.Add(jobClass);
                await _context.SaveChangesAsync();
                return RedirectToAction("Index", new { id = jobClass.JobCategoryId });
            }
            JobCategory category = _context.JobCategory.Single(c => c.Id == jobClass.JobCategoryId);
            ViewData["Category"] = category;
            return View(jobClass);
        }

        // GET: JobClass/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var jobClass = await _context.JobClass.SingleOrDefaultAsync(m => m.Id == id);
            if (jobClass == null)
            {
                return NotFound();
            }

            JobCategory category = _context.JobCategory.Single(c => c.Id == jobClass.JobCategoryId);
            ViewData["Category"] = category;

            return View(jobClass);
        }

        // POST: JobClass/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("Id,JobCategoryId,Name")] JobClass jobClass)
        {
            if (id != jobClass.Id)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(jobClass);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!JobClassExists(jobClass.Id))
                    {
                        return NotFound();
                    }
                    else
                    {
                        throw;
                    }
                }
                return RedirectToAction("Index", new { id = jobClass.JobCategoryId });
            }
            JobCategory category = _context.JobCategory.Single(c => c.Id == jobClass.JobCategoryId);
            ViewData["Category"] = category;
            return View(jobClass);
        }

        // GET: JobClass/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var jobClass = await _context.JobClass.SingleOrDefaultAsync(m => m.Id == id);
            if (jobClass == null)
            {
                return NotFound();
            }

            JobCategory category = _context.JobCategory.Single(c => c.Id == jobClass.JobCategoryId);
            ViewData["Category"] = category;

            return View(jobClass);
        }

        // POST: JobClass/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var jobClass = await _context.JobClass.SingleOrDefaultAsync(m => m.Id == id);
            _context.JobClass.Remove(jobClass);
            await _context.SaveChangesAsync();
            return RedirectToAction("Index", new { id = jobClass.JobCategoryId });
        }

        private bool JobClassExists(int id)
        {
            return _context.JobClass.Any(e => e.Id == id);
        }
    }
}
