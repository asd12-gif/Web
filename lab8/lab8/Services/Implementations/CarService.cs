using lab8.Models;
using lab8.Repository.Interface;
using lab8.Services.Interfaces;

namespace lab8.Services.Implementations
{
    public class CarService : ICarService
    {
        private readonly ICarRepository _repository;
        public CarService(ICarRepository repository)
        {
            _repository = repository;
        }
        public List<Car> GetAllCars()
        {
            return _repository.GetAll();
        }
        public Car? GetCarById(int id)
        {
            return _repository.GetById(id);
        }
        public void CreateCar(Car car)
        {
            _repository.Add(car);
        }
        public void UpdateCar(Car car)
        {
            _repository.Update(car);
        }
        public void DeleteCar(int id)
        {
            _repository.Delete(id);
        }
    }
}
