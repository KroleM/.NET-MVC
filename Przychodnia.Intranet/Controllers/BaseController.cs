using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.AspNetCore.Mvc.ViewFeatures;
using Microsoft.EntityFrameworkCore;
using Przychodnia.Database.Data;
using Przychodnia.Database.Data.CMS;
using Przychodnia.Database.Data.Visits;

namespace Przychodnia.Intranet.Controllers
{
    //public abstract class BaseController<TEntity> : Controller where TEntity : class
    public abstract class BaseController<T> : Controller where T : TEntity
    { 
        private readonly PrzychodniaContext _context;
        public PrzychodniaContext Context { get => _context; }
        public BaseController(PrzychodniaContext context)
        {
            _context = context;
        }
        //Funkcja jest abstrakcyjna, jeżeli nie ma bloku
        public abstract Task<List<T>> GetEntityList();
        public abstract Task<T> GetEntityItem(int? id);
        /// <summary>
        /// Ta metoda ustawia dane do wyboru z ComboBoxa (Selecta). 
        /// Inicjalizuje dane do wyboru w ComboBoxie. Jeżeli tabela (encja) będzie miała klucze obce, to wtedy nadpiszemy tę funkcję w klasach dziedziczących.
        /// Jeżeli tabela nie będzie miała kluczy obcych, to ta metoda nie zostanie nadpisana w klasie dziedziczącej i zwróci null.
        /// </summary>
        /// <returns></returns>
        public virtual Task SetSelectList()
        {
            return null;
        }
		// Analogicznie jak powyżej, ale ta metoda w zamierzeniu zwraca dane do Selecta z wybranym elementem, np. new SelectList(icons, "Id", "Name", item.IconId);
		public virtual Task SetSelectListItem(T item)
		{
			return null;
		}
		public async Task<IActionResult> Index()
		{
			return View(await GetEntityList());
		}
		//Ta metoda Create wywołuje się przy wejściu na widok dodawania rekordu
		public async Task<IActionResult> Create()
        {
            await (SetSelectList() ?? Task.CompletedTask);  //przed wywołaniem widoku inicjalizujemy ComboBoxy funkcją SetSelectList()
            return View(); //funkcja Create w kontrolerze włącza widok o tej samej nazwie, czyli Create
        }
        //Uniwersalna metoda, która wywołuje się po naciśnięciu przycisku "Dodaj" na widoku Create
        [HttpPost]
        public async Task<IActionResult> Create(T entity)
        {
            await Context.AddAsync(entity);
            await Context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null || Context.Set<T>() == null)
            {
                return NotFound();
            }

            var item = await Context.Set<T>().FindAsync(id);
            if (item == null)
            {
                return NotFound();
            }
            await (SetSelectListItem(item) ?? Task.CompletedTask);  // "await null" wyrzuci błąd
            return View(item);
        }
        
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, T entity)
        {
            if (id != entity.Id)
            {
                return NotFound();
            }

            try
            {
                Context.Update(entity);
                await Context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!TEntityExists(entity.Id))
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

		public async Task<IActionResult> Details(int? id)
		{
			if (id == null || Context.Set<T>() == null)
			{
				return NotFound();
			}

            var entity = GetEntityItem(id);
			if (entity == null)
			{
				return NotFound();
			}

			return View(await entity);
		}

		public async Task<IActionResult> Delete(int? id)
		{
			if (id == null || Context.Set<T>() == null)
			{
				return NotFound();
			}

			var entity = GetEntityItem(id);
			if (entity == null)
			{
				return NotFound();
			}

			return View(await entity);
		}

		[HttpPost, ActionName("Delete")]
		[ValidateAntiForgeryToken]
		public async Task<IActionResult> DeleteConfirmed(int id)
		{
			if (Context.Set<T>() == null)
			{
				return Problem($"Entity set 'PrzychodniaContext.{0}' is null.", nameof(T));
			}
			var entity = await Context.Set<T>().FindAsync(id);
			if (entity != null)
			{
				Context.Set<T>().Remove(entity);
			}

			await Context.SaveChangesAsync();
			return RedirectToAction(nameof(Index));
		}

		private bool TEntityExists(int id)
        {
            return (Context.Set<T>()?.Any(e => e.Id == id)).GetValueOrDefault();
        }
        
    }
}
