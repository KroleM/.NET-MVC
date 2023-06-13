using System.ComponentModel.DataAnnotations;
using System.Xml.Linq;

namespace Przychodnia.Database.Data.Visits
{
	public class Doctor : Person
	{
		[Required(ErrorMessage = "Numer licencji jest wymagany")]
		[Display(Name = "Numer licencji")]
		public string LicenceNumber { get; set; }
        [Display(Name = "Specjalizacja")]
        public int SpecializationId { get; set; }
		public Specialization Specialization { get; set; }
	}
}
