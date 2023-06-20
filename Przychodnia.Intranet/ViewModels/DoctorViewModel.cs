using Microsoft.EntityFrameworkCore.Metadata.Internal;
using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;
using System.Xml.Linq;
using Przychodnia.Database.Data.Visits;

namespace Przychodnia.Intranet.ViewModels
{
	public class DoctorViewModel
	{
		public int Id { get; set; }
		[Required(ErrorMessage = "Imię i nazwisko są wymagane")]
		[Display(Name = "Imię i nazwisko")]
		public string Name { get; set; }
		[Required(ErrorMessage = "Adres jest wymagany")]
		[Display(Name = "Adres zamieszkania")]
		public string Address { get; set; }
		[Required(ErrorMessage = "Data urodzenia jest wymagana")]
		[Display(Name = "Data urodzenia")]
		[Column(TypeName = "date")]
		public DateTime BirthDate { get; set; }
        [Required(ErrorMessage = "Zdjęcie jest wymagane")]
        public IFormFile Picture { get; set; }

		[Required(ErrorMessage = "Numer licencji jest wymagany")]
		[Display(Name = "Numer licencji")]
		public string LicenceNumber { get; set; }
		[Display(Name = "Specjalizacja")]
		public int SpecializationId { get; set; }
		//[Display(Name = "Specjalizacja")]
		//public Specialization Specialization { get; set; }
	}
}
