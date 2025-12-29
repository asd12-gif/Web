using Microsoft.AspNetCore.Mvc;

namespace CallAPI.Controllers
{
    public class CategoryController : Controller
    {
            public IActionResult Index() => View();
            public IActionResult Create() => View();
            public IActionResult Details(int id)
            {
                ViewBag.Id = id;
                return View();
            }
            public IActionResult Edit(int id) { ViewBag.Id = id; return View(); }
            public IActionResult Delete(int id) { ViewBag.Id = id; return View(); }
    }
}

