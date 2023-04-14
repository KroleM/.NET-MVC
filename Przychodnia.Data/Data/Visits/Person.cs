using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Przychodnia.Data.Data.Visits
{
    public class Person
    {
        [Key]
        public int Id { get; set; }
        [Required(ErrorMessage ="Imię i nazwisko są wymagane")]
        [Display(Name = "Imię i nazwisko")]
        public string Name { get; set; }
        [Required(ErrorMessage = "Adres jest wymagany")]
        [Display(Name = "Adres zamieszkania")]
        public string Address { get; set; }
        [Required(ErrorMessage = "Data urodzenia jest wymagana")]
        [Display(Name = "Data urodzenia")]
        [Column(TypeName ="date")]
        public DateTime BirthDate { get; set; }
        [Display(Name = "Wybierz zdjęcie")]
        public string PhotoURL { get; set; }
    }
}
