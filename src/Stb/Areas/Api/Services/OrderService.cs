using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Options;
using Stb.Api.JwtToken;
using Stb.Api.Models.AuthViewModels;
using Stb.Api.Models.OrderViewModels;
using Stb.Data;
using Stb.Data.Models;
using System;
using System.Collections.Generic;
using System.IdentityModel.Tokens.Jwt;
using System.Linq;
using System.Security.Claims;
using System.Security.Principal;
using System.Threading.Tasks;

namespace Stb.Api.Services
{
    public class OrderService
    {
        private readonly ApplicationDbContext _context;

        public OrderService(ApplicationDbContext context)
        {
            _context = context;
        }

        // 排长工单列表
        public async Task<List<PlatoonOrderData>> GetPlatoonOrdersAsync(string userId, string key)
        {
            var query = _context.Order.Include(o => o.ContractorStaff).Where(o => o.PlatoonId == userId && o.State <= 1);

            if (!string.IsNullOrWhiteSpace(key))
                query = query.Where(o => o.Id.Contains(key));

            query = query.OrderByDescending(o => o.Id);

            return (await query.ToListAsync()).Select(o => new PlatoonOrderData(o)).ToList();
        }

        // 班长工单列表
        public async Task<List<WorkerOrderData>> GetWorkerOrdersAsync(string userId, string key)
        {
            var query = _context.Order.Include(o => o.ContractorStaff).Where(o => o.LeadWorkerId == userId && o.State == 1);

            if (!string.IsNullOrWhiteSpace(key))
                query = query.Where(o => o.Id.Contains(key));

            query = query.OrderByDescending(o => o.Id);

            return (await query.ToListAsync()).Select(o => new WorkerOrderData(o)).ToList();
        }

        // 班长接受工单
        public async Task<bool> WorkerAcceptOrderAsync(string userId, string orderId)
        {
            Order order = await _context.Order.SingleAsync(o => o.Id == orderId);
            if (order.LeadWorkerId != userId)
                throw new ApiException("用户并非此工单班长");
            if (order.AcceptWorkerId == userId)
                throw new ApiException("已经接受过此工单");

            order.AcceptWorkerId = userId;
            await _context.SaveChangesAsync();
            return true;
        }

        // 排长签到
        public async Task<bool> PlatoonSignInAsync(string userId, string orderId, string pics, string location, string address)
        {
            // todo：比对签到地点和施工地点
            // 
            DateTime now = DateTime.Now;
            Signment signment = await _context.Signment.FirstOrDefaultAsync(s => s.OrderId == orderId && s.EndUserId == userId && s.Time.Date == now.Date);
            if (signment != null)
                throw new ApiException("今日已签到");

            signment = new Signment
            {
                OrderId = orderId,
                EndUserId = userId,
                Pics = pics,
                Location = location,
                Address = address,
                InOut = true,
                Time = now,
            };
            _context.Add(signment);
            await _context.SaveChangesAsync();
            return true;
        }


        // 今日排长是否签到
        public async Task<bool> IsPlatoonSignInAsync(string userId, string orderId)
        {
            DateTime now = DateTime.Now;
            return await _context.Signment.AnyAsync(s => s.OrderId == orderId && s.EndUserId == userId && s.Time.Date == now.Date);
        }

        public async Task<ProgressData> GetOrderProgressAsync(string orderId)
        {
            var progressData = new ProgressData();

            var signmentQuery = _context.Signment.Where(s => s.OrderId == orderId && s.Type == 2).OrderByDescending(s => s.Id);

            progressData.SignmentCount = await signmentQuery.CountAsync();
            if (progressData.SignmentCount > 0)
                progressData.Signment = new SignmentData(await signmentQuery.FirstAsync());

            var issueQuery = _context.Issue.Where(i => i.OrderId == orderId).OrderByDescending(i => i.Id);
            progressData.IssueCount = await issueQuery.CountAsync();
            if (progressData.IssueCount > 0)
                progressData.Issue = new IssueData(await issueQuery.FirstAsync());

            return progressData;
        }

        // 班长签到、签退
        public async Task<bool> WorkerSignInAsync(string userId, string orderId, string pics, string location, string address, bool inOut)
        {
            Order order = await _context.Order.FindAsync(orderId);
            if (order == null)
                throw new ApiException("工单不存在");

            // todo：比对签到地点和施工地点
            // 

            var state = await WorkerSignStateAsync(userId, orderId);
            DateTime now = DateTime.Now;
            if (inOut && state != 0)
                throw new ApiException("今日已签到");
            if (!inOut && state == 0)
                throw new ApiException("今日尚未签到");
            if (!inOut && state == 2)
                throw new ApiException("今日已签退");

            var signment = new Signment
            {
                OrderId = orderId,
                EndUserId = userId,
                Pics = pics,
                Location = location,
                Address = address,
                InOut = inOut,
                Time = now,
            };
            _context.Add(signment);

            // 添加消息
            Worker worker = await _context.Worker.FindAsync(userId);
            if (worker != null)
            {
                Message message = new Message
                {
                    EndUserId = order.PlatoonId,
                    IsRead = false,
                    OrderId = orderId,
                    Title = $"{(inOut ? "签到" : "签退")}消息",
                    Text = $"工单{orderId}有来自{worker.Name}的{(inOut ? "签到" : "签退")}消息",
                    Time = DateTime.Now,
                    Type = 3,
                    InOut = inOut,
                    RootUserName = worker.Name,
                };
                _context.Message.Add(message);
            }

            // todo 推送通知
            // 

            await _context.SaveChangesAsync();
            return true;
        }



        // 今日班长是否签到状态：0-无签到信息；1-已签到；2-已签退
        public async Task<int> WorkerSignStateAsync(string userId, string orderId)
        {
            DateTime now = DateTime.Now;

            var signments = await _context.Signment.Where(s => s.OrderId == orderId && s.EndUserId == userId && s.Time.Date == now.Date).ToListAsync();

            return signments.Count;
        }


        // 班长当日签到数据
        public async Task<WorkerSignmentData> GetWorkerSignmentsAsync(string orderId, string userId)
        {
            DateTime now = DateTime.Now;

            var signments = await _context.Signment.Where(s => s.OrderId == orderId && s.EndUserId == userId && s.Time.Date == now.Date).ToListAsync();

            var signmentData = new WorkerSignmentData { State = signments.Count };
            if (signmentData.State >= 1)
                signmentData.SignIn = new SignmentData(signments.Find(s => s.InOut == true));
            if (signmentData.State == 2)
                signmentData.SignOut = new SignmentData(signments.Find(s => s.InOut == false));
            return signmentData;
        }

        // 工人签到列表
        public async Task<List<SignmentData>> GetWorkerSignmentsAsync(string orderId)
        {
            var list = await _context.Signment.Where(s => s.OrderId == orderId && s.Type == 2).OrderByDescending(s => s.Id).ToListAsync();

            return list.Select(s => new SignmentData(s)).ToList();
        }


        // 班长记录问题
        public async Task<bool> WorkerReportIssueAsync(string userId, string orderId, int issueType, int solutionType, string pics, string audios)
        {
            Order order = await _context.Order.FindAsync(orderId);
            if (order == null)
                throw new ApiException("工单不存在");

            Issue issue = new Issue
            {
                EndUserId = userId,
                OrderId = orderId,
                IssueType = issueType,
                SolutionType = solutionType,
                Pics = pics,
                Audios = audios,
            };

            _context.Issue.Add(issue);

            // 添加消息
            Worker worker = await _context.Worker.FindAsync(userId);
            if (worker != null)
            {
                Message message = new Message
                {
                    EndUserId = order.PlatoonId,
                    IsRead = false,
                    OrderId = orderId,
                    Title = "新消息",
                    Text = $"工单{orderId}有来自{worker.Name}的施工问题消息",
                    Time = DateTime.Now,
                    Type = 4,
                    RootUserName = worker.Name,
                };
                _context.Message.Add(message);
            }

            // todo 推送通知
            // 

            await _context.SaveChangesAsync();

            return true;
        }

        // 工单问题列表
        public async Task<List<IssueData>> GetIssueAsync(string orderId)
        {
            var list = await _context.Issue.Where(i => i.OrderId == orderId).ToListAsync();
            return list.Select(i => new IssueData(i)).ToList();
        }

        // 工单排长签到列表
        public async Task<List<SignmentData>> GetPlatoonSignmentAsync(string orderId)
        {
            var list = await _context.Signment.Where(s => s.OrderId == orderId && s.Type == 1).OrderByDescending(s => s.Id).ToListAsync();

            return list.Select(s => new SignmentData(s)).ToList();
        }

        // 完成施工
        public async Task<bool> FinishOrderWorkAsync(string orderId, string userId)
        {
            Order order = await _context.Order.FindAsync(orderId);
            if (order == null)
                throw new ApiException("工单不存在");

            if (order.PlatoonId != userId)
                throw new ApiException("不能更改非自己负责的工单的状态");

            order.State = 2;
            await _context.SaveChangesAsync();

            return true;
        }

    }
}
