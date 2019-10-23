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
        public DbSet<Cursus> Cursussen { get; set; }
        public DbSet<Traject> Trajecten { get; set; }

        public DbSet<Winkelwagen> Winkelwagens { get; set; }
        public DbSet<Klant> Klanten { get; set; }
    }
}
