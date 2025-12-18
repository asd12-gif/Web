using lab8.Models;
using Microsoft.EntityFrameworkCore;

namespace lab8.Data
{
        public class ApplicationDbContext : DbContext
        {
            public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options)
            : base(options) { }
            public DbSet<Brand> Brands { get; set; }
            public DbSet<Car> Cars { get; set; }
            public DbSet<CarModel> CarModel { get; set; }

            protected override void OnModelCreating(ModelBuilder modelBuilder)
            {
                base.OnModelCreating(modelBuilder);
                modelBuilder.Entity<CarModel>()
                    .HasOne(cm => cm.Brand)
                    .WithMany(b => b.CarModels)
                    .HasForeignKey(cm => cm.BrandId)
                    .OnDelete(DeleteBehavior.NoAction);
            }
        }
  }
