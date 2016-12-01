using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Stb.Data;
using Stb.Data.Models;

namespace Stb.Api.Controllers
{
    [Produces("application/json")]
    [Route("api/Platoon")]
    public class PlatoonController : Controller
    {
        private readonly ApplicationDbContext _context;

        public PlatoonController(ApplicationDbContext context)
        {
            _context = context;
        }
       
        /// <summary>
        /// Web¶Ë£ºËÑË÷ÅÅ³¤
        /// </summary>
        /// <param name="districtId"></param>
        /// <param name="search"></param>
        /// <returns></returns>
        [HttpGet("Search")]
        public IEnumerable<Platoon> Search([FromQuery]int districtId, [FromQuery]string search)
        {
            var query = _context.Platoon.Where(c => c.Enabled && c.EndUserDistricts.Any(d => d.DistrictId == districtId));
            if (!string.IsNullOrWhiteSpace(search))
            {
                query = query.Where(w => w.Name.Contains(search) || w.UserName.Contains(search));
            }
            return query.Take(10);
        }
    }
}