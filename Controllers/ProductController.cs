using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace PureScanApp.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ProductController : ControllerBase
    {
        [HttpPost("upload")]
        public IActionResult UploadImage([FromForm] IFormFile file)
        {
            if (file == null || file.Length == 0)
                return BadRequest("Dosya seçilmedi.");

            var result = new
            {
                productName = "Örnek Ürün",
                ingredients = new[]
                {
                    new { name = "Paraben", risk = "Yüksek" },
                    new { name = "Aqua", risk = "Düşük" },
                    new { name = "Alkol", risk = "Orta" }
                }
            };

            return Ok(result);
        }
    }
}