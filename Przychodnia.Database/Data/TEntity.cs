using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Przychodnia.Database.Data
{
    public class TEntity
    {
        [Key]
        public int Id { get; set; }
        //public bool IsActive { get; set; }
        //      public DateTime DataDodania { get; set; }
        //      public int KtoDodal { get; set; } //id osoby dodającej
        //      public DateTime DataModyfikacji { get; set; }
        //public int KtoModyfikowal { get; set; }
        //public DateTime DataUsuniecia { get; set; }
        //public int KtoKasowal { get; set; }
    }
}
