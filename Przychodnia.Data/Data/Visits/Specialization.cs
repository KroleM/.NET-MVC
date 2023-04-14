using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Linq;

namespace Przychodnia.Data.Data.Visits
{
    public class Specialization
    {
        [Key]
        public int Id { get; set; }
        [Required(ErrorMessage = "Podaj nazwę specjalizacji")]
        [MaxLength(30, ErrorMessage = "Nazwa specjalizacji może mieć max. 30 znaków")]
        [Display(Name = "Nazwa specjalizacji")]
        public string Name { get; set; }

        public List<Doctor> Doctors { get; set; }
    }
}
