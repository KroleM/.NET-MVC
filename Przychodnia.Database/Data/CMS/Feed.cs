using Microsoft.EntityFrameworkCore.Metadata.Internal;
using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;
using System.Xml.Linq;
using Przychodnia.Database.Data.Visits;

namespace Przychodnia.Database.Data.CMS
{
    public class Feed : TEntity
	{
        //[Key]
        //public int Id { get; set; }
        [Required(ErrorMessage = "Wpisz tytuł aktualności")]
        [MaxLength(30, ErrorMessage = "Tytuł aktualności powinien zawierać max. 30 znaków")]
        [Display(Name = "Tytuł")]
        public string Title { get; set; }
		[Required(ErrorMessage = "Wpisz treść aktualności")]
        [Column(TypeName = "nvarchar(MAX)")]
		[MaxLength(160, ErrorMessage = "Treść aktualności może zawierać max. 160 znaków")]
		[Display(Name = "Treść")]
		public string Content { get; set; }
        [Required(ErrorMessage = "Wpisz pozycję wyświetlania")]
		[Range(1, 100, ErrorMessage = "Liczba z zakresu 1 - 100")]
		[Display(Name = "Pozycja wyświetlania")]
		public int Priority { get; set; }
		//[Required(ErrorMessage = "Ikona musi zostać wybrana")]
		[Display(Name = "Nazwa ikony")]
		public int IconId { get; set; }
		[Display(Name = "Nazwa ikony")]
		public Icon Icon { get; set; }

	}
}
