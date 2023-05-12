using Microsoft.EntityFrameworkCore;
using Przychodnia.Database.Data.CMS;
using Przychodnia.Database.Data.Visits;

namespace Przychodnia.Database.Data
{
    public class PrzychodniaContext : DbContext
    {
        public PrzychodniaContext(DbContextOptions<PrzychodniaContext> options)
            : base(options)
        {
        }

        public DbSet<Feed>? Feed { get; set; }
        public DbSet<Page>? Page { get; set; }
        public DbSet<Service>? Service { get; set; }
        public DbSet<Doctor>? Doctor { get; set; }
        public DbSet<Specialization>? Specialization { get; set; }

    }
}
