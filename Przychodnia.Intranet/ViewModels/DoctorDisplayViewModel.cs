using Przychodnia.Database.Data.Visits;
using System.ComponentModel.DataAnnotations;
using System.Xml.Linq;

namespace Przychodnia.Intranet.ViewModels
{
	public class DoctorDisplayViewModel
	{
		public int Id { get; set; }
		[Display(Name = "Imię i nazwisko")]
		public string Name { get; set; }
		[Display(Name = "Adres zamieszkania")]
		public string Address { get; set; }
		[Display(Name = "Data urodzenia")]
		public DateTime BirthDate { get; set; }
		[Display(Name = "Zdjęcie")]
		public string? Picture { get; set; }
		public string? PictureFormat { get; set; }
		[Display(Name = "Numer licencji")]
		public string LicenceNumber { get; set; }
		[Display(Name = "Specjalizacja")]
		public int SpecializationId { get; set; }
		[Display(Name = "Specjalizacja")]
		public Specialization Specialization { get; set; }
	}
}
