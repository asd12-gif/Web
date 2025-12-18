using lab8.Data;
using lab8.Models;
using lab8.Repository.Interface;
using Microsoft.EntityFrameworkCore;

namespace lab8.Repository.Implementations
{
    public class CarModelRepository : ICarModelRepository
    {
        private readonly ApplicationDbContext _context;
        public CarModelRepository(ApplicationDbContext context)
        {
            _context = context;
        }
        public List<CarModelVm> GetAll()
        {
            return _context.CarModel
            .Include(cm => cm.Brand)
            .Select(cm => new CarModelVm
            {
                Id = cm.Id,
                CarModelName = cm.Name,
                BrandId = cm.BrandId,
                BrandName = cm.Brand.Name
            })
            .ToList();
        }
        public CarModel? GetById(int id)
        {
            return _context.CarModel.Find(id);
        }
        public void Add(CarModel carModel)
        {
            _context.CarModel.Add(carModel);
            _context.SaveChanges();
        }
        public void Update(CarModel carModel)
        {
            _context.CarModel.Update(carModel);
            _context.SaveChanges();
        }
        public CarModelVm GetDetails(int id)
        {
            return _context.CarModel
                .Include(cm => cm.Brand)
                .Select(cm => new CarModelVm
                {
                    Id = cm.Id,
                    CarModelName = cm.Name,
                    BrandId = cm.BrandId,
                    BrandName = cm.Brand.Name
                })
                .FirstOrDefault(cm => cm.Id == id);
        }
        public void Delete(int id)
        {
            var carModel = _context.CarModel.Find(id); if (carModel != null)
            {
                _context.CarModel.Remove(carModel);
                _context.SaveChanges();
            }
        }
    }
}

