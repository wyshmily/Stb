using System.Collections.Generic;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Authorization;
using Stb.Api.Models.OrderViewModels;
using Stb.Api.Models;
using Stb.Api.Services;

namespace Stb.Api.Controllers
{
    [Produces("application/json")]
    [Route("api/Platoon/Order")]
    [Authorize(ActiveAuthenticationSchemes = "Bearer", Roles = "排长")]
    [ApiExceptionFilter]
    public class PlatoonOrderController : Controller
    {
        private readonly OrderService _orderService;

        public PlatoonOrderController(OrderService orderService)
        {
            _orderService = orderService;
        }

        /// <summary>
        /// App排长端：工单列表
        /// </summary>
        /// <remarks>
        /// 只返回状态为0（准备状态）和1（施工状态）的工单
        /// 目前key只对工单号进行查询
        /// </remarks>
        /// <param name="key">Optional - 工单查询关键字</param>
        /// <returns></returns>
        [HttpGet]
        public async Task<ApiOutput<List<PlatoonOrderData>>> GetOrderAsync([FromQuery]string key = null)
        {
            return new ApiOutput<List<PlatoonOrderData>>(await _orderService.GetPlatoonOrdersAsync(this.UserId(), key));
        }

        /// <summary>
        /// App排长端：排长签到
        /// </summary>
        /// <param name="orderId">Required - 工单Id</param>
        /// <param name="pics">Required - 签到照片，逗号分隔</param>
        /// <param name="location">Required - 签到地点坐标 lng,lat</param>
        /// <param name="address">Required - 签到地点地址</param>
        /// <returns></returns>
        [HttpGet("SignIn")]
        public async Task<ApiOutput<bool>> SignInAsync([RequiredFromQuery]string orderId, [RequiredFromQuery]string pics, [RequiredFromQuery]string location, [RequiredFromQuery]string address)
        {
            return new ApiOutput<bool>(await _orderService.PlatoonSignInAsync(this.UserId(), orderId, pics, location, address));
        }

        /// <summary>
        /// App排长端：排长当日此工单是否已签到
        /// </summary>
        /// <param name="orderId">Required - 工单Id</param>
        /// <returns></returns>
        [HttpGet("IsSignIn")]
        public async Task<ApiOutput<bool>> IsSignInAsync([RequiredFromQuery]string orderId)
        {
            return new ApiOutput<bool>(await _orderService.IsPlatoonSignInAsync(this.UserId(), orderId));
        }

        /// <summary>
        /// App排长端：工单进度
        /// </summary>
        /// <param name="orderId">Required - 工单Id</param>
        /// <returns></returns>
        [HttpGet("Progress")]
        public async Task<ApiOutput<ProgressData>> GetOrderProgressAsync([RequiredFromQuery]string orderId)
        {
            return new ApiOutput<ProgressData>(await _orderService.GetOrderProgressAsync(orderId));
        }

        /// <summary>
        /// App排长端：工人签到列表
        /// </summary>
        /// <param name="orderId">Required - 工单Id</param>
        /// <returns></returns>
        [HttpGet("WorkerSignments")]
        public async Task<ApiOutput<List<SignmentData>>> GetWorkerSignmentsAsync([RequiredFromQuery]string orderId)
        {
            return new ApiOutput<List<SignmentData>>(await _orderService.GetWorkerSignmentsAsync(orderId));
        }

        /// <summary>
        /// App排长端：工人记录问题列表
        /// </summary>
        /// <param name="orderId">Required - 工单Id</param>
        /// <returns></returns>
        [HttpGet("Issues")]
        public async Task<ApiOutput<List<IssueData>>>GetOrderIssueAsync([RequiredFromQuery] string orderId)
        {
            return new ApiOutput<List<IssueData>>(await _orderService.GetIssueAsync(orderId));
        }

        /// <summary>
        /// App排长端：工单排长签到列表
        /// </summary>
        /// <param name="orderId">Required - 工单Id</param>
        /// <returns></returns>
        [HttpGet("Signments")]
        public async Task<ApiOutput<List<SignmentData>>> GetPlatoonSignmentAsync([RequiredFromQuery] string orderId)
        {
            return new ApiOutput<List<SignmentData>>(await _orderService.GetPlatoonSignmentAsync(orderId));
        }


        /// <summary>
        /// App排长端：完成施工
        /// </summary>
        /// <param name="orderId">Required - 工单Id</param>
        /// <returns></returns>
        [HttpGet("Finish")]
        public async Task<ApiOutput<bool>> FinishOrderWorkAsync([RequiredFromQuery]string orderId)
        {
            return new ApiOutput<bool>(await _orderService.FinishOrderWorkAsync(orderId, this.UserId()));
        }


    }
}