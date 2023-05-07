using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.ViewFeatures;
using Przychodnia.Data.Data;
using Przychodnia.PortalWWW.Models;
using System.Diagnostics;

namespace Przychodnia.PortalWWW.Controllers
{
    public class HomeController : Controller
    {
        private readonly ILogger<HomeController> _logger;

        private readonly PrzychodniaContext _context; // this object symbolizes all classes, which enable DB connection

        public HomeController(PrzychodniaContext context)
        {
            _context = context;
        }

        public IActionResult Index(int? id) //parametrem jest Id strony, którą kliknięto
        {
            //ViewBag to taki "listonosz" który przenosi dane między kontrolerem a widokiem
            ViewBag.PageModel =   //nazwę "PageModel" nadajemy sami (może być inna)
                (
                    from page in _context.Page
                    orderby page.Priority
                    select page
                ).ToList();
            ViewBag.FeedModel =
                (
                    from feed in _context.Page
                    orderby feed.Priority
                    select feed
                ).ToList();
            //przy pierwszym uruchomieniu jeszcze nic nie kliknięto, wtedy wyświetli się pierwsza strona
            if (id == null)
            {
                id = _context.Page.First().Id;
            }
            //wyszukujemy w BD stronę o danym klikniętym Id lub pierwszą stronę w przypadku pierwszego uruchomienia
            var item = _context.Page.Find(id);
            //znalezioną stronę przekazujemy do widoku
            return View(item);
        }

        //public IActionResult Index()
        //{
        //    return View();
        //}
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