using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Stb.Data;
using Stb.Data.Models;
using Microsoft.AspNetCore.Authorization;

namespace Stb.Areas.Api.Controllers
{
    [Produces("application/json")]
    [Route("api/JobClass")]
    public class JobClassController : Controller
    {
        private readonly ApplicationDbContext _context;

        public JobClassController(ApplicationDbContext context)
        {
            _context = context;
        }

        // GET: api/JobClass
        [HttpGet]
        public IEnumerable<JobClass> GetJobClass([FromQuery]int? categoryId)
        {
            if (categoryId == null)
                return _context.JobClass;
            else
                return _context.JobClass.Where(c => c.JobCategoryId == categoryId);
        }

        // GET: api/JobClass/5
        [HttpGet("{id}")]
        public async Task<IActionResult> GetJobClass([FromRoute] int id)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            JobClass jobClass = await _context.JobClass.SingleOrDefaultAsync(m => m.Id == id);

            if (jobClass == null)
            {
                return NotFound();
            }

            return Ok(jobClass);
        }

        // PUT: api/JobClass/5
        [HttpPut("{id}")]
        public async Task<IActionResult> PutJobClass([FromRoute] int id, [FromBody] JobClass jobClass)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            if (id != jobClass.Id)
            {
                return BadRequest();
            }

            _context.Entry(jobClass).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!JobClassExists(id))
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

        // POST: api/JobClass
        [HttpPost]
        public async Task<IActionResult> PostJobClass([FromBody] JobClass jobClass)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            _context.JobClass.Add(jobClass);
            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateException)
            {
                if (JobClassExists(jobClass.Id))
                {
                    return new StatusCodeResult(StatusCodes.Status409Conflict);
                }
                else
                {
                    throw;
                }
            }

            return CreatedAtAction("GetJobClass", new { id = jobClass.Id }, jobClass);
        }

        // DELETE: api/JobClass/5
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteJobClass([FromRoute] int id)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            JobClass jobClass = await _context.JobClass.SingleOrDefaultAsync(m => m.Id == id);
            if (jobClass == null)
            {
                return NotFound();
            }

            _context.JobClass.Remove(jobClass);
            await _context.SaveChangesAsync();

            return Ok(jobClass);
        }

        private bool JobClassExists(int id)
        {
            return _context.JobClass.Any(e => e.Id == id);
        }
    }
}