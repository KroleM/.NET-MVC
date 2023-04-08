using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using Przychodnia.Intranet.Data;
using Przychodnia.Intranet.Models.CMS;

namespace Przychodnia.Intranet.Controllers
{
    public class FeedController : Controller
    {
        private readonly PrzychodniaIntranetContext _context;

        public FeedController(PrzychodniaIntranetContext context)
        {
            _context = context;
        }

        // GET: Feed
        public async Task<IActionResult> Index()
        {
              return _context.Feed != null ? 
                          View(await _context.Feed.ToListAsync()) :
                          Problem("Entity set 'PrzychodniaIntranetContext.Feed'  is null.");
        }

        // GET: Feed/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null || _context.Feed == null)
            {
                return NotFound();
            }

            var feed = await _context.Feed
                .FirstOrDefaultAsync(m => m.Id == id);
            if (feed == null)
            {
                return NotFound();
            }

            return View(feed);
        }

        // GET: Feed/Create
        public IActionResult Create()
        {
            return View();
        }

        // POST: Feed/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("Id,LinkTitle,Title,Content,Priority")] Feed feed)
        {
            if (ModelState.IsValid)
            {
                _context.Add(feed);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            return View(feed);
        }

        // GET: Feed/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null || _context.Feed == null)
            {
                return NotFound();
            }

            var feed = await _context.Feed.FindAsync(id);
            if (feed == null)
            {
                return NotFound();
            }
            return View(feed);
        }

        // POST: Feed/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("Id,LinkTitle,Title,Content,Priority")] Feed feed)
        {
            if (id != feed.Id)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(feed);
                    await _context.SaveChangesAsync();
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
            return View(feed);
        }

        // GET: Feed/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null || _context.Feed == null)
            {
                return NotFound();
            }

            var feed = await _context.Feed
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
            if (_context.Feed == null)
            {
                return Problem("Entity set 'PrzychodniaIntranetContext.Feed'  is null.");
            }
            var feed = await _context.Feed.FindAsync(id);
            if (feed != null)
            {
                _context.Feed.Remove(feed);
            }
            
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool FeedExists(int id)
        {
          return (_context.Feed?.Any(e => e.Id == id)).GetValueOrDefault();
        }
    }
}
