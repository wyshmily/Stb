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

namespace Stb.Api.Controllers
{
    [Produces("application/json")]
    [Route("api/JobClass")]
    public class JobClassController : Controller
    {
        private readonly ApplicationDbContext _context;

        public JobClassController(ApplicationDbContext context)
        {
            _context = context;
        }

        /// <summary>
        /// Web端：获取工作门类下的工作种类
        /// </summary>
        /// <param name="categoryId"></param>
        /// <returns></returns>
        [HttpGet]
        public IEnumerable<JobClass> GetJobClass([FromQuery]int? categoryId)
        {
            if (categoryId == null)
                return _context.JobClass;
            else
                return _context.JobClass.Where(c => c.JobCategoryId == categoryId);
        }
    }
}