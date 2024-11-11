using Microsoft.AspNetCore.Mvc;
using MultiShop.DtoLayer.CatalogDtos.FavoriteProductDtos;
using MultiShop.WebUI.Services.CatalogServices.FavoriteProductServices;
using MultiShop.WebUI.Services.Interface;

namespace MultiShop.WebUI.Areas.User.Controllers
{
    [Area("User")]
    public class FavoriteProductController : Controller
    {
        private readonly IUserService _userService;
        private readonly IFavoriteProductService _favoriteProductService;

        public FavoriteProductController(IUserService userService, IFavoriteProductService favoriteProductService)
        {
            _userService = userService;
            _favoriteProductService = favoriteProductService;
        }

        public async Task<IActionResult> CreateFavoriteProduct(string id)
        {
            var user = await _userService.GetUserInfo();
            var createFavoriteProductDto = new CreateFavoriteProductDto
            {
                ProductId = id,
                UserId = user.Id,
            };
            await _favoriteProductService.CreateFavoriteProductAsync(createFavoriteProductDto);
            return View("GetFavoriteProductList", "FavoriteProduct");
        }

        public IActionResult GetFavoriteProductList() {
            ViewBag.directory1 = "Ana Sayfa";
            ViewBag.directory2 = "Ürünler";
            ViewBag.directory3 = "Favorilerim";
            return View();
        }

        public async Task<JsonResult> GetByUserIdFavoriteProductList()
        {
            var user = await _userService.GetUserInfo();
            var values = await _favoriteProductService.GetByUserIdFavoriteProdutAsync(user.Id);
            return Json(values);
        }

        public async Task<IActionResult> DeleteFavoriteProduct(string id)
        {
            await _favoriteProductService.DeleteFavoriteProductAsync(id);
            return View("GetFavoriteProductList");
        }
    }
}
