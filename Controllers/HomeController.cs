using System.Diagnostics;
using Microsoft.AspNetCore.Mvc;
using PureScanApp.Models;

namespace PureScanApp.Controllers
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
        public IActionResult UrunInceleme()
        {
            return View();
        }

        
        public IActionResult Makaleler()
        {
            return View();
        }
        public IActionResult Hakkimizda()
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
