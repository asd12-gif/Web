using lab8.Models;
using lab8.Services.Interfaces;
using Microsoft.AspNetCore.Mvc;

namespace lab8.Controllers
{
    public class CarModelController : Controller
    {
        private readonly ICarModelService _carModelService;
        private readonly IBrandService _brandService;
        public CarModelController(
        ICarModelService carModelService,
        IBrandService brandService)
        {
            _carModelService = carModelService;
            _brandService = brandService;
        }
        // GET: /CarModel
        public IActionResult Index()
        {
            var data = _carModelService.GetCarModels();
            return View(data);
        }
        // GET: /CarModel/Create
        public IActionResult Create()
        {
            ViewBag.Brands = _brandService.GetAllBrands();
            return View();
        }
        // POST: /CarModel/Create
        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Create(CarModel carModel)
        {
            if (!ModelState.IsValid)
            {
                ViewBag.Brands = _brandService.GetAllBrands();
                return View(carModel);
            }
            _carModelService.CreateCarModel(carModel);
            return RedirectToAction(nameof(Index));
        }
        // GET: /CarModel/Edit/5
        public IActionResult Edit(int id)
        {
            var carModel = _carModelService.GetCarModelById(id);
            if (carModel == null) return NotFound();
            ViewBag.Brands = _brandService.GetAllBrands();
            return View(carModel);
        }
        // POST: /CarModel/Edit
        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Edit(CarModel carModel)
        {
            if (!ModelState.IsValid)
            {
                ViewBag.Brands = _brandService.GetAllBrands();
                return View(carModel);
            }
            _carModelService.UpdateCarModel(carModel);
            return RedirectToAction(nameof(Index));
        }
        // GET: CarModel/Details/5
        public IActionResult Details(int id)
        {
            if (id <= 0) return NotFound();

            var carModel = _carModelService.GetCarModelDetails(id);

            if (carModel == null) return NotFound();

            return View(carModel);
        }
        // GET: CarModel/Delete/5
        public IActionResult Delete(int id)
        {
            var carModel = _carModelService.GetCarModelById(id);
            if (carModel == null) return NotFound();

            return View(carModel); 
        }

        // POST: /CarModel/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public IActionResult DeleteConfirmed(int id)
        {
            _carModelService.DeleteCarModel(id);
            return RedirectToAction(nameof(Index));
        }
    }
}
