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
using Stb.Platform.Models.ContractorUserViewModels;
using Microsoft.AspNetCore.Identity;

namespace Stb.Platform.Controllers
{
    [Area(AreaNames.Platform)]
    [Authorize(Roles = Roles.AdminAndCustomerService)]
    public class ContractorStaffController : Controller
    {
        private readonly ApplicationDbContext _context;
        private readonly UserManager<ContractorUser> _userManager;

        public ContractorStaffController(ApplicationDbContext context, UserManager<ContractorUser> userManager)
        {
            _context = context;
            _userManager = userManager;
        }

        // GET: ContractorStaff
        public async Task<IActionResult> Index(int id)
        {
            ViewData["Contractor"] = _context.Contractor.Single(c => c.Id == id);
            return View((await _context.ContractorUser./*Include(s => s.Contractor).*/Where(s => s.ContractorId == id).ToListAsync()).Select(u => new ContractorUserViewModel(u)));
        }

        // GET: ContractorStaff/Details/5
        public async Task<IActionResult> Details(string id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var contractorStaff = await _context.ContractorUser.Include(s => s.Contractor).SingleOrDefaultAsync(m => m.Id == id);
            if (contractorStaff == null)
            {
                return NotFound();
            }

            return View(new ContractorUserViewModel(contractorStaff));
        }

        // GET: ContractorStaff/Create
        public async Task<IActionResult> Create(int id)
        {
            Contractor contractor = await _context.Contractor.SingleOrDefaultAsync(c => c.Id == id);
            ContractorUserViewModel user = new ContractorUserViewModel
            {
                ContractorId = contractor.Id
            };
            return View(user);
        }

        // POST: ContractorStaff/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("ContractorId,Name,UserName,Password")]ContractorUserViewModel viewModel)
        {
            if (ModelState.IsValid)
            {
                ContractorUser user = viewModel.ToContractorUser();

                if (await _userManager.Users.AnyAsync(u => u.UserName == user.UserName))
                {
                    ModelState.AddModelError("UserName", $"用户{user.UserName}已存在！");
                    return View(viewModel);
                }

                await _userManager.CreateAsync(user, viewModel.Password);
                await _userManager.AddToRoleAsync(user, Roles.Contractor);
                //_context.Add(contractorStaff);
                //await _context.SaveChangesAsync();
                return RedirectToAction("Index", new { id = viewModel.ContractorId });
            }
            return View(viewModel);
        }

        // GET: ContractorStaff/Edit/5
        public async Task<IActionResult> Edit(string id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var contractorStaff = await _context.ContractorUser.Include(s => s.Contractor).SingleOrDefaultAsync(m => m.Id == id);
            if (contractorStaff == null)
            {
                return NotFound();
            }
            return View(new ContractorUserViewModel(contractorStaff));
        }

        // POST: ContractorStaff/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(string id, ContractorUserViewModel contractorStaff)
        {
            if (id != contractorStaff.Id)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    var appUser = await _userManager.FindByIdAsync(id);
                    contractorStaff.Update(ref appUser);
                    await _userManager.UpdateAsync(appUser);
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!ContractorUserExists(contractorStaff.Id))
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
        public async Task<IActionResult> Delete(string id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var contractorStaff = await _context.ContractorUser.Include(s => s.Contractor).SingleOrDefaultAsync(m => m.Id == id);
            if (contractorStaff == null)
            {
                return NotFound();
            }

            return View(new ContractorUserViewModel(contractorStaff));
        }

        // POST: ContractorStaff/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(string id)
        {
            var contractorStaff = await _context.ContractorUser.Include(s => s.Orders).SingleOrDefaultAsync(m => m.Id == id);
            foreach (var order in contractorStaff.Orders)
                order.ContractorUserId = null;
            _context.ContractorUser.Remove(contractorStaff);
            await _context.SaveChangesAsync();
            return RedirectToAction("Index", new { id = contractorStaff.ContractorId });
        }

        private bool ContractorUserExists(string id)
        {
            return _context.ContractorUser.Any(e => e.Id == id);
        }
    }
}
