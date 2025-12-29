using Microsoft.AspNetCore.Mvc;

namespace CallAPI.Controllers
{
    public class ProductController : Controller
    {
        public IActionResult Index() => View();
        public IActionResult Create() => View();
        public IActionResult Edit(int id) { ViewBag.Id = id; return View(); }
        public IActionResult Details(int id) { ViewBag.Id = id; return View(); }
        public IActionResult Delete(int id)
        {
            ViewBag.Id = id;
            return View();
        }
    }
}
