using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.ViewFeatures;
using Przychodnia.Database.Data;
using Przychodnia.PortalWWW.Models.BusinessLogic;
using Przychodnia.PortalWWW.ViewModels;

namespace Przychodnia.PortalWWW.Controllers
{
	public class BasketController : Controller
	{
		public readonly PrzychodniaContext _context;

		public BasketController(PrzychodniaContext context)
		{
			this._context = context;
		}
		//Ta funkcja wystawia dane do wyświetlania koszyka
		public async Task<IActionResult> Index()
		{
			//Tworzę obiekt klasy logiki biznesowej BasketB, zdefiniowanej w warstwie model, z którego to obiektu użyję 2 funkcji
			BasketB basketB = new BasketB(this._context, this.HttpContext);
			//Chcemy przekazać do widoku 2 rzeczy: listę elementów koszyka i sumę Razem. Po to stworzona została klasa DaneDoKoszyka
			//Dzięki temu nie musimy używać ViewBag, czy ViewData, co byłoby mniej profesjonalne
			var basketViewModel = new BasketViewModel
			{
				BasketElements = await basketB.GetBasketElements(),
				Total = await basketB.GetTotal()
			};
			//W jednym obiekcie przekazuję 2 rzeczy do widoku (można przekazać tylko jeden obiekt!)
			return View(basketViewModel);
		}
		public async Task<IActionResult> DodajDoKoszyka(int id)
		{
			BasketB basketB = new BasketB(this._context, this.HttpContext);
			basketB.AddToBasket(await _context.DoctorDateTime.FindAsync(id));
			return RedirectToAction("Index");
		}
	}
}
