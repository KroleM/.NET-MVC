using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Przychodnia.Database.Data;
using Przychodnia.PortalWWW.Models;
using System.Diagnostics;

namespace Przychodnia.PortalWWW.Controllers
{
    public class HomeController : Controller
    {
        private readonly ILogger<HomeController> _logger;
        private readonly PrzychodniaContext context_;

        public HomeController(PrzychodniaContext context)
        {
            context_ = context;
        }

        public IActionResult Index(int? id)
        {
            ViewBag.PageModel =
            (
                    from page in context_.Page
                    orderby page.Priority
                    select page
                ).ToList();
            
            if (id == null)
            {
                id = context_.Page.First().Id;
            }
            var item = context_.Page.Find(id);

            return View(item);
        }
        public IActionResult Privacy()
        {
            return View();
        }
        public IActionResult Pricelist()
        {
            return View();
        }
        public IActionResult Facilities()
        {
            return View();
        }
        public IActionResult Doctors()
        {
            return View();
        }
        public IActionResult About()
        {
            return View();
        }

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }
    }
}