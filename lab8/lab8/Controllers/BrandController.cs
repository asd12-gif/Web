using lab8.Models;
using lab8.Services.Interfaces;
using Microsoft.AspNetCore.Mvc;

namespace lab8.Controllers
{
    public class BrandController : Controller
    {
        private readonly IBrandService _brandService;

        public BrandController(IBrandService brandService)
        {
            _brandService = brandService;
        }

        // GET: /Brand
        public IActionResult Index()
        {
            var brands = _brandService.GetAllBrands();
            return View(brands);
        }

        // GET: /Brand/Create
        public IActionResult Create() => View();

        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Create(Brand brand)
        {
            if (!ModelState.IsValid) return View(brand);
            _brandService.CreateBrand(brand);
            return RedirectToAction(nameof(Index));
        }

        // GET: /Brand/Edit/5
        public IActionResult Edit(int id)
        {
            var brand = _brandService.GetBrandById(id);
            if (brand == null) return NotFound();
            return View(brand);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Edit(Brand brand)
        {
            if (!ModelState.IsValid) return View(brand);
            _brandService.UpdateBrand(brand);
            return RedirectToAction(nameof(Index));
        }

        public IActionResult Details(int id)
        {
            var brand = _brandService.GetBrandById(id);
            if (brand == null) return NotFound();
            return View(brand);
        }

        // GET: /Brand/Delete/5
        [HttpGet]
        public IActionResult Delete(int id)
        {
            var brand = _brandService.GetBrandById(id);
            if (brand == null) return NotFound();
            return View(brand);
        }

        // POST: /Brand/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public IActionResult DeleteConfirmed(int id)
        {
            try
            {
                _brandService.DeleteBrand(id);
                return RedirectToAction(nameof(Index));
            }
            catch (Exception)
            {
                TempData["DeleteError"] = "Không thể xóa hãng xe này vì vẫn còn các dòng xe liên quan. Hãy xóa các dòng xe thuộc hãng này trước!";
                return RedirectToAction(nameof(Delete), new { id = id });
            }
        }
    }
}