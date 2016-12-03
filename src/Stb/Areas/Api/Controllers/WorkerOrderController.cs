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
        /// <remarks>
        /// 只返回状态为1（施工状态）的工单
        /// 目前key只对工单号进行查询
        /// </remarks>
        /// <param name="key">Optional - 工单查询关键字</param>
        /// <returns></returns>
        [HttpGet]
        public async Task<ApiOutput<List<WorkerOrderData>>> GetOrderAsync([FromQuery]string key = null)
        {
            return new ApiOutput<List<WorkerOrderData>>(await _orderService.GetWorkerOrdersAsync(this.UserId(), key));
        }

        /// <summary>
        /// App班长端：接受工单
        /// </summary>
        /// <param name="orderId">工单Id</param>
        /// <returns></returns>
        [HttpGet("Accept")]
        public async Task<ApiOutput<bool>> AcceptOrderAsync([FromQuery]string orderId)
        {
            return new ApiOutput<bool>(await _orderService.WorkerAcceptOrderAsync(this.UserId(), orderId));
        }

        /// <summary>
        /// App班长端：班长签到
        /// </summary>
        /// <param name="orderId">Required - 工单Id</param>
        /// <param name="pics">Required - 签到照片，逗号分隔</param>
        /// <param name="location">Required - 签到地点坐标 lng,lat</param>
        /// <param name="address">Required - 签到地点地址</param>
        /// <returns></returns>
        [HttpGet("SignIn")]
        public async Task<ApiOutput<bool>> SignInAsync([RequiredFromQuery]string orderId, [RequiredFromQuery]string pics, [RequiredFromQuery]string location, [RequiredFromQuery]string address)
        {
            return new ApiOutput<bool>(await _orderService.WorkerSignInAsync(this.UserId(), orderId, pics, location, address, true));
        }

        /// <summary>
        /// App班长端：班长签退
        /// </summary>
        /// <param name="orderId">Required - 工单Id</param>
        /// <param name="pics">Required - 签到照片，逗号分隔</param>
        /// <param name="location">Required - 签到地点坐标 lng,lat</param>
        /// <param name="address">Required - 签到地点地址</param>
        /// <returns></returns>
        [HttpGet("SignOut")]
        public async Task<ApiOutput<bool>> SignOutAsync([RequiredFromQuery]string orderId, [RequiredFromQuery]string pics, [RequiredFromQuery]string location, [RequiredFromQuery]string address)
        {
            return new ApiOutput<bool>(await _orderService.WorkerSignInAsync(this.UserId(), orderId, pics, location, address, false));
        }

        /// <summary>
        /// App班长端：今日签到状态
        /// </summary>
        /// <param name="orderId">Required - 工单Id</param>
        /// <returns></returns>
        [HttpGet("SignState")]
        public async Task<ApiOutput<int>> SignStateAsync([RequiredFromQuery]string orderId)
        {
            return new ApiOutput<int>(await _orderService.WorkerSignStateAsync(this.UserId(), orderId));
        }

        /// <summary>
        /// App班长端：今日签到信息
        /// </summary>
        /// <param name="orderId">Required - 工单Id</param>
        /// <returns></returns>
        [HttpGet("Signments")]
        public async Task<ApiOutput<WorkerSignmentData>> SignmentsAsync([RequiredFromQuery]string orderId)
        {
            return new ApiOutput<WorkerSignmentData>(await _orderService.GetWorkerSignmentsAsync(orderId, this.UserId()));
        }

        /// <summary>
        /// App班长端：记录问题
        /// </summary>
        /// <param name="orderId">Required - 工单id</param>
        /// <param name="issueType">Required - 问题类型：1-设计问题；2-业主要求；3-现场环境不具备施工条件；4-不可抗力</param>
        /// <param name="solutionType">Required - 解决方法类型：1-重新施工；2-推迟施工；3-修改设计；4-更换设备型号</param>
        /// <param name="pics">Optional - 图片链接，逗号分隔</param>
        /// <param name="audios">Optional - 录音链接，逗号分隔</param>
        /// <returns></returns>
        [HttpGet("ReportIssue")]
        public async Task<ApiOutput<bool>> ReportIssueAsync([RequiredFromQuery]string orderId, [RequiredFromQuery]int issueType, [RequiredFromQuery]int solutionType, [FromQuery]string pics, [FromQuery] string audios)
        {
            return new ApiOutput<bool>(await _orderService.WorkerReportIssueAsync(this.UserId(), orderId, issueType, solutionType, pics, audios));
        }

    }
}