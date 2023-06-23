using System.ComponentModel.DataAnnotations;
using System.Xml.Linq;

namespace Przychodnia.Database.Data.Visits
{
	public class Doctor : Person
	{
		[Required(ErrorMessage = "Numer licencji jest wymagany")]
		[Display(Name = "Numer licencji")]
		public string LicenceNumber { get; set; }
		[Required(ErrorMessage = "Podaj cenę wizyty")]
		[Display(Name = "Cena wizyty")]
		public double Price { get; set; }
		[Display(Name = "Specjalizacja")]
        public int SpecializationId { get; set; }
		[Display(Name = "Specjalizacja")]
		public Specialization Specialization { get; set; }
        public List<VisitDateTime>? VisitDateTimes { get; set; }
	}
}
