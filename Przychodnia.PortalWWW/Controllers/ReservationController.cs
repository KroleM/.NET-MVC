using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Przychodnia.Database.Data;

namespace Przychodnia.PortalWWW.Controllers
{
    public class ReservationController : Controller
    {
        private readonly PrzychodniaContext _context;

		public ReservationController(PrzychodniaContext context)
		{
			_context = context;
		}

		public async Task<IActionResult> Index(int? id) //id - kliknięta specjalizacja
		{
			ViewBag.Specializations = await _context.Specialization.ToListAsync();

			if (id == null)
			{
				var pierwszy = await _context.Specialization.FirstAsync();
				id = pierwszy.Id;
			}
			return View(await _context.Doctor.Where(t => t.SpecializationId == id).ToListAsync());
		}
        public async Task<IActionResult> Details(int? id)
        {
            ViewBag.Specializations = await _context.Specialization.ToListAsync();
            return View(await _context.Doctor.Where(t => t.Id == id).FirstOrDefaultAsync());

        }
    }
}
