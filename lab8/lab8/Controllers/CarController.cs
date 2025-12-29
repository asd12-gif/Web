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

        private void PopulateCarModels(object? selectedModel = null)
        {
            var models = _carModelService.GetCarModels();
            ViewBag.CarModelId = new SelectList(models, "Id", "CarModelName", selectedModel);
        }

        public IActionResult Index() => View(_carService.GetAllCars());

        public IActionResult Create()
        {
            PopulateCarModels();
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Create(Car car, IFormFile ImageFile)
        {
            ModelState.Remove("CarModel");
            ModelState.Remove("ImageUrl"); // Bỏ qua validate vì mình sẽ gán code tay

            if (ModelState.IsValid)
            {
                if (ImageFile != null && ImageFile.Length > 0)
                {
                    // 1. Định nghĩa đường dẫn lưu file
                    string fileName = Guid.NewGuid().ToString() + Path.GetExtension(ImageFile.FileName);
                    string uploadPath = Path.Combine(Directory.GetCurrentDirectory(), "wwwroot", "images", "cars");

                    // 2. Tạo thư mục nếu chưa có
                    if (!Directory.Exists(uploadPath)) Directory.CreateDirectory(uploadPath);

                    // 3. Lưu file vào thư mục
                    string filePath = Path.Combine(uploadPath, fileName);
                    using (var stream = new FileStream(filePath, FileMode.Create))
                    {
                        ImageFile.CopyTo(stream);
                    }

                    // 4. Lưu đường dẫn vào database
                    car.ImageUrl = "/images/cars/" + fileName;
                }

                _carService.CreateCar(car);
                return RedirectToAction(nameof(Index));
            }
            PopulateCarModels(car.CarModelId);
            return View(car);
        }

        public IActionResult Edit(int id)
        {
            var car = _carService.GetCarById(id);
            if (car == null) return NotFound();
            PopulateCarModels(car.CarModelId);
            return View(car);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Edit(Car car, IFormFile? ImageFile)
        {
            ModelState.Remove("CarModel");
            ModelState.Remove("ImageUrl");

            if (ModelState.IsValid)
            {
                if (ImageFile != null && ImageFile.Length > 0)
                {
                    // 1. Lưu ảnh mới vào thư mục
                    string fileName = Guid.NewGuid().ToString() + Path.GetExtension(ImageFile.FileName);
                    string uploadPath = Path.Combine(Directory.GetCurrentDirectory(), "wwwroot", "images", "cars");

                    if (!Directory.Exists(uploadPath)) Directory.CreateDirectory(uploadPath);

                    string filePath = Path.Combine(uploadPath, fileName);
                    using (var stream = new FileStream(filePath, FileMode.Create))
                    {
                        ImageFile.CopyTo(stream);
                    }

                    // 2. Gán đường dẫn ảnh mới
                    car.ImageUrl = "/images/cars/" + fileName;
                }
                // Lưu ý: Nếu ImageFile == null, car.ImageUrl vẫn giữ giá trị từ input hidden

                _carService.UpdateCar(car);
                return RedirectToAction(nameof(Index));
            }
            PopulateCarModels(car.CarModelId);
            return View(car);
        }
        public IActionResult Details(int id)
        {
            var car = _carService.GetCarById(id);
            return car == null ? NotFound() : View(car);
        }

        // GET: Delete
        public IActionResult Delete(int id)
        {
            var car = _carService.GetCarById(id);
            if (car == null) return NotFound();
            return View(car);
        }

        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public IActionResult DeleteConfirmed(int id)
        {
            _carService.DeleteCar(id);
            return RedirectToAction(nameof(Index));
        }
    }
}