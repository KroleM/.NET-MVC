using System.ComponentModel.DataAnnotations;
using System.Xml.Linq;

namespace Przychodnia.Intranet.Models.Visits
{
	public class Doctor : Person
	{
		[Required(ErrorMessage = "Numer licencji jest wymagany")]
		[Display(Name = "Numer licencji")]
		public string LicenceNumber { get; set; }

		public int SpecializationId { get; set; }
		public Specialization Specialization { get; set; }
	}
}
