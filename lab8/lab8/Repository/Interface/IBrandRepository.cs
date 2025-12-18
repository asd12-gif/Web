using lab8.Models;

namespace lab8.Repository.Interface
{
    public interface IBrandRepository
    {
        List<Brand> GetAll();
        Brand? GetById(int id);
        void Add(Brand brand);
        void Update(Brand brand);
        void Delete(int id);
    }

}
