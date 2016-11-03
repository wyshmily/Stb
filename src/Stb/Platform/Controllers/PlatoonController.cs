using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using Stb.Data;
using Stb.Platform.Models;
using Microsoft.AspNetCore.Authorization;

namespace Stb.Platform.Controllers
{
    [Area("platform")]
    [Authorize]
    public class PlatoonController : Controller
    {
        private readonly ApplicationDbContext _context;

        public PlatoonController(ApplicationDbContext context)
        {
            _context = context;
        }

        // GET: Platoon
        public async Task<IActionResult> Index()
        {
            return View(await _context.Platoon.ToListAsync());
        }

        // GET: Platoon/Details/5
        public async Task<IActionResult> Details(string id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var platoon = await _context.Platoon.SingleOrDefaultAsync(m => m.Id == id);
            if (platoon == null)
            {
                return NotFound();
            }

            return View(platoon);
        }

        // GET: Platoon/Create
        public IActionResult Create()
        {
            return View();
        }

        // POST: Platoon/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("Id,AccessFailedCount,ConcurrencyStamp,Email,EmailConfirmed,LockoutEnabled,LockoutEnd,Name,NormalizedEmail,NormalizedUserName,PasswordHash,PhoneNumber,PhoneNumberConfirmed,SecurityStamp,TwoFactorEnabled,UserName,Alipay,Birthday,Enabled,Gender,HealthStatus,IdCardNumber,NativePlace,QQ,Wechat,ArmyPost,ArmyRank,DischargeTime,MilitaryTime")] Platoon platoon)
        {
            if (ModelState.IsValid)
            {
                _context.Add(platoon);
                await _context.SaveChangesAsync();
                return RedirectToAction("Index");
            }
            return View(platoon);
        }

        // GET: Platoon/Edit/5
        public async Task<IActionResult> Edit(string id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var platoon = await _context.Platoon.SingleOrDefaultAsync(m => m.Id == id);
            if (platoon == null)
            {
                return NotFound();
            }
            return View(platoon);
        }

        // POST: Platoon/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(string id, [Bind("Id,AccessFailedCount,ConcurrencyStamp,Email,EmailConfirmed,LockoutEnabled,LockoutEnd,Name,NormalizedEmail,NormalizedUserName,PasswordHash,PhoneNumber,PhoneNumberConfirmed,SecurityStamp,TwoFactorEnabled,UserName,Alipay,Birthday,Enabled,Gender,HealthStatus,IdCardNumber,NativePlace,QQ,Wechat,ArmyPost,ArmyRank,DischargeTime,MilitaryTime")] Platoon platoon)
        {
            if (id != platoon.Id)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(platoon);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!PlatoonExists(platoon.Id))
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
            return View(platoon);
        }

        // GET: Platoon/Delete/5
        public async Task<IActionResult> Delete(string id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var platoon = await _context.Platoon.SingleOrDefaultAsync(m => m.Id == id);
            if (platoon == null)
            {
                return NotFound();
            }

            return View(platoon);
        }

        // POST: Platoon/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(string id)
        {
            var platoon = await _context.Platoon.SingleOrDefaultAsync(m => m.Id == id);
            _context.Platoon.Remove(platoon);
            await _context.SaveChangesAsync();
            return RedirectToAction("Index");
        }

        private bool PlatoonExists(string id)
        {
            return _context.Platoon.Any(e => e.Id == id);
        }
    }
}
