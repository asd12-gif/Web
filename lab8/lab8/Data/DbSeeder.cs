using lab8.Models;
using System.Linq;

namespace lab8.Data
{
    public static class DbSeeder
    {
        public static void Seed(ApplicationDbContext context)
        {           
            if (context.Brands.Any()) return;

            // 1. Seed 10 Brands
            var b1 = new Brand { Name = "Toyota", Country = "Japan" };
            var b2 = new Brand { Name = "Hyundai", Country = "Korea" };
            var b3 = new Brand { Name = "BMW", Country = "Germany" };
            var b4 = new Brand { Name = "Mercedes-Benz", Country = "Germany" };
            var b5 = new Brand { Name = "Honda", Country = "Japan" };
            var b6 = new Brand { Name = "Ford", Country = "USA" };
            var b7 = new Brand { Name = "Kia", Country = "Korea" };
            var b8 = new Brand { Name = "VinFast", Country = "Vietnam" };
            var b9 = new Brand { Name = "Audi", Country = "Germany" };
            var b10 = new Brand { Name = "Mazda", Country = "Japan" };

            context.Brands.AddRange(b1, b2, b3, b4, b5, b6, b7, b8, b9, b10);
            context.SaveChanges(); 

            // 2. Seed 10 CarModels 
            var m1 = new CarModel { Name = "Vios", BrandId = b1.Id };
            var m2 = new CarModel { Name = "Accent", BrandId = b2.Id };
            var m3 = new CarModel { Name = "3 Series", BrandId = b3.Id };
            var m4 = new CarModel { Name = "C-Class", BrandId = b4.Id };
            var m5 = new CarModel { Name = "Civic", BrandId = b5.Id };
            var m6 = new CarModel { Name = "Ranger", BrandId = b6.Id };
            var m7 = new CarModel { Name = "Seltos", BrandId = b7.Id };
            var m8 = new CarModel { Name = "VF8", BrandId = b8.Id };
            var m9 = new CarModel { Name = "A4", BrandId = b9.Id };
            var m10 = new CarModel { Name = "CX-5", BrandId = b10.Id };

            context.CarModel.AddRange(m1, m2, m3, m4, m5, m6, m7, m8, m9, m10);
            context.SaveChanges();

            // 3. Seed 10 Cars
            context.Cars.AddRange(
                new Car { Name = "Vios 1.5G 2024", CarModelId = m1.Id },
                new Car { Name = "Accent AT Đặc biệt", CarModelId = m2.Id },
                new Car { Name = "BMW 320i Sport Line", CarModelId = m3.Id },
                new Car { Name = "Mercedes C200 Avantgarde", CarModelId = m4.Id },
                new Car { Name = "Honda Civic RS", CarModelId = m5.Id },
                new Car { Name = "Ford Ranger Wildtrak", CarModelId = m6.Id },
                new Car { Name = "Kia Seltos Premium", CarModelId = m7.Id },
                new Car { Name = "VinFast VF8 Plus", CarModelId = m8.Id },
                new Car { Name = "Audi A4 40 TFSI", CarModelId = m9.Id },
                new Car { Name = "Mazda CX-5 Signature", CarModelId = m10.Id }
            );
            context.SaveChanges();
        }
    }
}