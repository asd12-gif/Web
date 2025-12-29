using lab8.Models;
using lab8.Repository.Interface;
using lab8.Services.Interfaces;

namespace lab8.Services.Implementations
{
    public class OrderService : IOrderService
    {
        private readonly IOrderRepository _orderRepository;

        public OrderService(IOrderRepository orderRepository)
        {
            _orderRepository = orderRepository;
        }

        public IEnumerable<Order> GetAllOrders() => _orderRepository.GetAll();

        public Order? GetOrderById(int id) => _orderRepository.GetById(id);

        public void CreateOrder(Order order)
        {
            _orderRepository.Add(order);
        }

        public void UpdateOrder(Order order) => _orderRepository.Update(order);

        public void DeleteOrder(int id) => _orderRepository.Delete(id);
    }
}