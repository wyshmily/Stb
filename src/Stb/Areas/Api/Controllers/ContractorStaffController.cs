using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Stb.Data;
using Stb.Data.Models;
using Stb.Platform.Models.ContractorViewModels;

namespace Stb.Areas.Api.Controllers
{
    [Produces("application/json")]
    [Route("api/ContractorStaff")]
    public class ContractorStaffController : Controller
    {
        private readonly ApplicationDbContext _context;

        public ContractorStaffController(ApplicationDbContext context)
        {
            _context = context;
        }

        [HttpGet("Search")]
        public IEnumerable<ContractorStaff> Search(int contractorId, string search)
        {
           var query = _context.ContractorStaff.Where(c => c.ContractorId == contractorId);
            if (!string.IsNullOrWhiteSpace(search))
            {
                query = query.Where(w => w.Name.Contains(search) || w.Phone.Contains(search));
            }
            return query.Take(10);
        }


        // GET: api/ContractorStaff
        [HttpGet]
        public IEnumerable<ContractorStaff> GetContractorStaff()
        {
            return _context.ContractorStaff;
        }

        // GET: api/ContractorStaff/5
        [HttpGet("{id}")]
        public async Task<IActionResult> GetContractorStaff([FromRoute] int id)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            ContractorStaff contractorStaff = await _context.ContractorStaff.SingleOrDefaultAsync(m => m.Id == id);

            if (contractorStaff == null)
            {
                return NotFound();
            }

            return Ok(contractorStaff);
        }

        // PUT: api/ContractorStaff/5
        [HttpPut("{id}")]
        public async Task<IActionResult> PutContractorStaff([FromRoute] int id, [FromBody] ContractorStaff contractorStaff)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            if (id != contractorStaff.Id)
            {
                return BadRequest();
            }

            _context.Entry(contractorStaff).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!ContractorStaffExists(id))
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

        // POST: api/ContractorStaff
        [HttpPost]
        public async Task<IActionResult> PostContractorStaff([FromBody] ContractorStaff contractorStaff)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            _context.ContractorStaff.Add(contractorStaff);
            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateException)
            {
                if (ContractorStaffExists(contractorStaff.Id))
                {
                    return new StatusCodeResult(StatusCodes.Status409Conflict);
                }
                else
                {
                    throw;
                }
            }

            return CreatedAtAction("GetContractorStaff", new { id = contractorStaff.Id }, contractorStaff);
        }

        // DELETE: api/ContractorStaff/5
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteContractorStaff([FromRoute] int id)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            ContractorStaff contractorStaff = await _context.ContractorStaff.SingleOrDefaultAsync(m => m.Id == id);
            if (contractorStaff == null)
            {
                return NotFound();
            }

            _context.ContractorStaff.Remove(contractorStaff);
            await _context.SaveChangesAsync();

            return Ok(contractorStaff);
        }

        private bool ContractorStaffExists(int id)
        {
            return _context.ContractorStaff.Any(e => e.Id == id);
        }
    }
}