using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.ViewFeatures;
using Microsoft.EntityFrameworkCore;
using Przychodnia.Database.Data;

namespace Przychodnia.PortalWWW.Controllers
{
	public class SpecializationsMenuComponent : ViewComponent
	{
		private readonly PrzychodniaContext _context;
		public SpecializationsMenuComponent(PrzychodniaContext context)
		{
			_context = context;
		}
		public async Task<IViewComponentResult> InvokeAsync()
		{
			//do widoku SpecializationsMenuComponent przekazujemy wszystkie rodzaje z BD asynchronicznie (bez użycia ViewBagów)
			return View("SpecializationsMenuComponent", await _context.Specialization.ToListAsync());
		}
	}
}
