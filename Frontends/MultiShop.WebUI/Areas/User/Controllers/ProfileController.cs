using Microsoft.AspNetCore.Mvc;

namespace MultiShop.WebUI.Areas.User.Controllers
{
    [Area("Profile")]
    public class ProfileController : Controller
    {
        public IActionResult Index()
        {
            return View();
        }
    }
}
