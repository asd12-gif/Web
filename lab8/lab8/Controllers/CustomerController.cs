using lab8.Models;
using lab8.Services.Interfaces;
using Microsoft.AspNetCore.Mvc;

namespace lab8.Controllers
{
    public class CustomerController : Controller
    {
        private readonly ICustomerService _customerService;

        public CustomerController(ICustomerService customerService)
        {
            _customerService = customerService;
        }

        public IActionResult Index() => View(_customerService.GetAllCustomers());

        public IActionResult Details(int id)
        {
            var customer = _customerService.GetCustomerById(id);
            return customer == null ? NotFound() : View(customer);
        }

        public IActionResult Create() => View();

        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Create(Customer customer)
        {
            if (ModelState.IsValid)
            {
                _customerService.CreateCustomer(customer);
                return RedirectToAction(nameof(Index));
            }
            return View(customer);
        }

        public IActionResult Edit(int id)
        {
            var customer = _customerService.GetCustomerById(id);
            if (customer == null) return NotFound();
            return View(customer);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Edit(Customer customer)
        {
            if (ModelState.IsValid)
            {
                _customerService.UpdateCustomer(customer);
                return RedirectToAction(nameof(Index));
            }
            return View(customer);
        }

        public IActionResult Delete(int id)
        {
            var customer = _customerService.GetCustomerById(id);
            if (customer == null) return NotFound();
            return View(customer);
        }

        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public IActionResult DeleteConfirmed(int id)
        {
            _customerService.DeleteCustomer(id);
            return RedirectToAction(nameof(Index));
        }
    }
}