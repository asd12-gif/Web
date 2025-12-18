using lab8.Models;

namespace lab8.Services.Interfaces
{
    public interface ICarService
    {
        List<Car> GetAllCars();
        Car? GetCarById(int id);
        void CreateCar(Car car);
        void UpdateCar(Car car);
        void DeleteCar(int id);
    }
}
