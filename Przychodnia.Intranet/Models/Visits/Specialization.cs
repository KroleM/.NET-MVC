using System.ComponentModel.DataAnnotations;
using System.Xml.Linq;

namespace Przychodnia.Intranet.Models.Visits
{
	public class Specialization
	{
		[Key]
		public int Id { get; set; }
		[Required(ErrorMessage = "Podaj nazwę specjalizacji")]
		[MaxLength(30, ErrorMessage = "Nazwa specjalizacji może mieć max. 30 znaków")]
		[Display(Name = "Nazwa specjalizacji")]
		public string Name { get; set; }

		public List<Doctor>? Doctors { get; set; }
	}
}
