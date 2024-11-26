using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using MultiShop.DtoLayer.CatalogDtos.ProductImageDtos;
using MultiShop.WebUI.Services.CatalogServices.ProductImageServices;
using Newtonsoft.Json;
using System.Text;

namespace MultiShop.WebUI.Areas.Admin.Controllers
{
    [AllowAnonymous]
    [Area("Admin")]
    [Route("Admin/ProductImage")]
    public class ProductImageController : Controller
    {
        private readonly IHttpClientFactory _httpClientFactory;
        private readonly IProductImageService _productImageService;

        public ProductImageController(IHttpClientFactory httpClientFactory, IProductImageService productImageService)
        {
            _httpClientFactory = httpClientFactory;
            _productImageService = productImageService;
        }
        public async Task<IActionResult> ProductImageDetail(string id)
        {
            return View();
        }

        [Route("Index/{id}")]
        [HttpGet]
        public async Task<IActionResult> Index(string id)
        {
            ViewBag.v0 = "Ürün Resim İşlemleri";
            ViewBag.v1 = "Ana Sayfa";
            ViewBag.v2 = "Ürün Resimleri";
            ViewBag.v3 = "Ürün Resim Listesi";
            ViewBag.productId = id;
            var values = await _productImageService.GetByProductIdProductImageAsync(id);
            return View(values);
        }

        [Route("CreateProductImage")]
        [HttpGet]
        public IActionResult CreateProductImage()
        {
            ViewBag.v0 = "Ürün Resim İşlemleri";
            ViewBag.v1 = "Ana Sayfa";
            ViewBag.v2 = "Ürün Resim";
            ViewBag.v3 = "Ürün Resim Ekleme";
            return View();
        }

        [Route("CreateProductImage")]
        [HttpPost]
        public async Task<IActionResult> CreateProductImage(CreateProductImageDto createProductImageDto)
        {
            await _productImageService.CreateProductImageAsync(createProductImageDto);
            return View("CreateProductImage");
        }

        [Route("DeleteProductImage/")]
        public async Task<IActionResult> DeleteProductImage(string productImageId, string productId)
        {
            await _productImageService.DeleteProductImageAsync(productImageId);
            return RedirectToAction("Index", "ProductImage", new { area = "Admin", id = productId });
        }

        [Route("UpdateProductImage/{id}")]
        [HttpGet]
        public async Task<IActionResult> UpdateProductImage(string id)
        {
            ViewBag.v0 = "Ürün Resim İşlemleri";
            ViewBag.v1 = "Ana Sayfa";
            ViewBag.v2 = "Ürün Resim";
            ViewBag.v3 = "Ürün Resim Güncelleme";
            var values = await _productImageService.GetByIdProductImageAsync(id);
            return View(values);
        }

        [Route("UpdateProductImage/{id}")]
        [HttpPost]
        public async Task<IActionResult> UpdateProductImage(UpdateProductImageDto updateProductImageDto)
        {
            await _productImageService.UpdateProductImageAsync(updateProductImageDto);
            return RedirectToAction("Index", "ProductImage", new { area = "Admin", id = updateProductImageDto.ProductId });
        }
    }
}
