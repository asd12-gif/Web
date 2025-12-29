using lab8.Models;
using Microsoft.EntityFrameworkCore;

namespace lab8.Data
{
    public class ApplicationDbContext : DbContext
    {
        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options)
            : base(options) { }

        public DbSet<Brand> Brands { get; set; }
        public DbSet<CarModel> CarModels { get; set; }
        public DbSet<Car> Cars { get; set; }
        public DbSet<Customer> Customers { get; set; }
        public DbSet<Order> Orders { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);

            // 1. Cấu hình quan hệ Brand - CarModel
            modelBuilder.Entity<CarModel>()
                .HasOne(cm => cm.Brand)
                .WithMany(b => b.CarModels)
                .HasForeignKey(cm => cm.BrandId)
                .OnDelete(DeleteBehavior.Restrict);

            // 2. Cấu hình quan hệ Order - Customer
            modelBuilder.Entity<Order>()
                .HasOne(o => o.Customer)
                .WithMany(c => c.Orders)
                .HasForeignKey(o => o.CustomerId)
                .OnDelete(DeleteBehavior.Cascade); 

            // 3. Cấu hình quan hệ Order - Car
            modelBuilder.Entity<Order>()
                .HasOne(o => o.Car)
                .WithMany()
                .HasForeignKey(o => o.CarId)
                .OnDelete(DeleteBehavior.NoAction);

            // 4. Cấu hình kiểu dữ liệu cho TotalPrice (Sửa lỗi decimal truncation)
            modelBuilder.Entity<Order>()
                .Property(o => o.TotalPrice)
                .HasColumnType("decimal(18,2)");

        }
    }
}