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
    [Route("api/JobMeasurement")]
    public class JobMeasurementController : Controller
    {
        private readonly ApplicationDbContext _context;

        public JobMeasurementController(ApplicationDbContext context)
        {
            _context = context;
        }

        /// <summary>
        /// Web端：获取工作种类下的工作量项目
        /// </summary>
        /// <param name="classId"></param>
        /// <returns></returns>
        [HttpGet]
        public IEnumerable<JobMeasurement> GetJobMeasurement([FromQuery]int? classId)
        {
            if (classId == null)
                return _context.JobMeasurement;
            else
                return _context.JobMeasurement.Where(c => c.JobClassId == classId);
        }
    }
}