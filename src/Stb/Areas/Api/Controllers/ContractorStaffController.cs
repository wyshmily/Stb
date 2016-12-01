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

namespace Stb.Api.Controllers
{
    [Produces("application/json")]
    [Route("api/ContractorStaff")]
    public class ContractorStaffController : Controller
    {
        private readonly ApplicationDbContext _context;

        public ContractorStaffController(ApplicationDbContext context)
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
        public IEnumerable<ContractorStaff> Search([FromQuery]int contractorId, [FromQuery]string search)
        {
           var query = _context.ContractorStaff.Where(c => c.ContractorId == contractorId);
            if (!string.IsNullOrWhiteSpace(search))
            {
                query = query.Where(w => w.Name.Contains(search) || w.Phone.Contains(search));
            }
            return query.Take(10);
        }
    }
}