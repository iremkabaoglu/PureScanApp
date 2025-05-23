using System.Diagnostics;
using Microsoft.AspNetCore.Mvc;
using PureScanApp.Models;
using PureScanApp.Services;

namespace PureScanApp.Controllers
{
    public class HomeController : Controller
    {
        private readonly ILogger<HomeController> _logger;

        public HomeController(ILogger<HomeController> logger)
        {
            _logger = logger;
        }

        public IActionResult Index() => View();
        public IActionResult UrunInceleme() => View();
        public IActionResult Makaleler() => View();
        public IActionResult Hakkimizda() => View();

        [HttpPost]
        public IActionResult UrunInceleme(IFormFile file)
        {
            if (file == null || file.Length == 0)
            {
                ViewBag.Result = "Lütfen bir dosya seçin.";
                return View();
            }

            var uploadsFolder = Path.Combine(Directory.GetCurrentDirectory(), "wwwroot", "images");
            if (!Directory.Exists(uploadsFolder))
                Directory.CreateDirectory(uploadsFolder);

            var filePath = Path.Combine(uploadsFolder, file.FileName);

            using (var stream = new FileStream(filePath, FileMode.Create))
            {
                file.CopyTo(stream);
            }

            var predictor = new PredictionService();
            var result = predictor.Predict(filePath);

            ViewBag.Result = $"Tahmin Edilen Sýnýf: {result}";
            return View();
        }

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }
    }
}
