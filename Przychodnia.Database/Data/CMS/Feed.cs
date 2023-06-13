using Microsoft.EntityFrameworkCore.Metadata.Internal;
using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;
using System.Xml.Linq;
using Przychodnia.Database.Data.Visits;

namespace Przychodnia.Database.Data.CMS
{
    public class Feed
    {
        [Key]
        public int Id { get; set; }
        [Required(ErrorMessage = "Wpisz tytuł aktualności")]
        [MaxLength(30, ErrorMessage = "Tytuł aktualności powinien zawierać max 30 znaków")]
        [Display(Name = "Tytuł")]
        public string Title { get; set; }
        [Display(Name = "Treść")]
        [Column(TypeName = "nvarchar(MAX)")]
        public string Content { get; set; }
        [Display(Name = "Pozycja wyświetlania")]
        [Required(ErrorMessage = "Wpisz pozycję wyświetlania")]
        public int Priority { get; set; }
		//[Required(ErrorMessage = "Ikona musi zostać wybrana")]
		[Display(Name = "Nazwa ikony")]
		public int IconId { get; set; }
		public Icon Icon { get; set; }

	}
}
