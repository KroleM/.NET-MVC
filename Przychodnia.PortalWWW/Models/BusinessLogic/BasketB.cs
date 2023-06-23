using Microsoft.AspNetCore.Mvc.ViewFeatures;
using Przychodnia.Database.Data;
using Przychodnia.Database.Data.Visits;

namespace Przychodnia.PortalWWW.Models.BusinessLogic
{
	public class BasketB
	{
		/*
		private readonly PrzychodniaContext _context;
		private string BasketSessionId;  //tu jest przechowywane Id przeglądarki, która łączy się z systemem
		public BasketB(PrzychodniaContext context, HttpContext httpContext)
		{
			_context = context;
			BasketSessionId = GeBasketSessionId(httpContext);
		}
		//To jest funkcja, która pobiera indentyfikator przeglądarki, która łączy się z systemem.
		private string GeBasketSessionId(HttpContext httpContext)
		{
			//Jeżeli w sesji BasketSessionId jest nullem, to trzeba to IdSesji wygenerować.
			if (httpContext.Session.GetString("BasketSessionId") == null)
			{
				//Generujemy to BasketSessionId na podstawie httpContext.User.Identity.Name
				//Jeżeli to Name nie jest puste, albo nie ma białych znaków...
				if (!string.IsNullOrWhiteSpace(httpContext.User.Identity.Name))
				{
					//... to wtedy httpContext.User.Identity.Name staje się BasketSessionId
					httpContext.Session.SetString("BasketSessionId", httpContext.User.Identity.Name);
				}
				else
				{
					//W przeciwnym wypadku generujemy to Id przy pomocy Giud'a
					Guid tempBasketSessionId = Guid.NewGuid();
					//I wysyłamy wygenerowane BasketSessionId jako wygenerowane cookie
					httpContext.Session.SetString("BasketSessionId", tempBasketSessionId.ToString());
				}
			}
			return httpContext.Session.GetString("BasketSessionId").ToString();
		}
		//To jest funkcja dodająca nowy doctorDateTime do koszyka
		public void AddToBasket(DoctorDateTime doctorDateTime)
		{
			//Najpierw sprawdzamy, czy dany doctorDateTime nie istnieje już w koszyku danego klienta
			var basketElement =
				(
					from element in _context.BasketElement
					where element.TowarId == doctorDateTime.IdTowaru && element.IdSesjiKoszyka == this.BasketSessionId
					select element
				).FirstOrDefault();
			//var basketElement = _context.ElementKoszyka.FirstOrDefaultAsync(elem => elem.TowarId == doctorDateTime.IdTowaru && elem.BasketSessionId == this.BasketSessionId);
			//jeżeli towaru brak w koszyku
			if (basketElement == null)
			{
				//tworzymy doctorDateTime w koszyku
				basketElement = new ElementKoszyka()
				{
					IdSesjiKoszyka = this.BasketSessionId,
					TowarId = doctorDateTime.IdTowaru,
					Towar = _context.Towar.Find(doctorDateTime.IdTowaru),
					Ilosc = 1,
					DataUtworzenia = DateTime.Now
				};
				//dodajemy element koszyka do BD
				_context.ElementKoszyka.Add(basketElement);
			}
			else //jeżeli doctorDateTime jest już w koszyku
			{
				basketElement.Ilosc++;
			}
			//zapis zmian w BD
			_context.SaveChanges();
		}
		// Funkcja pobiera wszystkie elementy koszyka danej przeglądarki (sesji)
		public async Task<List<ElementKoszyka>> GetElementyKoszyka()
		{
			return await _context.ElementKoszyka.Where(e => e.IdSesjiKoszyka == this.BasketSessionId).Include(e => e.Towar).ToListAsync();
			//Include, aby załadować relacje między tabelami (jeden do wielu?)
		}
		//Funkcja, która oblicza wartość koszyka
		public async Task<decimal> GetRazem()
		{
			var items =
			(
				from element in _context.ElementKoszyka
				where element.IdSesjiKoszyka == this.BasketSessionId
				select (decimal?)element.Ilosc * element.Towar.Cena
			);
			return await items.SumAsync() ?? 0;
		}
		*/
	}
}
