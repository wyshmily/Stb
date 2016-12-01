using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Stb.Data;
using Stb.Data.Models;
using Stb.Api.Models.ContractorViewModels;

namespace Stb.Api.Controllers
{
    [Produces("application/json")]
    [Route("api/Contractor")]
    public class ContractorController : Controller
    {
        private readonly ApplicationDbContext _context;

        public ContractorController(ApplicationDbContext context)
        {
            _context = context;
        }

       
        /// <summary>
        /// Web¶Ë£ºËÑË÷³Ð°üÉÌ
        /// </summary>
        /// <param name="search"></param>
        /// <returns></returns>
        [HttpGet("Search")]
        public IEnumerable<ContractorViewModel> GetContractor([FromQuery]string search)
        {
            if (search == null)
                search = "";
            var query = (from c in _context.Contractor
                         join s in _context.ContractorStaff on c.HeadStaffId equals s.Id into temp
                         from tt in temp.DefaultIfEmpty()
                         where c.Enabled && c.Name.Contains(search)
                         select new
                         {
                             contractor = c,
                             header = tt
                         });

            return query.Take(10).Select(c => new ContractorViewModel
            {
                Id = c.contractor.Id,
                Name = c.contractor.Name,
                HeadStaffId = c.header == null ? null : (int?)c.header.Id,
                HeadStaffName = c.header == null ? null : c.header.Name,
                HeadStaffPhone = c.header == null ? null : c.header.Phone
            });
        }
    }
}