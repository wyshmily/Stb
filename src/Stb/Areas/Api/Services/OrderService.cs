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

        public async Task<List<OrderData>> GetPlatoonOrdersAsync(string userId, string key)
        {
            var query = _context.Order.Include(o => o.ContractorStaff).Where(o => o.PlatoonId == userId && o.State <= 1);

            if (!string.IsNullOrWhiteSpace(key))
                query = query.Where(o => o.Id.Contains(key));

            query = query.OrderByDescending(o => o.Id);

            return (await query.ToListAsync()).Select(o => new OrderData(o)).ToList();
        }

        public async Task<List<OrderData>> GetWorkerOrdersAsync(string userId, string key)
        {
            var query = _context.Order.Include(o => o.ContractorStaff).Where(o => o.LeadWorkerId == userId && o.State <= 1);

            if (!string.IsNullOrWhiteSpace(key))
                query = query.Where(o => o.Id.Contains(key));

            query = query.OrderByDescending(o => o.Id);

            return (await query.ToListAsync()).Select(o => new OrderData(o)).ToList();
        }
    }
}
