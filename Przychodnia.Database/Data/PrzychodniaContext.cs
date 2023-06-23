using Microsoft.EntityFrameworkCore;
using Przychodnia.Database.Data.CMS;
using Przychodnia.Database.Data.Visits;
using System.Reflection.PortableExecutable;

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
        public DbSet<Icon>? Icon { get; set; }
        public DbSet<Doctor>? Doctor { get; set; }
        public DbSet<Specialization>? Specialization { get; set; }
		public DbSet<VisitDateTime>? VisitDateTime { get; set; }
        public DbSet<DoctorDateTime>? DoctorDateTime { get; set; }
        public DbSet<BasketElement>? BasketElement { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
		{
            base.OnModelCreating(modelBuilder);
            // Konfiguracja dla DoctorDateTime
            modelBuilder.Entity<Doctor>()
				.HasMany(e => e.VisitDateTimes)
				.WithMany(e => e.Doctors)
				.UsingEntity<DoctorDateTime>();
		}
	}
}
