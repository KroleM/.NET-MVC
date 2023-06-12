﻿using Microsoft.AspNetCore.Mvc;
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
			return View("FeedComponent", await _context.Feed.ToListAsync());
		}
	}
}