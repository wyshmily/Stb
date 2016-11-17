using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Stb.Data;
using Stb.Data.Models;
using Stb.Areas.Api.Models.ContractorViewModels;

namespace Stb.Areas.Api.Controllers
{
    [Produces("application/json")]
    [Route("api/Contractor")]
    public class ContractorController : Controller
    {
        private readonly ApplicationDbContext _context;

        public ContractorController(ApplicationDbContext context)
        {
            _context = context;
        }

        // GET: api/Contractor
        [HttpGet]
        public IEnumerable<Contractor> GetContractor()
        {
            return _context.Contractor;
        }

        [HttpGet("Search")]
        public IEnumerable<ContractorViewModel> GetContractor(string search)
        {
            if (search == null)
                search = "";
            var query = (from c in _context.Contractor
                         join s in _context.ContractorStaff on c.HeadStaffId equals s.Id into temp
                         from tt in temp.DefaultIfEmpty()
                         where c.Enabled && c.Name.Contains(search)
                         select new
                         {
                             contractor = c,
                             header = tt
                         });

            return query.Take(10).Select(c => new ContractorViewModel
            {
                Id = c.contractor.Id,
                Name = c.contractor.Name,
                HeadStaffId = c.header == null ? null : (int?)c.header.Id,
                HeadStaffName = c.header == null ? null : c.header.Name,
                HeadStaffPhone = c.header == null ? null : c.header.Phone
            });
        }

        // GET: api/Contractor/5
        [HttpGet("{id}")]
        public async Task<IActionResult> GetContractor([FromRoute] int id)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            Contractor contractor = await _context.Contractor.SingleOrDefaultAsync(m => m.Id == id);

            if (contractor == null)
            {
                return NotFound();
            }

            return Ok(contractor);
        }

        // PUT: api/Contractor/5
        [HttpPut("{id}")]
        public async Task<IActionResult> PutContractor([FromRoute] int id, [FromBody] Contractor contractor)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            if (id != contractor.Id)
            {
                return BadRequest();
            }

            _context.Entry(contractor).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!ContractorExists(id))
                {
                    return NotFound();
                }
                else
                {
                    throw;
                }
            }

            return NoContent();
        }

        // POST: api/Contractor
        [HttpPost]
        public async Task<IActionResult> PostContractor([FromBody] Contractor contractor)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            _context.Contractor.Add(contractor);
            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateException)
            {
                if (ContractorExists(contractor.Id))
                {
                    return new StatusCodeResult(StatusCodes.Status409Conflict);
                }
                else
                {
                    throw;
                }
            }

            return CreatedAtAction("GetContractor", new { id = contractor.Id }, contractor);
        }

        // DELETE: api/Contractor/5
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteContractor([FromRoute] int id)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            Contractor contractor = await _context.Contractor.Include(c => c.Orders).SingleOrDefaultAsync(m => m.Id == id);
            if (contractor == null)
            {
                return NotFound();
            }

            foreach (var order in contractor.Orders)
                order.ContractorId = null;
            _context.Contractor.Remove(contractor);
            await _context.SaveChangesAsync();

            return Ok(contractor);
        }

        private bool ContractorExists(int id)
        {
            return _context.Contractor.Any(e => e.Id == id);
        }
    }
}