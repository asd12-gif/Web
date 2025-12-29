using lab8.Models;
using lab8.Repository.Interface;
using lab8.Services.Interfaces;

namespace lab8.Services.Implementations
{
    public class CustomerService : ICustomerService
    {
        private readonly ICustomerRepository _customerRepository;

        public CustomerService(ICustomerRepository customerRepository)
        {
            _customerRepository = customerRepository;
        }

        public IEnumerable<Customer> GetAllCustomers() => _customerRepository.GetAll();

        public Customer? GetCustomerById(int id) => _customerRepository.GetById(id);

        public void CreateCustomer(Customer customer) => _customerRepository.Add(customer);

        public void UpdateCustomer(Customer customer) => _customerRepository.Update(customer);

        public void DeleteCustomer(int id) => _customerRepository.Delete(id);
    }
}