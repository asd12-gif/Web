using lab8.Models;

namespace lab8.Services.Interfaces
{
    public interface ICarModelService
    {
        List<CarModelVm> GetCarModels();
        CarModelVm GetCarModelDetails(int id);
        CarModel? GetCarModelById(int id);
        void CreateCarModel(CarModel carModel);
        void UpdateCarModel(CarModel carModel);
        void DeleteCarModel(int id);
    }
}
