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
    [Authorize]
    [Area(AreaNames.Platform)]
    public class JobMeasurementController : Controller
    {
        private readonly ApplicationDbContext _context;

        public JobMeasurementController(ApplicationDbContext context)
        {
            _context = context;
        }

        // GET: JobMeasurement
        public async Task<IActionResult> Index(int id)
        {
            JobClass jobClass = _context.JobClass.Include(c => c.JobCategory).Single(c => c.Id == id);
            ViewData["JobClass"] = jobClass;
            return View(await _context.JobMeasurement.Where(m=>m.JobClassId==id).ToListAsync());
        }

        // GET: JobMeasurement/Create
        public IActionResult Create(int id)
        {
            JobClass jobClass = _context.JobClass.Include(c => c.JobCategory).Single(c => c.Id == id);
            ViewData["JobClass"] = jobClass;
            JobMeasurement jobMeasurement = new JobMeasurement { JobClassId = id };
            return View(jobMeasurement);
        }

        // POST: JobMeasurement/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("JobClassId,Name,Unit")] JobMeasurement jobMeasurement)
        {
            if (ModelState.IsValid)
            {
                _context.Add(jobMeasurement);
                await _context.SaveChangesAsync();
                return RedirectToAction("Index", new { id = jobMeasurement.JobClassId });
            }
            JobClass jobClass = _context.JobClass.Include(c => c.JobCategory).Single(c => c.Id == jobMeasurement.JobClassId);
            ViewData["JobClass"] = jobClass;
            return View(jobMeasurement);
        }

        // GET: JobMeasurement/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var jobMeasurement = await _context.JobMeasurement.SingleOrDefaultAsync(m => m.Id == id);
            if (jobMeasurement == null)
            {
                return NotFound();
            }
            JobClass jobClass = _context.JobClass.Include(c => c.JobCategory).Single(c => c.Id == jobMeasurement.JobClassId);
            ViewData["JobClass"] = jobClass;
            return View(jobMeasurement);
        }

        // POST: JobMeasurement/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("Id,JobClassId,Name,Unit")] JobMeasurement jobMeasurement)
        {
            if (id != jobMeasurement.Id)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(jobMeasurement);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!JobMeasurementExists(jobMeasurement.Id))
                    {
                        return NotFound();
                    }
                    else
                    {
                        throw;
                    }
                }
                return RedirectToAction("Index", new { id = jobMeasurement.JobClassId });
            }
            JobClass jobClass = _context.JobClass.Include(c => c.JobCategory).Single(c => c.Id == jobMeasurement.JobClassId);
            ViewData["JobClass"] = jobClass;
            return View(jobMeasurement);
        }

        // GET: JobMeasurement/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var jobMeasurement = await _context.JobMeasurement.SingleOrDefaultAsync(m => m.Id == id);
            if (jobMeasurement == null)
            {
                return NotFound();
            }

            JobClass jobClass = _context.JobClass.Include(c => c.JobCategory).Single(c => c.Id == jobMeasurement.JobClassId);
            ViewData["JobClass"] = jobClass;

            return View(jobMeasurement);
        }

        // POST: JobMeasurement/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var jobMeasurement = await _context.JobMeasurement.SingleOrDefaultAsync(m => m.Id == id);
            _context.JobMeasurement.Remove(jobMeasurement);
            await _context.SaveChangesAsync();
            return RedirectToAction("Index", new { id = jobMeasurement.JobClassId });
        }

        private bool JobMeasurementExists(int id)
        {
            return _context.JobMeasurement.Any(e => e.Id == id);
        }
    }
}
