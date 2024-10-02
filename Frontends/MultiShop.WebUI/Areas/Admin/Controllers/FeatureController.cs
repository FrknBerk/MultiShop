using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using MultiShop.DtoLayer.CatalogDtos.FeatureDtos;
using MultiShop.WebUI.Services.CatalogServices.FeatureServices;
using Newtonsoft.Json;
using System.Text;

namespace MultiShop.WebUI.Areas.Admin.Controllers
{
    [AllowAnonymous]
    [Area("Admin")]
    [Route("Admin/Feature")]
    public class FeatureController : Controller
    {
        private readonly IFeatureService _featureService;

        public FeatureController(IFeatureService featureService)
        {
            _featureService = featureService;
        }

        [Route("Index")]
        public async Task<IActionResult> Index()

        {
            ViewBag.v0 = "Öne Çıkan Alan İşlemleri";
            ViewBag.v1 = "Ana Sayfa";
            ViewBag.v2 = "Öne Çıkan ALanlar";
            ViewBag.v3 = "Öne Çıkan Alan Listesi";
            var values = await _featureService.GetAllFeatureAsync();
            return View(values);
        }

        [Route("CreateFeature")]
        [HttpGet]
        public IActionResult CreateFeature()
        {
            ViewBag.v0 = "Öne Çıkan Alan İşlemleri";
            ViewBag.v1 = "Ana Sayfa";
            ViewBag.v2 = "Öne Çıkan ALanlar";
            ViewBag.v3 = "Öne Çıkan Alan Ekleme";
            return View();
        }

        [Route("CreateFeature")]
        [HttpPost]
        public async Task<IActionResult> CreateFeature(CreateFeatureDto createFeatureDto)
        {
            await _featureService.CreateFeatureAsync(createFeatureDto);
            return RedirectToAction("Index", "Feature", new { area = "Admin" });
        }

        [Route("DeleteFeature/{id}")]
        public async Task<IActionResult> DeleteFeature(string id)
        {
            await _featureService.DeleteFeatureAsync(id);
            return RedirectToAction("Index", "Feature", new { area = "Admin" });
        }

        [Route("UpdateFeature/{id}")]
        [HttpGet]
        public async Task<IActionResult> UpdateFeature(string id)
        {
            ViewBag.v0 = "Öne Çıkan Alan İşlemleri";
            ViewBag.v1 = "Ana Sayfa";
            ViewBag.v2 = "Öne Çıkan ALanlar";
            ViewBag.v3 = "Öne Çıkan Alan Güncelleme";
            var values = await _featureService.GetByIdFeatureAsync(id);
            return View(values);
        }

        [Route("UpdateFeature/{id}")]
        [HttpPost]
        public async Task<IActionResult> UpdateFeature(UpdateFeatureDto updateFeatureDto)
        {
            await _featureService.UpdateFeatureAsync(updateFeatureDto);
            return RedirectToAction("Index", "Feature", new { area = "Admin" });
        }
    }
}
