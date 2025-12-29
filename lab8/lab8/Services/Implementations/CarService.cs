using lab8.Data;
using lab8.Models;
using lab8.Services.Interfaces;
using Microsoft.EntityFrameworkCore;

namespace lab8.Services
{
    public class CarService : ICarService
    {
        private readonly ApplicationDbContext _context;

        public CarService(ApplicationDbContext context)
        {
            _context = context;
        }

        // Lấy danh sách xe kèm theo thông tin Dòng xe (CarModel)
        public IEnumerable<Car> GetAllCars()
        {
            return _context.Cars
                           .Include(c => c.CarModel) // Quan trọng để hiện tên dòng xe ở Index
                           .ToList();
        }

        // Lấy 1 xe cụ thể kèm thông tin dòng xe
        public Car GetCarById(int id)
        {
            return _context.Cars
                           .Include(c => c.CarModel)
                           .FirstOrDefault(m => m.Id == id);
        }

        public void CreateCar(Car car)
        {
            _context.Cars.Add(car);
            _context.SaveChanges();
        }

        public void UpdateCar(Car car)
        {
            _context.Cars.Update(car);
            _context.SaveChanges();
        }

        public void DeleteCar(int id)
        {
            var car = _context.Cars.Find(id);
            if (car != null)
            {
                _context.Cars.Remove(car);
                _context.SaveChanges();
            }
        }
    }
}