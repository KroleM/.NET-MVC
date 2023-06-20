using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.AspNetCore.Mvc.ViewFeatures;
using Microsoft.EntityFrameworkCore;
using Przychodnia.Database.Data;
using Przychodnia.Database.Data.Visits;
using Przychodnia.Intranet.ViewModels;

namespace Przychodnia.Intranet.Controllers
{
	public class DoctorController : Controller //: BaseController<Doctor>
	{
		private readonly PrzychodniaContext _context;
		public DoctorController(PrzychodniaContext context)
		{
			_context = context;

		}
		/*
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
        */
		// GET : Doctor
		public async Task<IActionResult> Index()
		{
			if (_context.Doctor == null)
			{
				Problem("Entity set 'PrzychodniaContext.Doctor' is null.");
			}
			var doctors = await _context.Doctor.Include(d => d.Specialization).ToListAsync();
			var doctorVMs = new List<DoctorDisplayViewModel>();
			foreach (var doc in doctors)
			{
				var viewModel = new DoctorDisplayViewModel
				{
					Id = doc.Id,
					Name = doc.Name,
					Address = doc.Address,
					BirthDate = doc.BirthDate,
					LicenceNumber = doc.LicenceNumber,
					SpecializationId = doc.SpecializationId,
					Specialization = doc.Specialization
				};
				if (doc.Picture != null)
				{
					viewModel.Picture = Convert.ToBase64String(doc.Picture);
					viewModel.PictureFormat = doc.PictureFormat;
				}
				doctorVMs.Add(viewModel);
			}
			return View(doctorVMs);
		}

		// ===============================================================
		// GET: Doctor/Details/5
		public async Task<IActionResult> Details(int? id)
		{
			if (id == null || _context.Doctor == null)
			{
				return NotFound();
			}

			var doctor = await _context.Doctor
				.Include(d => d.Specialization)
				.FirstOrDefaultAsync(m => m.Id == id);
			if (doctor == null)
			{
				return NotFound();
			}
			var doctorVM = new DoctorDisplayViewModel
			{
				Id = doctor.Id,
				Name = doctor.Name,
				Address = doctor.Address,
				BirthDate = doctor.BirthDate,
				LicenceNumber = doctor.LicenceNumber,
				SpecializationId = doctor.SpecializationId,
				Specialization = doctor.Specialization
			};
			if (doctor.Picture != null)
			{
				doctorVM.Picture = Convert.ToBase64String(doctor.Picture);
				doctorVM.PictureFormat = doctor.PictureFormat;
			}

			return View(doctorVM);
		}

		// GET: Doctor/Edit/5
		public async Task<IActionResult> Edit(int? id)
		{
			if (id == null || _context.Doctor == null)
			{
				return NotFound();
			}

			var doctor = await _context.Doctor.FindAsync(id);
			if (doctor == null)
			{
				return NotFound();
			}
			ViewData["SpecializationId"] = new SelectList(_context.Specialization, "Id", "Name", doctor.SpecializationId);
			var doctorVM = new DoctorViewModel
			{
				Id = doctor.Id,
				Name = doctor.Name,
				Address = doctor.Address,
				BirthDate = doctor.BirthDate,
				LicenceNumber = doctor.LicenceNumber,
				SpecializationId = doctor.SpecializationId,
			};

			return View(doctorVM);
		}

		// POST: Doctor/Edit/5
		// To protect from overposting attacks, enable the specific properties you want to bind to.
		// For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
		[HttpPost]
		[ValidateAntiForgeryToken]
		public async Task<IActionResult> Edit(int id, DoctorViewModel doctorViewModel)
		{
            if (id != doctorViewModel.Id)
            {
                return NotFound();
            }

            var doctor = new Doctor
            {
				Id = doctorViewModel.Id,
                Name = doctorViewModel.Name,
                Address = doctorViewModel.Address,
                BirthDate = doctorViewModel.BirthDate,
                LicenceNumber = doctorViewModel.LicenceNumber,
                SpecializationId = doctorViewModel.SpecializationId,
                PictureFormat = doctorViewModel.Picture?.ContentType
            };

            if (doctorViewModel.Picture != null)
            {
                var memoryStream = new MemoryStream();
                doctorViewModel.Picture.CopyTo(memoryStream);
                doctor.Picture = memoryStream.ToArray();
            }

			//if (ModelState.IsValid)
			{
				try
				{
					_context.Update(doctor);
					await _context.SaveChangesAsync();
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
			ViewData["SpecializationId"] = new SelectList(_context.Specialization, "Id", "Name", doctor.SpecializationId);
			return View(doctorViewModel);
		}

		// GET: Doctor/Delete/5
		public async Task<IActionResult> Delete(int? id)
		{
			if (id == null || _context.Doctor == null)
			{
				return NotFound();
			}

			var doctor = await _context.Doctor
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
			if (_context.Doctor == null)
			{
				return Problem("Entity set 'PrzychodniaContext.Doctor' is null.");
			}
			var doctor = await _context.Doctor.FindAsync(id);
			if (doctor != null)
			{
				_context.Doctor.Remove(doctor);
			}

			await _context.SaveChangesAsync();
			return RedirectToAction(nameof(Index));
		}

		private bool DoctorExists(int id)
		{
			return (_context.Doctor?.Any(e => e.Id == id)).GetValueOrDefault();
		}

		public async Task<IActionResult> Create()
		{
			var specs = await _context.Specialization.ToListAsync();
			ViewBag.Specializations = new SelectList(specs, "Id", "Name");
			return View();
		}
		// Metoda wygenerowana - zastąpiona przez Create z BaseController
		// POST: Doctor/Create
		// To protect from overposting attacks, enable the specific properties you want to bind to.
		// For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.       
		[HttpPost]
		[ValidateAntiForgeryToken]
		public async Task<IActionResult> Create(DoctorViewModel doctorViewModel)
		{
			/*
            if (doctorViewModel == null)
            {
                throw new ArgumentNullException(nameof(DoctorViewModel));
            }
            if (!ModelState.IsValid)
            {
                return View();
            }
            */
			var doctor = new Doctor
			{
				Name = doctorViewModel.Name,
				Address = doctorViewModel.Address,
				BirthDate = doctorViewModel.BirthDate,
				LicenceNumber = doctorViewModel.LicenceNumber,
				SpecializationId = doctorViewModel.SpecializationId,
				PictureFormat = doctorViewModel.Picture?.ContentType
			};

			if (doctorViewModel.Picture != null)
			{
				var memoryStream = new MemoryStream();
				doctorViewModel.Picture.CopyTo(memoryStream);
				doctor.Picture = memoryStream.ToArray();
			}

			_context.Add(doctor);
			await _context.SaveChangesAsync();
			return RedirectToAction(nameof(Index));

			//ViewData["SpecializationId"] = new SelectList(_context.Specialization, "Id", "Name", doctor.SpecializationId);
			//return View(doctor);
		}

	}
}
