using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Stb.Data;
using Stb.Data.Models;
using Stb.Platform.Models.WorkerViewModels;

namespace Stb.Api.Controllers
{
    [Produces("application/json")]
    [Route("api/Worker")]
    public class WorkerController : Controller
    {
        private readonly ApplicationDbContext _context;

        public WorkerController(ApplicationDbContext context)
        {
            _context = context;
        }

        /// <summary>
        /// Web¶Ë£ºËÑË÷¹¤ÈË
        /// </summary>
        /// <param name="search"></param>
        /// <returns></returns>
        [HttpGet]
        public IEnumerable<WorkerSimpleViewModel> GetWorkers([FromQuery]string search)
        {
            var query = _context.Worker.AsQueryable();
            if (!string.IsNullOrWhiteSpace(search))
            {
                query = query.Where(w => w.Name.Contains(search) || w.UserName.Contains(search));
            }
            return query.Take(10).ToList().Select(w => new WorkerSimpleViewModel(w));
        }

        /// <summary>
        /// Web¶Ë£ºËÑË÷°à³¤
        /// </summary>
        /// <param name="search"></param>
        /// <returns></returns>
        [HttpGet("Header")]
        public IEnumerable<Worker> Header([FromQuery]string search)
        {
            var query = _context.Worker.Where(w => w.IsHeader);
            if (!string.IsNullOrWhiteSpace(search))
            {
                query = query.Where(w => w.Name.Contains(search) || w.UserName.Contains(search));
            }
            return query.Take(10);
        }


    }
}