using Microsoft.AspNetCore.Mvc;
using MultiShop.WebUI.Services.CatalogServices.ProductServices;

namespace MultiShop.WebUI.Areas.User.Controllers
{
    [Area("User")]
    public class ProductController : Controller
    {
        private readonly IProductService _productService;

        public ProductController(IProductService productService)
        {
            _productService = productService;
        }

        public async Task<JsonResult> GetByProductId(string productId)
        {
            var value =await _productService.GetByProductIdProductAsync(productId);
            return Json(value);
        }
    }
}
