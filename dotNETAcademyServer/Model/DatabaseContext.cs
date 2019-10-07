using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace dotNETAcademyServer.Model
{
    public class DatabaseContext : DbContext
    {
        public DatabaseContext(DbContextOptions<DatabaseContext> options)
                            : base(options)
        {

        }
        public DbSet<Cursus> Cursussen { get; set; }
        public DbSet<Traject> Trajecten { get; set; }
    }
}
