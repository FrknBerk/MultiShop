using Microsoft.AspNetCore.Mvc;

namespace MultiShop.WebUI.Areas.User.Controllers
{
    [Area("Cargo")]
    public class CargoController : Controller
    {
        public IActionResult Index()
        {
            return View();
        }
    }
}
