using DMCProject1.Models;
using System.Data.Entity;
using System.Data.Entity.ModelConfiguration.Conventions;

namespace DMCProject1.DAL
{
    public class DmcContext : DbContext
    {

        public DmcContext() : base("DmcContext")
        {
        }

        public DbSet<UserColor> UserColors { get; set; }
        public DbSet<Pattern> Patterns { get; set; }
        public DbSet<DmcColor> DmcColors { get; set; }

        protected override void OnModelCreating(DbModelBuilder modelBuilder)
        {
            modelBuilder.Conventions.Remove<PluralizingTableNameConvention>();
        }

        public System.Data.Entity.DbSet<DMCProject1.Models.PatternColor> PatternColors { get; set; }
    }
}
