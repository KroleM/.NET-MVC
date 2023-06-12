using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.AspNetCore.Mvc.ViewFeatures;
using Microsoft.EntityFrameworkCore;
using Przychodnia.Database.Data;
using Przychodnia.Database.Data.Visits;

namespace Przychodnia.Intranet.Controllers
{
    public class DoctorController : BaseController<Doctor>
    {
        public DoctorController(PrzychodniaContext context)
            : base(context)
        {
        }
        public override async Task<List<Doctor>> GetEntityList()
        {
            //z bazy danych pobieramy asynchronicznie listę wszystkich lekarzy
            //Include(...) ładuje klucz obcy - może być ich wiele
            return await Context.Doctor.Include(d => d.Specialization).ToListAsync();
            //tutaj tak naprawdę powinno być zapytanie LinQ, które wyświetli tylko te kolumny, które nas interesują
        }
        public override async Task SetSelectList()
        {
            var specs = await Context.Specialization.ToListAsync();
            ViewBag.Specializations = new SelectList(specs, "Id", "Name");
            //ViewData["Specializations"] = new SelectList(specs, "Id", "Name");    // <-- wersja alternatywna
        }



        // ===============================================================
        // GET: Doctor/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null || Context.Doctor == null)
            {
                return NotFound();
            }

            var doctor = await Context.Doctor
                .Include(d => d.Specialization)
                .FirstOrDefaultAsync(m => m.Id == id);
            if (doctor == null)
            {
                return NotFound();
            }

            return View(doctor);
        }

        // GET: Doctor/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null || Context.Doctor == null)
            {
                return NotFound();
            }

            var doctor = await Context.Doctor.FindAsync(id);
            if (doctor == null)
            {
                return NotFound();
            }
            ViewData["SpecializationId"] = new SelectList(Context.Specialization, "Id", "Name", doctor.SpecializationId);
            return View(doctor);
        }

        // POST: Doctor/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("LicenceNumber,SpecializationId,Id,Name,Address,BirthDate,PhotoURL")] Doctor doctor)
        {
            if (id != doctor.Id)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    Context.Update(doctor);
                    await Context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!DoctorExists(doctor.Id))
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
            ViewData["SpecializationId"] = new SelectList(Context.Specialization, "Id", "Name", doctor.SpecializationId);
            return View(doctor);
        }

        // GET: Doctor/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null || Context.Doctor == null)
            {
                return NotFound();
            }

            var doctor = await Context.Doctor
                .Include(d => d.Specialization)
                .FirstOrDefaultAsync(m => m.Id == id);
            if (doctor == null)
            {
                return NotFound();
            }

            return View(doctor);
        }

        // POST: Doctor/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            if (Context.Doctor == null)
            {
                return Problem("Entity set 'PrzychodniaContext.Doctor'  is null.");
            }
            var doctor = await Context.Doctor.FindAsync(id);
            if (doctor != null)
            {
                Context.Doctor.Remove(doctor);
            }
            
            await Context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool DoctorExists(int id)
        {
          return (Context.Doctor?.Any(e => e.Id == id)).GetValueOrDefault();
        }

        // Metoda wygenerowana - zastąpiona przez Create z BaseController
        // POST: Doctor/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        /*
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("LicenceNumber,SpecializationId,Id,Name,Address,BirthDate,PhotoURL")] Doctor doctor)
        {
            if (ModelState.IsValid)
            {
                _context.Add(doctor);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            ViewData["SpecializationId"] = new SelectList(_context.Specialization, "Id", "Name", doctor.SpecializationId);
            return View(doctor);
        }
        */
    }
}
