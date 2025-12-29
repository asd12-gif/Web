using lab8.Models;
using lab8.Services.Interfaces;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Http;
using System.IO;

namespace lab8.Controllers
{
    public class CarModelController : Controller
    {
        private readonly ICarModelService _carModelService;
        private readonly IBrandService _brandService;

        public CarModelController(ICarModelService carModelService, IBrandService brandService)
        {
            _carModelService = carModelService;
            _brandService = brandService;
        }

        // 1. DANH SÁCH DÒNG XE
        public IActionResult Index()
        {
            var data = _carModelService.GetCarModels();
            return View(data);
        }

        // 2. TẠO MỚI (GET)
        public IActionResult Create()
        {
            ViewBag.Brands = _brandService.GetAllBrands();
            return View();
        }

        // 3. TẠO MỚI (POST)
        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Create(CarModel carModel, IFormFile? ImageFile)
        {
            ModelState.Remove("Brand");
            ModelState.Remove("ImageUrl");

            if (ModelState.IsValid)
            {
                if (ImageFile != null && ImageFile.Length > 0)
                {
                    string fileName = Guid.NewGuid().ToString() + Path.GetExtension(ImageFile.FileName);
                    // Lưu vào đúng thư mục images/cars như trong ảnh cấu trúc folder của bạn
                    string uploadPath = Path.Combine(Directory.GetCurrentDirectory(), "wwwroot", "images", "cars");

                    if (!Directory.Exists(uploadPath)) Directory.CreateDirectory(uploadPath);

                    string filePath = Path.Combine(uploadPath, fileName);
                    using (var stream = new FileStream(filePath, FileMode.Create))
                    {
                        ImageFile.CopyTo(stream);
                    }
                    // SỬA LỖI TẠI ĐÂY: Đổi /carss/ thành /cars/
                    carModel.ImageUrl = "/images/cars/" + fileName;
                }
                else
                {
                    // Nếu không chọn ảnh, dùng ảnh mặc định có sẵn trong folder cars
                    carModel.ImageUrl = "/images/cars/car.jpg";
                }

                _carModelService.CreateCarModel(carModel);
                return RedirectToAction(nameof(Index));
            }

            ViewBag.Brands = _brandService.GetAllBrands();
            return View(carModel);
        }

        // 4. CHỈNH SỬA (GET)
        public IActionResult Edit(int id)
        {
            var carModel = _carModelService.GetCarModelById(id);
            if (carModel == null) return NotFound();

            ViewBag.Brands = _brandService.GetAllBrands();
            return View(carModel);
        }

        // 5. CHỈNH SỬA (POST)
        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Edit(CarModel carModel, IFormFile? ImageFile)
        {
            ModelState.Remove("Brand");
            ModelState.Remove("ImageUrl");

            if (ModelState.IsValid)
            {
                if (ImageFile != null && ImageFile.Length > 0)
                {
                    string fileName = Guid.NewGuid().ToString() + Path.GetExtension(ImageFile.FileName);
                    string uploadPath = Path.Combine(Directory.GetCurrentDirectory(), "wwwroot", "images", "cars");

                    if (!Directory.Exists(uploadPath)) Directory.CreateDirectory(uploadPath);

                    using (var stream = new FileStream(Path.Combine(uploadPath, fileName), FileMode.Create))
                    {
                        ImageFile.CopyTo(stream);
                    }
                    carModel.ImageUrl = "/images/cars/" + fileName;
                }
                // Nếu ImageFile == null, carModel.ImageUrl sẽ giữ giá trị từ input hidden gửi lên

                _carModelService.UpdateCarModel(carModel);
                return RedirectToAction(nameof(Index));
            }

            ViewBag.Brands = _brandService.GetAllBrands();
            return View(carModel);
        }

        // 6. CHI TIẾT (GET)
        public IActionResult Details(int id)
        {
            if (id <= 0) return NotFound();
            var carModel = _carModelService.GetCarModelDetails(id);
            if (carModel == null) return NotFound();

            return View(carModel);
        }

        // 7. XÓA (GET)
        public IActionResult Delete(int id)
        {
            var carModel = _carModelService.GetCarModelById(id);
            if (carModel == null) return NotFound();

            return View(carModel);
        }

        // 8. XÓA (POST)
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public IActionResult DeleteConfirmed(int id)
        {
            _carModelService.DeleteCarModel(id);
            return RedirectToAction(nameof(Index));
        }
    }
}