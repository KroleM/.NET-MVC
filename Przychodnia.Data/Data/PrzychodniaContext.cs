using Microsoft.EntityFrameworkCore;
using Przychodnia.Data.Data.CMS;
using Przychodnia.Data.Data.Visits;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Przychodnia.Data.Data
{
    public class PrzychodniaContext : DbContext
    {
        public PrzychodniaContext(DbContextOptions<PrzychodniaContext> options)
            : base(options)
        { }
        public DbSet<Feed> Feed { get; set; }
        public DbSet<Page> Page { get; set; }
        public DbSet<Specialization> Specialization { get; set; }
        public DbSet<Doctor> Doctor { get; set; }
    }
}
