using Microsoft.AspNetCore.Mvc.ViewFeatures;
using Microsoft.EntityFrameworkCore;
using Przychodnia.Database.Data;
using Przychodnia.Database.Data.Visits;

namespace Przychodnia.PortalWWW.Models.BusinessLogic
{
	public class BasketB
	{
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
					//W przeciwnym wypadku generujemy to Id przy pomocy Guid'a
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
					where element.Id == doctorDateTime.Id && element.BasketSessionId == this.BasketSessionId
					select element
				).FirstOrDefault();

			//jeżeli towaru brak w koszyku
			if (basketElement == null)
			{
				//tworzymy doctorDateTime w koszyku
				basketElement = new BasketElement()
				{
					BasketSessionId = this.BasketSessionId,
					DoctorDateTimeId = doctorDateTime.Id,
					DoctorDateTime = _context.DoctorDateTime.Find(doctorDateTime.Id),
					CreationDate = DateTime.Now
				};
				//dodajemy element koszyka do BD
				_context.BasketElement.Add(basketElement);
				_context.SaveChanges();
			}
		}
		// Funkcja pobiera wszystkie elementy koszyka danej przeglądarki (sesji)
		public async Task<List<BasketElement>> GetBasketElements()
		{
			return await _context.BasketElement.Where(e => e.BasketSessionId == this.BasketSessionId)
				.Include(e => e.DoctorDateTime.VisitDateTime)
				.Include(f => f.DoctorDateTime.Doctor)
				.Include(g => g.DoctorDateTime.Doctor.Specialization)
				.OrderBy(h => h.DoctorDateTime.VisitDateTime.Date)
				.ToListAsync();
			//Include, aby załadować relacje między tabelami (jeden do wielu?)
		}
		//Funkcja, która oblicza wartość koszyka
		public async Task<double> GetTotal()
		{
			var items =
			(
				from element in _context.BasketElement
				where element.BasketSessionId == this.BasketSessionId
				select element.DoctorDateTime.Doctor.Price
			);
			return await items.SumAsync();	// ?? 0;
		}
		public async Task Pay()
		{
			var basketElements = await _context.BasketElement.Where(x => x.BasketSessionId == this.BasketSessionId).ToListAsync(); ;
			foreach (var item in basketElements)
			{
				var ddt = await _context.DoctorDateTime.FindAsync(item.DoctorDateTimeId);
				ddt.IsBooked = true;
				_context.BasketElement.Remove(item);
				_context.SaveChanges();
			}
		}
	}
}
