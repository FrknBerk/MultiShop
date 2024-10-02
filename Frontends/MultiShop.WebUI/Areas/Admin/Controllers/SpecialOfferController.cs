using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using MultiShop.DtoLayer.CatalogDtos.SpecialOfferDtos;
using MultiShop.WebUI.Services.CatalogServices.SpecialOfferServices;
using Newtonsoft.Json;
using System.Text;

namespace MultiShop.WebUI.Areas.Admin.Controllers
{
    [Area("Admin")]
    [Route("Admin/SpecialOffer")]
    public class SpecialOfferController : Controller
    {
        private readonly IHttpClientFactory _httpClientFactory;
        private readonly ISpecialOfferService _specialOfferService;

        public SpecialOfferController(IHttpClientFactory httpClientFactory, ISpecialOfferService specialOfferService)
        {
            _httpClientFactory = httpClientFactory;
            _specialOfferService = specialOfferService;
        }

        [Route("Index")]
        public async Task<IActionResult> Index()

        {
            ViewBag.v0 = "Özel Teklif İşlemleri Listesi";
            ViewBag.v1 = "Ana Sayfa";
            ViewBag.v2 = "Özel Teklifler";
            ViewBag.v3 = "Özel Teklif ve Günün İndirim Listesi";
            var values =await _specialOfferService.GetAllSpecialOfferAsync();
            return View(values);
        }

        [Route("CreateSpecialOffer")]
        [HttpGet]
        public IActionResult CreateSpecialOffer()
        {
            ViewBag.v0 = "Özel Teklif İşlemleri Listesi";
            ViewBag.v1 = "Ana Sayfa";
            ViewBag.v2 = "Özel Teklifler";
            ViewBag.v3 = "Özel Teklif ve Günün İndirim Ekleme";
            return View();
        }

        [Route("CreateSpecialOffer")]
        [HttpPost]
        public async Task<IActionResult> CreateSpecialOffer(CreateSpecialOfferDto createSpecialOfferDto)
        {
            await _specialOfferService.CreateSpecialOfferAsync(createSpecialOfferDto);
            return RedirectToAction("Index", "SpecialOffer", new { area = "Admin" });
        }

        [Route("DeleteSpecialOffer/{id}")]
        public async Task<IActionResult> DeleteSpecialOffer(string id)
        {
            await _specialOfferService.DeleteSpecialOfferAsync(id);
            return RedirectToAction("Index", "SpecialOffer", new { area = "Admin" });
        }

        [Route("UpdateSpecialOffer/{id}")]
        [HttpGet]
        public async Task<IActionResult> UpdateSpecialOffer(string id)
        {
            ViewBag.v0 = "Özel Teklif İşlemleri Listesi";
            ViewBag.v1 = "Ana Sayfa";
            ViewBag.v2 = "Özel Teklifler";
            ViewBag.v3 = "Özel Teklif ve Günün İndirim Güncelleme";
            var values = await _specialOfferService.GetByIdSpecialOfferAsync(id);
            return View(values);
        }

        [Route("UpdateSpecialOffer/{id}")]
        [HttpPost]
        public async Task<IActionResult> UpdateCategory(UpdateSpecialOfferDto updateSpecialOfferDto)
        {
            await _specialOfferService.UpdateSpecialOfferAsync(updateSpecialOfferDto);
            return RedirectToAction("Index", "SpecialOffer", new { area = "Admin" });
        }
    }
}
