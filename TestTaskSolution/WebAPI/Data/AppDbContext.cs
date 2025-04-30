using Microsoft.EntityFrameworkCore;
using Core.Models;

namespace WebAPI.Data
{
    public class AppDbContext : DbContext
    {
     /// <summary>
     /// Контекст базы данных для работы с изображениями (Entity Framework Core).
     /// </summary>
  
        // Таблица в базе данных 
        public DbSet<Image> Images { get; set; }
        public AppDbContext(DbContextOptions<AppDbContext> options)
       : base(options) { }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            // Настройка для DateTime (чтобы PostgreSQL принимал только UTC)
            modelBuilder.Entity<Image>()
                .Property(i => i.UploadDate)
                .HasConversion(
                    v => v.ToUniversalTime(),        
                    v => DateTime.SpecifyKind(v, DateTimeKind.Utc) 
                );
        }
    }
}
