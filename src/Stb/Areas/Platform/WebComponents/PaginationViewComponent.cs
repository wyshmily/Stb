using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Stb.Platform.Models;

namespace Stb.Platform.WebComponents
{
    public class PaginationViewComponent : ViewComponent
    {
        public async Task<IViewComponentResult> InvokeAsync(string action, int page, int totalPage)
        {
            return View(new PaginationViewModel
            {
                Action = action,
                Page = page,
                TotalPage = totalPage,
            });
        }
    }
}
