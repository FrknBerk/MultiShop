using Microsoft.AspNetCore.Mvc;
using MultiShop.WebUI.Services.StatisticServices.CatalogStatisticServices;

namespace MultiShop.WebUI.Areas.Admin.Controllers
{
    [Area("Admin")]
    public class StatisticController : Controller
    {
        private readonly ICatalogStatisticService _catalogStatisticService;

        public StatisticController(ICatalogStatisticService catalogStatisticService)
        {
            _catalogStatisticService = catalogStatisticService;
        }

        public async Task<IActionResult> Index()
        {
            var brandCount = await _catalogStatisticService.GetBrandCount();
            var categoryCount = await _catalogStatisticService.GetCategoryCount();
            var productCount = await _catalogStatisticService.GetProductCount();
            var maxProductPriceCount = await _catalogStatisticService.GetMaxPriceProductName();
            var minProductPriceCount = await _catalogStatisticService.GetMinPriceProductName();
            var avgPriceCount = await _catalogStatisticService.GetProductAvgPrice();
            ViewBag.BrandCount = brandCount;
            ViewBag.CategoryCount = categoryCount;
            ViewBag.ProductCount = productCount;
            ViewBag.MaxProductPriceCount = maxProductPriceCount;
            ViewBag.MinProductPriceCount = minProductPriceCount;
            ViewBag.AvgProductPriceCount = avgPriceCount;
            return View();
        }
    }
}
