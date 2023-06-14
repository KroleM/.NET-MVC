using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Przychodnia.Database.Data;
using Przychodnia.Database.Data.CMS;
using Przychodnia.Database.Data.Visits;

namespace Przychodnia.Intranet.Controllers
{
    public class SpecializationController : BaseController<Specialization>
    {
        public SpecializationController(PrzychodniaContext context)
            : base(context)
        {
        }

		public override async Task<List<Specialization>> GetEntityList()
		{
			return await Context.Specialization.ToListAsync();
		}
		public override async Task<Specialization> GetEntityItem(int? id)
		{
			return await Context.Specialization
				.FirstOrDefaultAsync(m => m.Id == id);
		}
	}
}
