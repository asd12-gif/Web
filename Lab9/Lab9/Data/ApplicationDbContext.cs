using Lab9.Models;
using Microsoft.EntityFrameworkCore;

namespace Lab9.Data
{
    public class ApplicationDbContext : DbContext
    {
        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options)
            : base(options)
        {
            // KHÔNG ĐỂ GÌ Ở ĐÂY CẢ
        }

        public DbSet<Category> Categories { get; set; }
        public DbSet<Product> Products { get; set; }

        // BẠN PHẢI THÊM ĐOẠN NÀY VÀO
        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);

            // Gọi hàm Seed ở đây mới đúng
            modelBuilder.Seed();
            modelBuilder.Entity<Product>()
        .HasOne(p => p.Category)
        .WithMany(c => c.Products)
        .HasForeignKey(p => p.CategoryId)
        .OnDelete(DeleteBehavior.Restrict);
        }
    }
}