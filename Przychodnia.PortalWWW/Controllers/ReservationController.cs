using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Przychodnia.Database.Data;
using Przychodnia.PortalWWW.ViewModels;

namespace Przychodnia.PortalWWW.Controllers
{
    public class ReservationController : Controller
    {
        private readonly PrzychodniaContext _context;

		public ReservationController(PrzychodniaContext context)
		{
			_context = context;
		}

		public async Task<IActionResult> Index(int? id) //id - kliknięta specjalizacja
		{
			ViewBag.Specializations = await _context.Specialization.ToListAsync();

			if (id == null)
			{
				var pierwszy = await _context.Specialization.FirstAsync();
				id = pierwszy.Id;
			}

            var doctors = await _context.Doctor.Where(d => d.SpecializationId == id).ToListAsync();
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
            //return View(await _context.Doctor.Where(t => t.SpecializationId == id).ToListAsync());
		}
        public async Task<IActionResult> Details(int? id)
        {
            ViewBag.Specializations = await _context.Specialization.ToListAsync();
            return View(await _context.Doctor.Where(t => t.Id == id).FirstOrDefaultAsync());

        }
    }
}
