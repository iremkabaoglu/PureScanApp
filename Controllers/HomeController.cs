using System.Diagnostics;
using Microsoft.AspNetCore.Mvc;
using PureScanApp.Models;
using PureScanApp.Services;
using System.Threading.Tasks;
using System.Collections.Generic;
using System.IO;
using System.Text.Json;

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
        public async Task<IActionResult> UrunInceleme(IFormFile file)
        {
            if (file == null || file.Length == 0)
            {
                ViewBag.Result = "L�tfen bir dosya se�in.";
                return View();
            }

            // 1. Dosyay� kaydet
            var uploadsFolder = Path.Combine(Directory.GetCurrentDirectory(), "wwwroot", "images");
            if (!Directory.Exists(uploadsFolder))
                Directory.CreateDirectory(uploadsFolder);

            var filePath = Path.Combine(uploadsFolder, file.FileName);
            using (var stream = new FileStream(filePath, FileMode.Create))
            {
                file.CopyTo(stream);
            }

            //  Kullan�c�n�n y�kledi�i dosyan�n yolu
            ViewBag.UploadedImagePath = "/images/" + file.FileName;

            // 2. Model tahmini yap
            var predictor = new PredictionService();
            var result = predictor.Predict(filePath); // �rn: "ISANA Med"

            // 3. ViewBag'e tahmini yaz
            ViewBag.Result = result;

            // 4. Supabase'deki do�ru �r�n ismi ile e�le
            var mapping = new Dictionary<string, string>
            {
                { "ISANA Med", "ISANA V�CUT LOSYONU" },
                { "DOA NEMLEND�R�C�", "DOA NEMLEND�R�C�" },
                { "Arko nemlendirici bak�m kremi", "Arko nemlendirici bak�m kremi" },
                { "Bepanthol", "Bepanthol" },
                { "Cream Co", "Cream Co nemlendirici" },
                { "Maruderm Kil Maskesi", "Maruderm Maske" },
                { "ArkoNem Krem", "Arko el kremi" },
                { "Mia", "Mia Klinika Retinol" },
                { "Urban Sa� Bak�m Keremi", "Urban Sa� Krem" },
                { "Maruderm G�ne� Kremi", "Maruderm G�ne� Kremi" }
            };

            if (mapping.ContainsKey(result))
                result = mapping[result];

            // 5. Supabase verisini al
            var productInfoService = new ProductInfoService();
            var productData = await productInfoService.GetProductInfoByName(result);

            if (productData != null)
            {
                ViewBag.Score = productData["score"]?.ToString();
                ViewBag.ImageUrl = productData["image_url"]?.ToString();

                // ingredients JSON'undan liste �ret
                var ingredientsJson = productData["ingredients"]?.ToString();
                var ingredientsList = new List<(string Content, string Risk)>();

                if (!string.IsNullOrEmpty(ingredientsJson))
                {
                    try
                    {
                        var parsed = JsonSerializer.Deserialize<List<Dictionary<string, string>>>(ingredientsJson);
                        foreach (var item in parsed!)
                        {
                            item.TryGetValue("��erik", out var content);
                            item.TryGetValue("Risk", out var risk);
                            ingredientsList.Add((content ?? "", risk ?? ""));
                        }
                        ViewBag.IngredientsList = ingredientsList;
                    }
                    catch
                    {
                        ViewBag.IngredientsList = null;
                    }
                }
            }
            else
            {
                ViewBag.Score = "-";
                ViewBag.ImageUrl = null;
                ViewBag.IngredientsList = null;
            }

            return View();
        }

        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }
    }
}
