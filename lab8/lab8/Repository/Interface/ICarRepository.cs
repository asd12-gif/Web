using lab8.Models;

namespace lab8.Repository.Interface
{
    public interface ICarRepository
    {
        List<Car> GetAll();
        Car? GetById(int id);
        void Add(Car car);
        void Update(Car car);
        void Delete(int id);
    }
}
