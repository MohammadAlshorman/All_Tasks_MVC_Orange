using System.Diagnostics;
using Microsoft.AspNetCore.Mvc;
using Task_16_2_2025.Models;

namespace Task_16_2_2025.Controllers
{
    public class HomeController : Controller
    {
        private readonly ILogger<HomeController> _logger;

        public HomeController(ILogger<HomeController> logger)
        {
            _logger = logger;
        }

        public IActionResult Index()
        {
            return View();
        }

        public IActionResult Privacy()
        {
            return View();
        }


        public IActionResult about_us()
        {
            return View();
        }


        public IActionResult fraamskt()
        {
            return View();
        }

        public IActionResult dashboard()
        {
            return View();
        }

        public IActionResult team()
        {
            return View();
        }


        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }
    }
}
