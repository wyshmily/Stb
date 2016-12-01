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
    [Route("api/JobCategory")]
    public class JobCategoryController : Controller
    {
        private readonly ApplicationDbContext _context;

        public JobCategoryController(ApplicationDbContext context)
        {
            _context = context;
        }

        /// <summary>
        /// Web端：获取工作门类列表
        /// </summary>
        /// <returns></returns>
        [HttpGet]
        public IEnumerable<JobCategory> GetJobCategory()
        {
            return _context.JobCategory;
        }
    }
}