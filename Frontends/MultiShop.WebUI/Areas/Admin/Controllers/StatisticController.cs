using Microsoft.AspNetCore.Mvc;
using MultiShop.WebUI.Services.StatisticServices.CatalogStatisticServices;
using MultiShop.WebUI.Services.StatisticServices.CommentStatisticService;
using MultiShop.WebUI.Services.StatisticServices.DiscountStatisticService;
using MultiShop.WebUI.Services.StatisticServices.MessageStatisticServices;
using MultiShop.WebUI.Services.StatisticServices.UserStatisticServices;

namespace MultiShop.WebUI.Areas.Admin.Controllers
{
    [Area("Admin")]
    public class StatisticController : Controller
    {
        private readonly ICatalogStatisticService _catalogStatisticService;
        private readonly IUserStatisticService _userStatisticService;
        private readonly ICommentStatisticService _commentStatisticService;
        private readonly IDiscountStatisticService _discountStatisticService;
        private readonly IMessageStatisticService _messageStatisticService;

        public StatisticController(ICatalogStatisticService catalogStatisticService, IUserStatisticService userStatisticService, ICommentStatisticService commentStatisticService, IDiscountStatisticService discountStatisticService, IMessageStatisticService messageStatisticService)
        {
            _catalogStatisticService = catalogStatisticService;
            _userStatisticService = userStatisticService;
            _commentStatisticService = commentStatisticService;
            _discountStatisticService = discountStatisticService;
            _messageStatisticService = messageStatisticService;
        }

        public async Task<IActionResult> Index()
        {
            //var brandCount = await _catalogStatisticService.GetBrandCount();
            //var categoryCount = await _catalogStatisticService.GetCategoryCount();
            //var productCount = await _catalogStatisticService.GetProductCount();
            //var maxProductPriceCount = await _catalogStatisticService.GetMaxPriceProductName();
            //var minProductPriceCount = await _catalogStatisticService.GetMinPriceProductName();
            //var avgPriceCount = await _catalogStatisticService.GetProductAvgPrice();
            var getUserCount = await _userStatisticService.GetUserCount();
            var getTotalCommentCount = await _commentStatisticService.GetTotalCommentCount();
            var getActiveCommentCount = await _commentStatisticService.GetActiveCommentCount();
            var getPassiveCommentCount = await _commentStatisticService.GetPassiveCommentCount();
            var getTotalDiscountCount = await _discountStatisticService.GetDiscountCouponCount();
            var getTotalMessageCount = await _messageStatisticService.GetTotalMessageCount();
            //ViewBag.BrandCount = brandCount;
            //ViewBag.CategoryCount = categoryCount;
            //ViewBag.ProductCount = productCount;
            //ViewBag.MaxProductPriceCount = maxProductPriceCount;
            //ViewBag.MinProductPriceCount = minProductPriceCount;
            //ViewBag.AvgProductPriceCount = avgPriceCount;
            ViewBag.GetUserCount = getUserCount;
            ViewBag.GetTotalCommentCount = getTotalCommentCount;
            ViewBag.GetActiveCommentCount = getActiveCommentCount;
            ViewBag.GetPassiveCommentCount = getPassiveCommentCount;
            ViewBag.GetTotalDiscountCount = getTotalDiscountCount;
            ViewBag.GetTotalMessageCount = getTotalMessageCount;
            return View();
        }
    }
}
