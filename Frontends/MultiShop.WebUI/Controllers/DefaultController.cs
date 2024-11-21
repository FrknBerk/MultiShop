using IdentityModel.AspNetCore.AccessTokenManagement;
using Microsoft.AspNetCore.Mvc;
using MultiShop.WebUI.Services.TokenService;

namespace MultiShop.WebUI.Controllers
{
    public class DefaultController : Controller
    { 
        public async Task<IActionResult> Index()
        {
            ViewBag.directory1 = "Ana Sayfa";
            ViewBag.directory2 = "Ürün Listesi";
            return View();
        }
    }
}
