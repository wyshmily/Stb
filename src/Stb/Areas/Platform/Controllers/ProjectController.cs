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
    public class ProjectController : Controller
    {
        private readonly ApplicationDbContext _context;

        public ProjectController(ApplicationDbContext context)
        {
            _context = context;    
        }

        // GET: Project
        public async Task<IActionResult> Index()
        {
            var applicationDbContext = _context.Project.Include(p => p.Contractor).Include(p => p.District);
            return View(await applicationDbContext.ToListAsync());
        }

        // GET: Project/Details/5
        public async Task<IActionResult> Details(string id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var project = await _context.Project.SingleOrDefaultAsync(m => m.Id == id);
            if (project == null)
            {
                return NotFound();
            }

            return View(project);
        }

        // GET: Project/Create
        public IActionResult Create()
        {
            ViewData["ContractorId"] = new SelectList(_context.Contractor, "Id", "Name");
            ViewData["DistrictId"] = new SelectList(_context.District, "Id", "Id");
            return View();
        }

        // POST: Project/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("Id,ContactDeadline,ContactorStaffId,ContractorId,Description,DistrictId,ExpectedDays,ExpectedStartTime,LeadWorkerId,PlatoonId,ProjectType,WorkAddress,WorkLocation,WorkerNeeded")] Project project)
        {
            if (ModelState.IsValid)
            {
                _context.Add(project);
                await _context.SaveChangesAsync();
                return RedirectToAction("Index");
            }
            ViewData["ContractorId"] = new SelectList(_context.Contractor, "Id", "Name", project.ContractorId);
            ViewData["DistrictId"] = new SelectList(_context.District, "Id", "Id", project.DistrictId);
            return View(project);
        }

        // GET: Project/Edit/5
        public async Task<IActionResult> Edit(string id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var project = await _context.Project.SingleOrDefaultAsync(m => m.Id == id);
            if (project == null)
            {
                return NotFound();
            }
            ViewData["ContractorId"] = new SelectList(_context.Contractor, "Id", "Name", project.ContractorId);
            ViewData["DistrictId"] = new SelectList(_context.District, "Id", "Id", project.DistrictId);
            return View(project);
        }

        // POST: Project/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(string id, [Bind("Id,ContactDeadline,ContactorStaffId,ContractorId,Description,DistrictId,ExpectedDays,ExpectedStartTime,LeadWorkerId,PlatoonId,ProjectType,WorkAddress,WorkLocation,WorkerNeeded")] Project project)
        {
            if (id != project.Id)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(project);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!ProjectExists(project.Id))
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
            ViewData["ContractorId"] = new SelectList(_context.Contractor, "Id", "Name", project.ContractorId);
            ViewData["DistrictId"] = new SelectList(_context.District, "Id", "Id", project.DistrictId);
            return View(project);
        }

        // GET: Project/Delete/5
        public async Task<IActionResult> Delete(string id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var project = await _context.Project.SingleOrDefaultAsync(m => m.Id == id);
            if (project == null)
            {
                return NotFound();
            }

            return View(project);
        }

        // POST: Project/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(string id)
        {
            var project = await _context.Project.SingleOrDefaultAsync(m => m.Id == id);
            _context.Project.Remove(project);
            await _context.SaveChangesAsync();
            return RedirectToAction("Index");
        }

        private bool ProjectExists(string id)
        {
            return _context.Project.Any(e => e.Id == id);
        }
    }
}
