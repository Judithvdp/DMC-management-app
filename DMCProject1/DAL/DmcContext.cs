using DMCProject1.Models;
using System.Data.Entity;
using System.Data.Entity.ModelConfiguration.Conventions;

namespace DMC_MVC.DAL
{
    public class DmcContext : DbContext
    {

        public DmcContext() : base("SchoolContext")
        {
        }

        public DbSet<UserColor> UserColors { get; set; }
        public DbSet<Pattern> Patterns { get; set; }
        public DbSet<DmcColor> DmcColors { get; set; }

        protected override void OnModelCreating(DbModelBuilder modelBuilder)
        {
            modelBuilder.Conventions.Remove<PluralizingTableNameConvention>();
        }
    }
}
