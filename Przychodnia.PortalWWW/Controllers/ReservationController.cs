using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Przychodnia.Database.Data;
using Przychodnia.Database.Data.Visits;
using Przychodnia.PortalWWW.Helpers;
using Przychodnia.PortalWWW.Models.BusinessLogic;
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

			List<Doctor> doctors = new();
			if (id == null)
			{
				doctors = await _context.Doctor.ToListAsync();
			}
			else
			{
				doctors = await _context.Doctor.Where(d => d.SpecializationId == id).ToListAsync();
			}
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

			var startDay = param.StartDay < 0 ? 0 : param.StartDay;

			var doctor = await _context.Doctor.Where(t => t.Id == param.DoctorId).FirstOrDefaultAsync();
			var visits = await
				(
					from element in _context.DoctorDateTime
					where element.DoctorId == param.DoctorId 
					&& element.IsBooked == false
					&& element.VisitDateTime.Date >= DateTime.Today.AddDays(startDay)
					&& element.VisitDateTime.Date < DateTime.Today.AddDays(startDay + 7)
					orderby element.VisitDateTime.Date
					select element
				).Include(ddt => ddt.VisitDateTime).ToListAsync();

			Dictionary<DateTime, List<DoctorDateTime>>? visitsDictionary = new();

			foreach (var visit in visits)
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
				Calendar = visitsDictionary,
				ScheduleDay = startDay
			};
			if (doctor.Picture != null)
			{
				viewModel.Picture = Convert.ToBase64String(doctor.Picture);
				viewModel.PictureFormat = doctor.PictureFormat;
			}

			//return View(await _context.Doctor.Where(t => t.Id == id).FirstOrDefaultAsync());
			return View(viewModel); //to powinno zostać
		}
		public async Task<IActionResult> Pay()
		{
			BasketB basketB = new BasketB(this._context, this.HttpContext);
			await basketB.Pay();
			return RedirectToAction("Index");
		}
	}
}
