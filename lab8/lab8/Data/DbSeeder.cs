using lab8.Models;
using Microsoft.EntityFrameworkCore;

namespace lab8.Data
{
    public static class DbSeeder
    {
        public static void Seed(ApplicationDbContext context)
        {
            context.Database.EnsureCreated();

            if (context.Brands.Any()) return;

            // 1. Seed Brands (Giữ nguyên hoặc cập nhật logo riêng)
            var brands = new List<Brand>
            {
                new Brand { Name = "Toyota", Country = "Japan", ImageUrl = "/images/brands/logo.jpg", Description = "Hãng xe bền bỉ và phổ biến nhất thế giới" },
                new Brand { Name = "Honda", Country = "Japan", ImageUrl = "/images/brands/logo.jpg", Description = "Đỉnh cao của sự bền bỉ và sức mạnh động cơ" },
                new Brand { Name = "Hyundai", Country = "Korea", ImageUrl = "/images/brands/logo.jpg", Description = "Thiết kế hiện đại, trang bị nhiều option" },
                new Brand { Name = "Ford", Country = "USA", ImageUrl = "/images/brands/logo.jpg", Description = "Mạnh mẽ, an toàn và đậm chất cơ bắp Mỹ" },
                new Brand { Name = "BMW", Country = "Germany", ImageUrl = "/images/brands/logo.jpg", Description = "Đẳng cấp xe sang và cảm giác lái phấn khích" }
            };
            context.Brands.AddRange(brands);
            context.SaveChanges();

            // 2. Seed CarModels (Cập nhật ảnh riêng biệt cho từng dòng xe)
            var carModels = new List<CarModel>
            {
                new CarModel { Name = "Vios", BrandId = brands[0].Id, ImageUrl = "/images/cars/car.jpg", Description = "Sedan hạng B kinh điển" },
                new CarModel { Name = "Camry", BrandId = brands[0].Id, ImageUrl = "/images/cars/car.jpg", Description = "Sedan hạng D doanh nhân" },
                new CarModel { Name = "Civic", BrandId = brands[1].Id, ImageUrl = "/images/cars/car.jpg", Description = "Sedan thể thao mạnh mẽ" },
                new CarModel { Name = "CR-V", BrandId = brands[1].Id, ImageUrl = "/images/cars/car.jpg", Description = "SUV gia đình tiện nghi" },
                new CarModel { Name = "Accent", BrandId = brands[2].Id, ImageUrl = "/images/cars/car.jpg", Description = "Xe gia đình nhỏ gọn, hiện đại" },
                new CarModel { Name = "SantaFe", BrandId = brands[2].Id, ImageUrl = "/images/cars/car.jpg", Description = "SUV 7 chỗ đẳng cấp" },
                new CarModel { Name = "Ranger", BrandId = brands[3].Id, ImageUrl = "/images/cars/car.jpg", Description = "Vua bán tải tại Việt Nam" },
                new CarModel { Name = "Everest", BrandId = brands[3].Id, ImageUrl = "/images/cars/car.jpg", Description = "SUV off-road mạnh mẽ" },
                new CarModel { Name = "320i Sport", BrandId = brands[4].Id, ImageUrl = "/images/cars/car.jpg", Description = "Sedan hạng sang thể thao" },
                new CarModel { Name = "X5 xDrive", BrandId = brands[4].Id, ImageUrl = "/images/cars/car.jpg", Description = "SUV hạng sang đa dụng" }
            };
            context.CarModels.AddRange(carModels);
            context.SaveChanges();

            // 3. Seed Cars (Lấy ảnh từ CarModel truyền xuống cho đồng bộ)
            var cars = new List<Car>();
            for (int i = 0; i < carModels.Count; i++)
            {
                cars.Add(new Car
                {
                    Name = $"{carModels[i].Name} 2025 Standard",
                    CarModelId = carModels[i].Id,
                    ImageUrl = carModels[i].ImageUrl, // Kế thừa ảnh từ CarModel
                    Description = $"Phiên bản nâng cấp công nghệ mới nhất của {carModels[i].Name}"
                });
            }
            context.Cars.AddRange(cars);
            context.SaveChanges();

            // 4 & 5: Seed Customers và Orders giữ nguyên như code cũ của bạn...
            // (Đoạn này giữ nguyên để đảm bảo logic đơn hàng không lỗi)
            var customers = new List<Customer>
            {
                new Customer { FullName = "Nguyễn Văn A", Email = "vana@gmail.com", Phone = "0901234567", Address = "Hà Nội", ImageUrl = "/images/customers/customer.jpg" },
                new Customer { FullName = "Trần Thị B", Email = "thib@gmail.com", Phone = "0902234567", Address = "TP.HCM", ImageUrl = "/images/customers/customer.jpg" }
            };
            context.Customers.AddRange(customers);
            context.SaveChanges();

            var random = new Random();
            for (int i = 0; i < 2; i++)
            {
                context.Orders.Add(new Order
                {
                    OrderDate = DateTime.Now.AddDays(-random.Next(1, 10)),
                    TotalPrice = random.Next(600, 2000) * 1000000,
                    CustomerId = customers[i].Id,
                    CarId = cars[i].Id,
                    Note = "Đặt cọc qua Seeder"
                });
            }
            context.SaveChanges();
        }
    }
}