using Microsoft.EntityFrameworkCore.Metadata.Internal;
using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;
using System.Xml.Linq;

namespace Przychodnia.Database.Data.CMS
{
	public class Service
	{
		[Key]
		public int Id { get; set; }
		[Required(ErrorMessage = "Wpisz tytuł linku")]
		[MaxLength(20, ErrorMessage = "Tytuł linku powinien zawierać max. 20 znaków")]
		[Display(Name = "Tytuł odnośnika")]
		public string LinkTitle { get; set; }
		[Required(ErrorMessage = "Wpisz nazwę usługi")]
		[MaxLength(50, ErrorMessage = "Nazwa powinna zawierać max. 50 znaków")]
		[Display(Name = "Nazwa")]
		public string Name { get; set; }
		[Display(Name = "Opis")]
		[Column(TypeName = "nvarchar(MAX)")]
		public string Description { get; set; }
		[Display(Name = "Aktywny")]
		[Required(ErrorMessage = "Wpisz czy serwis jest aktywny")]
		public bool IsActive { get; set; }
		//photoULR ??
	}
}
