using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Stb.Data;
using Stb.Data.Models;
using Stb.Platform.Models.WorkerViewModels;

namespace Stb.Areas.Api.Controllers
{
    [Produces("application/json")]
    [Route("api/Worker")]
    public class WorkerController : Controller
    {
        private readonly ApplicationDbContext _context;

        public WorkerController(ApplicationDbContext context)
        {
            _context = context;
        }

        // GET: api/Worker
        [HttpGet]
        public IEnumerable<WorkerSimpleViewModel> GetWorkers([FromQuery]string search)
        {
            var query = _context.Worker.AsQueryable();
            if (!string.IsNullOrWhiteSpace(search))
            {
                query = query.Where(w => w.Name.Contains(search) || w.UserName.Contains(search));
            }
            return query.Take(10).ToList().Select(w => new WorkerSimpleViewModel(w));
        }

        [HttpGet("Header")]
        public IEnumerable<Worker> Header(string search)
        {
            var query = _context.Worker.Where(w => w.IsHeader);
            if(!string.IsNullOrWhiteSpace(search))
            {
                query = query.Where(w => w.Name.Contains(search) || w.UserName.Contains(search));
            }
            return query.Take(10);
        }

        // GET: api/Worker/5
        [HttpGet("{id}")]
        public async Task<IActionResult> GetWorker([FromRoute] string id)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            Worker worker = await _context.Worker.SingleOrDefaultAsync(m => m.Id == id);

            if (worker == null)
            {
                return NotFound();
            }

            return Ok(worker);
        }

        // PUT: api/Worker/5
        [HttpPut("{id}")]
        public async Task<IActionResult> PutWorker([FromRoute] string id, [FromBody] Worker worker)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            if (id != worker.Id)
            {
                return BadRequest();
            }

            _context.Entry(worker).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!WorkerExists(id))
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

        // POST: api/Worker
        [HttpPost]
        public async Task<IActionResult> PostWorker([FromBody] Worker worker)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            _context.Worker.Add(worker);
            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateException)
            {
                if (WorkerExists(worker.Id))
                {
                    return new StatusCodeResult(StatusCodes.Status409Conflict);
                }
                else
                {
                    throw;
                }
            }

            return CreatedAtAction("GetWorker", new { id = worker.Id }, worker);
        }

        // DELETE: api/Worker/5
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteWorker([FromRoute] string id)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            Worker worker = await _context.Worker.SingleOrDefaultAsync(m => m.Id == id);
            if (worker == null)
            {
                return NotFound();
            }

            _context.Worker.Remove(worker);
            await _context.SaveChangesAsync();

            return Ok(worker);
        }

        private bool WorkerExists(string id)
        {
            return _context.Worker.Any(e => e.Id == id);
        }
    }
}