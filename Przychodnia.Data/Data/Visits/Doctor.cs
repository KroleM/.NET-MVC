using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Linq;

namespace Przychodnia.Data.Data.Visits
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
