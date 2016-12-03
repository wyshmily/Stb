using Microsoft.EntityFrameworkCore;
using Stb.Api.Models.MessageViewModels;
using Stb.Data;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Stb.Api.Services
{
    public class MessageService
    {
        private readonly ApplicationDbContext _context;

        public MessageService(ApplicationDbContext context)
        {
            _context = context;
        }

        // 未读消息数量
        public async Task<int> GetMessageCountAsync(string userId, int type)
        {
            return await _context.Message.CountAsync(m => m.EndUserId == userId && m.Type == type && m.IsRead == false);
        }

        // 工人
        public async Task<List<MessageData>> GetMessageAsync(string userId, int type)
        {
            var list = await _context.Message.Where(m => m.EndUserId == userId && m.Type == type).OrderByDescending(m => m.Id).ToListAsync();

            return list.Select(m => new MessageData(m)).ToList();
        }

        // 设置消息已读
        public async Task<bool> SetMessageReadAsync(int msgId, string userId)
        {
            var message = await _context.Message.FindAsync(msgId);
            if (message == null)
                throw new ApiException("工单不存在");

            if (message.EndUserId != userId)
                throw new ApiException("只能阅读自己的消息");

            message.IsRead = true;
            await _context.SaveChangesAsync();

            return true;
        }
    }
}
