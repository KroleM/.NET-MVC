using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Linq;

namespace Przychodnia.Data.Data.CMS
{
    public class Page
    {
        [Key]
        public int Id { get; set; }
        [Required(ErrorMessage = "Wpisz tytuł linku")]
        [MaxLength(20, ErrorMessage = "Tytuł linku powinien zawierać max. 20 znaków")]
        [Display(Name = "Tytuł odnośnika")]
        public string LinkTitle { get; set; }
        [Required(ErrorMessage = "Wpisz tytuł strony")]
        [MaxLength(30, ErrorMessage = "Tytuł powinien zawierać max 30 znaków")]
        [Display(Name = "Tytuł")]
        public string Title { get; set; }
        [Display(Name = "Treść")]
        [Column(TypeName = "nvarchar(MAX)")]
        public string Content { get; set; }
        [Display(Name = "Pozycja strony w menu")]
        [Required(ErrorMessage = "Wpisz pozycję strony w menu")]
        public int Priority { get; set; }
    }
}
