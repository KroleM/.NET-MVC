using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Przychodnia.Database.Data.Visits
{
	public class BasketElement
	{
		[Key]
		public int Id { get; set; }
		public string BasketSessionId { get; set; }
        public int DoctorDateTimeId { get; set; }
        public DoctorDateTime DoctorDateTime { get; set; }
		public DateTime CreationDate { get; set; }
	}
}
