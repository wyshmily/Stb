using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Net;
using System.Text.RegularExpressions;
using Stb.Official.Models;
using System.Text;

namespace Stb.Official.Controllers
{
    [Area(AreaNames.Official)]
    public class HomeController : Controller
    {
        public IActionResult Index()
        {
            return View();
        }

        public IActionResult Talent()
        {
            return View();
        }

        async public Task<IActionResult> Service()
        {
            HttpClient client = new HttpClient();
            client.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));
            HttpResponseMessage response = await client.GetAsync("http://42.96.155.165:8080/stb-web/bill/standardlist2.do");
            List<Standard> itemList = new List<Standard>();
            if (response.StatusCode == HttpStatusCode.OK)
            {
                string json = await response.Content.ReadAsStringAsync();
                if (json.StartsWith("null(") && json.EndsWith(")"))
                {
                    json = json.Substring("null(".Length, json.Length - "null(".Length - 1);
                    itemList = Newtonsoft.Json.JsonConvert.DeserializeObject<List<Standard>>(json);
                }
            }

            ServiceViewModel serviceViewModel = new ServiceViewModel
            {
                standardList = new Microsoft.AspNetCore.Mvc.Rendering.SelectList(itemList, "standardid", "name"),
            };

            return View(serviceViewModel);
        }

        public IActionResult JoinUs()
        {
            return View();
        }

        public IActionResult Contact()
        {
            ViewData["Message"] = "Your contact page.";

            return View();
        }

        public IActionResult Error()
        {
            return View();
        }

        // POST: Movies/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        async public Task<IActionResult> PubService(/*[Bind("phone,content")]*/ ServiceViewModel service)
        {
            if (ModelState.IsValid)
            {
                HttpClient client = new HttpClient();
                client.BaseAddress = new Uri("http://42.96.155.165:8080/stb-web/apply/applyBill.do");
                client.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));
                HttpResponseMessage response = await client.GetAsync($"?type={service.job.standardid}&description={service.job.content}&payphone={service.job.phone}");
            }
            return Ok();
        }
    }
}
