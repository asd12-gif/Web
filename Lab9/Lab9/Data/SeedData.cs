using Lab9.Models;
using Microsoft.EntityFrameworkCore;

namespace Lab9.Data
{
    public static class SeedData
    {
        public static void Seed(this ModelBuilder modelBuilder)
        {
            // 1. Seed Categories (Giữ nguyên ID để không lỗi ràng buộc)
            modelBuilder.Entity<Category>().HasData(
                new Category { Id = 1, Name = "Điện thoại" },
                new Category { Id = 2, Name = "Laptop" },
                new Category { Id = 3, Name = "Phụ kiện" }
            );

            // 2. Seed 10 Products với link ảnh từ Unsplash (Đảm bảo hiển thị 100%)
            modelBuilder.Entity<Product>().HasData(
                new Product
                {
                    Id = 1,
                    Name = "iPhone 15 Pro",
                    Price = 25000000,
                    CategoryId = 1,
                    ImageUrl = "https://cdnv2.tgdd.vn/mwg-static/tgdd/Products/Images/42/281570/iphone-15-blue-2-638629450171966290-750x500.jpg"
                },

                new Product
                {
                    Id = 2,
                    Name = "Samsung S24 Ultra",
                    Price = 30000000,
                    CategoryId = 1,
                    ImageUrl = "https://cdn.tgdd.vn/Products/Images/42/307174/samsung-galaxy-s24-ultra-xam-1-750x500.jpg"
                },

                new Product
                {
                    Id = 3,
                    Name = "MacBook Pro M3",
                    Price = 45000000,
                    CategoryId = 2,
                    ImageUrl = "https://cdnv2.tgdd.vn/mwg-static/tgdd/Products/Images/1363/334807/bo-dan-macbook-pro-14-inch-5-in-1-jcpal-den-2-638755775390613978-750x500.jpg"
                },

                new Product
                {
                    Id = 4,
                    Name = "Dell XPS 15",
                    Price = 35000000,
                    CategoryId = 2,
                    ImageUrl = "https://cdn.tgdd.vn/Products/Images/44/314837/dell-xps-15-9530-i7-71015716-1-750x500.jpg"
                },

                new Product
                {
                    Id = 5,
                    Name = "AirPods Pro",
                    Price = 5500000,
                    CategoryId = 3,
                    ImageUrl = "https://cdnv2.tgdd.vn/mwg-static/tgdd/Products/Images/54/344776/tai-nghe-bluetooth-apple-airpods-pro-3-trang-mfhp4-1-638996042534094939-750x500.jpg"
                },

                new Product
                {
                    Id = 6,
                    Name = "Mechanical Keyboard",
                    Price = 2500000,
                    CategoryId = 3,
                    ImageUrl = "https://cdn.tgdd.vn/Products/Images/4547/327810/ban-phim-co-co-day-gaming-hp-hyperx-alloy-origins-core-4p5p3aa-1-750x500.jpg"
                },

                new Product
                {
                    Id = 7,
                    Name = "iPad Air 5",
                    Price = 15000000,
                    CategoryId = 1,
                    ImageUrl = "https://cdn.tgdd.vn/Products/Images/522/248096/ipad-air-5-wifi-pink-thumb-600x600.jpg"
                },

                new Product
                {
                    Id = 8,
                    Name = "HP Spectre x360",
                    Price = 32000000,
                    CategoryId = 2,
                    ImageUrl = "https://cdnv2.tgdd.vn/mwg-static/tgdd/Products/Images/44/326268/hp-spectre-x360-14-eu0050tu-ultra-7-a19blpa-glr-1-638647607976964037-750x500.jpg"
                },

                new Product
                {
                    Id = 9,
                    Name = "Sony WH-1000XM5",
                    Price = 8000000,
                    CategoryId = 3,
                    ImageUrl = "https://cdnv2.tgdd.vn/mwg-static/common/News/1585965/tai-nghe-sony-gia-tot-1.jpg"
                },

                new Product
                {
                    Id = 10,
                    Name = "Logitech MX Master 3",
                    Price = 2200000,
                    CategoryId = 3,
                    ImageUrl = "https://cdn.tgdd.vn/Files/2020/07/08/1268608/thumb_800x450.jpg"
                }
            );
        }
    }
}