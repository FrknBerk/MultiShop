using Microsoft.AspNetCore.Mvc;
using MultiShop.WebUI.Services.CatalogServices.ProductPropertyServices;

namespace MultiShop.WebUI.Areas.Admin.Controllers
{
    [Area("Admin")]
    [Route("Admin/ProductProperty")]
    public class ProductPropertyController : Controller
    {
        private readonly IProductPropertyService _productPropertyService;

        public ProductPropertyController(IProductPropertyService productPropertyService)
        {
            _productPropertyService = productPropertyService;
        }

        [Route("CreateProductProperty/{id}")]
        public IActionResult CreateProductProperty(string id)
        {
            return View();
        }
    }
}
