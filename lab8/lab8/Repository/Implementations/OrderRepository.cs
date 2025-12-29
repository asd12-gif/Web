using lab8.Data;
using lab8.Models;
using lab8.Repository.Interface;
using Microsoft.EntityFrameworkCore;

namespace lab8.Repository.Implementations
{
    public class OrderRepository : IOrderRepository
    {
        private readonly ApplicationDbContext _context;

        public OrderRepository(ApplicationDbContext context) => _context = context;

        public IEnumerable<Order> GetAll()
            => _context.Orders.Include(o => o.Customer).Include(o => o.Car).ToList();

        public Order? GetById(int id)
            => _context.Orders.Include(o => o.Customer).Include(o => o.Car).FirstOrDefault(o => o.Id == id);

        public void Add(Order order)
        {
            _context.Orders.Add(order);
            _context.SaveChanges();
        }

        public void Update(Order order)
        {
            _context.Orders.Update(order);
            _context.SaveChanges();
        }

        public void Delete(int id)
        {
            var order = GetById(id);
            if (order != null)
            {
                _context.Orders.Remove(order);
                _context.SaveChanges();
            }
        }
    }
}