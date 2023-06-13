using Microsoft.EntityFrameworkCore.Metadata.Internal;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Linq;
using Przychodnia.Database.Data.Visits;

namespace Przychodnia.Database.Data.CMS
{
	public class Icon
	{
		[Key]
		public int Id { get; set; }
		[Required(ErrorMessage = "Wpisz nazwę ikony 'Material'")]
		[Display(Name = "Tytuł")]
		public string Name { get; set; }

        public List<Feed>? Feeds { get; set; }
    }
}
