using lab8.Models;

namespace lab8.Services.Interfaces
{
    public interface ICustomerService
    {
        IEnumerable<Customer> GetAllCustomers();
        Customer? GetCustomerById(int id);
        void CreateCustomer(Customer customer);
        void UpdateCustomer(Customer customer);
        void DeleteCustomer(int id);
    }
}