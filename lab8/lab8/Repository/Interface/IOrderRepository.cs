using lab8.Models;

namespace lab8.Repository.Interface
{
    public interface IOrderRepository
    {
        IEnumerable<Order> GetAll();
        Order? GetById(int id);
        void Add(Order order);
        void Update(Order order);
        void Delete(int id);
    }
}