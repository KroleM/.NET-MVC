using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using Przychodnia.Intranet.Models.CMS;
using Przychodnia.Intranet.Models.Visits;

namespace Przychodnia.Intranet.Data
{
    public class PrzychodniaIntranetContext : DbContext
    {
        public PrzychodniaIntranetContext (DbContextOptions<PrzychodniaIntranetContext> options)
            : base(options)
        {
        }

        public DbSet<Przychodnia.Intranet.Models.CMS.Feed> Feed { get; set; } = default!;

        public DbSet<Przychodnia.Intranet.Models.CMS.Page>? Page { get; set; }
        public DbSet<Przychodnia.Intranet.Models.CMS.Service>? Service { get; set; }
        public DbSet<Przychodnia.Intranet.Models.Visits.Doctor>? Doctor { get; set; }
        public DbSet<Przychodnia.Intranet.Models.Visits.Specialization>? Specialization { get; set; }

    }
}
