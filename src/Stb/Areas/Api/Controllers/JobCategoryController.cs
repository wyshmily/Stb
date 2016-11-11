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
    [Route("api/JobCategory")]
    public class JobCategoryController : Controller
    {
        private readonly ApplicationDbContext _context;

        public JobCategoryController(ApplicationDbContext context)
        {
            _context = context;
        }

        // GET: api/JobCategory
        [HttpGet]
        public IEnumerable<JobCategory> GetJobCategory()
        {
            return _context.JobCategory;
        }

        // GET: api/JobCategory/5
        [HttpGet("{id}")]
        public async Task<IActionResult> GetJobCategory([FromRoute] int id)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            JobCategory jobCategory = await _context.JobCategory.SingleOrDefaultAsync(m => m.Id == id);

            if (jobCategory == null)
            {
                return NotFound();
            }

            return Ok(jobCategory);
        }

        // PUT: api/JobCategory/5
        [HttpPut("{id}")]
        public async Task<IActionResult> PutJobCategory([FromRoute] int id, [FromBody] JobCategory jobCategory)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            if (id != jobCategory.Id)
            {
                return BadRequest();
            }

            _context.Entry(jobCategory).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!JobCategoryExists(id))
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

        // POST: api/JobCategory
        [HttpPost]
        public async Task<IActionResult> PostJobCategory([FromBody] JobCategory jobCategory)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            _context.JobCategory.Add(jobCategory);
            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateException)
            {
                if (JobCategoryExists(jobCategory.Id))
                {
                    return new StatusCodeResult(StatusCodes.Status409Conflict);
                }
                else
                {
                    throw;
                }
            }

            return CreatedAtAction("GetJobCategory", new { id = jobCategory.Id }, jobCategory);
        }

        // DELETE: api/JobCategory/5
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteJobCategory([FromRoute] int id)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            JobCategory jobCategory = await _context.JobCategory.SingleOrDefaultAsync(m => m.Id == id);
            if (jobCategory == null)
            {
                return NotFound();
            }

            _context.JobCategory.Remove(jobCategory);
            await _context.SaveChangesAsync();

            return Ok(jobCategory);
        }

        private bool JobCategoryExists(int id)
        {
            return _context.JobCategory.Any(e => e.Id == id);
        }
    }
}