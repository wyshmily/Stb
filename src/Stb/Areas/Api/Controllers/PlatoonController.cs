using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Stb.Data;
using Stb.Data.Models;

namespace Stb.Areas.Api.Controllers
{
    [Produces("application/json")]
    [Route("api/Platoon")]
    public class PlatoonController : Controller
    {
        private readonly ApplicationDbContext _context;

        public PlatoonController(ApplicationDbContext context)
        {
            _context = context;
        }

        // GET: api/Platoon
        [HttpGet]
        public IEnumerable<Platoon> GetPlatoon()
        {
            return _context.Platoon;
        }

        [HttpGet("Search")]
        public IEnumerable<Platoon> Search(string search)
        {
            var query = _context.Platoon.Where(c =>c.Enabled);
            if (!string.IsNullOrWhiteSpace(search))
            {
                query = query.Where(w => w.Name.Contains(search) || w.UserName.Contains(search));
            }
            return query.Take(10);
        }

        // GET: api/Platoon/5
        [HttpGet("{id}")]
        public async Task<IActionResult> GetPlatoon([FromRoute] string id)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            Platoon platoon = await _context.Platoon.SingleOrDefaultAsync(m => m.Id == id);

            if (platoon == null)
            {
                return NotFound();
            }

            return Ok(platoon);
        }

        // PUT: api/Platoon/5
        [HttpPut("{id}")]
        public async Task<IActionResult> PutPlatoon([FromRoute] string id, [FromBody] Platoon platoon)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            if (id != platoon.Id)
            {
                return BadRequest();
            }

            _context.Entry(platoon).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!PlatoonExists(id))
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

        // POST: api/Platoon
        [HttpPost]
        public async Task<IActionResult> PostPlatoon([FromBody] Platoon platoon)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            _context.Platoon.Add(platoon);
            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateException)
            {
                if (PlatoonExists(platoon.Id))
                {
                    return new StatusCodeResult(StatusCodes.Status409Conflict);
                }
                else
                {
                    throw;
                }
            }

            return CreatedAtAction("GetPlatoon", new { id = platoon.Id }, platoon);
        }

        // DELETE: api/Platoon/5
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeletePlatoon([FromRoute] string id)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            Platoon platoon = await _context.Platoon.SingleOrDefaultAsync(m => m.Id == id);
            if (platoon == null)
            {
                return NotFound();
            }

            _context.Platoon.Remove(platoon);
            await _context.SaveChangesAsync();

            return Ok(platoon);
        }

        private bool PlatoonExists(string id)
        {
            return _context.Platoon.Any(e => e.Id == id);
        }
    }
}