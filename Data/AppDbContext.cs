using Microsoft.EntityFrameworkCore;
using SmartInventory.API.Models
    ;
namespace SmartInventory.API.Data
{
    public class AppDbContext : DbContext
    {
        public AppDbContext(DbContextOptions<AppDbContext> options) : base(options)
        {
        }

        // Tables will be added here
        public DbSet<Product> Products { get; set; }
    }
}