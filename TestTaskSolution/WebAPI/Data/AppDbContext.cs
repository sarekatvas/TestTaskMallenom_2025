using Microsoft.EntityFrameworkCore;
using Core.Models;

namespace WebAPI.Data
{
    public class AppDbContext : DbContext
    {
        public DbSet<Image> Images { get; set; }
        public AppDbContext(DbContextOptions<AppDbContext> options)
       : base(options) { }
    }
}
