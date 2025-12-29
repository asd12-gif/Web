using lab8.Models;

namespace lab8.Repository.Interface
{
    public interface ICustomerRepository
    {
        IEnumerable<Customer> GetAll();
        Customer? GetById(int id);
        void Add(Customer customer);
        void Update(Customer customer);
        void Delete(int id);
    }
}