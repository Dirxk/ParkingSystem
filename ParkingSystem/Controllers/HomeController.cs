using Microsoft.AspNetCore.Mvc;
using ParkingSystem.Models;
using System.Diagnostics;

namespace ParkingSystem.Controllers
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
        public IActionResult Panel()
        {
            return View();
        }
        public IActionResult EntradasSalidas()
        {
            return View();
        }
        public IActionResult Vehiculo()
        {
            return View();
        }

        public IActionResult VUsuarios()
        {
            return View();
        }


        public IActionResult Privacy()
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
