using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace PureScanApp.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class MakaleController : ControllerBase
    {
        [HttpGet]
        public IActionResult GetArticles()
        {
            var articles = new[]
            {
        new {
            title = "Gebelikte Kişisel Bakım...",
            author = "Canan Sarı",
            year = 2021,
            pages = "N/A",
            citedBy = 1,
            doi = "https://doi.org/10.21763/tjfmpc.930868",
            summary = "Bu makalede gebelik döneminde kişisel bakım ürünlerinin kullanımı ve fetal sağlık üzerindeki etkileri incelenmiştir."
        },
        new {
            title = "Kozmetik Ürünler ve Kadın Sağlığı",
            author = "Kocaöz, Semra;Eroğlu, Kafiye",
            year = 2014,
            pages = "N/A",
            citedBy = 0,
            doi = "https://research.ebsco.com/c/vhojtv/search/details/jsnrrysygb?db=asn",
            summary = "Günümüzde kozmetik ürünler, çok çeşitli etkileri nedeniyle modern toplumun vazgeçilmez bir ürünü olarak karşımıza çıkmaktadır. "
        },
        new {
            title = "Kozmetik Ürünlerin Tüketiminde Sağlık Bilincinin Rolü",
            author = "Metin Saygılı",
            year = 2019,
            pages = "N/A",
            citedBy = 2,
            doi = "https://www.researchgate.net/profile/Metin-Saygili/publication/331982493_Kozmetik_Urunlerin_Tuketiminde_Saglik_Bilincinin_Rolu/links/5c98de06a6fdccd46039ca6d/Kozmetik-Ueruenlerin-Tueketiminde-Saglik-Bilincinin-Rolue.pdf",
            summary = "Bu çalışma, bireylerin kozmetik ürün tüketiminde sağlık bilinci düzeyinin etkisini değerlendirmektedir."
        },
        new {
            title = "Yaşlanma Karşıtı Kozmetik Yaklaşımları Ve Ürün Bileşenleri",
            author = "Evren Alğın Yapar,Sakine Tuncay Tanrıverdi",
            year = 2016,
            pages = "199–216",
            citedBy = 3,
            doi = "https://dergipark.org.tr/en/pub/balikesirsbd/issue/38438/452600",
            summary = "Cilt yaşlanması; iç veya dış faktörlerin veya bunların birlikteliğinin neden olduğu karmaşık biyolojik bir süreçtir. "
        }

    };

            return Ok(articles);
        }
    }
}
