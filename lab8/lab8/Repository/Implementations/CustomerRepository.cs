using lab8.Data;
using lab8.Models;
using lab8.Repository.Interface;

namespace lab8.Repository.Implementations
{
    public class CustomerRepository : ICustomerRepository
    {
        private readonly ApplicationDbContext _context;

        public CustomerRepository(ApplicationDbContext context) => _context = context;

        public IEnumerable<Customer> GetAll() => _context.Customers.ToList();

        public Customer? GetById(int id) => _context.Customers.Find(id);

        public void Add(Customer customer)
        {
            _context.Customers.Add(customer);
            _context.SaveChanges();
        }

        public void Update(Customer customer)
        {
            _context.Customers.Update(customer);
            _context.SaveChanges();
        }

        public void Delete(int id)
        {
            var customer = GetById(id);
            if (customer != null)
            {
                _context.Customers.Remove(customer);
                _context.SaveChanges();
            }
        }
    }
}