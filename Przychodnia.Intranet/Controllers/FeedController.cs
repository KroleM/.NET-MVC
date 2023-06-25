using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using Przychodnia.Database.Data;
using Przychodnia.Database.Data.CMS;

namespace Przychodnia.Intranet.Controllers
{
    public class FeedController : BaseController<Feed>
	{
        public FeedController(PrzychodniaContext context)
            : base(context)
        {
        }
		public override async Task<List<Feed>> GetEntityList()
		{
			//z bazy danych pobieramy asynchronicznie listę wszystkich Aktualności (Feed)
			//Include(...) ładuje klucz obcy - może być ich wiele
			return await Context.Feed.Include(f => f.Icon).ToListAsync();
			//tutaj tak naprawdę powinno być zapytanie LinQ, które wyświetli tylko te kolumny, które nas interesują
		}
		public override async Task<Feed> GetEntityItem(int? id)
        {
            return await Context.Feed
				.Include(f => f.Icon)
				.FirstOrDefaultAsync(m => m.Id == id);
		}
		public override async Task SetSelectList()
		{
			var icons = await Context.Icon.ToListAsync();
			ViewBag.Icons = new SelectList(icons, "Id", "Name");
			//ViewData["Icons"] = new SelectList(icons, "Id", "Name");    // <-- wersja alternatywna
		}
        public override async Task SetSelectListItem(Feed item)
        {
            var icons = await Context.Icon.ToListAsync();
            ViewBag.Icons = new SelectList(icons, "Id", "Name", item.IconId);
			//ViewData["Icons"] = new SelectList(icons, "Id", "Name", item.IconId);    // <-- wersja alternatywna
		}

        // Poniżej jest oryginalna automatycznie wygenerowana treść kontrolera
		/*
        public async Task<IActionResult> Index()
		{
			return _context.Feed != null ?
						  View(await _context.Feed.Include(f => f.Icon).ToListAsync()) :
						  Problem("Entity set 'PrzychodniaContext.Feed'  is null.");
		}
        // GET: Feed/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null || Context.Feed == null)
            {
                return NotFound();
            }

            var feed = await Context.Feed
				.Include(f => f.Icon)
				.FirstOrDefaultAsync(m => m.Id == id);
            if (feed == null)
            {
                return NotFound();
            }

            return View(feed);
        }
        /*
        // GET: Feed/Create
        public async Task<IActionResult> Create()
		{
			var icons = await _context.Icon.ToListAsync();
			ViewBag.Icons = new SelectList(icons, "Id", "Name");
			return View();
        }

        // POST: Feed/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        //public async Task<IActionResult> Create([Bind("Id,Title,Content,Priority,IconId")] Feed feed)
        public async Task<IActionResult> Create(Feed feed)
        {
            //if (ModelState.IsValid)
            {
                await _context.AddAsync(feed);    //_context.Feed.Add(feed); - działanie to samo
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
			ViewData["Icons"] = new SelectList(_context.Icon, "Id", "Name", feed.IconId);
			return View(feed);
        }
        */
		// GET: Feed/Edit/5
		/*
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null || Context.Feed == null)
            {
                return NotFound();
            }

            var feed = await Context.Feed.FindAsync(id);
            if (feed == null)
            {
                return NotFound();
            }
			ViewData["Icons"] = new SelectList(Context.Icon, "Id", "Name", feed.IconId);
			return View(feed);
        }
        
        // POST: Feed/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("Id,Title,Content,Priority,IconId")] Feed feed)
        {
            if (id != feed.Id)
            {
                return NotFound();
            }

            //if (ModelState.IsValid)
            {
                try
                {
                    Context.Update(feed);
                    await Context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!FeedExists(feed.Id))
                    {
                        return NotFound();
                    }
                    else
                    {
                        throw;
                    }
                }
                return RedirectToAction(nameof(Index));
            }
            ViewData["Icons"] = new SelectList(Context.Icon, "Id", "Name", feed.IconId);  //??
            return View(feed);
        }
        
        // GET: Feed/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null || Context.Feed == null)
            {
                return NotFound();
            }

            var feed = await Context.Feed
				.Include(f => f.Icon)
				.FirstOrDefaultAsync(m => m.Id == id);
            if (feed == null)
            {
                return NotFound();
            }

            return View(feed);
        }

        // POST: Feed/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            if (Context.Feed == null)
            {
                return Problem("Entity set 'PrzychodniaContext.Feed'  is null.");
            }
            var feed = await Context.Feed.FindAsync(id);
            if (feed != null)
            {
                Context.Feed.Remove(feed);
            }
            
            await Context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool FeedExists(int id)
        {
          return (Context.Feed?.Any(e => e.Id == id)).GetValueOrDefault();
        }
        */
	}
}
