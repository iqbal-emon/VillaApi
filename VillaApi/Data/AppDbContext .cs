using Microsoft.EntityFrameworkCore;
using VillaApi.Model;

namespace VillaApi.Data
{
    public class AppDbContext: DbContext
    {
        public DbSet<Villa> Villas { get; set; }
        public DbSet<User> Users { get; set; }

        public AppDbContext(DbContextOptions<AppDbContext> options)
            : base(options)
        {
        }
    }
}
