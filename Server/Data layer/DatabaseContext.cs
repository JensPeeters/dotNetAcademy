using Data_layer.Model;
using Microsoft.EntityFrameworkCore;

namespace Data_layer
{
    public class DatabaseContext : DbContext
    {
        public DatabaseContext(DbContextOptions<DatabaseContext> options)
                            : base(options)
        {

        }
        public virtual DbSet<Cursus> Cursussen { get; set; }
        public virtual DbSet<Traject> Trajecten { get; set; }

        public virtual DbSet<Winkelwagen> Winkelwagens { get; set; }
        public virtual DbSet<Klant> Klanten { get; set; }
        public virtual DbSet<Admin> Admins { get; set; }
        public virtual DbSet<Bestelling> Bestellingen { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Cursus>()
                .HasIndex(b => b.Titel)
                .IsUnique();
            modelBuilder.Entity<Traject>()
                .HasIndex(b => b.Titel)
                .IsUnique();


        }
    }
}
