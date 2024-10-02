using Microsoft.AspNetCore.Mvc;
using MultiShop.DtoLayer.CatalogDtos.FeatureSliderDtos;
using MultiShop.WebUI.Services.CatalogServices.SliderServices;
using Newtonsoft.Json;
using System.Text;

namespace MultiShop.WebUI.Areas.Admin.Controllers
{
    [Area("Admin")]
    [Route("Admin/FeatureSlider")]
    public class FeatureSliderController : Controller
    {
        private readonly IHttpClientFactory _httpClientFactory;
        private readonly IFeatureSliderService _featureSliderService;

        public FeatureSliderController(IHttpClientFactory httpClientFactory, IFeatureSliderService featureSliderService)
        {
            _httpClientFactory = httpClientFactory;
            _featureSliderService = featureSliderService;
        }

        [Route("Index")]
        public async Task<IActionResult> Index()

        {
            ViewBag.v0 = "Öne Çıkan görsel İşlemleri";
            ViewBag.v1 = "AnaSayfa ";
            ViewBag.v2 = "Slider Öne Çıkan Görsel Listesi";
            ViewBag.v3 = "Öne Çıkan Görsel işlemleri";
            var values = await _featureSliderService.GetAllFeatureSliderAsync();
            return View(values);
        }

        [Route("CreateFeatureSlider")]
        [HttpGet]
        public IActionResult CreateFeatureSlider()
        {
            ViewBag.v0 = "Öne Çıkan görsel İşlemleri";
            ViewBag.v1 = "AnaSayfa ";
            ViewBag.v2 = "Slider Öne Çıkan Görsel Listesi";
            ViewBag.v3 = "Öne Çıkan Görsel Ekleme";
            return View();
        }

        [Route("CreateFeatureSlider")]
        [HttpPost]
        public async Task<IActionResult> CreateFeatureSlider(CreateFeatureSliderDto createFeatureSliderDto)
        {
            await _featureSliderService.CreateFeatureSliderAsync(createFeatureSliderDto);
            return RedirectToAction("Index", "FeatureSlider", new { area = "Admin" });
        }

        [Route("DeleteFeatureSlider/{id}")]
        public async Task<IActionResult> DeleteFeatureSlider(string id)
        {
            await _featureSliderService.DeleteFeatureSliderAsync(id);
            return RedirectToAction("Index", "FeatureSlider", new { area = "Admin" });
        }

        [Route("UpdateFeatureSlider/{id}")]
        [HttpGet]
        public async Task<IActionResult> UpdateFeatureSlider(string id)
        {
            ViewBag.v0 = "Öne Çıkan görsel İşlemleri";
            ViewBag.v1 = "AnaSayfa ";
            ViewBag.v2 = "Slider Öne Çıkan Görsel Listesi";
            ViewBag.v3 = "Öne Çıkan Görsel Güncelleme";
            var values = await _featureSliderService.GetByIdFeatureSliderAsync(id);
            return View(values);
        }

        [Route("UpdateFeatureSlider/{id}")]
        [HttpPost]
        public async Task<IActionResult> UpdateFeatureSlider(UpdateFeatureSliderDto updateFeatureSliderDto)
        {
            await _featureSliderService.UpdateFeatureSliderAsync(updateFeatureSliderDto);
            return RedirectToAction("Index", "FeatureSlider", new { area = "Admin" });
        }
    }
}
