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
using Stb.Api.Models.MessageViewModels;

namespace Stb.Api.Controllers
{
    [Produces("application/json")]
    [Route("api/Message")]
    [Authorize(ActiveAuthenticationSchemes = "Bearer", Roles = "排长,工人")]
    [ApiExceptionFilter]
    public class MessageController : Controller
    {
        private readonly MessageService _messageService;

        public MessageController(MessageService messageService)
        {
            _messageService = messageService;
        }

        /// <summary>
        /// App通用：未读消息数量
        /// </summary>
        /// <param name="type">Required - 消息类型：1-平台下单（排长端）；2-排长下单（班长端）；3-班长签到（排长端）；4-施工问题（排长端）</param>
        /// <returns></returns>
        [HttpGet("Count")]
        public async Task<ApiOutput<int>> GetMsgCountAsync([RequiredFromQuery]int type)
        {
            return new ApiOutput<int>(await _messageService.GetMessageCountAsync(this.UserId(), type));
        }

        /// <summary>
        /// App通用：消息列表
        /// </summary>
        /// <param name="type">Required - 消息类型：1-平台下单（排长端）；2-排长下单（班长端）；3-班长签到（排长端）；4-施工问题（排长端）</param>
        /// <returns></returns>
        [HttpGet]
        public async Task<ApiOutput<List<MessageData>>> GetMessageAsync([RequiredFromQuery]int type)
        {
            return new ApiOutput<List<MessageData>>(await _messageService.GetMessageAsync(this.UserId(), type));
        }

        /// <summary>
        /// App通用：设置消息已读
        /// </summary>
        /// <param name="msgId">Required - 消息Id</param>
        /// <returns></returns>
        [HttpGet("Read")]
        public async Task<ApiOutput<bool>> SetMessageReadAsync([RequiredFromQuery]int msgId)
        {
            return new ApiOutput<bool>(await _messageService.SetMessageReadAsync(msgId, this.UserId()));
        }
    }
}