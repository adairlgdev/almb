using almb.Models;
using Microsoft.EntityFrameworkCore;

namespace almb
{
    public class AppDbContext(DbContextOptions<AppDbContext> options) : DbContext(options)
    {
        public DbSet<Image> Images { get; set; }
    }
}
