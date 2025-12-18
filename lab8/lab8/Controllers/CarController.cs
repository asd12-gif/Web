using lab8.Models;
using lab8.Services.Interfaces;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;

namespace lab8.Controllers
{
    public class CarController : Controller
    {
        private readonly ICarService _carService;
        private readonly ICarModelService _carModelService;

        public CarController(ICarService carService, ICarModelService carModelService)
        {
            _carService = carService;
            _carModelService = carModelService;
        }

        private void PopulateCarModels()
        {
            var models = _carModelService.GetCarModels();
            ViewBag.CarModels = models ?? new List<CarModelVm>();
        }

        public IActionResult Index() => View(_carService.GetAllCars());

        public IActionResult Details(int id)
        {
            var car = _carService.GetCarById(id);
            return car == null ? NotFound() : View(car);
        }

        // GET: Create
        public IActionResult Create()
        {
            PopulateCarModels();
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Create(Car car)
        {
            if (ModelState.IsValid)
            {
                _carService.CreateCar(car);
                return RedirectToAction(nameof(Index));
            }
            PopulateCarModels(); 
            return View(car);
        }

        // GET: Edit
        public IActionResult Edit(int id)
        {
            var car = _carService.GetCarById(id);
            if (car == null) return NotFound();
            PopulateCarModels();
            return View(car);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Edit(Car car)
        {
            if (ModelState.IsValid)
            {
                _carService.UpdateCar(car);
                return RedirectToAction(nameof(Index));
            }
            PopulateCarModels();
            return View(car);
        }

        // GET: Delete (Xác nhận)
        public IActionResult Delete(int id)
        {
            var car = _carService.GetCarById(id);
            if (car == null) return NotFound();
            return View(car);
        }

        // POST: Delete (Thực thi)
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public IActionResult DeleteConfirmed(int id)
        {
            _carService.DeleteCar(id);
            return RedirectToAction(nameof(Index));
        }
    }
}