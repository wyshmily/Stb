using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Stb.Data;
using Stb.Data.Models;
using Stb.Platform.Models.ContractorViewModels;
using Stb.Api.Models.ContractorUserViewModels;

namespace Stb.Api.Controllers
{
    [Produces("application/json")]
    [Route("api/ContractorUser")]
    public class ContractorUserController : Controller
    {
        private readonly ApplicationDbContext _context;

        public ContractorUserController(ApplicationDbContext context)
        {
            _context = context;
        }

        /// <summary>
        /// Web端：搜索承包商员工
        /// </summary>
        /// <param name="contractorId"></param>
        /// <param name="search"></param>
        /// <returns></returns>
        [HttpGet("Search")]
        public IEnumerable<ContractorUserData> Search([FromQuery]int contractorId, [FromQuery]string search)
        {
           var query = _context.ContractorUser.Where(c => c.ContractorId == contractorId);
            if (!string.IsNullOrWhiteSpace(search))
            {
                query = query.Where(w => w.Name.Contains(search) || w.UserName.Contains(search));
            }
            return query.Take(10).ToList().Select(u => new ContractorUserData(u));
        }
    }
}