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
using System.Security.Claims;
using Stb.Api.Models.OrderViewModels;
using Stb.Api.Models;
using Stb.Api.Services;

namespace Stb.Api.Controllers
{
    [Produces("application/json")]
    [Route("api/Worker/Order")]
    [Authorize(ActiveAuthenticationSchemes = "Bearer", Roles = "工人")]
    [ApiExceptionFilter]
    public class WorkerOrderController : Controller
    {
        private readonly OrderService _orderService;

        public WorkerOrderController(OrderService orderService)
        {
            _orderService = orderService;
        }

        /// <summary>
        /// App班长端：工单列表
        /// </summary>
        /// <param name="key">模糊查询</param>
        /// <returns></returns>
        [HttpGet]
        public async Task<ApiOutput<List<OrderData>>> GetOrder([FromQuery]string key = null)
        {
            string userId = User.Claims.First(c => c.Type == ClaimTypes.NameIdentifier).Value;

            return new ApiOutput<List<OrderData>>(await _orderService.GetWorkerOrdersAsync(userId, key));
        }

    }
}