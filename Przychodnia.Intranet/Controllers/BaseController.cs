using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.ViewFeatures;
using Przychodnia.Database.Data;

namespace Przychodnia.Intranet.Controllers
{
    public abstract class BaseController<TEntity> : Controller
    {
        private readonly PrzychodniaContext _context;
        public PrzychodniaContext Context { get => _context; }
        public BaseController(PrzychodniaContext context)
        {
            _context = context;
        }
        //Funkcja jest abstrakcyjna, jeżeli nie ma bloku
        public abstract Task<List<TEntity>> GetEntityList();
        public async Task<IActionResult> Index()
        {
            return View(await GetEntityList());
        }
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
        //Ta metoda Create wywołuje się przy wejściu na widok dodawania rekordu
        public async Task<IActionResult> Create()
        {
            await SetSelectList();  //przed wywołaniem widoku inicjalizujemy ComboBoxy funkcją SetSelectList()
            return View(); //funkcja Create w kontrolerze włącza widok o tej samej nazwie, czyli Create
        }
        //Uniwersalna metoda, która wywołuje się po naciśnięciu przycisku "Dodaj" na widoku Create
        [HttpPost]
        public async Task<IActionResult> Create(TEntity entity)
        {
            await Context.AddAsync(entity);
            await Context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

    }
}
