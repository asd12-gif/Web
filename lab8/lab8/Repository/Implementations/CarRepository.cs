using lab8.Data;
using lab8.Models;
using lab8.Repository.Interface;

namespace lab8.Repository.Implementations
{
    public class CarRepository : ICarRepository
    {
        private readonly ApplicationDbContext _context;
        public CarRepository(ApplicationDbContext context)
        {
            _context = context;
        }
        public List<Car> GetAll()
        {
            return _context.Cars.ToList();
        }
        public Car? GetById(int id)
        {
            return _context.Cars.Find(id);
        }
        public void Add(Car car)
        {
            _context.Cars.Add(car);
            _context.SaveChanges();
        }
        public void Update(Car car)
        {
            _context.Cars.Update(car);
            _context.SaveChanges();
        }
        public void Delete(int id)
        {
            var car = _context.Cars.Find(id);
            if (car != null)
            {
                _context.Cars.Remove(car); _context.SaveChanges();
            }
        }

    }
}
