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
    [Route("api/Order")]
    public class OrderController : Controller
    {
        private readonly ApplicationDbContext _context;

        public OrderController(ApplicationDbContext context)
        {
            _context = context;
        }

        // GET: api/Order
        [HttpGet]
        public IEnumerable<Order> GetOrder()
        {
            return _context.Order;
        }

        // GET: api/Order/5
        [HttpGet("{id}")]
        public async Task<IActionResult> GetOrder([FromRoute] string id)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            var order = await _context.Order.SingleOrDefaultAsync(m => m.Id == id);

            if (order == null)
            {
                return NotFound();
            }

            return Ok(order);
        }

        // PUT: api/Order/5
        [HttpPut("{id}")]
        public async Task<IActionResult> PutOrder([FromRoute] string id, [FromBody] Order order)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            if (id != order.Id)
            {
                return BadRequest();
            }

            _context.Entry(order).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!OrderExists(id))
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

        // POST: api/Order
        [HttpPost]
        public async Task<IActionResult> PostOrder([FromBody] Order order)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            _context.Order.Add(order);
            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateException)
            {
                if (OrderExists(order.Id))
                {
                    return new StatusCodeResult(StatusCodes.Status409Conflict);
                }
                else
                {
                    throw;
                }
            }

            return CreatedAtAction("GetOrder", new { id = order.Id }, order);
        }

        // DELETE: api/Order/5
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteOrder([FromRoute] string id)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            var order = await _context.Order.SingleOrDefaultAsync(m => m.Id == id);
            if (order == null)
            {
                return NotFound();
            }

            _context.Order.Remove(order);
            await _context.SaveChangesAsync();

            return Ok(order);
        }

        [HttpPost("Workers")]
        public async Task<IActionResult> SetWorkers(string orderId, string leaderId, List<string> workerIds)
        {
            Order order = await _context.Order.SingleOrDefaultAsync(o => o.Id == orderId);
            if (order == null)
                return NotFound("工单不存在");

            if (order.State == 2)
            {
                return BadRequest("工单已完成，不可进行人员修改");
            }

            if (leaderId == null)
            {
                return BadRequest("请为工单指定班长");
            }

            if (!workerIds.Contains(leaderId))
            {
                return BadRequest("参数错误");
            }

            order.LeadWorkerId = leaderId;

            List<OrderWorker> curOrderWorkers = await _context.OrderWorker.Where(ow => ow.OrderId == orderId).ToListAsync();

            foreach (var orderWorker in curOrderWorkers)
            {
                orderWorker.Removed = false;
            }

            foreach (var orderWorker in curOrderWorkers.Where(ow => !workerIds.Contains(ow.WorkerId)))
            {
                if (order.State == 0)
                {
                    _context.OrderWorker.Remove(orderWorker);
                }
                else if (order.State == 1)
                {
                    orderWorker.Removed = true;
                }
            }

            foreach (var workerId in workerIds.Where(id => !curOrderWorkers.Exists(ow => ow.WorkerId == id)))
            {
                OrderWorker orderWorker = new OrderWorker
                {
                    OrderId = orderId,
                    WorkerId = workerId,
                };
                _context.OrderWorker.Add(orderWorker);
            }

           

            if (order.State == 0)
                order.State = 1;

            await _context.SaveChangesAsync();

            return Ok();
        }

        private bool OrderExists(string id)
        {
            return _context.Order.Any(e => e.Id == id);
        }
    }
}