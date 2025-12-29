using lab8.Models;
using lab8.Services.Interfaces;
using Microsoft.AspNetCore.Mvc;

namespace lab8.Controllers
{
    public class OrderController : Controller
    {
        private readonly IOrderService _orderService;
        private readonly ICustomerService _customerService;
        private readonly ICarService _carService;

        public OrderController(IOrderService orderService, ICustomerService customerService, ICarService carService)
        {
            _orderService = orderService;
            _customerService = customerService;
            _carService = carService;
        }

        public IActionResult Index() => View(_orderService.GetAllOrders());

        private void PopulateDropdowns()
        {
            ViewBag.Customers = _customerService.GetAllCustomers();
            ViewBag.Cars = _carService.GetAllCars();
        }

        public IActionResult Create()
        {
            PopulateDropdowns();
            return View(new Order { OrderDate = DateTime.Now });
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Create(Order order)
        {
            if (ModelState.IsValid)
            {
                _orderService.CreateOrder(order);
                return RedirectToAction(nameof(Index));
            }
            PopulateDropdowns();
            return View(order);
        }

        public IActionResult Details(int id)
        {
            var order = _orderService.GetOrderById(id);
            return order == null ? NotFound() : View(order);
        }

        public IActionResult Delete(int id)
        {
            var order = _orderService.GetOrderById(id);
            if (order == null) return NotFound();
            return View(order);
        }

        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public IActionResult DeleteConfirmed(int id)
        {
            _orderService.DeleteOrder(id);
            return RedirectToAction(nameof(Index));
        }
    }
}