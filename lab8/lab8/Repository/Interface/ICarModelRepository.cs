using lab8.Models;

namespace lab8.Repository.Interface
{
    public interface ICarModelRepository
    {
        List<CarModelVm> GetAll();
        CarModelVm? GetDetails(int id);
        CarModel? GetById(int id);
        void Add(CarModel carModel);
        void Update(CarModel carModel);
        void Delete(int id);
    }

}
