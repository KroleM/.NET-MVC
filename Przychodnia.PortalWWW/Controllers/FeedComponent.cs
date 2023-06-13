using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Przychodnia.Database.Data;

namespace Przychodnia.PortalWWW.Controllers
{
	public class FeedComponent : ViewComponent
	{
		private readonly PrzychodniaContext _context;
		public FeedComponent(PrzychodniaContext context)
		{
			_context = context;
		}
		public async Task<IViewComponentResult> InvokeAsync()
		{
			//dopisane - do testów
			var model = await _context.Feed.ToListAsync();
			model.OrderBy(x => x.Priority);
			//alternatywnie
			var model1 = await
			(
				from feed in _context.Feed
				orderby feed.Priority
				select feed
			).ToListAsync();
			//koniec

			return View("FeedComponent", await _context.Feed.Include(f => f.Icon).ToListAsync());
		}
	}
}
