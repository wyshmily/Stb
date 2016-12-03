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

namespace Stb.Api.Controllers
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

        /// <summary>
        /// Web端：排长设置或修改工单班长和工人列表
        /// </summary>
        /// <param name="orderId"></param>
        /// <param name="leaderId"></param>
        /// <param name="workerIds"></param>
        /// <returns></returns>
        [HttpPost("Workers")]
        public async Task<IActionResult> SetWorkersAsync([FromForm]string orderId, [FromForm]string leaderId, [FromForm]List<string> workerIds)
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

            if (order.LeadWorkerId != leaderId)
            {
                order.LeadWorkerId = leaderId;

                // 添加消息
                Message message = new Message
                {
                    EndUserId = leaderId,
                    IsRead = false,
                    OrderId = order.Id,
                    Title = "收到新工单",
                    Text = $"工单{order.Id}已下发",
                    Time = DateTime.Now,
                    Type = 2,
                };
                _context.Message.Add(message);

                // todo 推送通知
                // 
            }

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
    }
}