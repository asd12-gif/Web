using Microsoft.AspNetCore.Mvc;

namespace Lab2.Controllers
{
    public class HelloWorldController : Controller
    {
        public IActionResult Index()
        {
            return View();
        }
        public IActionResult Welcome(string name, int numTimes = 1)
        {
            ViewData["Message"] = "Hello " + name;      // Lưu thông điệp
            ViewData["NumTimes"] = numTimes;            // Lưu số lần lặp
            return View();
        }

    }
}
