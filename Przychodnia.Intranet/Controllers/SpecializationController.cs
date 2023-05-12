using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Przychodnia.Database.Data;
using Przychodnia.Database.Data.Visits;

namespace Przychodnia.Intranet.Controllers
{
    public class SpecializationController : Controller
    {
        private readonly PrzychodniaContext _context;

        public SpecializationController(PrzychodniaContext context)
        {
            _context = context;
        }

        // GET: Specialization
        public async Task<IActionResult> Index()
        {
              return _context.Specialization != null ? 
                          View(await _context.Specialization.ToListAsync()) :
                          Problem("Entity set 'PrzychodniaContext.Specialization'  is null.");
        }

        // GET: Specialization/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null || _context.Specialization == null)
            {
                return NotFound();
            }

            var specialization = await _context.Specialization
                .FirstOrDefaultAsync(m => m.Id == id);
            if (specialization == null)
            {
                return NotFound();
            }

            return View(specialization);
        }

        // GET: Specialization/Create
        public IActionResult Create()
        {
            return View();
        }

        // POST: Specialization/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("Id,Name")] Specialization specialization)
        {
            if (ModelState.IsValid)
            {
                _context.Add(specialization);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            return View(specialization);
        }

        // GET: Specialization/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null || _context.Specialization == null)
            {
                return NotFound();
            }

            var specialization = await _context.Specialization.FindAsync(id);
            if (specialization == null)
            {
                return NotFound();
            }
            return View(specialization);
        }

        // POST: Specialization/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("Id,Name")] Specialization specialization)
        {
            if (id != specialization.Id)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(specialization);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!SpecializationExists(specialization.Id))
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
            return View(specialization);
        }

        // GET: Specialization/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null || _context.Specialization == null)
            {
                return NotFound();
            }

            var specialization = await _context.Specialization
                .FirstOrDefaultAsync(m => m.Id == id);
            if (specialization == null)
            {
                return NotFound();
            }

            return View(specialization);
        }

        // POST: Specialization/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            if (_context.Specialization == null)
            {
                return Problem("Entity set 'PrzychodniaContext.Specialization'  is null.");
            }
            var specialization = await _context.Specialization.FindAsync(id);
            if (specialization != null)
            {
                _context.Specialization.Remove(specialization);
            }
            
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool SpecializationExists(int id)
        {
          return (_context.Specialization?.Any(e => e.Id == id)).GetValueOrDefault();
        }
    }
}
