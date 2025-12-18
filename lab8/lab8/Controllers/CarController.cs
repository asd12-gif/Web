using lab8.Models;
using lab8.Services.Interfaces;
using Microsoft.AspNetCore.Mvc;

namespace lab8.Controllers
{
    public class CarController : Controller
    {
        private readonly ICarService _carService;
        public CarController(ICarService carService)
        {
            _carService = carService;
        }
        // GET: /Car
        public IActionResult Index()
        {
            return View(_carService.GetAllCars());
        }
        // GET: /Car/Details/5
        public IActionResult Details(int id)
        {
            var car = _carService.GetCarById(id);
            if (car == null) return NotFound();
            return View(car);
        }
        // GET: /Car/Create
        public IActionResult Create()
        {
            return View();
        }
        // POST: /Car/Create
        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Create(Car car)
        {
            if (!ModelState.IsValid)
                return View(car);
            _carService.CreateCar(car);
            return RedirectToAction(nameof(Index));
        }
        // GET: /Car/Edit/5
        public IActionResult Edit(int id)
        {
            var car = _carService.GetCarById(id);
            if (car == null) return NotFound();
            return View(car);
        }
        // POST: /Car/Edit
        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Edit(Car car)
        {
            if (!ModelState.IsValid)
                return View(car);
            _carService.UpdateCar(car);
            return RedirectToAction(nameof(Index));
        }
        // GET: /Car/Delete/5
        public IActionResult Delete(int id)
        {
            _carService.DeleteCar(id);
            return RedirectToAction(nameof(Index));
        }
    }

}
