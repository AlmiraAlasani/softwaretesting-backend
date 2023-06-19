using System.Collections.Generic;

namespace PRO.Data
{
    public class AppDbContext : IdentityDbContext
    {
        public AppDbContext(DbContextOptions<AppDbContext> options) : base(options) { }

       // public DbSet<Item> Items { get; set; }

    }
}
