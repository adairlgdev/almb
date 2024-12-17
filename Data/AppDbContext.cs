using almb.Models;
using Microsoft.EntityFrameworkCore;

namespace almb.Data
{
    public class AppDbContext(DbContextOptions<AppDbContext> options) : DbContext(options)
    {
        public DbSet<Image> Images { get; set; }
        public DbSet<User> Users { get; set; }
    }
}
