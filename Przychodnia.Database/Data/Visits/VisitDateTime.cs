using Microsoft.EntityFrameworkCore.Metadata.Internal;
using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;

namespace Przychodnia.Database.Data.Visits
{
	public class VisitDateTime : TEntity
	{
		[Required(ErrorMessage = "Data jest wymagana")]
		[Display(Name = "Data i godzina")]
		//[Column(TypeName = "date")]
		public DateTime Date { get; set; }
		//[Required(ErrorMessage = "Czas jest wymagana")]
		//[Display(Name = "Godzina")]
		//[Column(TypeName = "time")]
		//public DateTime Time { get; set; }
		public List<Doctor>? Doctors { get; set; }
	}
}
