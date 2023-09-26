using Microsoft.EntityFrameworkCore;
using StockServer.Entities;

namespace StockServer.Contexts
{
    public class StockDbContext : DbContext
    {
        public DbSet<Product> Products { get; set; }
        public DbSet<Category> Categories { get; set; }
        public DbSet<Tag> Tags { get; set; }
        public DbSet<Login> Login { get; set; }

        public StockDbContext(DbContextOptions<StockDbContext> options):base(options) { }
    }
}
