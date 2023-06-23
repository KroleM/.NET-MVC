using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Przychodnia.Database.Data;
using Przychodnia.Database.Data.Visits;
using Przychodnia.PortalWWW.Helpers;
using Przychodnia.PortalWWW.ViewModels;
using System.Numerics;

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
					Price = doc.Price,
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
		public async Task<IActionResult> Details(DoctorParameter param)
		{
			ViewBag.Specializations = await _context.Specialization.ToListAsync();

			var doctor = await _context.Doctor.Where(t => t.Id == param.DoctorId).FirstOrDefaultAsync();
			var visits = await _context.DoctorDateTime.Where(d => d.DoctorId == param.DoctorId).Include(ddt => ddt.VisitDateTime).ToListAsync();

			Dictionary<DateTime, List<DoctorDateTime>>? visitsDictionary = new();

			foreach (var visit in visits.Where(d => d.VisitDateTime.Date >= DateTime.Today.AddDays(param.StartDay) && d.VisitDateTime.Date < DateTime.Today.AddDays(param.StartDay + 7)))
			{
				if (!visitsDictionary.ContainsKey(visit.VisitDateTime.Date.Date))
				{
					visitsDictionary.Add(visit.VisitDateTime.Date.Date, new List<DoctorDateTime>());
				}
				visitsDictionary[visit.VisitDateTime.Date.Date].Add(visit);
			}

			var viewModel = new DoctorDisplayViewModel
			{
				Id = doctor.Id,
				Name = doctor.Name,
				//Address = doc.Address,
				//BirthDate = doc.BirthDate,
				Price = doctor.Price,
				LicenceNumber = doctor.LicenceNumber,
				SpecializationId = doctor.SpecializationId,
				Specialization = doctor.Specialization,
				Calendar = visitsDictionary
			};
			if (doctor.Picture != null)
			{
				viewModel.Picture = Convert.ToBase64String(doctor.Picture);
				viewModel.PictureFormat = doctor.PictureFormat;
			}

			//return View(await _context.Doctor.Where(t => t.Id == id).FirstOrDefaultAsync());
			return View(viewModel); //to powinno zostać
		}
	}
}
