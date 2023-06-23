using Microsoft.EntityFrameworkCore.Metadata.Internal;
using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;
using System.Net;

namespace Przychodnia.Database.Data.Visits
{
	public class DoctorDateTime : TEntity
	{
        public bool IsBooked { get; set; }
        public int DoctorId { get; set; }
		[Display(Name = "Lekarz")]
		[ForeignKey(nameof(DoctorId))]
		public Doctor? Doctor { get; set; }
        public int VisitDateTimeId { get; set; }
		[ForeignKey(nameof(VisitDateTimeId))]
		public VisitDateTime? VisitDateTime { get; set; }
    }
}
